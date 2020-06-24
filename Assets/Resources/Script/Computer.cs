using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Computer : Player
{
    
    public Computer(Color color)
    {
        this.ColorPlayer = color;
    }
    public override Move FindMove(QuanCo[,] BanCo)
    {
        Move move = null;
        List<Move> lMove = GetPossibleBoards(BanCo, ColorPlayer);
        List<Move> lBestMove = new List<Move>();
        int CountMove = lMove.Count;
        int depth = 3;
        //if (CountMove > 30)
        //    if ((move = FindBookMove(BanCo)) != null)
        //        return move;
        if (CountMove < 7)
                depth = 7;
        else if (CountMove < 10)
            depth = 6;
        else if (CountMove < 15)
            depth = 5;
        else if (CountMove < 25)
            depth = 4;
        int BestMoveValue = int.MinValue;
        for (int i = 0; i < lMove.Count; i++)
        {
            DoFakeMove(BanCo, lMove[i]);
            int value = FindMinScoreForBoard(BanCo, int.MinValue, int.MaxValue, depth);
            UnDoFakeMove(BanCo);
            if (value == BestMoveValue)
                lBestMove.Add(lMove[i]);
            else if (value > BestMoveValue)
            {
                BestMoveValue = value;
                lBestMove.Clear();
                lBestMove.Add(lMove[i]);
            }
        }
        move = lBestMove[Random.Range(0, lBestMove.Count)];
        return move;
    }
    
    /// <summary>
    /// Hàm lấy toàn bộ nước đi của người chơi
    /// </summary>
    /// <param name="BanCo"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    private List<Move> GetPossibleBoards(QuanCo[,] BanCo, Color color)
    {
        List<Move> lMove = new List<Move>();
        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 9; j++)
                if(BanCo[i,j] != null)
                    if(BanCo[i,j].ColorPiece == color)
                        lMove.AddRange(BanCo[i, j].ListCanMove(BanCo));
        return lMove;
    }
    private int[] Diem = { 0, 6000, 600, 285, 270, 120, 120, 60 };
    #region Điểm
    static int[,,] ViTri =
    {
                //Null
        {
            {0,0,0, 0,0,0, 0,0,0},
            {0,0,0, 0,0,0, 0,0,0},
            {0,0,0, 0,0,0, 0,0,0},
            {0,0,0, 0,0,0, 0,0,0},
            {0,0,0, 0,0,0, 0,0,0},

            {0,0,0, 0,0,0, 0,0,0},
            {0,0,0, 0,0,0, 0,0,0},
            {0,0,0, 0,0,0, 0,0,0},
            {0,0,0, 0,0,0, 0,0,0},
            {0,0,0, 0,0,0, 0,0,0}
        },

        // Tướng
        {
            {0,0,0, 0,0,0, 0,0,0},
            {0,0,0, 0,0,0, 0,0,0},
            {0,0,0, 0,0,0, 0,0,0},
            {0,0,0, 0,0,0, 0,0,0},
            {0,0,0, 0,0,0, 0,0,0},

            {0,0,0, 0,0,0, 0,0,0},
            {0,0,0, 0,0,0, 0,0,0},
            {0,0,0, 0,0,0, 0,0,0},
            {0,0,0, -2,0,-2, 0,0,0},
            {0,0,0, 0,5,0, 0,0,0}
        },

        // Xe
        {
            {-2, 10, 6, 14, 12, 14, 6, 10,-2},
            { 8, 4, 8, 16, 8, 16, 8, 4, 8},
            { 4, 8, 6, 14, 12, 14, 6, 8, 4},
            {6,10,8, 14,12,14, 8,10,6},
            {12,16,14, 20,20,20, 14,16,12},

            {12,14,12, 18,18,18, 12,14,12},
            {12,18,16, 22,22,22, 16,18,12},
            {12,12,12, 18,18,18, 12,12,12},
            {16,20,18, 24,26,24, 16,20,18},
            {14,14,12, 18,16,18, 12,14,14}
        },
// Pháo
{
    {6,4,0, -10,-12,-10, 0,4,6},
    {2,-2,0, -4,-14,-4, 0,-2,2},
    {2,-2,0, -10,-8,-10, 0,-2,2},
    {0,0,-2, 4,10,4, -2,0,0},
    {0,0,0, 2,8,2, 0,0,0},

    {-2,0,4, 2,6,2, 4,0,-2},
    {0,0,0, 2,4,2, 0,0,0},
    {4,0,8, 6,10,6, 8,0,4},
    {0,2,4, 6,2,6, 4,2,0},
    {0,0,2, 6,1,6, 2,0,0}
},
// Mã
{
    {4,8,16, 12,4,12, 16,8,4},
    {4,10,28, 16,8,16, 28,10,4},
    {12,-14,16, 20,18,20, 16,-14,12},
    {8,24,18, 24,20,24, 18,24,8},
    {6,16,14, 18,16,18, 14,16,6},

    {4,12,16, 14,12,14, 16,12,4},
    {2,6,8, 6,10,6, 8,6,2},
    {4,2,8, 8,4,8, 8,2,4},
    {0,2,4, 4,-4,4, 4,2,0},
    {0,-4,0, -2,-4,-2, 0,-4,0}
},
// Tịnh
{
    {0,0,0, 0,0,0, 0,0,0},
    {0,0,0, 0,0,0, 0,0,0},
    {0,0,0, 0,0,0, 0,0,0},
    {0,0,0, 0,0,0, 0,0,0},
    {0,0,0, 0,0,0, 0,0,0},

    {0,0,2, 0,0,0, 2,0,0},
    {0,0,0, 0,0,0, 0,0,0},
    {4,0,0, 0,15,0, 0,0,4},
    {0,0,0, 0,0,0, 0,0,0},
    {0,0,10, 0,0,0, 10,0,0}
},
// sĩ
{
    {0,0,0, 0,0,0, 0,0,0},
    {0,0,0, 0,0,0, 0,0,0},
    {0,0,0, 0,0,0, 0,0,0},
    {0,0,0, 0,0,0, 0,0,0},
    {0,0,0, 0,0,0, 0,0,0},

    {0,0,0, 0,0,0, 0,0,0},
    {0,0,0, 0,0,0, 0,0,0},
    {0,0,0, 1,0,1, 0,0,0},
    {0,0,0, 0,7,0, 0,0,0},
    {0,0,0, 5,0,5, 0,0,0}
},



// tốt
{
    {0,3,6, 9,12,9, 6,3,0},
    {18,36,56, 80,120,80, 56,36,18},
    {14,26,42, 60,80,60, 42,26,14},
    {10,20,30, 34,40,34, 30,20,10},
    {6,12,18, 18,20,18, 18,12,6},

    {2,0,8, 0,8,0, 8,0,2},
    {0,0,-2, 0,4,0, -2,0,0},
    {0,0,0, 0,0,0, 0,0,0},
    {0,0,0, 0,0,0, 0,0,0},
    {0,0,0, 0,0,0, 0,0,0}
} };
    #endregion
    /// <summary>
    /// Tính điểm cho người chơi
    /// </summary>
    /// <param name="BanCo"></param>
    /// <returns></returns>
    int CalculateScoreForBoard(QuanCo[,] BanCo)
    {
        int Sum = 0;
        if (ColorPlayer == Color.GREEN)
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 9; j++)
                    if (BanCo[i, j] != null)
                    {
                        int type = (int)BanCo[i, j].TypePiece;
                        if (BanCo[i, j].ColorPiece == ColorPlayer)
                            Sum += Diem[type] + ViTri[type, i, j];
                        else Sum -= Diem[type] + ViTri[type, 9 - i, 8 - j];
                    }
        }
        else
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 9; j++)
                    if (BanCo[i, j] != null)
                    {
                        int type = (int)BanCo[i, j].TypePiece;
                        if (BanCo[i, j].ColorPiece == ColorPlayer)
                            Sum += Diem[type] + ViTri[type, 9 - i, 8 - j];
                        else Sum -= Diem[type] + ViTri[type, i, j];
                    }
        }
        return Sum;
    }
    private int FindMinScoreForBoard(QuanCo[,] BanCo, int alpha, int beta, int depth)
    {
        int val = CalculateScoreForBoard(BanCo);
        if (depth == 0 || val < -5000)
            return val - depth;
        List<Move> lMove = GetPossibleBoards(BanCo, 1 - ColorPlayer);
        int value = int.MaxValue;
        for (int i = 0; i < lMove.Count; i++)
        {
            DoFakeMove(BanCo, lMove[i]);
            value = Mathf.Min(value, FindMaxScoreForBoard(BanCo, alpha, beta, depth - 1));
            UnDoFakeMove(BanCo);
            beta = Mathf.Min(beta, value);
            if (alpha >= beta)
                break;
            //Debug.Log("min: " + lMove[i].HangCu + " " + lMove[i].CotCu + ":" + lMove[i].HangMoi + " " + lMove[i].CotMoi + ": " + value);
        }

        return beta;
    }
    private int FindMaxScoreForBoard(QuanCo[,] BanCo, int alpha, int beta, int depth)
    {
        int val = CalculateScoreForBoard(BanCo);
        if (depth == 0 || val > 5000)
            return val + depth;
        List<Move> lMove = GetPossibleBoards(BanCo, ColorPlayer);
        int value = int.MinValue;
        for (int i = 0; i < lMove.Count; ++i)
        {
            DoFakeMove(BanCo, lMove[i]);
            value = Mathf.Max(value, FindMinScoreForBoard(BanCo, alpha, beta, depth - 1));
            UnDoFakeMove(BanCo);
            alpha = Mathf.Max(alpha, value);
            if (alpha >= beta)
                break;
            //Debug.Log("max: " + lMove[i].HangCu + " " + lMove[i].CotCu + ":" + lMove[i].HangMoi + " " + lMove[i].CotMoi + ": " + value);
        }

        return alpha;
    }
    private void DoFakeMove(QuanCo[,] BanCo, Move move)
    {
        stkFakeMove.Push(move);
        BanCo[move.HangMoi, move.CotMoi] = BanCo[move.HangCu, move.CotCu];
        BanCo[move.HangCu, move.CotCu] = null;
    }
    private void UnDoFakeMove(QuanCo[,] BanCo)
    {
        if (stkFakeMove.Count == 0)
            return;
        Move move = stkFakeMove.Pop();
        BanCo[move.HangCu, move.CotCu] = BanCo[move.HangMoi, move.CotMoi];
        BanCo[move.HangMoi, move.CotMoi] = move.TargetPiece;
    }
    private Move FindBookMove(QuanCo[,] BanCo)
    {
        Move move = null;
        string str = EndCodingChessBoard(BanCo);
        //str += "|" + ((ColorPlayer == Color.RED) ? "w" : "b");

        StreamReader reader = new StreamReader("BOOK.DAT.txt");
        string line;
        while ((line = reader.ReadLine()) != null)
            if (line.IndexOf(str) > 0)
            {
                move = new Move();
                move.HangCu = System.Convert.ToInt32(line[1]) - 48;
                move.CotCu = System.Convert.ToInt32(line[0]) - 97;
                move.HangMoi = System.Convert.ToInt32(line[3]) - 48;
                move.CotMoi = System.Convert.ToInt32(line[2]) - 97;
                Debug.Log(move.HangCu + " " + move.CotCu + " " + move.HangMoi + " " + move.CotMoi);
                return move;
            }
        reader.Close();
        return null;
    }
    private string EndCodingChessBoard(QuanCo[,] BanCo)
    {
        string str = "";
        int CountSpace = 0;
        if(ColorPlayer == Color.RED)
        {
            for (int i = 9; i >= 0; i--)
            {
                for (int j = 0; j < 9; j++)
                {
                    if(BanCo[i,j] != null)
                    {
                        if (CountSpace != 0) str += CountSpace.ToString();
                        CountSpace = 0;
                        str += ColorToString(BanCo[i, j].ColorPiece, BanCo[i, j].TypePiece);
                    }
                    else
                    {
                        ++CountSpace;
                    }
                }
                if (CountSpace != 0) str += CountSpace.ToString();
                CountSpace = 0;
                if (i > 0) str += @"/";
            }
        }
        else
        {
            for (int i = 9; i >= 0; i--)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (BanCo[i, j] != null)
                    {
                        if(CountSpace != 0) str += CountSpace.ToString();
                        CountSpace = 0;
                        str += ColorToString(1-BanCo[i, j].ColorPiece, BanCo[i, j].TypePiece);
                    }
                    else
                    {
                        ++CountSpace;
                    }
                }
                if (CountSpace != 0) str += CountSpace.ToString();
                CountSpace = 0;
                if (i > 0) str += @"/";
            }
        }
        return str;
    }
    private char ColorToString(Color color, Type type)
    {
        char c = ' ';
        switch (type)
        {
            case Type.TUONG: 
                c = 'k';
                break;
            case Type.XE:
                c = 'r';
                break;
            case Type.PHAO:
                c = 'c';
                break;
            case Type.MA:
                c = 'n';
                break;
            case Type.TINH:
                c = 'b';
                break;
            case Type.SY:
                c = 'a';
                break;
            case Type.TOT:
                c = 'p';
                break;
        }
        if (color == ColorPlayer) c = char.ToUpper(c);
        return c;
    }
}
