using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ma : QuanCo
{
    public Ma(Color color, int Hang, int Cot) : base(Hang, Cot)
    {
        this.TypePiece = Type.MA;
        this.ColorPiece = color;
        string path = (color == Color.RED) ? "Prefabs/QuanCo/2Ma" : "Prefabs/Quanco/1Ma";
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
        if (CalculateDistance(Hang, Cot, HangMoi, CotMoi) != 5)
            return false;
        if (CotMoi < 0 || CotMoi > 8 || HangMoi < 0 || HangMoi > 9)
            return false;
        if (BanCo[HangMoi, CotMoi] != null)
            if (ColorPiece == BanCo[HangMoi, CotMoi].ColorPiece)
                return false;
        //Nếu có quân cản thì loại
        if (HangMoi - Hang == 2 && BanCo[Hang + 1, Cot] != null)
            return false;
        else if (HangMoi - Hang == -2 && BanCo[Hang - 1, Cot] != null)
            return false;
        else if(CotMoi - Cot == 2 && BanCo[Hang, Cot + 1] != null)
            return false;
        else if(CotMoi - Cot == -2 && BanCo[Hang, Cot - 1] != null)
            return false;
        
        return true;
    }
    public override List<Move> ListCanMove(QuanCo[,] BanCo)
    {
        List<Move> lMove = new List<Move>();
        if (Rule(BanCo, Hang + 2, Cot + 1))
            lMove.Add(new Move(Hang, Cot, Hang + 2, Cot + 1, BanCo[Hang + 2, Cot + 1]));
        if (Rule(BanCo, Hang + 1, Cot + 2))
            lMove.Add(new Move(Hang, Cot, Hang + 1, Cot + 2, BanCo[Hang + 1, Cot + 2]));
        if (Rule(BanCo, Hang + 2, Cot - 1))
            lMove.Add(new Move(Hang, Cot, Hang + 2, Cot - 1, BanCo[Hang + 2, Cot - 1]));
        if (Rule(BanCo, Hang + 1, Cot - 2))
            lMove.Add(new Move(Hang, Cot, Hang + 1, Cot - 2, BanCo[Hang + 1, Cot - 2]));
        if (Rule(BanCo, Hang - 2, Cot + 1))
            lMove.Add(new Move(Hang, Cot, Hang - 2, Cot + 1, BanCo[Hang - 2, Cot + 1]));
        if (Rule(BanCo, Hang - 1, Cot + 2))
            lMove.Add(new Move(Hang, Cot, Hang - 1, Cot + 2, BanCo[Hang - 1, Cot + 2]));
        if (Rule(BanCo, Hang - 2, Cot - 1))
            lMove.Add(new Move(Hang, Cot, Hang - 2, Cot - 1, BanCo[Hang - 2, Cot - 1]));
        if (Rule(BanCo, Hang - 1, Cot - 2))
            lMove.Add(new Move(Hang, Cot, Hang - 1, Cot - 2, BanCo[Hang - 1, Cot - 2]));
        return lMove;
    }
}
