using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn : MonoBehaviour {


    //public GameObject _GOpc;
    public PlayerController _pcPlayer;
    public PlayerController _pcTarget;

    private string name;

	// Use this for initialization
	void Start () {

        //_pc = _GOpc.GetComponent<PlayerController>();
        name = GetComponent<Button>().name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeState() {
        
        if (name.Equals("asBtn")){
            // _pcPlayer.state = PlayerController.CLASSES.assassin;
            _pcPlayer.GetComponent<PhotonView>().RPC("ChangeState", PhotonTargets.AllBufferedViaServer, (int)PlayerController.CLASSES.assassin);
            _pcTarget._Slots[0] = false;
        }

        if (name.Equals("tankBtn")) {
            _pcPlayer.GetComponent<PhotonView>().RPC("ChangeState", PhotonTargets.AllBufferedViaServer, (int)PlayerController.CLASSES.tank);
            // _pcPlayer.state = PlayerController.CLASSES.tank;
            _pcTarget._Slots[1] = false;
        }

        if (name.Equals("supBtn")) {
            _pcPlayer.GetComponent<PhotonView>().RPC("ChangeState", PhotonTargets.AllBufferedViaServer, (int)PlayerController.CLASSES.support);
            // _pcPlayer.state = PlayerController.CLASSES.support;
            _pcTarget._Slots[2] = false;
        }
        
    }
}
