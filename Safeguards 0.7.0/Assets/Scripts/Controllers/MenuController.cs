using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    //prefabs, for instantiation
    public IPanel itemElement;
    public GameObject unitMenu;


    public GameObject[] actionMenu;
    // Set the unit UI panels to these values in Editor

    //I need to improve how I handle the menus

    void Start()
    {
        //InputController.mSelect.AddListener(ShowActionUI);
        //InputController.mDeselect.AddListener(HideActionUI);
        InputController.SelectEvent.dynamicCalls += ShowActionUI;
        InputController.DeselectEvent.dynamicCalls += HideActionUI;
    }

    public void ShowEquipUI(StatsManager user)
    {

        foreach (Weapon item in user.statsData.weapons)
        {
            //create an instance of the weapon panel prefab
            //set the instance's item value to item
        }
        foreach (Item item in user.statsData.accessories)
        {

        }
        foreach (Item item in user.statsData.inventory)
        {

        }

    }

    public void ShowActionUI(GameObject owner)
    {
        //create new menu and attach it to the owner
        GameObject newMenu = Instantiate(unitMenu);

        newMenu.GetComponentInChildren<UIHolder>().target = owner;
        //actionMenu[0].SetActive(true);
        //actionMenu[1].SetActive(true);
    }

    public void HideActionUI()
    {
        //Destroy(menu);
        Destroy(GameObject.Find("UnitMenuCanvas(Clone)"));
    }
}
