using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

//UnityCollege gave me an interesting idea.  What if this data was broken up into sections?
//for example, what if the attributes were their own thing, and then there was another component that held equipped weapon, then another for level/exp
//we could have a SO for each weapon being used, then just tie the SO to the data holder.
//could be a decent solution for our unique HP problem- maybe enemy units have a separate data holder for their HP value?

[CreateAssetMenu(menuName = "UnitData", fileName = "UnitData")]
public class UnitData : ScriptableObject
{
    //public enum stat_types : int { Current = 0, Base = 1, Growth = 2, Progress = 3 };
    //public string[] stat_names = new string[] { "Health", "Might", "Focus", "Skill", "Speed", "Favor", "Armor", "Ward" };

    //note that only public fields of certain types are serialized.
    public int[] health = { 0, 0, 0, 0 };
    public int[] might = { 0, 0, 0, 0 };
    public int[] focus = { 0, 0, 0, 0 };
    public int[] skill = { 0, 0, 0, 0 };
    public int[] speed = { 0, 0, 0, 0 };
    public int[] favor = { 0, 0, 0, 0 };
    public int[] armor = { 0, 0, 0, 0 };
    public int[] ward = { 0, 0, 0, 0 };

    public string unit_name;
    public int level = 1;
    public int movement = 0;  //if this is public, maybe we don't need get/set movement

    public List<string> status = new List<string>();
    [SerializeField]
    public Weapon weapon; //for ease of use
    public List<Item> accessories = new List<Item>(); //these are separate so that we can tell what's equipped and what's not.
    public List<Item> inventory = new List<Item>();
    public List<Talent> talents = new List<Talent>();
    public List<Training> mastery = new List<Training>();
    public List<string> actions = new List<string>(); 


    //regular units won't need this data, but for some reason unity doesn't see that my CharacterData class inherits from MonoBehavior.  :(
    public int experience = 0;
    public int expToLevel = 100;
    public int growthToBump = 100;

    public Dictionary<string, int> supports = new Dictionary<string, int>();




    public void TakeDamage(int amount)
    {
        if(amount > 0)
        {
            health[0] -= amount;
        }
        if(health[0] <= 0)
        {
            //call "Down" event
        }
    }

    public int DeriveAttack(Weapon weapon)
    {
        int temp = 0;
        temp += weapon.statMods[0];
        switch (weapon.damageType)
        {
            case "Physical":
                temp += might[1];
                //add a check for Hybrid Strike in the user's talents
                break;
            case "Magical":
                temp += focus[1];
                break;
            case "Hybrid":
                temp += might[1];
                temp += focus[1];
                break;
        }
        return temp;
    }

    public int DeriveDefense(Weapon weapon)
    {
        int temp = 0;
        temp += this.weapon.statMods[1];
        foreach(Item item in accessories)
        {
            temp += item.statMods[1];
        }
        switch (weapon.damageType)
        {
            case "Physical":
                temp += armor[1];
                break;
            case "Magical":
                temp += ward[1];
                break;
            case "Hybrid":
                temp += armor[1];
                temp += ward[1];
                break;
        }
        return temp;
    }

    public int DeriveFollowup()
    {
        int temp = 0;
        temp += speed[1];
        return temp;
    }

    public int DeriveAccuracy()
    {
        int temp = 0;
        temp += skill[1] * 2;
        temp += favor[1];
        return temp;
    }

    public int DeriveEvasion()
    {
        int temp = 0;
        temp += speed[1] * 2;
        temp += favor[1];
        return temp;
    }

    public int DeriveCritical()
    {
        int temp = 0;
        temp += (int)Mathf.Floor(skill[1] / 2);
        return temp;
    }

    public int DeriveGuard()
    {
        int temp = 0;
        temp += favor[1];
        return temp;
    }












}
