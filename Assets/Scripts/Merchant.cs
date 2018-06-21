using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour {
    public GameObject player;
    public MeshRenderer shopText;
    public MeshRenderer noMoneyText;
    void Start()
    {
        //  shopText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {   //Controllo sempre se preme esc, perchè se si sposta  dal collider per qualche motivo la canvas non si disattiverà
        if (Input.GetKey(KeyCode.Escape) && transform.GetChild(0).gameObject.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            shopText.gameObject.SetActive(true);
            player.gameObject.GetComponent<MovementGhost>().enabled = true;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Mi sta toccando un player");
            player = other.gameObject;
            shopText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if (!myPhotonView.isMine)
        //    return;
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.V))
            {
                transform.GetChild(0).gameObject.SetActive(true);
                shopText.gameObject.SetActive(false);
                player.gameObject.GetComponent<MovementGhost>().enabled = false;//disabilito il movimento al player
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (!myPhotonView.isMine)
        //    return;
        shopText.gameObject.SetActive(false);
    }

    public void RaiseAtk()
    {
        //if (!myPhotonView.isMine)
        //    return;
        if (player.transform.parent.GetComponent<CoinSystem>().ReturnCoin() >= 600)
        {
            player.transform.parent.GetComponent<PlayerController>().RaiseAtk(10f);
            player.transform.parent.GetComponent<CoinSystem>().RaiseAtk();
            gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            noMoneyText.gameObject.SetActive(true);
            StartCoroutine("DisappearText");
        }
    }

    public void RaiseSpeed()
    {
        //if (!myPhotonView.isMine)
        //    return;
        if (player.transform.parent.GetComponent<CoinSystem>().ReturnCoin() >= 600)
        {
            player.transform.parent.GetComponent<PlayerController>().RaiseSpeed(5f);
            player.transform.parent.GetComponent<CoinSystem>().RaiseSpeed();
            gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            noMoneyText.gameObject.SetActive(true);
            StartCoroutine("DisappearText");
        }
    }
    public void RaiseHealth()
    {
        //if (!myPhotonView.isMine)
        //    return;
        if (player.transform.parent.GetComponent<CoinSystem>().ReturnCoin() >= 600)
        {
            player.transform.parent.GetComponent<PlayerController>().RaiseHealth(100f);
            player.transform.parent.GetComponent<CoinSystem>().RaiseHealth();
            gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            noMoneyText.gameObject.SetActive(true);
            StartCoroutine("DisappearText");
        }
    }
    public void Revive()
    {
        //if (!myPhotonView.isMine)
        //    return;
        if (player.transform.parent.GetComponent<CoinSystem>().ReturnCoin() >= 300)
        {
            player.transform.parent.GetComponent<PlayerController>().Revive();
            player.transform.parent.GetComponent<CoinSystem>().Revive();
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            noMoneyText.gameObject.SetActive(true);
            StartCoroutine("DisappearText");
        }
    }
    IEnumerator DisappearText()
    {   //La coroutine serve solo ad attivare e disattivare i testi
        //if (!myPhotonView.isMine)
        //    return;
        yield return new WaitForSeconds(2f);
        noMoneyText.gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
    }

}
