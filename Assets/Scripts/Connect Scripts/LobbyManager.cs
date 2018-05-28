using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class LobbyManager : Photon.PunBehaviour
{
    public GameObject[] roomList;
    public GameObject roomPrefab, gridPrefab;
    public GameObject gridParent;
    public GameObject mainCamera;
    public GameObject instanceGrid;

    #region User Methods
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
        UIManager.Instance.EnablePanel(UIManager.Instance.Panels[3]);
    }

    public void UpdateRoom(RoomInfo room)
    {
        GameObject go = Instantiate(roomPrefab, instanceGrid.transform);
        go.transform.GetChild(1).GetComponent<Text>().text = room.Name.Split('§')[0]; // room's name
        go.transform.GetChild(3).GetComponent<Text>().text = room.Name.Split('§')[1]; // room's owner
        go.transform.GetChild(5).GetComponent<Text>().text = room.PlayerCount.ToString() + " / 4"; // number of players
        go.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(() => JoinRoom(room.Name)); // Join's button 
    }

    public GameObject RestoreGridLayout()
    {
        GameObject gp = Instantiate(gridPrefab, gridParent.transform);
        gp.transform.SetAsFirstSibling();
        gridParent.GetComponentInParent<ScrollRect>().content = gp.GetComponent<RectTransform>();
        return gp;
    }
    #endregion

    #region Photon.PunBehaviour Callbacks
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
    }

    public override void OnReceivedRoomListUpdate()
    {
        base.OnReceivedRoomListUpdate();
        if(instanceGrid != null)
        {
            Destroy(instanceGrid);
        }
        instanceGrid = RestoreGridLayout();
        foreach (RoomInfo room in PhotonNetwork.GetRoomList())
        {
            UpdateRoom(room);
        }
    }
    #endregion
}
