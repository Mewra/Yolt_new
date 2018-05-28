using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support : MonoBehaviour
{
    private Camera cam;
    private IEnumerator coroutineQ;
    private IEnumerator coroutineW;
    private IEnumerator coroutineR;
    private IEnumerator coroutineCasting;
    private IEnumerator coroutineCasting1;

    private IEnumerator coroutineCDQ;
    private IEnumerator coroutineCDW;
    private IEnumerator coroutineCDR;

    public GameObject _slow;
    public GameObject _redemption;
    public GameObject _taric;

    //public GameObject _GOpc;
    private PlayerController _pc;

    public GameObject _TransformPsW;
    private ParticleSystem _psW;


    private MeshRenderer _slowMesh;
    private MeshRenderer _redemptionMesh;
    private MeshRenderer _taricMesh;

    private SphereCollider _redemptionColl;
    private SphereCollider _taricColl;
    private CapsuleCollider _slowColl;

    //private bool visibileRedemption = false;
    private bool visibileTaric = false;

    private Material _materialslow;

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

        _slowMesh = _slow.GetComponent<MeshRenderer>();
        _redemptionMesh = _redemption.GetComponent<MeshRenderer>();
        _taricMesh = _taric.GetComponent<MeshRenderer>();

        _redemptionColl = _redemption.GetComponent<SphereCollider>();
        _taricColl = _taric.GetComponent<SphereCollider>();
        _slowColl = _slow.GetComponent<CapsuleCollider>();

        _psW = _TransformPsW.GetComponent<ParticleSystem>();

        _slowMesh.enabled = false;
        _redemptionMesh.enabled = false;
        _taricMesh.enabled = false;

        usableQ = true;
        usableW = true;
        usableR = true;




        //_materialslow = _slow.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Ray pos = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(pos.origin, pos.direction * 30, Color.yellow, 1);
        RaycastHit hit;

        if (Physics.Raycast(pos, out hit))
        {
            bas = hit.point;
            bas.y = 0;
            _redemption.transform.position = bas;
            _taric.transform.position = bas;
        }

        if (_pc.getLuth() < 20)
        {
            enoughluth = false;
        }
        else { enoughluth = true; }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _slowMesh.enabled = true;

        }

        
        if (Input.GetKeyUp(KeyCode.Q))
        {
            _slowMesh.enabled = false;

            if (usableQ && enoughluth)
            {
                usableQ = false;

                //////////////////////////RPC////////////////////
                GetComponentInParent<PlayerController>().DecreaseLùth(Qcost);

                _slowColl.enabled = true;

                coroutineQ = FieldSlowDuration(_slowColl);
                StartCoroutine(coroutineQ);

                coroutineCDQ = CooldownQ(10.0f);
                StartCoroutine(coroutineCDQ);

            }
        }


        if (Input.GetKeyDown(KeyCode.W))
        {
            _redemptionMesh.enabled = true;
        }

        
        if (Input.GetKeyUp(KeyCode.W))
        {
            _redemptionMesh.enabled = false;

            if (usableW && enoughluth)
            {
                usableW = false;

                /////////////////RPC//////////////////
                GetComponentInParent<PlayerController>().DecreaseLùth(Wcost);

                _TransformPsW.transform.position = new Vector3(_redemption.transform.position.x, 0.5f, _redemption.transform.position.z);
                _psW.Clear();
                _psW.Simulate(0.0f, true, true);
                _psW.Play();

                coroutineCasting = CastingDuration(3.0f);
                StartCoroutine(coroutineCasting);

                coroutineCDW = CooldownW(10.0f);
                StartCoroutine(coroutineCDW);

            }
        }

        /*if (visibileRedemption)
        {
            coroutineW = Instant(_redemptionColl, visibileRedemption);
            StartCoroutine(coroutineW);
        }*/



        if (Input.GetKeyDown(KeyCode.R))
        {
            _taricMesh.enabled = true;
        }

        if (usableR) {

            if (Input.GetKeyUp(KeyCode.R))
            {
                ////////////////////RPC///////////////////////
                GetComponentInParent<PlayerController>().DecreaseLùth(maxluth);
                _taricMesh.enabled = false;

                _taricColl.enabled = true;

                coroutineCasting1 = Instant(_taricColl, visibileTaric);
                StartCoroutine(coroutineCasting1);
            }
        }

        /*if (visibileTaric)
        {
            coroutineR = Instant(_taricColl, visibileTaric);
            StartCoroutine(coroutineR);
        }*/


    }

    public IEnumerator FieldSlowDuration(Collider slow)
    {   
        //per quanto tempo resta per terra lo slow
        yield return new WaitForSeconds(5.0f);
        slow.enabled = false;

    }

    public IEnumerator Instant(Collider cast, bool vis)
    {
        
        yield return new WaitForSeconds(0.5f);
        cast.enabled = false;
        vis = false;
        

    }

    public IEnumerator CastingDuration(float dur) {


        yield return new WaitForSeconds(dur);
        _redemptionColl.enabled = true;
        yield return new WaitForSeconds(0.5f);
        _redemptionColl.enabled = false;
        _psW.Clear();
        _psW.Simulate(0.0f, true, true);
        _psW.Stop();

    }

    private IEnumerator CooldownQ(float dur)
    {
        yield return new WaitForSeconds(dur);
        usableQ = true;

    }
    private IEnumerator CooldownW(float dur)
    {
        yield return new WaitForSeconds(dur);
        usableW = true;

    }



}



    /*// Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
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
                    if (intoExp.transform.tag == "Enemy") // non dovrebbe essere il tag del giocatore?
                    {
                        intoExp.GetComponent<Health>().AreaHeal(10);
                    }
                }

            }




        }


        //redemption
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
                        intoExp.GetComponent<EnemyManager>().Slow(10);
                    }
                }

            }

        }

    }

    

    */
