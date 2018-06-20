using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : Photon.PunBehaviour
{
    public GameObject LobbyManager;
    public Text roomText;
    public GameObject[] place;
    public GameObject[] readyLabel;
    public Material unReadyMaterial, readyMaterial;
    public GameObject playerModel;
    public Material[] materials;
    private bool amIready = false;
    private int readyPlayer, myIndex;


    #region User Methods
    public void ExitRoom()
    {
        DestroyObjectOnStation();
        PhotonNetwork.LeaveRoom();
    }

    public void CallGetReady()
    {
        if(amIready) { amIready = false; }
        else { amIready = true; }
        photonView.RPC("GetReady", PhotonTargets.AllBufferedViaServer, amIready, myIndex);
    }

    public void CreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom(roomText.text + "§" + PhotonNetwork.playerName, new RoomOptions() { MaxPlayers = 4 }, null);
    }

    public void Populate()
    {
        int index = 0;
        List<PhotonPlayer> listOfPlayer = new List<PhotonPlayer>(PhotonNetwork.playerList);
        listOfPlayer.Sort(SortByID);
        foreach (PhotonPlayer player in listOfPlayer)
        {
            if(player.IsLocal)
            {
                myIndex = index;
            }
            GameObject go = Instantiate(playerModel,
                            place[index].transform.position,
                            new Quaternion(0f, 90f, 0f, 0f),
                            place[index].transform);
            go.transform.parent = place[index].transform;
            place[index].transform.parent.GetChild(5).GetComponentInChildren<Text>().text = player.NickName;
            Material[] tmpBody = go.GetComponentInChildren<SkinnedMeshRenderer>().materials;
            tmpBody[3] = materials[index];
            go.GetComponentInChildren<SkinnedMeshRenderer>().materials = tmpBody;
            Material[] tmpHip = go.transform.GetChild(0).Find("Cube.003").GetComponent<SkinnedMeshRenderer>().materials;
            tmpHip[0] = materials[index];
            go.transform.GetChild(0).Find("Cube.003").GetComponent<SkinnedMeshRenderer>().materials = tmpHip;
            index++;
        }
    }

    public static int SortByID(PhotonPlayer a, PhotonPlayer b)
    {
        return a.ID.CompareTo(b.ID);
    }

    
    public void DestroyObjectOnStation()
    {
        foreach (GameObject player in place)
        {
            if(player.transform.childCount == 1)
            {
                player.transform.parent.GetComponentInChildren<Text>().text = "";
                Destroy(player.transform.GetChild(0).gameObject);
            }
        }
    }
    
    #endregion

    #region PunRPC
    [PunRPC]
    public void GetReady(bool b, int index)
    {
        if(b)
        {
            readyPlayer++;
            readyLabel[index].gameObject.SetActive(true);
        }
        else
        {
            readyPlayer--;
            readyLabel[index].gameObject.SetActive(false);
        }
        if(readyPlayer == PhotonNetwork.playerList.Length && PhotonNetwork.playerList.Length > 1)
        {
            // Debug this
            // PhotonNetwork.room.IsVisible = false;
            PhotonNetwork.LoadLevel(1);
        }
    }
    #endregion

    #region Photon.PunBehaviour Callbacks
    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        base.OnPhotonPlayerConnected(newPlayer);
        DestroyObjectOnStation();
        Populate();
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        base.OnPhotonPlayerDisconnected(otherPlayer);
        DestroyObjectOnStation();
        Populate();
    }

    public override void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);
        ExitRoom();
        UIManager.Instance.EnablePanel(UIManager.Instance.Panels[1]);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        DestroyObjectOnStation();
        Populate();
    }
    #endregion
}
