using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character_Stats", menuName = "Character Stats")]
[System.Serializable]
public class Stats_Character : Stats_Unit
{
    //regular units won't need this data
    public int experience = 0;
    public int expToLevel = 100;
    public int growthToBump = 100;

    public enum character_list : int {Avery, Chalice, Crastos, Dondi, Eve, Faber, Fant, Ghar, Gradio, Hadrian, Jor, Kallen, Lena, Leonardo, Liliane, Litugenos, Lyra, Nora, Saph, Trinity, Vyka};



    public override int getMovement()
    {
        throw new System.NotImplementedException();
    }

    public override int getStat()
    {
        throw new System.NotImplementedException();
    }

    public override bool growStat()
    {
        throw new System.NotImplementedException();
        //for when I can get the stats back into a 2D array
    }

    public override void growthUp()
    {
        throw new System.NotImplementedException();
    }

    public override void levelUp()
    {
        health[3] += health[2]; //we can split this off into a method once I figure out how to get 2D arrays working for stats.
        while(health[3] >= growthToBump)
        {
            health[3] -= growthToBump;
            health[1] += 1;
        }
    }

    public override void setMovement()
    {
        throw new System.NotImplementedException();
    }

    public void gainExperience(int exp)
    {
        experience += exp;
        while(experience >= expToLevel)
        {
            experience -= expToLevel;
            level += 1;
            levelUp();
        }
    }

    public override int derive_Accuracy()
    {
        int temp = 0;
        temp += skill[1]*2;
        temp += favor[1];
        //find other sources
        return temp;
    }

    public override int derive_Evasion()
    {
        int temp = 0;
        temp += speed[1] * 2;
        temp += favor[1];
        //find other sources
        return temp;
    }

    public override int derive_Critical()
    {
        int temp = 0;
        temp += skill[1] / 2;
        //find other sources
        return temp;
    }

    public override int derive_Guard()
    {
        int temp = 0;
        temp += favor[1];
        //find other sources
        return temp;
    }

    public override int derive_Attack(string dmgtype)
    {
        int temp = 0;
        //add weapon might
        if (dmgtype.Equals("Physical"))
        {
            temp += might[1];
        }
        else if(dmgtype.Equals("Magical"))
        {
            temp += focus[1];
        }
        else if (dmgtype.Equals("Hybrid"))
        {
            temp += might[1];
            temp += focus[1];
        }

        //find other sources
        return temp;
    }

    public override int derive_Defense(string dmgtype)
    {
        int temp = 0;
        //add weapon might
        if (dmgtype.Equals("Physical"))
        {
            temp += armor[1];
        }
        else if (dmgtype.Equals("Magical"))
        {
            temp += ward[1];
        }
        else if (dmgtype.Equals("Hybrid"))
        {
            temp += armor[1];
            temp += ward[1];
        }

        //find other sources
        return temp;
    }

    public override int derive_Follow_up()
    {
        int temp = 0;
        temp += speed[1];
        //find other sources
        return temp;
    }

    public override void takeDamage(int amount)
    {
        if(amount > 0)
        {
            health[0] -= amount;
        }
    }
}
