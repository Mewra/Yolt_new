using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {

        speed = 100;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Slow(float slow)
    {
        speed -= slow;
        //controlla se speed è nei bound, se è minore la porta a 0, maggiore a 100
        speed = Mathf.Clamp(speed, 0, 100);
        StartCoroutine(DebuffDuration(5.0f));
    }

    //oltre a non poter muoversi non possono neanche attaccare (da implementare dopo)
    public void Stun() {
        speed = 0;
        StartCoroutine(DebuffDuration(5.0f));
    }

    public IEnumerator DebuffDuration(float dur)
    {
        yield return new WaitForSeconds(dur);
        speed = 100;

    }
}
