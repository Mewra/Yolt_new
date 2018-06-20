using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterials : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        if(gameObject.GetPhotonView().isMine)
        {
            int index = 0;
            int myIndex = 0;
            List<PhotonPlayer> listOfPlayer = new List<PhotonPlayer>(PhotonNetwork.playerList);
            listOfPlayer.Sort(SortByID);
            foreach (PhotonPlayer player in listOfPlayer)
            {
                if (player.IsLocal)
                {
                    myIndex = index;
                }
                gameObject.GetPhotonView().RPC("ChangeMaterials", PhotonTargets.AllBufferedViaServer, gameObject.GetPhotonView().viewID, index, myIndex);
                index++;
            }
        }
    }

    public static int SortByID(PhotonPlayer a, PhotonPlayer b)
    {
        return a.ID.CompareTo(b.ID);
    }

    [PunRPC]
    public void ChangeMaterials(int ID, int index, int myIndex)
    {
        PhotonView view = PhotonView.Find(ID);
        if(view != null)
        {
            Material[] tmpBody = view.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials;
            Material[] tmpHip = view.gameObject.transform.GetChild(0).Find("Cube.003").GetComponent<SkinnedMeshRenderer>().materials;
            tmpBody[3] = GameManager.Instance.materials[(view.gameObject.GetComponent<PhotonView>().isMine) ? myIndex : index];
            tmpHip[0] = GameManager.Instance.materials[(gameObject.GetComponent<PhotonView>().isMine) ? myIndex : index];
            view.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials = tmpBody;
            view.gameObject.transform.GetChild(0).Find("Cube.003").GetComponent<SkinnedMeshRenderer>().materials = tmpHip;
        }
    }
}
