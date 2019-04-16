using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : MonoBehaviour
{
    public enum stat_types : int { Current = 0, Base = 1, Growth = 2, Progress = 3 };
    public string[] stat_names = new string[] { "Health", "Might", "Focus", "Skill", "Speed", "Favor", "Armor", "Ward" };

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
    public List<Item> equipped = new List<Item>(); //these are separate so that we can tell what's equipped and what's not.
    public List<Item> inventory = new List<Item>();
    public List<string> talents = new List<string>();
    public Dictionary<string, int> mastery = new Dictionary<string, int>();
    public Dictionary<string, string> actions = new Dictionary<string, string>(); //gives us a place to store a list of the unit's available actions
        //this is a dictionary so we can have an action paired with the section it should go under


    //regular units won't need this data, but for some reason unity doesn't see that my CharacterData class inherits from MonoBehavior.  :(
    public int experience = 0;
    public int expToLevel = 100;
    public int growthToBump = 100;

    public enum character_list : int { Avery, Chalice, Crastos, Dondi, Eve, Faber, Fant, Ghar, Gradio, Hadrian, Jor, Kallen, Lena, Leonardo, Liliane, Litugenos, Lyra, Nora, Saph, Trinity, Vyka };
    public Dictionary<string, int> supports = new Dictionary<string, int>();



    public void LoadData(string id)
    {
        //check in the persistent data path for the file name based on id
            //if found, set stats and exit
            //else, proceed
        //use id as a string to find this unit's data from a big table of generic units.
            //then use that data to fill in the variables above.
    }

    public void SaveData()
    {

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
        foreach(Item item in equipped)
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












    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
