using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn : MonoBehaviour {


    public GameObject _GOpc;
    private PlayerController _pc;

    private string name;
	// Use this for initialization
	void Start () {

        _pc = _GOpc.GetComponent<PlayerController>();
        name = GetComponent<Button>().name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeState() {
        if (name.Equals("asBtn")){
            _pc.state = PlayerController.CLASSES.assassin;
        }

        if (name.Equals("tankBtn")) {
            _pc.state = PlayerController.CLASSES.tank;
        }

        if (name.Equals("supBtn")) {
            _pc.state = PlayerController.CLASSES.support;
        }
        
    }
}
