using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int MAX_WAVE = 13;
    private static GameManager instance = null;
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
    #endregion

    // Use this for initialization
    void Start ()
    {
        pause = false;
        noSpawn = true;
        _SE = SE.GetComponent<SpawnEnemies>();
        _SM = SM.GetComponent<SpawnMerchant>();
        currentWave = 1;
        numberOfEnemiesToSpawn = 10; // mettere qui la funzione matematica in futuro
        numberOfEnemiesAlives = numberOfEnemiesToSpawn;
        _SE.Spawn(numberOfEnemiesToSpawn);
        // _SE.Spawn(1);
    }

    // Update is called once per frame
    void Update()
    {
      
		
	}

    public void enemyKilled() {
        
        numberOfEnemiesAlives--;
        Debug.Log("WAVE: " + numberOfEnemiesAlives + "/" + numberOfEnemiesToSpawn);
        if (numberOfEnemiesAlives == 0) {
            currentWave++;
            noSpawn = true;
            pause = true;
            StartCoroutine("PauseTime");

        }
    }

    public IEnumerator PauseTime()
    {
        _SM.Spawn();
        Debug.Log("Pause time");
        yield return new WaitForSeconds(15.0f);
        Debug.Log("Fine Pausa");
        //_SM.Despawn();
        //pause = false;
        //SpawnWave(currentWave);

    }

    public void SpawnWave(int numwave) {

        numberOfEnemiesToSpawn = numwave * 10 + 10;
        numberOfEnemiesAlives = numberOfEnemiesToSpawn;
        _SE.Spawn(numberOfEnemiesToSpawn);

    }
}
