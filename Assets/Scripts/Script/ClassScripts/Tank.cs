using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {


    private Camera cam;
    private IEnumerator coroutineQ;
    private IEnumerator coroutineW;
    private IEnumerator coroutineR;

    private IEnumerator coroutineCDQ;
    private IEnumerator coroutineCDW;
    private IEnumerator coroutineCDR;

    public GameObject _shield;
    public GameObject _cono;
    public GameObject _pull;

    //public GameObject _GOpc;
    private PlayerController _pc;


    private MeshRenderer _shieldMesh;
    private MeshRenderer _conoMesh;
    private MeshRenderer _pullMesh;

    private MeshCollider _conoColl;
    private SphereCollider _pullColl;
    private MeshCollider _shieldColl;


    public Material _CastingMaterialShield;
    public Material _SpellMaterialShield;
    private Renderer _materialShield;

    private Vector3 bas;

    private bool usableQ;
    private bool usableW;
    private bool usableR;
    private bool enoughluth;

    private float Qcost;
    private float Wcost;
    private float maxluth;




    // Use this for initialization
    void Start()
    {
        bas = new Vector3(0, 0, 0);
        cam = Camera.main;

        Qcost = 20;
        Wcost = 20;
        maxluth = 100;

        enoughluth = true;


        
        _pc = GetComponentInParent<PlayerController>();

        _shieldMesh = _shield.GetComponent<MeshRenderer>();
        _conoMesh = _cono.GetComponent<MeshRenderer>();
        _pullMesh = _pull.GetComponent<MeshRenderer>();

        _conoColl = _cono.GetComponent<MeshCollider>();
        _pullColl = _pull.GetComponent<SphereCollider>();
        _shieldColl = _shield.GetComponent<MeshCollider>();

        _shieldMesh.enabled = false;
        _conoMesh.enabled = false;
        _pullMesh.enabled = false;

        usableQ = true;
        usableW = true;
        usableR = true;

        _materialShield = _shield.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray pos = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(pos.origin, pos.direction * 30, Color.yellow, 1);
        RaycastHit hit;

        if (Physics.Raycast(pos, out hit))
        {
            /*bas = hit.collider.bounds.center;
            bas.y = 0;

            if (hit.collider.gameObject.tag == "Floor")
            {
                bas = hit.point;
            }*/

            bas = hit.point;
            bas.y = 0;

            _pull.transform.position = bas;

        }

        if (_pc.getLuth() < 20)
        {
            enoughluth = false;
        }
        else { enoughluth = true; }



        //if (Input.GetMouseButtonDown(0))

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _shieldMesh.enabled = true;

            //DebugExtension.DebugWireSphere(bas, 3f, 100, true);

        }

        
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (usableQ && enoughluth)
            {
                //_shieldMesh.enabled = false;

                usableQ = false;

                ////////////////RPC///////////////////////
                //GetComponentInParent<PlayerController>().DecreaseLùth(Qcost);
                gameObject.GetComponentInParent<PhotonView>().RPC("DecreaseLùth", PhotonTargets.AllViaServer, Qcost);

                _materialShield.material = _SpellMaterialShield;

                _shieldColl.enabled = true;

                coroutineQ = ShieldDuration(_shieldColl);
                StartCoroutine(coroutineQ);

                coroutineCDQ = Cooldown(10.0f, usableQ);
                StartCoroutine(coroutineCDQ);

            }
        }




        //cono
        if (Input.GetKeyDown(KeyCode.W))
        {
            _conoMesh.enabled = true;
        }

        if (usableW && enoughluth)
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                usableW = false;

                ////////////////////////RPC//////////////////////
                //GetComponentInParent<PlayerController>().DecreaseLùth(Wcost);
                gameObject.GetComponentInParent<PhotonView>().RPC("DecreaseLùth", PhotonTargets.AllViaServer, Wcost);

                //quando il mouse viene alzato disabilita la mesh renderer e abilita il collider
                _conoMesh.enabled = false;

                _conoColl.enabled = true;

                coroutineW = InstantStun(_conoColl);
                StartCoroutine(coroutineW);

                coroutineCDW = Cooldown(10.0f, usableW);
                StartCoroutine(coroutineCDW);

            }

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _pullMesh.enabled = true;
        }

        if (usableR) { 
            if (Input.GetKeyUp(KeyCode.R))

            {


                //////////////////RPC////////////////////
                //GetComponentInParent<PlayerController>().DecreaseLùth(maxluth);
                gameObject.GetComponentInParent<PhotonView>().RPC("DecreaseLùth", PhotonTargets.AllViaServer, maxluth);

                _pullMesh.enabled = false;

                _pullColl.enabled = true;

                //coroutineR = InstantDamage(_pullColl);
                //StartCoroutine(coroutineR);
            }
        }

    }

    public IEnumerator FieldDamageDuration()
    {
        yield return new WaitForSeconds(5.0f);
        _conoColl.enabled = false;

    }

    public IEnumerator ShieldDuration(Collider cast)
    {
        yield return new WaitForSeconds(5.0f);
        _shieldMesh.enabled = false;
        _materialShield.material = _CastingMaterialShield;
        cast.enabled = false;

    }

    public IEnumerator InstantStun(Collider stun)
    {
        yield return new WaitForSeconds(0.5f);
        stun.enabled = false;

    }

    private IEnumerator Cooldown(float dur, bool usable)
    {
        yield return new WaitForSeconds(dur);
        usable = true;

    }



}





        //aggiungere tasto sinistro rifletti proiettili
        /*
        if (Input.GetMouseButtonDown(1))
        {
            Ray pos = cam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(pos.origin, pos.direction * 30, Color.yellow, 100);
            RaycastHit hit;

            if (Physics.Raycast(pos, out hit))
            {
                Vector3 bas = hit.collider.bounds.center;
                bas.y = 0;

                if (hit.collider.gameObject.tag == "Floor")
                {
                    bas = hit.point;
                }

                DebugExtension.DebugWireSphere(bas, 2.5f, 100, true);
                Collider[] Arround = Physics.OverlapSphere(bas, 2.5f);

                foreach (Collider intoExp in Arround)
                {
                    if (intoExp.transform.tag == "Enemy")
                    {
                        intoExp.GetComponent<EnemyManager>().Stun();
                    }
                }

            }

        }*/


