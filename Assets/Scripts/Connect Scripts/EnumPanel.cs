using UnityEngine;

public class EnumPanel : MonoBehaviour
{
    public enum ActivePanel
    {
        ConnectPanel,
        LobbyPanel,
        CreatePanel,
        RoomPanel
    }
    public ActivePanel myActivePanel;
}

