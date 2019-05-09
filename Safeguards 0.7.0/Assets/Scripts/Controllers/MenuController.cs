using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    //set these to the menu items
    public GameObject[] unitMenu;
    public GameObject[] itemElements;

    //I need to improve how I handle the menus

    void Start()
    {
        InputController.SelectEvent.dynamicCalls += ShowActionUI;
        InputController.DeselectEvent.dynamicCalls += HideActionUI;
    }

    public EquipData HideEquipUI()
    {
        return new EquipData();
    }

    public void ShowEquipUI(Equip equip)
    {
        //assume units have default, and allow triggers to hook in from BeginEquip?
        //this allows Well-equipped or Load Bearer to create/destroy extra slots as needed

        //activate the equip gui
        itemElements[0].GetComponent<UIHolder>().target = equip.user.gameObject;
        itemElements[0].SetActive(true);

        //set all the texts to "none"
        for(int i=0; i<itemElements[0].transform.childCount; i++)
        {
            GameObject element = itemElements[0].transform.GetChild(i).gameObject;
            if(!(element.name == "BackButton" || element.name == "ConfirmButton")) //tried to do this using "is Button" but it doesn't work
            {
                element.GetComponentInChildren<Text>().text = "None";
            }
            element.SetActive(true);
        }

        //then we set the items into the slots
        try //try to get weapon and put it in the itemdragger
        {
            itemElements[0].transform.GetChild(0).gameObject.GetComponentInChildren<ItemDragger>().SetItem(equip.user.statsData.weapons[0]);
            //maybe fire an ItemEvent here?  could be useful for cleaning up this method
        }
        catch
        {

        }

        try //try to get accessory and put it in itemdragger
        {
            itemElements[0].transform.GetChild(1).gameObject.GetComponentInChildren<ItemDragger>().SetItem(equip.user.statsData.accessories[0]);
        }
        catch
        {

        }

        try //try to set accessories
        {
            for (int i = 0; i < equip.user.statsData.inventory.ToArray().Length; i++)
            {
                //offset i by 2 to account for other slots
                itemElements[0].transform.GetChild(i + 2).gameObject.GetComponentInChildren<ItemDragger>().SetItem(equip.user.statsData.inventory[i]);
            }
        }
        catch
        {

        }

        /*
        //check for multiple weapons
        if (user.statsData.weapons.ToArray().Length > 1)
        {
            for (int i = 0; i < user.statsData.weapons.ToArray().Length; i++)
            { //offset by 1 because of the master level panel
                itemElements[i+1].GetComponentInChildren<ItemDragger>().item = user.statsData.weapons[i];
                itemElements[i+1].GetComponentInChildren<Text>().text = user.statsData.weapons[i].ItemName;
                //itemElements[i+1].SetActive(true);
            }

            //technically we can still check for accessories but that'll come later
        }
        //check for multiple accessories
        else if (user.statsData.accessories.ToArray().Length > 1)
        {
            itemElements[1].GetComponentInChildren<ItemDragger>().item = user.statsData.weapons[0];
            itemElements[1].GetComponentInChildren<Text>().text = user.statsData.weapons[0].ItemName;
            //itemElements[1].SetActive(true);

            for (int i = 0; i < user.statsData.accessories.ToArray().Length; i++)
            {
                //offset i by 2 to account for weapon already being set
                itemElements[i+2].GetComponentInChildren<ItemDragger>().item = user.statsData.accessories[i];
                itemElements[i+2].GetComponentInChildren<Text>().text = user.statsData.accessories[i].ItemName;
                //itemElements[i+2].SetActive(true);
            }
            
        }
        //normal equip
        else if(user.statsData.weapons.ToArray().Length > 0) //should I have the above ones just be Events?
        {
            
            
        }
        */


    }

    public void ShowActionUI(GameObject owner)
    {
        //[0] always is the higher level panel, and [1] is the action panel
        unitMenu[0].GetComponent<UIHolder>().target = owner;
        unitMenu[0].SetActive(true);
        unitMenu[1].SetActive(true);

        //create new menu and attach it to the owner
        //GameObject newMenu = Instantiate(unitMenu);

        //newMenu.GetComponentInChildren<UIHolder>().target = owner;
    }

    public void HideActionUI()
    {
        foreach(GameObject panel in unitMenu)
        {
            panel.SetActive(false);
        }
        for (int i = 0; i < itemElements[0].transform.childCount; i++)
        {
            itemElements[0].transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    /*
     * 
     * 
     * 
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
     * 
     * */
}
