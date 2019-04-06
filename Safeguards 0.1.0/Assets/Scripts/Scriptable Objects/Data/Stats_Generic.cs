using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stats_Generic : ScriptableObject
{
    //public enum stat_names : int {Health, Might, Focus, Skill, Speed, Favor, Armor, Ward};
    public string[] stat_names = new string[] { "Health", "Might", "Focus", "Skill", "Speed", "Favor", "Armor", "Ward" };
    public enum stat_types : int {Current =0, Base =1, Growth =2, Progress =3};
    public int[] health = {0,0,0,0};

    public abstract int getStat();
    public abstract bool growStat();

    public abstract void takeDamage(int amount);
}
