using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int MAX_WAVE = 3;
    private static GameManager instance = null;
    public List<GameObject> players = new List<GameObject>();
    //public GameObject[] players = null;
    public Material[] materials;
    public GameObject SE;
    private SpawnEnemies _SE;
    public GameObject SM;
    private SpawnMerchant _SM;
    public byte currentWave;
    public int numberOfEnemiesToSpawn;
    public int numberOfEnemiesAlives;
    public GameObject luth;
    public bool pause;
    public bool noSpawn;
    public bool pronto;

    public static GameManager Instance
    {
        get { return instance; }
    }

    #region Unity.MonoBehaviour Callbacks
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }


    // Use this for initialization
    private void Start ()
    {
        
        pause = false;
        noSpawn = true;
        pronto = false;
        _SE = SE.GetComponent<SpawnEnemies>();
        _SM = SM.GetComponent<SpawnMerchant>();
        currentWave = 1;
        numberOfEnemiesToSpawn = currentWave * 10 + 10;
        numberOfEnemiesAlives = numberOfEnemiesToSpawn;
        //StartCoroutine("Aspetta5Sec");
        //_SE.Invoke("Spawn", 5.0f);
        LocSpawn();
        //StartCoroutine("SpawnFirstWave");
    }
    #endregion

    public void enemyKilled() {
        
        numberOfEnemiesAlives--;
        //Debug.Log("WAVE: " + numberOfEnemiesAlives + "/" + numberOfEnemiesToSpawn);
        if (numberOfEnemiesAlives == 0)
        {
            currentWave++;
            noSpawn = true;
            pause = true;
            StartCoroutine("PauseTime");

        }
    }

    public IEnumerator PauseTime()
    {
        _SM.Spawn();
        Debug.Log("Hai 35 secondi per prepararti alla prossima wave");
        yield return new WaitForSeconds(30.0f);
        Debug.Log("La Pausa è finita");
        _SM.Despawn();
        numberOfEnemiesToSpawn = currentWave * 10 + 10;
        numberOfEnemiesAlives = numberOfEnemiesToSpawn;
        LocSpawn();
        pause = false;

    }

    public IEnumerator SpawnFirstWave()
    {
        _SE.Spawn();
        yield return new WaitForSeconds(5.0f);
    
    }





    public void LocSpawn() {
        //_SE.Invoke("Spawn", 5.0f);
        _SE.Spawn();
    }

}
