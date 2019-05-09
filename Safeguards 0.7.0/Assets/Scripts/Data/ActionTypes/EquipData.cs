using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipData //apparently structs are picky, lol
{
    public string user;
    public bool equippable;
    public bool success;
    public List<Item> inventory;
    public List<Item> accessories;
    public List<Weapon> weapons; //these can be the lists of what the user wants to equip.
    //so if the equip is accepted and valid, these lists will replace the user's lists for the same names

    public EquipData(StatsManager person)
    {
        user = person.statsData.displayName;
        equippable = false; //setting this to true initially because it loads the user's existing loadout, which must be valid by logic
        success = false;
        inventory = new List<Item>(person.statsData.inventory);
        accessories = new List<Item>(person.statsData.accessories);
        weapons = new List<Weapon>(person.statsData.weapons);
    }
    
}
