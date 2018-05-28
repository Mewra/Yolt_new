using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour {

    public int Coin;
	// Use this for initialization
	void Start ()
    {
        Coin = 100;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void unlockDoor()
    {
        Coin -= 100;
    }
    public int returnCoin()
    {
        return Coin;
    }
}
