using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    public QuanCo[,] BanCo;
    public Player[] Players;
    public bool isRedTurn;
    public static int GameMode = 1;
    public Stack<Move> stkMove;
    public GameObject Notify;

    public GameObject[] TimeText;
    private float[] time;

    public bool isEndGame;
    /// <summary>
    /// Hàm khởi tạo
    /// </summary>
    public void Init()
    {
        GameObject[] ObjCanMove = GameObject.FindGameObjectsWithTag("CanMove");
        for (int i = 0; i < ObjCanMove.Length; i++)
            Destroy(ObjCanMove[i]);
        stkMove = new Stack<Move>();
        Notify.SetActive(false);
        //SoundManager.playSoundReady();
        time = new float[2] { 900, 900 };
        isEndGame = false;
        isRedTurn = true;
        Players = new Player[2];
        if (GameMode == 2)
        {
            Players[0] = new Player(Color.RED);
            Players[1] = new Player(Color.GREEN);
        }
        else if (GameMode == 1)
        {
            Players[0] = new Player(Color.RED);
            Players[1] = new Computer(Color.GREEN);
        }
        else if(GameMode == 3)
        {
            Players[0] = new Computer(Color.RED);
            Players[1] = new Player(Color.GREEN);
        }
        else if (GameMode == 4)
        {
            Players[0] = new Computer(Color.RED);
            Players[1] = new Computer(Color.GREEN);
        }

        BanCo = new QuanCo[10, 9];
        GameObject.Find("selected").transform.position = new Vector3(-2, 0);
        GameObject.Find("selected2").transform.position = new Vector3(-2, 0);
        GameObject[] Pieces = GameObject.FindGameObjectsWithTag("Piece");
        for (int i = 0; i < Pieces.Length; i++)
        {
            Destroy(Pieces[i]);
        }
        #region Quân đỏ
        BanCo[0, 0] = new Xe(Color.RED, 0, 0);
        BanCo[0, 1] = new Ma(Color.RED, 0, 1);
        BanCo[0, 2] = new Tinh(Color.RED, 0, 2);
        BanCo[0, 3] = new Sy(Color.RED, 0, 3);
        BanCo[0, 4] = new Tuong(Color.RED, 0, 4);
        BanCo[0, 5] = new Sy(Color.RED, 0, 5);
        BanCo[0, 6] = new Tinh(Color.RED, 0, 6);
        BanCo[0, 7] = new Ma(Color.RED, 0, 7);
        BanCo[0, 8] = new Xe(Color.RED, 0, 8);
        BanCo[2, 1] = new Phao(Color.RED, 2, 1);
        BanCo[2, 7] = new Phao(Color.RED, 2, 7);
        BanCo[3, 0] = new Tot(Color.RED, 3, 0);
        BanCo[3, 2] = new Tot(Color.RED, 3, 2);
        BanCo[3, 4] = new Tot(Color.RED, 3, 4);
        BanCo[3, 6] = new Tot(Color.RED, 3, 6);
        BanCo[3, 8] = new Tot(Color.RED, 3, 8);
        #endregion
        #region Quân Xanh
        BanCo[9, 0] = new Xe(Color.GREEN, 9, 0);
        BanCo[9, 1] = new Ma(Color.GREEN, 9, 1);
        BanCo[9, 2] = new Tinh(Color.GREEN, 9, 2);
        BanCo[9, 3] = new Sy(Color.GREEN, 9, 3);
        BanCo[9, 4] = new Tuong(Color.GREEN, 9, 4);
        BanCo[9, 5] = new Sy(Color.GREEN, 9, 5);
        BanCo[9, 6] = new Tinh(Color.GREEN, 9, 6);
        BanCo[9, 7] = new Ma(Color.GREEN, 9, 7);
        BanCo[9, 8] = new Xe(Color.GREEN, 9, 8);
        BanCo[7, 1] = new Phao(Color.GREEN, 7, 1);
        BanCo[7, 7] = new Phao(Color.GREEN, 7, 7);
        BanCo[6, 0] = new Tot(Color.GREEN, 6, 0);
        BanCo[6, 2] = new Tot(Color.GREEN, 6, 2);
        BanCo[6, 4] = new Tot(Color.GREEN, 6, 4);
        BanCo[6, 6] = new Tot(Color.GREEN, 6, 6);
        BanCo[6, 8] = new Tot(Color.GREEN, 6, 8);
        #endregion
    }
    /// <summary>
    /// Kiểm tra hoà cờ
    /// </summary>
    /// <param name="BanCo"></param>
    public void KiemTraHoaCo(QuanCo[,] BanCo)
    {
        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 9; j++)
                if(BanCo[i,j] != null)
                    if(BanCo[i,j].TypePiece != Type.TUONG && BanCo[i,j].TypePiece != Type.SY 
                        && BanCo[i, j].TypePiece != Type.TINH)
                    return;
        EndGame("Tie!", UnityEngine.Color.blue);
    }
    /// <summary>
    /// Kiểm tra xem kết thúc game chưa
    /// </summary>
    /// <param name="BanCo"></param>
    public void KiemTraEndGame(QuanCo[,] BanCo)
    {
        bool TuongDo = false, TuongDen = false;
        for (int j = 3; j < 6; j++)
            for (int i = 0; i < 10; i++)
                if (BanCo[i, j] != null)
                    if (BanCo[i, j].TypePiece == Type.TUONG)
                        if (BanCo[i, j].ColorPiece == Color.RED)
                            TuongDo = true;
                        else
                            TuongDen = true;
        if (!TuongDo)
        {
            EndGame("Green Win!", UnityEngine.Color.green);
        }
        else if (!TuongDen)
        {
            EndGame("Red Win!", UnityEngine.Color.red);
        }
    }
    /// <summary>
    /// Kết thúc game
    /// </summary>
    /// <param name="text"></param>
    /// <param name="color"></param>
    public void EndGame(string text, UnityEngine.Color color)
    {
        isEndGame = true;
        Notify.SetActive(true);
        Notify.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = text;
        Notify.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = color;
    }
    /// <summary>
    /// Kiểm tra chiếu tướng
    /// </summary>
    /// <param name="BanCo"></param>
    public void KiemTraChieuTuong(QuanCo[,] BanCo)
    {
        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 9; j++)
                if(BanCo[i,j] != null)
                {
                    List<Move> lMove = BanCo[i, j].ListCanMove(BanCo);
                    for (int k = 0; k < lMove.Count; k++)
                        if (BanCo[lMove[k].HangMoi, lMove[k].CotMoi] != null)
                            if (BanCo[lMove[k].HangMoi, lMove[k].CotMoi].TypePiece == Type.TUONG)
                                StartCoroutine(ShowCheckMate());
                }
    }
    /// <summary>
    /// Hiện thông báo chiếu tướng
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowCheckMate()
    {
        yield return new WaitForSeconds(0.1f);
        Notify.SetActive(true);
        Notify.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Checkmate!";
        SoundManager.playSoundChieu();
        StartCoroutine(HideCheckMate());
    }
    /// <summary>
    /// Tắt thông báo chiếu tướng
    /// </summary>
    /// <returns></returns>
    IEnumerator HideCheckMate()
    {
        yield return new WaitForSeconds(0.5f);
        if (!isEndGame) 
            Notify.SetActive(false);
        else KiemTraEndGame(BanCo);
    }
    public void Timer(bool isRedTurn)
    {
        //new Thread(() =>
        //{
            if (isRedTurn)
            {
                time[0] = time[0] - Time.deltaTime;
                TimeText[0].GetComponent<TMPro.TextMeshProUGUI>().text =
                    "Time: " + (int)time[0] / 60 + ":" + (int)time[0] % 60;
                if (time[0] < 0)
                    EndGame("Red Win!", UnityEngine.Color.red);
            }
            else
            {
                time[1] = time[1] - Time.deltaTime;
                TimeText[1].GetComponent<TMPro.TextMeshProUGUI>().text =
                    "Time: " + (int)time[1] / 60 + ":" + (int)time[1] % 60;
                if (time[1] < 0)
                    EndGame("Green Win!", UnityEngine.Color.green);
            }
        //}).Start();
        
    }
    /// <summary>
    /// Đi lại quân
    /// </summary>
    public void Undo()
    {
        try
        {
            GameObject[] ObjCanMove = GameObject.FindGameObjectsWithTag("CanMove");
            for (int i = 0; i < ObjCanMove.Length; i++)
            {
                Destroy(ObjCanMove[i]);
            }
            if (stkMove.Count == 0)
                return;
            Move move = stkMove.Pop();
            if (stkMove.Count > 0)
            {
                GameObject.Find("selected").transform.position = new Vector3(stkMove.Peek().CotCu, stkMove.Peek().HangCu);
                GameObject.Find("selected2").transform.position = new Vector3(stkMove.Peek().CotMoi, stkMove.Peek().HangMoi);
            }
            else
            {
                GameObject.Find("selected").transform.position = new Vector3(-1, 0);
                GameObject.Find("selected2").transform.position = new Vector3(-1, 0);
            }
            BanCo[move.HangMoi, move.CotMoi].TryMove(move.HangCu, move.CotCu);
            BanCo[move.HangCu, move.CotCu] = BanCo[move.HangMoi, move.CotMoi];
            BanCo[move.HangMoi, move.CotMoi] = move.TargetPiece;
            BanCo[move.HangMoi, move.CotMoi].Instance.transform.position =
                new Vector3(move.CotMoi, move.HangMoi);
        }
        catch
        {

        }
    }
}
