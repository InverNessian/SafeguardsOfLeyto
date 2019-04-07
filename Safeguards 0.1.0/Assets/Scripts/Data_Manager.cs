using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//somehow we need to import CsvHelper
using CsvHelper;

public class Data_Manager : MonoBehaviour
{

    public Stats_Character character_data;
    public string character_name;

    public string damage_type;
    // Start is called before the first frame update
    void Start()
    {
        if (character_data == null)
        {
            //attempt to load stats

            //otherwise generate new stats
        }
        else
        {
            //create a reader to load data from our CSV
            /*using (var reader = new System.IO.StreamReader("path\\to\\file.csv"))
            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords<dynamic>();
            }
            */
        }
        character_data.unit_name = character_name; //get rid of this later
    }

    public void gainExp(int amount)
    {
        character_data.gainExperience(amount);
    }

    public void attackTarget(string target)
    {
        //fetch the Stats Object of the enemy
            //later we'll have to update this to check for objects too
        Stats_Unit opponent = GameObject.Find(target).GetComponent<Data_Manager>().character_data;

        int hitRate = calculateHitRate(opponent);
        int critChance = calculateCritical(opponent);
        int damage = calculateDamage(opponent, damage_type, new string[] {"Normal"});
        
        //generate randoms
        int hitRoll = Random.Range(1, 100);
        int critRoll = Random.Range(1, 100);
        Debug.Log("Hit Result: " + hitRoll + ", Crit Result: " + critRoll);
        Debug.Log("Damage: " + damage);

        //compare rng results
        if (hitRoll <= hitRate)
        {
            bool critical = false;
            bool guard = false;
            Debug.Log(character_name + " hits with this attack!");
            //check if attack is critical or guard.  need a better way to flag the damage modifiers.
                //maybe we have a list of strings we pass?  IE "critical", "quick", etc that tell us what type of attack it is?
            if(critChance < 0 && critRoll <= Mathf.Abs(critChance))
            {
                guard = true;
                
            }
            else if (critRoll <= critChance)
            {
                critical = true;
            }

            if (critical)
            {
                Debug.Log(character_name + " deals amplified damage!");
                damage = calculateDamage(opponent, damage_type, new string[] { "Critical" });
            }
            else if (guard)
            {
                Debug.Log(character_name + " deals reduced damage...");
                damage = calculateDamage(opponent, damage_type, new string[] { "Guard" });
            }
            else
            {
                Debug.Log(character_name + " deals normal damage!");
            }
            //now we finally commit the damage
            opponent.takeDamage(damage);
        }
        else
        {
            Debug.Log(character_name + " missed this attack...");
        }
    }


    public int calculateHitRate(Stats_Unit opponent)
    {
        
        int temp = 50;
        temp += character_data.derive_Accuracy();
        temp -= opponent.derive_Evasion();
        //find other sources
        return temp;

    }

    public int calculateCritical(Stats_Unit opponent)
    {

        int temp = 0;
        temp += character_data.derive_Critical();
        temp -= opponent.derive_Guard();
        //find other sources
        return temp;

    }

    public int calculateDamage(Stats_Unit opponent, string dmgtype, string[] atktype)
    {

        int temp = 0;
        //attack step
        int atk = character_data.derive_Attack(dmgtype);
        double multiplier = 1.0;
        //check for other multipliers
        foreach (string type in atktype)
        {
            if (type.Equals("Critical")){
                multiplier += 0.5;
            }
            else if (type.Equals("Guard"))
            {
                multiplier -= 0.5;
            }
        }
        temp += (int)Mathf.Floor((float)(atk * multiplier));

        //defense step
        int def = opponent.derive_Defense(dmgtype);
        multiplier = 1.0;
        //check for other multipliers
        //if character_stats.talents.contains("Pierce"), multiplier -= 0.2

        temp -= (int)Mathf.Floor((float)(def * multiplier));

        //damage step
        multiplier = 1.0;
        //check for other multipliers and additives
        foreach (string type in atktype)
        {
            if (type.Equals("Power"))
            {
                multiplier += 1.0;
            }
            else if (type.Equals("Quick"))
            {
                multiplier -= 0.5;
            }
        }
        temp = (int)Mathf.Floor((float)(temp * multiplier));

        //find other sources
        return temp;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
