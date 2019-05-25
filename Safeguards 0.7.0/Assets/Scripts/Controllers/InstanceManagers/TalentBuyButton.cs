using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TalentBuyButton : MonoBehaviour, IPointerClickHandler
{
    public TalentMenu menu;

    private void Awake()
    {
        menu = GameObject.Find("TalentMenuPanel").GetComponent<TalentMenu>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        menu.TryLearnTalent();
    }

}
