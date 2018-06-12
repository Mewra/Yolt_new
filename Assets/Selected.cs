using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Selected : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        SelectionManager.roomSelected = gameObject.transform.GetChild(0).GetComponent<Text>().text + "§" + gameObject.transform.GetChild(2).GetComponent<Text>().text;
        // Debug.Log(SelectionManager.roomSelected);
        GetComponent<Image>().color =  new Color(1f, 1f, 1f, 1f);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        // SelectionManager.roomSelected = "";
        GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
    }

}
