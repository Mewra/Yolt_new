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
    public void JoinRoom()
    {
        Debug.Log(SelectionManager.roomSelected);
        if (SelectionManager.roomSelected != "")
        {
            PhotonNetwork.JoinRoom(SelectionManager.roomSelected);
            UIManager.Instance.EnablePanel(UIManager.Instance.Panels[3]);
        }
    }

    public void UpdateRoom(RoomInfo room)
    {
        GameObject go = Instantiate(roomPrefab, instanceGrid.transform);
        go.transform.GetChild(0).GetComponent<Text>().text = room.Name.Split('§')[0];
        go.transform.GetChild(1).GetComponent<Text>().text = room.PlayerCount.ToString() + go.transform.GetChild(1).GetComponent<Text>().text;
        go.transform.GetChild(2).GetComponent<Text>().text = room.Name.Split('§')[1];
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
