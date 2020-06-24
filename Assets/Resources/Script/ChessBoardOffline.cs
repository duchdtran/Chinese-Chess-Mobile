using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ChessBoardOffline : ChessBoard
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Move move = null;
        if (!isEndGame)
        {
            Timer(isRedTurn);
            if (isRedTurn)
                move = Players[0].FindMove(BanCo);
            else
                move = Players[1].FindMove(BanCo);

            if (move != null)
            {
                stkMove.Push(move);
                move.TryMove(BanCo);
                KiemTraChieuTuong(BanCo);
                KiemTraHoaCo(BanCo);
                KiemTraEndGame(BanCo);
                isRedTurn = !isRedTurn;
            }
        }
    }

}
