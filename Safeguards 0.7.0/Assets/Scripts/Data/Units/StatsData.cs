using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

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

    public string displayName = "";

    public List<string> status = new List<string>();
    public List<Item> inventory = new List<Item>();

    //not quite sure what to do with these three yet, putting them here for now.
    public List<Talent> talents = new List<Talent>();
    public List<Training> mastery = new List<Training>();
    public List<string> actions = new List<string>();


    void Start()
    {

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
        temp += weapon.statMods[0];
        switch (weapon.damageType)
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
        //temp += this.weapon.statModsValue;
        //foreach (Item item in accessories)
        //{
        //    temp += item.statModsValue;
        //}
        switch (weapon.damageType)
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
        return temp;
    }

    public int DeriveAccuracy()
    {
        int temp = 0;
        temp += skillValue * 2;
        temp += favorValue;
        return temp;
    }

    public int DeriveEvasion()
    {
        int temp = 0;
        temp += speedValue * 2;
        temp += favorValue;
        return temp;
    }

    public int DeriveCritical()
    {
        int temp = 0;
        temp += (int)Mathf.Floor(skillValue / 2);
        return temp;
    }

    public int DeriveGuard()
    {
        int temp = 0;
        temp += favorValue;
        return temp;
    }
}
