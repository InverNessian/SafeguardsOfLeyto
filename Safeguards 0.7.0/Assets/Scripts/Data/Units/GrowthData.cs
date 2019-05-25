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

    //talents
    public int talentPoints = 0; //this is incremented at every even level
    public List<string> naturalTalents; //this holds a list of the natural talent list the user has
    public List<string> acquiredTalents; //for adding/removing talents based on supports, etc

        //supports
    public int maxSupportTicks;
    public List<string> friends;
    public List<int> ticks;
    public List<int> ranks; //noted separately in case they don't want to commit to a support
    //public Dictionary<string, int> supports = new Dictionary<string, int>();

    //methods
    public bool LearnTalent(string tname)
    {
        //check if talent is native
        int temp = ImportController.GetTalentInfo(tname).Cost;
        if (!naturalTalents.Contains(tname))
        {
            temp++;
        }
        //check if unit has enough talent points
        if (talentPoints >= temp)
        {
            talentPoints -= temp;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ApplySupportBonuses(Support support)
    {
        List<int> growthMods = ImportController.ConvertSupportToGrowths(support);
        healthGrowth += growthMods[0];
        mightGrowth += growthMods[1];
        focusGrowth += growthMods[2];
        skillGrowth += growthMods[3];
        speedGrowth += growthMods[4];
        favorGrowth += growthMods[5];
        armorGrowth += growthMods[6];
        wardGrowth += growthMods[7];
    }

    public bool GainSupportRank(string sname) //needs some adjusting; the logic doesn't quite work?
    {
        int temp = friends.IndexOf(sname);
        //elegant way to avoid a double condition
        //  if they have a higher rank in the ticks, they are guaranteed to have enough ticks to get that rank
        if (ImportController.GetRankFromTicks(ticks[temp]) > ranks[temp]) //to avoid a null error maybe set these initially to 0
        {
            ranks[temp] += 1;
            return true;
        }
        else
        {
            return false;
        }
    }

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
