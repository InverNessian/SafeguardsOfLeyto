using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(fileName ="GrowthData",menuName ="GrowthData")]
public class GrowthData : ScriptableObject
{
    public string displayName;

        //growths
    public int healthGrowth = 0;
    public int mightGrowth = 0;
    public int focusGrowth = 0;
    public int skillGrowth = 0;
    public int speedGrowth = 0;
    public int favorGrowth = 0;
    public int armorGrowth = 0;
    public int wardGrowth = 0;

        //progress
    public int healthProgress = 0;
    public int mightProgress = 0;
    public int focusProgress = 0;
    public int skillProgress = 0;
    public int speedProgress = 0;
    public int favorProgress = 0;
    public int armorProgress = 0;
    public int wardProgress = 0;

        //level-related
    public int experience = 0;
    public int expToLevel = 100;
    public int progressToBump = 100;
    public int talentPoints = 0; //this is incremented at every even level


    public Dictionary<string, int> supports = new Dictionary<string, int>();

    //methods
    public void GainExperience(int amount)
    {
        experience += amount;
        if(experience >= expToLevel)
        {
            experience -= expToLevel;
            //Invoke(LevelUp);  //Since Level is separated in the new model, we'll want to invoke an event to alert them about it.
            GrowStats();
        }
    }

    private void GrowStats()
    {
        //check if level is even/odd
        //either Invoke health or increase talentPoints
        healthProgress += healthGrowth;
        if(healthProgress >= progressToBump)
        {
            healthProgress -= progressToBump;
            //Invoke(PlusStat("health"));
        }
        mightProgress += mightGrowth;
        if (mightProgress >= progressToBump)
        {
            mightProgress -= progressToBump;
            //Invoke(PlusStat("might"));
        }
        focusProgress += focusGrowth;
        if (focusProgress >= progressToBump)
        {
            focusProgress -= progressToBump;
            //Invoke(PlusStat("focus"));
        }
        skillProgress += skillGrowth;
        if (skillProgress >= progressToBump)
        {
            skillProgress -= progressToBump;
            //Invoke(PlusStat("skill"));
        }
        speedProgress += speedGrowth;
        if (speedProgress >= progressToBump)
        {
            speedProgress -= progressToBump;
            //Invoke(PlusStat("speed"));
        }
        favorProgress += favorGrowth;
        if (favorProgress >= progressToBump)
        {
            favorProgress -= progressToBump;
            //Invoke(PlusStat("favor"));
        }
        armorProgress += armorGrowth;
        if (armorProgress >= progressToBump)
        {
            armorProgress -= progressToBump;
            //Invoke(PlusStat("armor"));
        }
        wardProgress += wardGrowth;
        if (wardProgress >= progressToBump)
        {
            wardProgress -= progressToBump;
            //Invoke(PlusStat("ward"));
        }
    }
}
