using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xe : QuanCo
{
    public Xe(Color color, int Hang, int Cot) : base(Hang, Cot)
    {
        this.TypePiece = Type.XE;
        this.ColorPiece = color;
        string path = (color == Color.RED) ? "Prefabs/QuanCo/2Xe" : "Prefabs/Quanco/1Xe";
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
        if (CotMoi < 0 || CotMoi > 8 || HangMoi < 0 || HangMoi > 9)
            return false;
        if (Hang != HangMoi && Cot != CotMoi)
            return false;
        if (BanCo[HangMoi, CotMoi] != null)
            if (ColorPiece == BanCo[HangMoi, CotMoi].ColorPiece)
                return false;
        int min, max;
        if(Cot == CotMoi)
        {
            if(HangMoi > Hang)
            {
                min = Hang;
                max = HangMoi;
            }
            else
            {
                min = HangMoi;
                max = Hang;
            }
            for (int i = min+1; i < max; i++)
                if (BanCo[i, Cot] != null)
                    return false;
        }
        else
        {
            if (CotMoi > Cot)
            {
                min = Cot;
                max = CotMoi;
            }
            else
            {
                min = CotMoi;
                max = Cot;
            }
            for (int i = min + 1; i < max; i++)
                if (BanCo[Hang, i] != null)
                    return false;
        }
        return true;
    }
    public override List<Move> ListCanMove(QuanCo[,] BanCo)
    {
        List<Move> lMove = new List<Move>();
        //if(Hang < 9)
        for (int i = Hang + 1; i < 10; i++)
            if (BanCo[i, Cot] == null)
                lMove.Add(new Move(Hang, Cot, i, Cot, BanCo[i, Cot]));
            else
            {
                if (BanCo[i, Cot].ColorPiece != ColorPiece)
                    lMove.Add(new Move(Hang, Cot, i, Cot, BanCo[i, Cot]));
                break;
            }
        //if (Hang > 0)
        for (int i = Hang - 1; i >= 0; i--)
            if (BanCo[i, Cot] == null)
                lMove.Add(new Move(Hang, Cot, i, Cot, BanCo[i, Cot]));
            else
            {
                if (BanCo[i, Cot].ColorPiece != ColorPiece)
                    lMove.Add(new Move(Hang, Cot, i, Cot, BanCo[i, Cot]));
                break;
            }
        //if(Cot < 8)
        for (int i = Cot + 1; i < 9; i++)
            if (BanCo[Hang, i] == null)
                lMove.Add(new Move(Hang, Cot, Hang, i, BanCo[Hang, i]));
            else
            {
                if (BanCo[Hang, i].ColorPiece != ColorPiece)
                    lMove.Add(new Move(Hang, Cot, Hang, i, BanCo[Hang, i]));
                break;
            }
        //if (Hang > 0)
        for (int i = Cot - 1; i >= 0; i--)
            if (BanCo[Hang, i] == null)
                lMove.Add(new Move(Hang, Cot, Hang, i, BanCo[Hang, i]));
            else
            {
                if (BanCo[Hang, i].ColorPiece != ColorPiece)
                    lMove.Add(new Move(Hang, Cot, Hang, i, BanCo[Hang, i]));
                break;
            }
        return lMove;
    }
}
