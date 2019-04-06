using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stats_Unit : Stats_Generic
{
    public int[] might = { 0, 0, 0, 0 };
    public int[] focus = { 0, 0, 0, 0 };
    public int[] skill = { 0, 0, 0, 0 };
    public int[] speed = { 0, 0, 0, 0 };
    public int[] favor = { 0, 0, 0, 0 };
    public int[] armor = { 0, 0, 0, 0 };
    public int[] ward = { 0, 0, 0, 0 };

    public int level = 1;
    public int movement = 0;  //if this is public, maybe we don't need get/set movement

    public List<string> status = new List<string>();
    //we can store buffs/debuffs with this.  also usable with other "conditions" like Injured or Alone (they can be added to the list here on taking damage, for example)
    //public List<Item> inventory = new List<Item>(); //need to add an inventory


    public abstract void levelUp();
    public abstract void growthUp();
    public abstract int getMovement();
    public abstract void setMovement();

    //derived stats
    public abstract int derive_Accuracy();
    public abstract int derive_Evasion();
    public abstract int derive_Critical();
    public abstract int derive_Guard();
    public abstract int derive_Attack(string dmgtype); //update this later to allow for hybrid strike and adaptive weapons
    public abstract int derive_Defense(string dmgtype);
    public abstract int derive_Follow_up();
}
