using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ObjMenu;
    public GameObject ObjPlayOffline;
    public GameObject ObjPlayOnline;
    public GameObject ObjInfo;
    public GameObject ObjWaiting;

    public GameObject IPInput;
    public GameObject nameInput;

    public GameObject serverPrefab;
    public GameObject clientPrefab;
    #region Menu
    /// <summary>
    /// Chơi offline
    /// </summary>
    public void PlayOffline()
    {
        ObjMenu.SetActive(false);
        ObjPlayOffline.SetActive(true);
        ObjPlayOnline.SetActive(false);
        ObjInfo.SetActive(false);
    }
    /// <summary>
    /// Chơi online
    /// </summary>
    public void PlayOnline()
    {
        ObjMenu.SetActive(false);
        ObjPlayOffline.SetActive(false);
        ObjPlayOnline.SetActive(true);
        ObjInfo.SetActive(false);
    }
    /// <summary>
    /// Trờ về trang menu
    /// </summary>
    public void Back()
    {
        ObjMenu.SetActive(true);
        ObjPlayOffline.SetActive(false);
        ObjPlayOnline.SetActive(false);
        ObjInfo.SetActive(false);
        ObjWaiting.SetActive(false);
    }
    public void Info()
    {
        ObjMenu.SetActive(false);
        ObjPlayOffline.SetActive(false);
        ObjPlayOnline.SetActive(false);
        ObjInfo.SetActive(true);
    }
    /// <summary>
    /// Thoát khỏi chương trình
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }
    #endregion
    #region Option
    /// <summary>
    /// Quay về menu
    /// </summary>
    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Flag()
    {
        GameObject.FindGameObjectWithTag("ChessBoard").GetComponent<ChessBoardOffline>().Init();
    }
    public void Setting()
    {
        ChessBoard.GameMode = 3;
        GameObject.FindGameObjectWithTag("ChessBoard").GetComponent<ChessBoardOffline>().Init();
    }
    public void Undo()
    {
        GameObject.FindGameObjectWithTag("ChessBoard").GetComponent<ChessBoardOffline>().Undo();
    }
    #endregion
    #region Offline
    public void SinglePlay()
    {
        SceneManager.LoadScene("GameOffline");
        ChessBoard.GameMode = 1;
    }
    public void MultiPlay()
    {
        SceneManager.LoadScene("GameOffline");
        ChessBoard.GameMode = 2;
    }
    public void Challenge()
    {

    }
    #endregion
    #region Online
    public void Connect()
    {
        string IPAddress = IPInput.GetComponent<TextMeshProUGUI>().text;
        string hostAddress;
        int port;
        if (IPAddress.Length > 0)
        {
            string[] ip = IPAddress.Split('/');
            hostAddress = ip[0];
            port = int.Parse(ip[1]);
        }
        else
        {
            hostAddress = "192.168.1.101";
            port = 2706;
        }
        try
        {
            Client c = Instantiate(clientPrefab).GetComponent<Client>();
            c.clientName = nameInput.GetComponent<TextMeshProUGUI>().text;
            if (c.clientName == "")
                c.clientName = "Client";

            c.ConnectToServer(hostAddress, port);
            ObjWaiting.SetActive(true);
        }
        catch
        {

        }
    }
    #endregion
}
