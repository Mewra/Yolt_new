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
        Debug.Log("sono qui, dio cane");
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            shopText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
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
        shopText.gameObject.SetActive(false);
    }

    public void RaiseAtk5()
    {

        if (player.transform.parent.GetComponent<CoinSystem>().returnCoin() >= 50)
        {
            player.transform.parent.GetComponent<PlayerController>().raiseAtk(5f);
            player.transform.parent.GetComponent<CoinSystem>().raiseAtk5();
            //transform.parent.gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            noMoneyText.gameObject.SetActive(true);
            StartCoroutine("DisappearText");
        }
    }

    public void RaiseAtk10()
    {
        if (player.GetComponent<CoinSystem>().returnCoin() >= 100)
        {
            player.GetComponent<PlayerController>().raiseAtk(10f);
            player.transform.parent.GetComponent<CoinSystem>().raiseAtk10();
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
        yield return new WaitForSeconds(2f);
        noMoneyText.gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
    }

}
