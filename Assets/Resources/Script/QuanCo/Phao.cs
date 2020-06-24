using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phao : QuanCo
{
    public Phao(Color color, int Hang, int Cot) : base(Hang, Cot)
    {
        this.TypePiece = Type.PHAO;
        this.ColorPiece = color;
        string path = (color == Color.RED) ? "Prefabs/QuanCo/2Phao" : "Prefabs/Quanco/1Phao";
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
        //Nếu 2 vị trí cùng hàng thì đếm số quân giữa chúng
        int count = 0;
        if (Hang == HangMoi)
        {
            int min = Mathf.Min(Cot, CotMoi);
            int max = Mathf.Max(Cot, CotMoi);
            for (int i = min + 1; i < max; i++)
                if (BanCo[Hang, i] != null)
                    ++count;
        }
        //Nếu 2 vị trí cùng cột thì đếm số quân giữa chúng
        else if (Cot == CotMoi)
        {
            int min = Mathf.Min(Hang, HangMoi);
            int max = Mathf.Max(Hang, HangMoi);
            for (int i = min + 1; i < max; i++)
                if (BanCo[i, Cot] != null)
                    ++count;
        }
        //Nếu giữa 2 vị trí có nhiều hơn 1 quân thì loại
        if (count >= 2)
            return false;
        //Nếu giữa chúng có 1 quân và vị trí mới ko phải quân địch thì loại
        else if (count == 1)
        {
            if (BanCo[HangMoi, CotMoi] == null)
                return false;
        }
        //Nếu giữa chúng ko có quân và vị trí mới có quân địch thì loại
        else if (count == 0)
            if (BanCo[HangMoi, CotMoi] != null)
                return false;
        return true;
    }

    public override List<Move> ListCanMove(QuanCo[,] BanCo)
    {
        List<Move> lMove = new List<Move>();
        for (int i = Hang; i < 10; i++)
            if(Rule(BanCo, i, Cot))
                lMove.Add(new Move(Hang, Cot, i, Cot, BanCo[i, Cot]));
        for (int i = Hang; i >= 0; i--)
            if (Rule(BanCo, i, Cot))
                lMove.Add(new Move(Hang, Cot, i, Cot, BanCo[i, Cot]));
        for (int i = Cot; i < 9; i++)
            if (Rule(BanCo, Hang, i))
                lMove.Add(new Move(Hang, Cot, Hang, i, BanCo[Hang, i]));
        for (int i = Cot; i >= 0; i--)
            if (Rule(BanCo, Hang, i))
                lMove.Add(new Move(Hang, Cot, Hang, i, BanCo[Hang, i]));
        return lMove;
    }
}
