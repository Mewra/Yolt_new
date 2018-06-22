using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support : MonoBehaviour
{
    private PhotonView myPhotonView;
    private Camera cam;
    private IEnumerator coroutineQ;
    private IEnumerator coroutineW;
    private IEnumerator coroutineR;
    private IEnumerator coroutineCasting;
    private IEnumerator coroutineCasting1;

    private IEnumerator coroutineCDQ;
    private IEnumerator coroutineCDW;
    private IEnumerator coroutineCDR;

    private Ray cameraRay;
    private Plane groundPlane;
    private float rayLength;

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
    public Material _blutrasparente;
    public Material _effectslow;

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
        myPhotonView = GetComponentInParent<PhotonView>();
        // if (!myPhotonView.isMine)
        //     return;
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

        _materialslow = _slow.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (!myPhotonView.isMine)
             return;
        groundPlane = new Plane(Vector3.up, transform.position);
        cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            bas = cameraRay.GetPoint(rayLength);
            bas.y = 0f;
            _redemption.transform.position = bas;
            _taric.transform.position = bas;
        }


        enoughluth = (_pc.getLuth() < 20) ? false : true;

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
                
                gameObject.GetComponentInParent<PhotonView>().RPC("DecreaseLùth", PhotonTargets.AllViaServer, Qcost);

                _slowColl.enabled = true;
                _materialslow = _effectslow;

                coroutineQ = FieldSlowDuration();
                StartCoroutine(coroutineQ);

                coroutineCDQ = CooldownQ(1.0f);
                StartCoroutine(coroutineCDQ);

            }
        }


        if (Input.GetKeyDown(KeyCode.T))
        {
            _redemptionMesh.enabled = true;
        }

        
        if (Input.GetKeyUp(KeyCode.T))
        {
            _redemptionMesh.enabled = false;

            if (usableW && enoughluth)
            {
                usableW = false;
                
                gameObject.GetComponentInParent<PhotonView>().RPC("DecreaseLùth", PhotonTargets.AllViaServer, Wcost);

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



        if (Input.GetKeyDown(KeyCode.R))
        {
            _taricMesh.enabled = true;
        }

        if (usableR) {

            if (Input.GetKeyUp(KeyCode.R))
            {
                gameObject.GetComponentInParent<PhotonView>().RPC("DecreaseLùth", PhotonTargets.AllViaServer, maxluth);

                _taricMesh.enabled = false;

                _taricColl.enabled = true;

                coroutineCasting1 = Instant(_taricColl, visibileTaric);
                StartCoroutine(coroutineCasting1);
            }
        }
        
    }

    public IEnumerator FieldSlowDuration()
    {
        
        //_slow.transform.parent = null;
        //per quanto tempo resta per terra lo slow
        yield return new WaitForSeconds(5.0f);
        _materialslow = _blutrasparente;
        //_slow.transform.parent = this.gameObject.transform;
        _slowColl.enabled = false;

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