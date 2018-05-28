using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoorsWithCoins : MonoBehaviour {

    public MeshRenderer popupMesh;
    public MeshRenderer PoorText;
    private Animator DoorDown;
    private bool Paid = false;


    // Use this for initialization
    void Start()
    {
        DoorDown = transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !Paid) 
            popupMesh.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {  
            if (Input.GetKey(KeyCode.Z) && !Paid)
            {
                if (other.GetComponent<CoinSystem>().returnCoin() >= 100)
                {
                    DoorDown.SetTrigger("paid");
                    other.GetComponent<CoinSystem>().unlockDoor();
                    Paid = true;
                    popupMesh.enabled = false;
                    PoorText.enabled = false;
                    //transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    popupMesh.enabled = false;
                    PoorText.enabled = true;
                }
            }
        }
    }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                
                popupMesh.enabled = false;
                PoorText.enabled = false;
            }
            if(Paid)
        {
            Disable();
        }
        }

    public void Disable()
    {
        transform.parent.gameObject.SetActive(false);
    }
}

