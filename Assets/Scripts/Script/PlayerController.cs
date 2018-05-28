using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private GameObject GameManager;

    public Camera cam;
    private static float _lùth;
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
    public bool[] _Slots;

    //player = 0, ghost = 1, assassin = 2; tank = 3; support = 4;
    public enum CLASSES {player, ghost, assassin, tank, support};

    private Health _playerHealth;
    private GameManager _gameManager;

    
    public CLASSES state;

	// Use this for initialization
	void Start () {

        state = CLASSES.player;
        actual = player;

        transfPanel.SetActive(false);

        clickingPlayer = false;
        alive = true;
        //transformable = false;
        transformable = true;
        resurrection = false;

        lùthfinito = false;

        _lùth = 0;
        _maxlùth = 100;

        //_gameManager = GameManager.GetComponent<GameManager>();

        

        //0 è libero, 1 è occupato. x = Assassin, y = Support, z = Tank;
        //new Vector3(0,0,0)
        //
        _Slots = new bool[3];

        for (int i = 0; i < 3; i++)
            _Slots[i] = true;

        _playerHealth = GetComponent<Health>();
        
		
	}
	
	// Update is called once per frame
	void Update () {

        Ray pos = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(pos.origin, pos.direction * 30, Color.yellow, 1);
        RaycastHit hit;

        if (Physics.Raycast(pos, out hit))
        {
            /*bas = hit.collider.bounds.center;
            bas.y = 0;*/

            if (hit.collider.gameObject.tag == "Player")
            {
                other = hit.collider.gameObject;
                clickingPlayer = true;
            }
            else {
                clickingPlayer = false;
            }

            

        }


        switch ((int)state) {

            case 0:{
                    Debug.Log("Sono un player");
                    actual.SetActive(false);
                    player.SetActive(true);


                    //UI DEL PLAYER




                    /*player.SetActive(true);
                    ghost.SetActive(false);
                    assassin.SetActive(false);
                    tank.SetActive(false);
                    support.SetActive(false);*/

                    break;
                }
            case 1: {
                    Debug.Log("Sono un ghost");
                    actual.SetActive(false);
                    ghost.SetActive(true);

                    

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
                    actual.SetActive(false);
                    assassin.SetActive(true);
                    Debug.Log("Sono un assassino");


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
                    actual.SetActive(false);
                    tank.SetActive(true);
                    Debug.Log("Sono un tank");
                    /*player.SetActive(false);
                    ghost.SetActive(false);
                    assassin.SetActive(false);
                    tank.SetActive(true);
                    support.SetActive(false);*/

                    break;
                }
            case 4:
                {
                    actual.SetActive(false);
                    support.SetActive(true);
                    Debug.Log("Sono un support");
                    /*player.SetActive(false);
                    ghost.SetActive(true);
                    assassin.SetActive(false);
                    tank.SetActive(false);
                    support.SetActive(false);*/

                    break;
                }

        }



        

        if (_playerHealth._health == 0)
        {
            tag = "Ghost";
            alive = false;
            state = CLASSES.ghost;
            //change the model in the ghost

        }

        if (_lùth != _maxlùth) {
            //transformable = false;
            transformable = true;
        }


        if (resurrection) {

            _playerHealth._health = 100; //maxhealth
            tag = "Player";
            alive = true;
            resurrection = false;
            //change the model in the player

        }



        if (transformable && clickingPlayer) {
            
            if (Input.GetMouseButtonDown(0))
            {
                _asbtn.interactable = _Slots[0];
                _tankbtn.interactable = _Slots[1];
                _supbtn.interactable = _Slots[2];

                transfPanel.SetActive(true);
            }

                //canvas con i bottoni

                /* Questo è un GameObject -> _gameManager.playersConnected[i]
                 * 
                 * for each (GameObject player in _gameManager.playersConnected) {
                 * 
                 *      for(int i = 0; i<3;i++){
                 *          if(player._Slots[i]){
                 *              button1 = cliccabile;
                 *          }
                 *      }
                 *      
                 * }
                 * 
                 * 
                */
        }
		
	}

    public void FreeTheSlots() {

        for (int i = 0; i < 3; i++)
            _Slots[i] = true;

    }

    public void IncreaseLùth(float i) {

        Debug.Log(_lùth);

        if (_lùth == _maxlùth)
        {
            transformable = true;
            //cambia tag in class che scegli
            //cambia mesh renderer in class che scegli
        }
        else {
            _lùth += i;
        }

    }

    public void DecreaseLùth(float i) {

        _lùth -= i;

        if (_lùth < 0) {
            _lùth = 0;

            lùthfinito = true;
        }

    }

    public float getLuth() {
        return _lùth;
    }

    
}
