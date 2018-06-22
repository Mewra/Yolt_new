using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrack : MonoBehaviour {

    private void Awake()
    {
        AkSoundEngine.PostEvent("Music", gameObject);
        DontDestroyOnLoad(gameObject);
        AkSoundEngine.SetState("Music_States", "Home");
    }
	
	// Update is called once per frame
	void Update () {

        /*string nomeScena = SceneManagerHelper.ActiveSceneName;

        if (nomeScena.Equals("Connect"))
            AkSoundEngine.SetState("Music_States", "Home");
        else {    // (nomeScena.Equals("Gioco"))
            if (GameManager.Instance.pause == true)
                AkSoundEngine.SetState("Music_States", "Ambient");
            else
                AkSoundEngine.SetState("Music_States", "Battle");
        }*/

        

    }
}
