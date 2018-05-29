using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private GameObject GameManager;

    private Camera cam;
    public float _lùth;
    private float _maxlùth;
    private bool lùthfinito;

    private Vector3 bas;

    public GameObject transfPanel;
    public Button _asbtn;
    public Button _tankbtn;
    public Button _supbtn;

    private bool alive;
    private bool resurrection;
    private bool transformable;
    private bool clickingPlayer;

    public GameObject player;
    public GameObject ghost;


    public GameObject assassin;
    public GameObject tank;
    public GameObject support;
    private GameObject actual;
    private GameObject other;

    //public Vector3 _Slots;
    public bool[] _Slots = new bool[3];

    //player = 0, ghost = 1, assassin = 2; tank = 3; support = 4;
    public enum CLASSES {player, ghost, assassin, tank, support};

    private Health _playerHealth;
    private GameManager _gameManager;

    
    public CLASSES state;

    private void Awake()
    {
        _Slots = new bool[3];
        for (int i = 0; i < 3; i++)
            _Slots[i] = true;
    }
    // Use this for initialization
    void Start () {

        if(GetComponent<PhotonView>().isMine)
        {
            cam = Camera.main;
            state = CLASSES.player;
            actual = player;
            transfPanel = GameObject.FindGameObjectWithTag("Choose");
            _asbtn = transfPanel.transform.GetChild(0).GetComponent<Button>();
            _tankbtn = transfPanel.transform.GetChild(1).GetComponent<Button>();
            _supbtn = transfPanel.transform.GetChild(2).GetComponent<Button>();
            transfPanel.SetActive(false);


            clickingPlayer = false;
            alive = true;
            transformable = false;
            //transformable = true;
            resurrection = false;

            lùthfinito = false;

            _lùth = 0;
            _maxlùth = 100;

            //_gameManager = GameManager.GetComponent<GameManager>();

            
            _playerHealth = GetComponent<Health>();
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (GetComponent<PhotonView>().isMine)
        {
            Ray pos = cam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(pos.origin, pos.direction * 30, Color.yellow, 1);
            RaycastHit hit;

            if (Physics.Raycast(pos, out hit))
            {
                /*bas = hit.collider.bounds.center;
                bas.y = 0;*/

                if (hit.collider.gameObject.tag == "Player")
                {
                    Debug.Log("sono sul player, dio cammello berbero");
                    other = hit.collider.gameObject;
                    clickingPlayer = true;
                }
                else
                {
                    clickingPlayer = false;
                }



            }


            switch ((int)state)
            {

                case 0:
                    {
                        //player
                        if (actual != player) { 
                            actual.SetActive(false);
                            player.SetActive(true);
                            actual = player;
                        }


                        //UI DEL PLAYER
                        if (_playerHealth._health == 0 && GetComponent<PhotonView>().isMine)
                        {
                            //tag = "Ghost";
                            alive = false;
                            state = CLASSES.ghost;
                            //DEVE NOTIFICARE A TUTTI QUELLI CHE SONO ATTACCATI CHE DEVONO DIVENTARE GHOST ANCHE LORO

                            //HO USATO IF OTHER.GETACTIVE == FALSE diventano ghost anche loro

                        }



                        /*player.SetActive(true);
                        ghost.SetActive(false);
                        assassin.SetActive(false);
                        tank.SetActive(false);
                        support.SetActive(false);*/

                        break;
                    }
                case 1:
                    {
                        Debug.Log("Sono un ghost");

                        

                        if (actual != ghost)
                        {
                            actual.SetActive(false);
                            ghost.SetActive(true);
                            actual = ghost;
                            FreeTheSlots();
                        }

                        if (resurrection)
                        {
                            state = CLASSES.player;
                            _playerHealth._health = 100; //maxhealth
                            alive = true;
                            resurrection = false;

                        }

                        lùthfinito = false;
                        if (_lùth != _maxlùth)
                        {
                            //transformable = false;
                            transformable = false;
                        }
                        else transformable = true;


                        if (transformable && clickingPlayer)
                        {

                            if (Input.GetMouseButtonDown(0))
                            {
                                _asbtn.interactable = other.GetComponentInParent<PlayerController>()._Slots[0];
                                _tankbtn.interactable = other.GetComponentInParent<PlayerController>()._Slots[1];
                                _supbtn.interactable = other.GetComponentInParent<PlayerController>()._Slots[2];
                                
                                _asbtn.GetComponent<Btn>()._pcTarget = other.GetComponentInParent<PlayerController>();
                                _tankbtn.GetComponent<Btn>()._pcTarget = other.GetComponentInParent<PlayerController>();
                                _supbtn.GetComponent<Btn>()._pcTarget = other.GetComponentInParent<PlayerController>();

                                _asbtn.GetComponent<Btn>()._pcPlayer = this;
                                _tankbtn.GetComponent<Btn>()._pcPlayer = this;
                                _supbtn.GetComponent<Btn>()._pcPlayer = this;

                                transfPanel.SetActive(true);

                            }
                        }
                        //UI DEL GHOST


                        /*player.SetActive(false);
                        ghost.SetActive(true);
                        assassin.SetActive(false);
                        tank.SetActive(false);
                        support.SetActive(false);*/

                        break;
                    }

                case 2:
                    {
                        if (actual != assassin)
                        {
                            actual.SetActive(false);
                            assassin.SetActive(true);
                            actual = assassin;
                        }
                        Debug.Log("Sono un assassino");

                        assassin.GetComponent<CircleMov>()._player = other; //oppure other.GetComponentInParent<Transform>()

                        if (lùthfinito)
                        {
                            state = CLASSES.ghost;
                        }

                        if(other.GetActive() == false)
                        {
                            state = CLASSES.ghost;
                        }

                        //UI DELLA CLASSE

                        /*player.SetActive(false);
                        ghost.SetActive(false);
                        assassin.SetActive(true);
                        tank.SetActive(false);
                        support.SetActive(false);*/

                        break;
                    }
                case 3:
                    {
                        if (actual != tank)
                        {
                            actual.SetActive(false);
                            tank.SetActive(true);
                            actual = tank;
                        }
                        Debug.Log("Sono un tank");

                        tank.GetComponent<CircleMov>()._player = other;

                        if (lùthfinito)
                        {
                            state = CLASSES.ghost;
                        }

                        if (other.GetActive() == false)
                        {
                            state = CLASSES.ghost;
                        }

                        /*player.SetActive(false);
                        ghost.SetActive(false);
                        assassin.SetActive(false);
                        tank.SetActive(true);
                        support.SetActive(false);*/

                        break;
                    }
                case 4:
                    {
                        if (actual != support) { 
                            actual.SetActive(false);
                            support.SetActive(true);
                            actual = support;
                        }
                        Debug.Log("Sono un support");

                        support.GetComponent<CircleMov>()._player = other;

                        if (lùthfinito)
                        {
                            state = CLASSES.ghost;
                        }

                        if (other.GetActive() == false)
                        {
                            state = CLASSES.ghost;
                        }
                        /*player.SetActive(false);
                        ghost.SetActive(true);
                        assassin.SetActive(false);
                        tank.SetActive(false);
                        support.SetActive(false);*/

                        break;
                    }
            }  
        }	
	}

    public void FreeTheSlots() {

        for (int i = 0; i < 3; i++)
            _Slots[i] = true;

    }

    public void IncreaseLùth(float i) {

        Debug.Log(_lùth);

        if (state == CLASSES.ghost)
        {
            if (transformable != true)
            {
                _lùth += i;
                if (_lùth == _maxlùth)
                {
                    transformable = true;
                }
            }
        }

    }

    public void DecreaseLùth(float i) {

        if (state == CLASSES.assassin || state == CLASSES.support || state == CLASSES.tank)
        {
            _lùth -= i;

            if (_lùth < 0)
            {
                _lùth = 0;

                lùthfinito = true;
            }
        }

    }

    public float getLuth() {
        return _lùth;
    }

    
}
