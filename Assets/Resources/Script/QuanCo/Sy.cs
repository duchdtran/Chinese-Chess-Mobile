using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sy : QuanCo
{
    public Sy(Color color, int Hang, int Cot) : base(Hang, Cot)
    {
        this.TypePiece = Type.SY;
        this.ColorPiece = color;
        string path = (color == Color.RED) ? "Prefabs/QuanCo/2Sy" : "Prefabs/Quanco/1Sy";
        var Prefab = (Resources.Load(path)) as GameObject;
        if (Prefab == null)
        {
            Debug.Log("Ko tìm thấy Prefabs");
        }
        else
        {
            this.Instance = Object.Instantiate<GameObject>(Prefab, new Vector3(Cot, Hang), Quaternion.identity);
        }
    }
    public override bool Rule(QuanCo[,] BanCo, int HangMoi, int CotMoi)
    {
        if (CalculateDistance(Hang, Cot, HangMoi, CotMoi) != 2)
            return false; 
        if (CotMoi < 3 || CotMoi > 5)
            return false;
        
        if (ColorPiece == Color.RED){
            if (HangMoi < 0 || HangMoi > 2)
                return false;
        } else{
            if (HangMoi < 7 || HangMoi > 9)
                return false;
        }
        if (BanCo[HangMoi, CotMoi] != null)
            if (ColorPiece == BanCo[HangMoi, CotMoi].ColorPiece)
                return false;
        return true;
    }

    public override List<Move> ListCanMove(QuanCo[,] BanCo)
    {
        List<Move> lMove = new List<Move>();
        if(Rule(BanCo, Hang + 1, Cot+1))
            lMove.Add(new Move(Hang, Cot, Hang + 1, Cot+1, BanCo[Hang + 1, Cot + 1]));
        if (Rule(BanCo, Hang + 1, Cot-1))
            lMove.Add(new Move(Hang, Cot, Hang + 1, Cot-1, BanCo[Hang + 1, Cot - 1]));
        if (Rule(BanCo, Hang - 1, Cot+1))
            lMove.Add(new Move(Hang, Cot, Hang - 1, Cot + 1, BanCo[Hang - 1, Cot + 1]));
        if (Rule(BanCo, Hang - 1, Cot-1))
            lMove.Add(new Move(Hang, Cot, Hang - 1, Cot-1, BanCo[Hang - 1, Cot - 1]));
        return lMove;
    }
}
