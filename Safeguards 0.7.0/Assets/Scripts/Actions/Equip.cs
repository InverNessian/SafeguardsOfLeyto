using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Equip : Action
{
    //data
    public StatsManager user;
    public EquipData equipData;
    public IPanel panel; //maybe needed to reference the equip panel

    //delegates
    public delegate void TriggerEquipEffect(EquipData equip);

    TriggerEquipEffect MaxWeapons;
    TriggerEquipEffect MaxAccessories;
    TriggerEquipEffect TrainingType;

    public void PreviewLoadout()
    {
        //the equip window will show a list of the user's inventory on one side
        // and a couple of "slots" showing their weapon/accessory slots on the other side
        //  the user can drag items between inventory and slots to equip/unequip
        //   the slots will turn red / not allow equips that are invalid
        //    also they will display stat changes / updated stats so the player can see before committing to the update.
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
