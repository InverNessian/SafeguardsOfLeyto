using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TalentButton : MonoBehaviour, IPointerClickHandler
{
    public string talent;
    public TalentMenu menu;

    public void OnPointerClick(PointerEventData eventData)
    {
        menu.UpdateInfoDisplay(talent);
    }
}
