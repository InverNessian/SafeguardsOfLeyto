using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName ="StatData",menuName ="StatData")]
public class StatsData : ScriptableObject
{
    //stats
    public int level = 1;
    public int movement = 0;

    public int healthValue = 0;
    public int mightValue = 0;
    public int focusValue = 0;
    public int skillValue = 0;
    public int speedValue = 0;
    public int favorValue = 0;
    public int armorValue = 0;
    public int wardValue = 0;

    //meta info
    public string displayName = "";
    public List<string> status = new List<string>();
    public int actionsPerTurn = 2;
    public int followUpThreshold = 5;
    public int maxFollowUpAttacks = 1;

    //inventory
    public int maxInventory = 5;
    public int maxAccessories = 1; //update this on learning relevant talents
    public List<Weapon> weapons = new List<Weapon>(); //only people with Dual Wield or special weapons will ever have more than 1
    public List<Item> accessories = new List<Item>(); //only people with Well-Equipped or similar will have more than 1.
    public List<Item> inventory = new List<Item>();

    //weapon training
    public static List<string> trainingTypes = new List<string> { "Blade", "Spear", "Mace", "Bow", "Knife", "Beast", "Dark", "Anima", "Light", "Staff" };
    //public Dictionary<string, int> training; //unity is dumb and doesn't let us use dictionaries
    public List<string> training;
    public List<int> mastery;


    //talents and actions, stored here
    public List<string> talents = new List<string>(); //maybe this should be a List<string> or Dictionary?
    public List<string> actions = new List<string>();
    //there are triggers we need to create on the gameobject.  see statsmanager


    public void ApplySupportBonuses(Support support)
    {
        List<int> statMods = ImportController.ConvertSupportToValues(support);
        healthValue += statMods[0];
        mightValue += statMods[1];
        focusValue += statMods[2];
        skillValue += statMods[3];
        speedValue += statMods[4];
        favorValue += statMods[5];
        armorValue += statMods[6];
        wardValue += statMods[7];
    }

    public void LearnTalent(string tname)
    {
        talents.Add(tname);
    }

    public bool CheckTalents(string tname)
    {
        bool check = false;
        if (talents.Contains(tname))
        {
            check = true;
        }
        return check;
    }

    public bool CheckTraining(string type)
    {
        return training.Contains(type);
    }

    public void GainTraining(string type)
    {
        training.Add(type);
        mastery.Add(0);
    }

    public int CheckMastery(string type)
    {
        return mastery[training.IndexOf(type)];
    }

    public void PlusStat(string stat)
    {
        switch (stat)
        {
            case "health":
                healthValue += 1;
                break;
            case "might":
                mightValue += 1;
                break;
            case "focus":
                focusValue += 1;
                break;
            case "skill":
                skillValue += 1;
                break;
            case "speed":
                speedValue += 1;
                break;
            case "favor":
                favorValue += 1;
                break;
            case "armor":
                armorValue += 1;
                break;
            case "ward":
                wardValue += 1;
                break;
        }
    }

    public int DeriveAttack(Weapon weapon)
    {
        int temp = 0;
        foreach (Weapon wep in weapons)
        {
            temp += wep.Attack;
        }
        foreach (Item item in accessories)
        {
            temp += item.Attack;
        }
        switch (weapon.Damage)
        {
            case "Physical":
                temp += mightValue;
                //add a check for Hybrid Strike in the user's talents
                break;
            case "Magical":
                temp += focusValue;
                break;
            case "Hybrid":
                temp += mightValue;
                temp += focusValue;
                break;
        }
        return temp;
    }

    public int DeriveDefense(Weapon weapon)
    {
        int temp = 0;
        foreach (Weapon wep in weapons)
        {
            temp += wep.Defense;
        }
        foreach (Item item in accessories)
        {
            temp += item.Defense;
        }
        switch (weapon.Damage)
        {
            case "Physical":
                temp += armorValue;
                break;
            case "Magical":
                temp += wardValue;
                break;
            case "Hybrid":
                temp += armorValue;
                temp += wardValue;
                break;
        }
        return temp;
    }

    public int DeriveFollowup()
    {
        int temp = 0;
        temp += speedValue;

        foreach (Weapon wep in weapons)
        {
            temp += wep.Followup;
        }
        foreach (Item item in accessories)
        {
            temp += item.Followup;
        }
        return temp;
    }

    public int DeriveAccuracy()
    {
        int temp = 0;
        temp += skillValue * 2;
        temp += favorValue;

        foreach (Weapon wep in weapons)
        {
            temp += wep.Accuracy;
        }
        foreach (Item item in accessories)
        {
            temp += item.Accuracy;
        }
        return temp;
    }

    public int DeriveEvasion()
    {
        int temp = 0;
        temp += speedValue * 2;
        temp += favorValue;

        foreach (Weapon wep in weapons)
        {
            temp += wep.Evasion;
        }
        foreach (Item item in accessories)
        {
            temp += item.Evasion;
        }
        return temp;
    }

    public int DeriveCritical()
    {
        int temp = 0;
        temp += (int)Mathf.Floor(skillValue / 2);

        foreach (Weapon wep in weapons)
        {
            temp += wep.Critical;
        }
        foreach (Item item in accessories)
        {
            temp += item.Critical;
        }
        return temp;
    }

    public int DeriveGuard()
    {
        int temp = 0;
        temp += favorValue;

        foreach (Weapon wep in weapons)
        {
            temp += wep.Guard;
        }
        foreach (Item item in accessories)
        {
            temp += item.Guard;
        }
        return temp;
    }
}
