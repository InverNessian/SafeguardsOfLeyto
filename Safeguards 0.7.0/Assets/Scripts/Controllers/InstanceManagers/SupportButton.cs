using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SupportButton : MonoBehaviour, IPointerClickHandler
{
    public string support;
    public SupportMenu menu;

    public void OnPointerClick(PointerEventData eventData)
    {
        menu.UpdateInfoDisplay(support);
    }
}
