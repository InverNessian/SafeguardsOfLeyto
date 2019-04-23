using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    public GameObject[] panels;
    // Set the unit UI panels to these values in Editor


    void Start()
    {
        InputController.mSelect.AddListener(ShowActionUI);
        InputController.mDeselect.AddListener(HideActionUI);
    }


    public void ShowActionUI(GameObject owner)
    {
        //since we configure this in editor, assume panels[0] is always the OwnerPanel
        panels[0].GetComponent<UIHolder>().target = owner;
        panels[0].SetActive(true);
        panels[1].SetActive(true);
    }

    public void HideActionUI()
    {
        foreach(GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }
}
