using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : MonoBehaviour
{
    private PhotonView myPhotonView;
    private Camera cam;

    private IEnumerator coroutineQ;
    private IEnumerator coroutineW;
    private IEnumerator coroutineR;

    private IEnumerator coroutineCDQ;
    private IEnumerator coroutineCDW;
    private IEnumerator coroutineCDR;

    private Ray cameraRay;
    private Plane groundPlane;
    private float rayLength;

    public GameObject _sfera;
    public GameObject _cono;
    public GameObject _laserone;

    private PlayerController _pc;
    
    public ParticleSystem _psQ;
    public ParticleSystem _psW;


    private MeshRenderer _sferaMesh;
    private MeshRenderer _conoMesh;
    private MeshRenderer _laseroneMesh;

    private MeshCollider _conoColl;
    private CapsuleCollider _laseroneColl;
    private SphereCollider _sferaColl;

    private Material _materialSfera;
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
        //if (!myPhotonView.isMine)
        // {
          //  return;
        // }
        bas = new Vector3(0, 0, 0);
        cam = Camera.main;

        Qcost = 20;
        Wcost = 20;
        maxluth = 100;

        enoughluth = true;
        _pc = GetComponentInParent<PlayerController>();

        _sferaMesh = _sfera.GetComponent<MeshRenderer>();
        _conoMesh = _cono.GetComponent<MeshRenderer>();
        _laseroneMesh = _laserone.GetComponent<MeshRenderer>();

        _conoColl = _cono.GetComponent<MeshCollider>();
        _laseroneColl = _laserone.GetComponent<CapsuleCollider>();
        _sferaColl = _sfera.GetComponent<SphereCollider>();

        _psQ = _sfera.GetComponentInChildren<ParticleSystem>();
        _psW = _cono.GetComponentInChildren<ParticleSystem>();

        _sferaMesh.enabled = false;
        _conoMesh.enabled = false;
        _laseroneMesh.enabled = false;

        _materialSfera = _sfera.GetComponent<Renderer>().material;

        usableQ = true;
        usableW = true;
        usableR = true;
    }

    // Update is called once per frame
    void Update()
    {
        // if (!myPhotonView.isMine)
        // {
           //  return;
        // }
        groundPlane = new Plane(Vector3.up, transform.position);
        cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            bas = cameraRay.GetPoint(rayLength);
            bas.y = 0f;
            _sfera.transform.position = bas;
        }
        enoughluth = (_pc.getLuth() < 20) ? false : true;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _sferaMesh.enabled = true;
        }
 
        if (Input.GetKeyUp(KeyCode.Q))
        {
            _sferaMesh.enabled = false;
            if (usableQ && enoughluth)
            {
                Debug.Log("sto chiamando l'RPC");
                gameObject.GetComponentInParent<PhotonView>().RPC("StartParticleSystem", PhotonTargets.AllViaServer, 0, 0);
            }
        }

        //cono
        if (Input.GetKeyDown(KeyCode.W))
        {
            //appena clicka il mouse prende la mesh renderer del cono e la abilita
            _conoMesh.enabled = true;
        }

            if (Input.GetKeyUp(KeyCode.W))
            {
                _psW.Play();
                //quando il mouse viene alzato disabilita la mesh renderer e abilita il collider
                _conoMesh.enabled = false;

            if (usableW && enoughluth)
            {
                usableW = false;
                _conoColl.enabled = true;
                gameObject.GetComponentInParent<PhotonView>().RPC("DecreaseLùth", PhotonTargets.AllViaServer, Wcost);
                //GetComponentInParent<PlayerController>().DecreaseLùth(Wcost);
                coroutineW = FieldDamageDuration();
                StartCoroutine(coroutineW);
                coroutineCDW = CooldownW(1.0f);
                StartCoroutine(coroutineCDW);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _laseroneMesh.enabled = true;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            _laseroneMesh.enabled = false;

            if (usableR)
            {
                //GetComponentInParent<PlayerController>().DecreaseLùth(maxluth);
                gameObject.GetComponentInParent<PhotonView>().RPC("DecreaseLùth", PhotonTargets.AllViaServer, maxluth);
                _laseroneColl.enabled = true;
                coroutineR = InstantDamage(_laseroneColl);
                StartCoroutine(coroutineR);
            }
        }
    }

    public void QAbility()
    {
        Debug.Log("sono dentro QAbility");
        _psQ.Clear();
        _psQ.Simulate(0.0f, true, true);
        _psQ.Play();
        gameObject.GetComponentInParent<PhotonView>().RPC("DecreaseLùth", PhotonTargets.AllViaServer, Qcost);

        usableQ = false;
        _sferaColl.enabled = true;

        coroutineQ = InstantDamage(_sferaColl);
        StartCoroutine(coroutineQ);

        coroutineCDQ = CooldownQ(1.0f);
        StartCoroutine(coroutineCDQ);
    }

    private IEnumerator CooldownQ(float dur) {
        yield return new WaitForSeconds(dur);
        usableQ = true;
    }

    private IEnumerator CooldownW(float dur)
    {
        yield return new WaitForSeconds(dur);
        usableW = true;
    }

    public IEnumerator FieldDamageDuration(){
        yield return new WaitForSeconds(5.0f);
        _conoColl.enabled = false;
        _psW.Stop();
    }
    
    public IEnumerator InstantDamage(Collider cast)
    {
        yield return new WaitForSeconds(0.5f);
        cast.enabled = false;
    }
}
