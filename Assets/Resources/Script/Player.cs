using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    public string Name { get; set; }
    public Color ColorPlayer { get; set; }
    public Move move;
    public QuanCo Selected;
    private GameObject ObjCanMove;
    private Stack<GameObject> stkCanMove;
    public Stack<Move> stkFakeMove;

    public Player(Color color = Color.RED)
    {
        this.ColorPlayer = color;
        ObjCanMove = Resources.Load<GameObject>("Prefabs/CanMove");
        stkCanMove = new Stack<GameObject>();
        stkFakeMove = new Stack<Move>();
    }
    public virtual Move FindMove(QuanCo[,] BanCo)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int Hang = (int)(mousePos.y + 0.5f);
            int Cot = (int)(mousePos.x + 0.5f);
            if (Hang < 0 || Hang > 9 || Cot < 0 || Cot > 8)
                return null;
            if (Selected == null)
            {
                Selected = BanCo[Hang, Cot];
                if (Selected != null)
                {
                    if (Selected.ColorPiece == ColorPlayer)
                    {
                        ShowCanMove(BanCo, Hang, Cot);
                        move = new Move(Hang, Cot);
                    }
                    else Selected = null;
                }
            }
            else
            {
                DestroyCanMove();
                Selected = null;
                move.HangMoi = Hang;
                move.CotMoi = Cot;
                move.TargetPiece = BanCo[Hang, Cot];
                if (BanCo[move.HangCu,move.CotCu].Rule(BanCo, move.HangMoi, move.CotMoi))
                {
                    return move;
                }
            }
            
        }
        return null;
    }
    public void ShowCanMove(QuanCo[,] BanCo, int Hang, int Cot)
    {
        if (BanCo[Hang, Cot] == null)
            return;
        List<Move> lCanMove = BanCo[Hang, Cot].ListCanMove(BanCo);
        for (int i = 0; i < lCanMove.Count; i++)
        {
            stkCanMove.Push(GameObject.Instantiate(ObjCanMove, new Vector3(lCanMove[i].CotMoi, lCanMove[i].HangMoi), Quaternion.identity));
        }
    }
    public void DestroyCanMove()
    {
        while (stkCanMove.Count != 0)
        {
            GameObject.Destroy(stkCanMove.Pop());
        }
    }

}
