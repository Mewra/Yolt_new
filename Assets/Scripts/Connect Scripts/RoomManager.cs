using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : Photon.PunBehaviour
{
    public GameObject LobbyManager;
    public Text roomText;
    public GameObject[] station;
    public Material unReadyMaterial, readyMaterial;
    public GameObject playerModel;
    public Material[] materials;
    private bool amIready = false;
    private int readyPlayer, myIndex;


    #region User Methods
    public void ExitRoom()
    {
        DestroyObjectOnStation();
        ResetMaterial();
        PhotonNetwork.LeaveRoom();
    }

    public void CallGetReady()
    {
        if(amIready) { amIready = false; }
        else { amIready = true; }
        photonView.RPC("GetReady", PhotonTargets.AllBufferedViaServer, amIready, myIndex);
    }

    public void ResetMaterial()
    {
        foreach (GameObject st in station)
        {
            st.GetComponent<MeshRenderer>().material.color = unReadyMaterial.color;
        }
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
                            station[index].transform.position + (Vector3.up * 0.5f),
                            new Quaternion(0f, 90f, 0f, 0f),
                            station[index].transform); 
            go.GetComponent<MovementGhost>().enabled = false;
            go.GetComponent<RotatePlayer>().enabled = false;
            Material[] tmpBody = go.GetComponentInChildren<SkinnedMeshRenderer>().materials;
            tmpBody[3] = materials[index];
            go.GetComponentInChildren<SkinnedMeshRenderer>().materials = tmpBody;
            Material[] tmpHip = go.transform.Find("Cube.003").GetComponent<SkinnedMeshRenderer>().materials;
            tmpHip[0] = materials[index];
            go.transform.Find("Cube.003").GetComponent<SkinnedMeshRenderer>().materials = tmpHip;
            station[index].GetComponentInChildren<Text>().text = player.NickName;
            index++;
        }
    }

    public static int SortByID(PhotonPlayer a, PhotonPlayer b)
    {
        return a.ID.CompareTo(b.ID);
    }

    public void DestroyObjectOnStation()
    {
        foreach (GameObject player in station)
        {
            if(player.transform.childCount == 2)
            {
                Destroy(player.transform.GetChild(1).gameObject);
                player.GetComponentInChildren<Text>().text = "";
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
            station[index].GetComponent<MeshRenderer>().material.color = readyMaterial.color;
        }
        else
        {
            readyPlayer--;
            station[index].GetComponent<MeshRenderer>().material.color = unReadyMaterial.color;
        }
        if(readyPlayer == PhotonNetwork.playerList.Length && PhotonNetwork.playerList.Length > 1)
        {
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
