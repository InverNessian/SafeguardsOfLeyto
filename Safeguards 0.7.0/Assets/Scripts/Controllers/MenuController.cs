using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    public GameObject masterPanel;
    public GameObject actionPanel;
    // Start is called before the first frame update


    void Start()
    {
        //actionPanel = GameObject.Find("OwnerPanel");
        UnitController.mSelect.AddListener(ShowActionUI);
        UnitController.mDeselect.AddListener(HideActionUI);
    }


    public void ShowActionUI(GameObject owner)
    {

        //masterPanel.transform.position = owner.transform.position;
        //masterPanel.transform.position = new Vector3(0, 0, 0);
        masterPanel.GetComponent<UIHolder>().target = owner;
        masterPanel.SetActive(true);
        actionPanel.SetActive(true);
    }

    public void HideActionUI(GameObject owner)
    {
        masterPanel.SetActive(false);
        actionPanel.SetActive(false);
    }
}
