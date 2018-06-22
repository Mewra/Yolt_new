using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterials : MonoBehaviour
{

    public Material[] body;
    public Material[] hip;
    public Material[] bodyGhost;
    public Material[] hipGhost;

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
                gameObject.GetPhotonView().RPC("ChangeMaterials", PhotonTargets.AllBufferedViaServer, gameObject.GetPhotonView().viewID, myIndex);
                index++;
            }

            GameObject go = gameObject.transform.GetChild(5).gameObject;

            go.SetActive(true);

            ParticleSystem ps = go.transform.GetChild(0).GetComponent<ParticleSystem>();
            var col = ps.colorOverLifetime;
            Gradient grad = new Gradient();
            grad.SetKeys(new GradientColorKey[] { new GradientColorKey(GameManager.Instance.materials[myIndex].color, 0.0f), new GradientColorKey(GameManager.Instance.materials[myIndex].color, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 1.0f), new GradientAlphaKey(1.0f, 1.0f) });
            col.color = grad;

        }
    }

    public static int SortByID(PhotonPlayer a, PhotonPlayer b)
    {
        return a.ID.CompareTo(b.ID);
    }

    [PunRPC]
    public void ChangeMaterials(int ID, int myIndex)
    {
        PhotonView view = PhotonView.Find(ID);
        if(view != null)
        {
            Material[] tmpBody = body; // view.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials;
            Material[] tmpHip = hip; // view.gameObject.transform.GetChild(0).Find("Cube.003").GetComponent<SkinnedMeshRenderer>().materials;
            Material[] tmpBodyGhost = bodyGhost;
            Material[] tmpHipGhost = hipGhost;
            // set materials for player
            tmpBody[3] = GameManager.Instance.materials[myIndex];
            tmpHip[0] = GameManager.Instance.materials[myIndex];
            view.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials = tmpBody;
            view.gameObject.transform.GetChild(0).Find("Cube.003").GetComponent<SkinnedMeshRenderer>().materials = tmpHip;
            // set materials for ghost
            tmpBodyGhost[3] = GameManager.Instance.materials[myIndex];
            tmpHipGhost[0] = GameManager.Instance.materials[myIndex];
            view.gameObject.transform.GetChild(4).GetChild(1).GetComponent<SkinnedMeshRenderer>().materials = tmpBodyGhost;
            view.gameObject.transform.GetChild(4).GetChild(2).GetComponent<SkinnedMeshRenderer>().materials = tmpHipGhost;
        }
    }
}
