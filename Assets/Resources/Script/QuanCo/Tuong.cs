using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuong : QuanCo
{
    public Tuong(Color color, int Hang, int Cot) : base(Hang, Cot)
    {
        this.TypePiece = Type.TUONG;
        this.ColorPiece = color;
        string path = (color == Color.RED) ? "Prefabs/QuanCo/2Tuong" : "Prefabs/Quanco/1Tuong";
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

        if (CotMoi < 3 || CotMoi > 5 || HangMoi < 0 || HangMoi > 9)
            return false;
        if (isKingFace(BanCo, HangMoi, CotMoi)) return true;
        if (CalculateDistance(Hang, Cot, HangMoi, CotMoi) != 1)
            return false;
        if (ColorPiece == Color.RED)
        {
            if (HangMoi < 0 || HangMoi > 2)
                return false;
        }
        else
        {
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
        if (Rule(BanCo, Hang + 1, Cot))
            lMove.Add(new Move(Hang, Cot, Hang + 1, Cot, BanCo[Hang + 1, Cot]));
        if (Rule(BanCo, Hang, Cot + 1))
            lMove.Add(new Move(Hang, Cot, Hang, Cot + 1, BanCo[Hang, Cot + 1]));
        if (Rule(BanCo, Hang - 1, Cot))
            lMove.Add(new Move(Hang, Cot, Hang - 1, Cot, BanCo[Hang - 1, Cot]));
        if (Rule(BanCo, Hang, Cot - 1))
            lMove.Add(new Move(Hang, Cot, Hang, Cot - 1, BanCo[Hang, Cot - 1]));
        if (ColorPiece == Color.RED)
        {
            for (int i = 9; i > 6; i--)
                if (Rule(BanCo, i, Cot))
                    lMove.Add(new Move(Hang, Cot, i, Cot, BanCo[i, Cot]));
        }
        else
        {
            for (int i = 0; i < 3; i++)
                if (Rule(BanCo, i, Cot))
                    lMove.Add(new Move(Hang, Cot, i, Cot, BanCo[i, Cot]));
        }
        return lMove;
    }
    public bool isKingFace(QuanCo[,] BanCo, int HangMoi, int CotMoi)
    {
        int min = Mathf.Min(HangMoi, Hang);
        int max = Mathf.Max(HangMoi, Hang);
        if (BanCo[HangMoi, CotMoi] != null)
        {
            if (BanCo[HangMoi, CotMoi].TypePiece == Type.TUONG && BanCo[HangMoi, CotMoi].ColorPiece != ColorPiece)
            {
                for (int i = min + 1; i < max; i++)
                    if (BanCo[i, Cot] != null)
                        return false;
            }
            else return false;
        }
        else return false;
        return true;
    }
}
