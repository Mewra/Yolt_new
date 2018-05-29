using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;
    public EnumPanel[] Panels;

    public static UIManager Instance
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

    #region User Methods
    public void EnablePanel(EnumPanel panelType)
    {
        foreach (EnumPanel panel in Panels)
        {
            panel.gameObject.SetActive((panel.myActivePanel == panelType.myActivePanel) ? true : false);
        }
    }
    #endregion
}
