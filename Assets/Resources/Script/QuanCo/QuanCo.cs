using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuanCo 
{
    public GameObject Instance { get; set; }
    public int Hang { get; set; }
    public int Cot { get; set; }
    public Type TypePiece { get; set; }
    public Color ColorPiece { get; set; }
    public QuanCo(int Hang, int Cot)
    {
        this.Hang = Hang;
        this.Cot = Cot;
    }
    /// <summary>
    /// Kiểm tra quân cờ có thể đi tới vị trí mới không?
    /// </summary>
    public virtual bool Rule(QuanCo[,] BanCo, int HangMoi, int CotMoi)
    {
        return true;
    }
    /// <summary>
    /// Đưa ra danh sách các nước quân hiện tại có thể đi
    /// </summary>
    public virtual List<Move> ListCanMove(QuanCo[,] BanCo)
    {
        return null;
    }
    /// <summary>
    /// Di chuyển quân cờ tới vị trí mới
    /// </summary>
    /// <param name="HangMoi"></param>
    /// <param name="CotMoi"></param>
    public void TryMove(int HangMoi, int CotMoi)
    {
        this.Hang = HangMoi;
        this.Cot = CotMoi;
        this.Instance.transform.position = new Vector3(CotMoi, HangMoi);
    }
    /// <summary>
    /// Tính bình phương khoảng cách giữa điểm đầu và cuối
    /// </summary>
    public int CalculateDistance(int HangCu, int CotCu, int HangMoi, int CotMoi)
    {
        return  (HangMoi - HangCu) * (HangMoi - HangCu) + (CotMoi - CotCu) * (CotMoi - CotCu);
    }
}
/// <summary>
/// Màu của quân cờ
/// </summary>
public enum Color
{
    RED, GREEN, NULL
}
/// <summary>
/// Loại quân cờ
/// </summary>
public enum Type
{
    NULL ,
    TUONG ,
    XE,
    PHAO ,
    MA ,
    TINH ,
    SY ,
    TOT 
}
