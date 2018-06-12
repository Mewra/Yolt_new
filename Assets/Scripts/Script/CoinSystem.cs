using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{

    public int Coin;
    // Use this for initialization
    void Start()
    {
        Coin = 100;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void unlockDoor()
    {
        Coin -= 100;
    }
    public void raiseAtk5()
    {
        Coin -= 50;
        Debug.Log("I miei soldi sono:" + Coin);
    }

    public void raiseAtk10()
    {
        Coin -= 100;
        Debug.Log("I miei soldi sono:" + Coin);
    }

    public int returnCoin()
    {
        return Coin;

    }
}
