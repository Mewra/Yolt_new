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
    public void RaiseAtk()
    {
        Coin -= 600;
        Debug.Log("I miei soldi sono:" + Coin);
    }

    public void RaiseSpeed()
    {
        Coin -= 600;
        Debug.Log("I miei soldi sono:" + Coin);
    }
    public void RaiseHealth()
    {
        Coin -= 600;
    }
    public void Revive()
    {
        Coin -= 300;
    }

    public int ReturnCoin()
    {
        return Coin;

    }
}
