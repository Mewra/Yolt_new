using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioColoreParticle : MonoBehaviour {

    public bool cacca;
    public ParticleSystem ps;
    // Use this for initialization
    void Start () {
        cacca = true;
        ps = GetComponent<ParticleSystem>();
    }



    // Update is called once per frame
    void Update() {

        if (cacca)
        {
            
            

            Debug.Log("Ps" + ps.colorOverLifetime.color);
            
        }
        else {

            var col = ps.colorOverLifetime;
            Gradient grad = new Gradient();
            grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.green, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 1.0f), new GradientAlphaKey(1.0f, 1.0f) });
            col.color = grad;

        }

        

    }
}
