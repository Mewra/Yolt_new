using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int MAX_WAVE = 13;
    private static GameManager instance = null;
    public GameObject SE;
    private SpawnEnemies _SE;
    public byte currentWave;
    public int numberOfEnemiesToSpawn;
    public int numberOfEnemiesAlives;
    public GameObject luth;
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
        _SE = SE.GetComponent<SpawnEnemies>();
        currentWave = 0;
        numberOfEnemiesToSpawn = 20; // mettere qui la funzione matematica in futuro
        numberOfEnemiesAlives = numberOfEnemiesToSpawn;
        // _SE.Spawn(numberOfEnemiesToSpawn);
        _SE.Spawn(1);
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
