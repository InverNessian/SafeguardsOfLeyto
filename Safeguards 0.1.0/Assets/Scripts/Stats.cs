using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats : MonoBehaviour  // this should be serializable so it can be saved/loaded
{
    public string[] stat_names = new string[] {"Health", "Might", "Focus", "Skill", "Speed", "Favor", "Armor", "Ward"};

    public Dictionary<string, Dictionary<string, int>> stat_values = new Dictionary<string, Dictionary<string, int>>();
    //Dictionary<string, int[]> stat_values = new Dictionary<string, int[]>();

    //with prefabs, I can add/remove stuff as needed.
    int expToLevel = 100;
    int growthToBump = 100;  //Nora can modify these

    List<string> status = new List<string>();
    //we can store buffs/debuffs with this.  also usable with other "conditions" like Injured or Alone (they can be added to the list here on taking damage, for example)

    int movement = 0;
    
    void Start()
    {
        
        //if(stat_values.load == true){



        //else{
        Dictionary<string, int> temp = new Dictionary<string, int>
        {
            { "current", 0 },
            { "base", 0 },
            { "growth", 10 },
            { "progress", 0 }
        };

        foreach (string attr in stat_names)
        {
            stat_values.Add(attr, temp);
        }
    }

    public void levelUp()
    {
        bool check = false;
        bool[] bumps = { false, false, false, false, false, false, false, false };
        foreach (string attr in stat_names){
            check = growStat(attr);
            if (check)
            {
                //set the one in bumps to true
                check = false;
            }
        }
        //return bumps;
    }

    public bool growStat(string stat, int progress)
    {
        bool bump = false;
        //Dictionary<string, int> temp = stat_values[stat];
        stat_values[stat]["progress"] += progress;
        if(stat_values[stat]["progress"] >= growthToBump)
        {
            bump = true;
            stat_values[stat]["base"] += 1;
            stat_values[stat]["progress"] -= growthToBump;
        }
        return bump;
    }

    bool growStat(string stat)  //if you don't specify a progress value, it assumes you're leveling up and just uses the growth
    {
        bool bump = false;
        stat_values[stat]["progress"] += stat_values[stat]["growth"];
        if (stat_values[stat]["progress"] >= growthToBump)
        {
            bump = true;
            stat_values[stat]["base"] += 1;
            stat_values[stat]["progress"] -= growthToBump;
        }
        return bump;
    }

    public void growthUp(string stat, int growth, int flat)
    {
        stat_values[stat]["growth"] += growth;
        stat_values[stat]["base"] += flat;
    }

    public void growthUp(string stat, int growth)
    {
        stat_values[stat]["growth"] += growth;
    }

    int getBaseStat(string stat)
    {
        return stat_values[stat]["base"]; 
    }
    //this can be adjusted to return -1 for obstacles or stuff that don't have particular stat_values.

    int getCurrentStat(string stat)
    {
        return stat_values[stat]["current"];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int getMovement()
    {
        return movement;
    }

    void setMovement(int adjustment)
    {
        movement += adjustment;
    }
}
