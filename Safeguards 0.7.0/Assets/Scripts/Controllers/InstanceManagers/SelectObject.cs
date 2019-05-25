using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectObject : MonoBehaviour, IPointerClickHandler
{
    public UltEvents.UltEvent<string> SelectEvent = new UltEvents.UltEvent<string>();
    //create an instance event.  this means that calling it won't do the bullshit where each one gets called.
    //UltEvent also allows us to set listeners in the Editor?

    private void OnEnable()
    {
        //look for the mission controller or camp controller and add its "select" method as a listener to select event
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //PlayerPrefs.SetString("Selected", gameObject.name);
        SelectEvent.InvokeSafe(gameObject.name);
    }

}
