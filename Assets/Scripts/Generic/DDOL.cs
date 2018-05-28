using UnityEngine;

public class DDOL : MonoBehaviour
{
    #region Unity.MonoBehaviour Callbacks
    private void Awake ()
    {
        DontDestroyOnLoad(this);
	}
    #endregion
}
