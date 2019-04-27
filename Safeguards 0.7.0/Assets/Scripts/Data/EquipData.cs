using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipData //apparently structs are picky, lol
{
    public bool equippable;
    public bool success;
    public List<Item> inventory;
    public List<Item> accessories;
    public List<Weapon> weapons; //these can be the lists of what the user wants to equip.
    //so if the equip is accepted and valid, these lists will replace the user's lists for the same names

    public EquipData()
    {
        equippable = false;
        success = false;
        inventory = new List<Item>();
        accessories = new List<Item>();
        weapons = new List<Weapon>();
    }
    
}
