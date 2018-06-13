using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private GameObject GameManager;

    private Camera cam;
    public float _lùth;
    private float _maxlùth;
    private bool lùthfinito;
    private AnimatorManager myAniManager;
    public float hp;

    public Button _asbtn, _tankbtn, _supbtn;
    private bool alive, resurrection, transformable, clickingPlayer;

    public GameObject player, ghost, assassin, tank, support, transfPanel;
    private GameObject actual, other;

    private PhotonView myPV;

    public bool[] _Slots = new bool[3];

    //player = 0, ghost = 1, assassin = 2; tank = 3; support = 4;
    public enum CLASSES {player, ghost, assassin, tank, support};

    private HealthPlayer _playerHealth;
    private GameManager _gameManager;
    private float atk;//messa da Alfio
    public CLASSES state;

    private void Awake()
    {
        state = CLASSES.player;
        _Slots = new bool[3];
        for (int i = 0; i < 3; i++)
        {
            _Slots[i] = true;
        }
    }

    void Start ()
    {
        myPV = GetComponent<PhotonView>();
        state = CLASSES.player;
        actual = player;
        clickingPlayer = false;
        alive = true;
        transformable = false;
        resurrection = false;
        lùthfinito = false;
        _lùth = 0;
        atk = 3;
        _maxlùth = 100;
        hp = 100;
        _playerHealth = GetComponent<HealthPlayer>();
        myAniManager = GetComponent<AnimatorManager>();
        if (myPV.isMine)
        {
            cam = Camera.main;
            transfPanel = GameObject.FindGameObjectWithTag("Choose");
            _asbtn = transfPanel.transform.GetChild(0).GetComponent<Button>();
            _tankbtn = transfPanel.transform.GetChild(1).GetComponent<Button>();
            _supbtn = transfPanel.transform.GetChild(2).GetComponent<Button>();
            transfPanel.SetActive(false);
            //_gameManager = GameManager.GetComponent<GameManager>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (myPV.isMine)
        {
            Ray pos = cam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(pos.origin, pos.direction * 30, Color.yellow, 1);
            RaycastHit hit;
            if (Physics.Raycast(pos, out hit))
            {

                if (hit.collider.gameObject.tag == "Player")
                {
                    Debug.Log(hit.collider.name);
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
                        myPV.RPC("ChangeState", PhotonTargets.AllBufferedViaServer, (int)CLASSES.player);
                        myAniManager.RestoreAnimatorView(0);
                        if (_playerHealth._health <= 0 && myPV.isMine)
                        {
                            myPV.RPC("ChangeState", PhotonTargets.AllBufferedViaServer, (int)CLASSES.ghost);
                            myAniManager.RestoreAnimatorView(1);
                            // state = CLASSES.ghost;
                        }
                        break;
                    }

                case 1:
                    {
                        if (resurrection)
                        {
                            // state = CLASSES.player;
                            myPV.RPC("ChangeState", PhotonTargets.AllBufferedViaServer, (int)CLASSES.player);
                            myAniManager.RestoreAnimatorView(0);
                            _playerHealth._health = 100; //maxhealth
                            resurrection = false;
                        }
                        lùthfinito = false;
                        if (_lùth != _maxlùth)
                        {
                            transformable = false;
                        }
                        else
                        {
                            transformable = true;
                        }


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
                        break;
                    }

                case 2:
                    {
                        /*
                        if (actual != assassin)
                        {
                            actual.SetActive(false);
                            assassin.SetActive(true);
                            actual = assassin;
                        }
                        */
                        Debug.Log("Sono un assassino");

                        myPV.RPC("SetParentOnCircleMove", PhotonTargets.AllBufferedViaServer, other.GetComponentInParent<PhotonView>().viewID);
                        // assassin.GetComponent<CircleMov>()._player = other; //oppure other.GetComponentInParent<Transform>()

                        if (lùthfinito)
                        {
                            myPV.RPC("ChangeState", PhotonTargets.AllBufferedViaServer, (int)CLASSES.ghost);
                            myAniManager.RestoreAnimatorView(1);
                            // state = CLASSES.ghost;
                        }

                        if(other.GetActive() == false)
                        {
                            myPV.RPC("ChangeState", PhotonTargets.AllBufferedViaServer, (int)CLASSES.ghost);
                            myAniManager.RestoreAnimatorView(1);
                            // state = CLASSES.ghost;
                        }
                        break;
                    }

                case 3:
                    {
                        myPV.RPC("SetParentOnCircleMove", PhotonTargets.AllBufferedViaServer, other.GetComponentInParent<PhotonView>().viewID);
                        // tank.GetComponent<CircleMov>()._player = other;

                        if (lùthfinito)
                        {
                            myPV.RPC("ChangeState", PhotonTargets.AllBufferedViaServer, (int)CLASSES.ghost);
                            myAniManager.RestoreAnimatorView(1);
                            // state = CLASSES.ghost;
                        }

                        if (other.GetActive() == false)
                        {
                            myPV.RPC("ChangeState", PhotonTargets.AllBufferedViaServer, (int)CLASSES.ghost);
                            myAniManager.RestoreAnimatorView(1);
                            // state = CLASSES.ghost;
                        }
                        break;
                    }

                case 4:
                    {
                        myPV.RPC("SetParentOnCircleMove", PhotonTargets.AllBufferedViaServer, other.GetComponentInParent<PhotonView>().viewID);
                        // support.GetComponent<CircleMov>()._player = other;
                        if (lùthfinito)
                        {
                            // state = CLASSES.ghost;
                            myPV.RPC("ChangeState", PhotonTargets.AllBufferedViaServer, (int)CLASSES.ghost);
                            myAniManager.RestoreAnimatorView(1);
                        }

                        if (other.GetActive() == false)
                        {
                            // state = CLASSES.ghost;
                            myPV.RPC("ChangeState", PhotonTargets.AllBufferedViaServer, (int)CLASSES.ghost);
                            myAniManager.RestoreAnimatorView(1);
                        }
                        break;
                    }
            }  
        }	
	}

    public float getLuth()
    {
        return _lùth;
    }
    //metodo fatto da Alfio
    public float raiseAtk(float a)
    {
        atk = atk + a;
        Debug.Log("Ho alzato l'attacco di"+ a);
        Debug.Log("Il mio attacco è:"+ atk);
        return atk;
    }


    public void DecreaseLùth(float i)
    {
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

    [PunRPC]
    public void ChangeState(int _state)
    {
        switch((int)_state)
        {
            case 0:
                // player
                if(actual != player)
                {
                    state = CLASSES.player;
                    actual.SetActive(false);
                    player.SetActive(true);
                    actual = player;
                }
                break;

            case 1:
                // ghost
                if (actual != ghost)
                {
                    state = CLASSES.ghost;
                    actual.SetActive(false);
                    ghost.SetActive(true);
                    actual = ghost;
                    FreeTheSlots();
                }
                break;

            case 2:
                // assassin
                if (actual != assassin)
                {
                    state = CLASSES.assassin;
                    actual.SetActive(false);
                    assassin.SetActive(true);
                    actual = assassin;
                }
                break;

            case 3:
                // tank
                if (actual != tank)
                {
                    state = CLASSES.tank;
                    actual.SetActive(false);
                    tank.SetActive(true);
                    actual = tank;
                }
                break;

            case 4:
                // support
                if(actual != support)
                {
                    state = CLASSES.support;
                    actual.SetActive(false);
                    support.SetActive(true);
                    actual = support;
                }
                break;
        }
    }

    [PunRPC]
    public void FreeTheSlots()
    {
        for (int i = 0; i < 3; i++)
        {
            _Slots[i] = true;
        }
    }

    [PunRPC]
    public void IncreaseLùth(float i)
    {
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

    [PunRPC]
    public void SetParentOnCircleMove(int ID)
    {
        PhotonView view = PhotonView.Find(ID);
        if (view != null)
        {
            this.other = view.gameObject;
            actual.GetComponent<CircleMov>().SetTargetTransform(other);
        }
    }
    public float ReturnHp()
    {
        return hp;

    }
}
