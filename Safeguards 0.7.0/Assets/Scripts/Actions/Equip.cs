using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Equip : Action
{
    //data
    public StatsManager user;
    public EquipData equipData;
    public static GameObject equipMenu;

    //events
    public EquipEvent BeginEquip = new EquipEvent();
    public EquipEvent ItemSlot = new EquipEvent();
    public EquipEvent TrainingType = new EquipEvent();
    public EquipEvent UpdateEquip = new EquipEvent();
    public EquipEvent FinishEquip = new EquipEvent();

    //delegates
    /*
    public delegate void TriggerEquipEffect(EquipData equip);

    TriggerEquipEffect MaxWeapons;
    TriggerEquipEffect MaxAccessories;
    TriggerEquipEffect TrainingType;
    */

    public Equip(StatsManager person)
    {
        if(equipMenu == null)
        {
            equipMenu = GameObject.Find("EquipPanel");
        }
        user = person;
        equipData = new EquipData(person);
        SlotDragger.equip = this;
        MenuController.action = this;
        BeginEquip.dynamicCalls += GameObject.Find("MenuController").GetComponent<MenuController>().ShowEquipUI;
        //FinishEquip.dynamicCalls += GameObject.Find("MenuController").GetComponent<MenuController>().HideEquipUI;
    }

    public void SetEquipDelegates()
    {
        AbilityTrigger[] triggers = user.gameObject.GetComponents<AbilityTrigger>();
        foreach (AbilityTrigger trigger in triggers)
        {
            if (trigger is IEquip)
            {
                
            }
        }
    }

    public bool ValidateEquip(Item newI, Item oldI, string newSlot, string oldSlot)
    {
        //newI goes into newSlot, oldI goes into oldSlot
        bool check = false;
        bool startValue = equipData.equippable; //in case we need to reset
        equipData.equippable = check;

        //now, we do our checks.  we do it in stages so that there's no way to cheese a trigger into overwriting the previous ones.

        //ItemSlot step; we only check that weapons aren't going into accessory slots, etc
        switch (newSlot)
        {
            case "Weapon":
                if(newI is Weapon) { check = true; }
                break;
            case "Accessory":
                if (!(newI is Weapon)) { check = true; }
                break;
            case "Inventory":
                if (oldSlot.Equals("Inventory")) { check = true; }
                if (oldSlot.Equals("Accessory") && !(oldI is Weapon)) { check = true; }
                if (oldSlot.Equals("Weapon") && oldI is Weapon) { check = true; }
                break;
        }
        //set values for processing
        equipData.equippable = check;
        ItemSlot.InvokeSafe(equipData);
        check = equipData.equippable;

        //TrainingType step; here we only check that the user matches the training types
        if (check) //only do this step if the item slot clears validation
        {
            check = false;
            if (newSlot.Equals("Weapon") && newI is Weapon)
            {
                if (user.statsData.CheckTraining(newI.Type))
                {
                    check = true;
                } //had to nest these logical checks, otherwise the check training will always make it fail into the general else
            }
            else if (oldSlot.Equals("Weapon") && oldI is Weapon)
            {
                if (user.statsData.CheckTraining(oldI.Type))
                {
                    check = true;
                }
            }
            else
            {
                check = true;
            }

            //this check happens in this conditional
            equipData.equippable = check;
            TrainingType.InvokeSafe(equipData);
            check = equipData.equippable;
        }

        if (!check) //if this isn't a valid item swap, reset equippable bool
        {
            equipData.equippable = startValue;
        }
        else //if it is valid, update the equipData
        {
            switch (newSlot)
            {
                case "Weapon":
                    equipData.weapons.Remove(oldI as Weapon);
                    equipData.weapons.Add(newI as Weapon);
                    break;
                case "Accessory":
                    equipData.accessories.Remove(oldI);
                    equipData.accessories.Add(newI);
                    break;
                case "Inventory":
                    equipData.inventory.Remove(oldI);
                    equipData.inventory.Add(newI);
                    break;
            }
            switch (oldSlot)
            {
                case "Weapon":
                    equipData.weapons.Remove(newI as Weapon);
                    equipData.weapons.Add(oldI as Weapon);
                    break;
                case "Accessory":
                    equipData.accessories.Remove(newI);
                    equipData.accessories.Add(oldI);
                    break;
                case "Inventory":
                    equipData.inventory.Remove(newI);
                    equipData.inventory.Add(oldI);
                    break;
            }
        }
        //return our result
        return check;
    }

    public void PreviewLoadout()
    {
        //the equip window will show a list of the user's inventory on one side
        // and a couple of "slots" showing their weapon/accessory slots on the other side
        //  the user can drag items between inventory and slots to equip/unequip
        //   the slots will turn red / not allow equips that are invalid
        //    also they will display stat changes / updated stats so the player can see before committing to the update.

        //because of how the GUI works this method may not be necessary
        BeginEquip.Invoke(equipData);
    }

    public void EquipLoadout()
    {
        //user equip method, replace existing ones and move them back to inventory
        user.statsData.weapons = equipData.weapons;
        user.statsData.accessories = equipData.accessories;
        user.statsData.inventory = equipData.inventory;
    }



    public override void UndoAction()
    {
        throw new System.NotImplementedException();
    }

}
