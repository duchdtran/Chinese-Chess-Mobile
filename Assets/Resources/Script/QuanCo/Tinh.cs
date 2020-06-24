using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tinh : QuanCo
{

    public Tinh(Color color ,int Hang, int Cot) : base(Hang, Cot)
    {
        this.TypePiece = Type.TINH;
        this.ColorPiece = color;
        string path = (color == Color.RED) ? "Prefabs/QuanCo/2Tinh" : "Prefabs/Quanco/1Tinh";
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
        if (CalculateDistance(Hang, Cot, HangMoi, CotMoi) != 8)
            return false;
        if (CotMoi < 0 || CotMoi > 8 || HangMoi < 0 || HangMoi > 9)
            return false;
        if (ColorPiece == Color.RED && HangMoi > 4)
            return false;
        if (ColorPiece == Color.GREEN && HangMoi < 5)
            return false;
        if (BanCo[(Hang + HangMoi) / 2, (Cot + CotMoi) / 2] != null)
            return false;
        if (BanCo[HangMoi, CotMoi] != null)
            if (ColorPiece == BanCo[HangMoi, CotMoi].ColorPiece)
                return false;
        return true;
    }

    public override List<Move> ListCanMove(QuanCo[,] BanCo)
    {
        List<Move> lMove = new List<Move>();
        if (Rule(BanCo, Hang + 2, Cot + 2))
            lMove.Add(new Move(Hang, Cot, Hang + 2, Cot + 2, BanCo[Hang + 2, Cot + 2]));
        if (Rule(BanCo, Hang + 2, Cot - 2))
            lMove.Add(new Move(Hang, Cot, Hang + 2, Cot - 2, BanCo[Hang + 2, Cot - 2]));
        if (Rule(BanCo, Hang - 2, Cot + 2))
            lMove.Add(new Move(Hang, Cot, Hang - 2, Cot + 2, BanCo[Hang - 2, Cot + 2]));
        if (Rule(BanCo, Hang - 2, Cot - 2))
            lMove.Add(new Move(Hang, Cot, Hang - 2, Cot - 2, BanCo[Hang - 2, Cot - 2]));
        return lMove;
    }
}
