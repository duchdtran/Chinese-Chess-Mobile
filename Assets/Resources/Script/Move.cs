using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lưu nước đi
/// </summary>
public class Move 
{
    public static GameObject Selected = GameObject.Find("selected");
    public static GameObject Selected1 = GameObject.Find("selected2");

    public int HangCu;
    public int CotCu;
    public int HangMoi;
    public int CotMoi;

    public QuanCo TargetPiece;
    public Move()
    {

    }
    public Move(int Hang, int Cot)
    {
        this.HangCu = Hang;
        this.CotCu = Cot;
    }
    public Move(int HangCu, int CotCu, int HangMoi, int CotMoi, QuanCo TargetPiece)
    {
        this.HangCu = HangCu;
        this.CotCu = CotCu;
        this.HangMoi = HangMoi;
        this.CotMoi = CotMoi;
        this.TargetPiece = TargetPiece;
    }
    /// <summary>
    /// Di chuyển quân cờ
    /// </summary>
    /// <param name="BanCo"></param>
    public void TryMove(QuanCo[,] BanCo)
    {
        
        MakeMove(BanCo);
    }

    public void MakeMove(QuanCo[,] BanCo)
    {
        GameObject.Find("selected").transform.position = new Vector3(CotCu, HangCu);
        GameObject.Find("selected2").transform.position = new Vector3(CotMoi, HangMoi);
        if (BanCo[HangCu, CotCu] == null)
            return;
        if (BanCo[HangMoi, CotMoi] != null)
        {
            TargetPiece = BanCo[HangMoi, CotMoi];
            SoundManager.playSoundTarget((int)BanCo[HangCu, CotCu].TypePiece);
            BanCo[HangMoi, CotMoi].Instance.transform.position = new Vector3(-2, 0);
        }
        else
            SoundManager.playSoundMark();
        BanCo[HangCu, CotCu].TryMove(HangMoi, CotMoi);
        BanCo[HangMoi, CotMoi] = BanCo[HangCu, CotCu];
        BanCo[HangCu, CotCu] = null;
    }
    public Color KiemTraChieuTuong(QuanCo[,] BanCo)
    {
        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 9; j++)
                if (BanCo[i, j] != null)
                {
                    List<Move> lMove = BanCo[i, j].ListCanMove(BanCo);
                    for (int k = 0; k < lMove.Count; k++)
                        if (BanCo[lMove[k].HangMoi, lMove[k].CotMoi] != null)
                            if (BanCo[lMove[k].HangMoi, lMove[k].CotMoi].TypePiece == Type.TUONG)
                                return BanCo[lMove[k].HangMoi, lMove[k].CotMoi].ColorPiece;
                }
        return Color.NULL;
    }
}
