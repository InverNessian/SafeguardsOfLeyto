using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CampMenuController : MonoBehaviour
{
    //set these to the menu items
    public GameObject unitMenu;
    public GameObject equipMenu;

    public static Action action;

    //I need to improve how I handle the menus

    void Start()
    {
        CampController.SelectEvent.dynamicCalls += ShowCampUI;
        CampController.DeselectEvent.dynamicCalls += HideCampUI;
    }


    public void ShowEquipUI(EquipData equipData)
    {
        //assume units have default, and allow triggers to hook in from BeginEquip?
        //this allows Well-equipped or Load Bearer to create/destroy extra slots as needed

        //activate the equip gui
        equipMenu.GetComponent<UIHolder>().target = (action as Equip).user.gameObject;
        equipMenu.SetActive(true);

        //initial setup of slots
        for (int i = 0; i < equipMenu.transform.childCount; i++)
        {
            GameObject element = equipMenu.transform.GetChild(i).gameObject;
            if (!(element.name == "BackButton" || element.name == "ConfirmButton")) //tried to do this using "is Button" but it doesn't work
            {
                element.GetComponentInChildren<Text>().text = "None";
            }
            element.SetActive(true);
            switch (i)
            {
                case 0: //maybe add a color change too
                    equipMenu.transform.GetChild(i).gameObject.GetComponent<SlotDragger>().SlotType = "Weapon";
                    break;
                case 1:
                    equipMenu.transform.GetChild(i).gameObject.GetComponent<SlotDragger>().SlotType = "Accessory";
                    break;
                case 2:
                    equipMenu.transform.GetChild(i).gameObject.GetComponent<SlotDragger>().SlotType = "Inventory";
                    break;
                case 3:
                    equipMenu.transform.GetChild(i).gameObject.GetComponent<SlotDragger>().SlotType = "Inventory";
                    break;
                case 4:
                    equipMenu.transform.GetChild(i).gameObject.GetComponent<SlotDragger>().SlotType = "Inventory";
                    break;
            }
        }

        //then we set the items into the slots
        try //try to get weapon and put it in the itemdragger
        {
            equipMenu.transform.GetChild(0).gameObject.GetComponentInChildren<ItemDragger>().SetItem((action as Equip).user.statsData.weapons[0]);
            //maybe fire an ItemEvent here?  could be useful for cleaning up this method
        }
        catch
        {

        }

        try //try to get accessory and put it in itemdragger
        {
            equipMenu.transform.GetChild(1).gameObject.GetComponentInChildren<ItemDragger>().SetItem((action as Equip).user.statsData.accessories[0]);
        }
        catch
        {

        }

        try //try to set accessories
        {
            for (int i = 0; i < (action as Equip).user.statsData.inventory.ToArray().Length; i++)
            {
                //offset i by 2 to account for other slots
                equipMenu.transform.GetChild(i + 2).gameObject.GetComponentInChildren<ItemDragger>().SetItem((action as Equip).user.statsData.inventory[i]);
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

    public void HideEquipUI()
    {
        (action as Equip).EquipLoadout();
        HideCampUI();
        ShowCampUI((action as Equip).user.gameObject);
        //action = null;
    }

    public void ShowCampUI(GameObject owner)
    {
        //[0] always is the higher level panel, and [1] is the action panel
        unitMenu.GetComponent<UIHolder>().target = owner;
        unitMenu.SetActive(true);
        for(int i = 0; i< unitMenu.transform.childCount; i++)
        {
            unitMenu.transform.GetChild(i).gameObject.SetActive(true);
        }
        

        //create new menu and attach it to the owner
        //GameObject newMenu = Instantiate(unitMenu);

        //newMenu.GetComponentInChildren<UIHolder>().target = owner;
    }

    public void HideCampUI()
    {
        for (int i = 0; i < unitMenu.transform.childCount; i++)
        {
            unitMenu.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < equipMenu.transform.childCount; i++)
        {
            equipMenu.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}
