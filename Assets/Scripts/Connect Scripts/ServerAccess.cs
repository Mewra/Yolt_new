using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerAccess : Photon.PunBehaviour
{

    public Text connectPanel;

    #region Unity.MonoBehaviour Callbacks
    void Awake()
    {
       
    }
    #endregion

    public void EnterLobby()
    {
        connectPanel.text = "Trying to connect to the server";
        PhotonNetwork.automaticallySyncScene = true;
        if (PhotonNetwork.connectionStateDetailed == ClientState.PeerCreated)
        {
            PhotonNetwork.ConnectUsingSettings("0.1");
        }
        if (System.String.IsNullOrEmpty(PhotonNetwork.playerName))
        {
            PhotonNetwork.playerName = "Guest#" + Random.Range(1, 9999);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    #region Photon.PunBehavoiur Callbacks
    public override void OnConnectedToMaster()
    {
        connectPanel.text = "Succesfully connected";
        PhotonNetwork.JoinLobby();
        UIManager.Instance.EnablePanel(UIManager.Instance.Panels[1]); // 1 is the Lobby Panel
    }

    public override void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        base.OnFailedToConnectToPhoton(cause);
        connectPanel.text = "Connection failed: " + cause;
        // open message with error
    }
    #endregion
}
