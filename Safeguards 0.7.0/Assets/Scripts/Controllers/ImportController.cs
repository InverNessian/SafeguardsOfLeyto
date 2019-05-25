using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.IO;
using UnityEditor;
using System.Text;
using CsvHelper.Configuration;

public class ImportController //: MonoBehaviour
{

    public static StringBuilder GetEventDialogue(string ename)
    {
        //check the list of events & supports for matching name, then return dialogue
        StringBuilder sb = new StringBuilder();
        return sb;
    }

    public static List<int> ConvertSupportToValues(Support info)
    {
        //initialize a list with 8 values of 0
        List<int> values = new List<int>{0, 0, 0, 0, 0, 0, 0, 0};

        //try this sometime later?
        /*
        switch (info.HP)
        {
            case "Primary":
                int temp = 1;
                do
                {
                    temp++;
                } while (info.Ticks < GetTicksFromRank(temp));
                values[0] = temp;
                break;
        }
        */
        //we check to see if the value for each stat is null
        if(info.HP != null)
        {
            //if it's not null, we increment the value by 1 per support rank if it's primary, or 1 per support rank -1 if it's secondary
            int temp = 0;
            if(info.Ticks > 10)
            {
                temp += info.HP.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 20)
            {
                temp += info.HP.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 40)
            {
                temp += info.HP.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 70)
            {
                temp += info.HP.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 100)
            {
                temp += info.HP.Equals("Primary") ? 1 : 0;
            }
            values[0] = temp;
        }
        if (info.Might != null)
        {
            int temp = 0;
            if (info.Ticks > 10)
            {
                temp += info.Might.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 20)
            {
                temp += info.Might.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 40)
            {
                temp += info.Might.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 70)
            {
                temp += info.Might.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 100)
            {
                temp += info.Might.Equals("Primary") ? 1 : 0;
            }
            values[1] = temp;
        }
        if (info.Focus != null)
        {
            int temp = 0;
            if (info.Ticks > 10)
            {
                temp += info.Focus.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 20)
            {
                temp += info.Focus.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 40)
            {
                temp += info.Focus.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 70)
            {
                temp += info.Focus.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 100)
            {
                temp += info.Focus.Equals("Primary") ? 1 : 0;
            }
            values[2] = temp;
        }
        if (info.Skill != null)
        {
            int temp = 0;
            if (info.Ticks > 10)
            {
                temp += info.Skill.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 20)
            {
                temp += info.Skill.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 40)
            {
                temp += info.Skill.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 70)
            {
                temp += info.Skill.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 100)
            {
                temp += info.Skill.Equals("Primary") ? 1 : 0;
            }
            values[3] = temp;
        }
        if (info.Speed != null)
        {
            int temp = 0;
            if (info.Ticks > 10)
            {
                temp += info.Speed.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 20)
            {
                temp += info.Speed.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 40)
            {
                temp += info.Speed.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 70)
            {
                temp += info.Speed.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 100)
            {
                temp += info.Speed.Equals("Primary") ? 1 : 0;
            }
            values[4] = temp;
        }
        if (info.Favor != null)
        {
            int temp = 0;
            if (info.Ticks > 10)
            {
                temp += info.Favor.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 20)
            {
                temp += info.Favor.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 40)
            {
                temp += info.Favor.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 70)
            {
                temp += info.Favor.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 100)
            {
                temp += info.Favor.Equals("Primary") ? 1 : 0;
            }
            values[5] = temp;
        }
        if (info.Armor != null)
        {
            int temp = 0;
            if (info.Ticks > 10)
            {
                temp += info.Armor.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 20)
            {
                temp += info.Armor.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 40)
            {
                temp += info.Armor.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 70)
            {
                temp += info.Armor.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 100)
            {
                temp += info.Armor.Equals("Primary") ? 1 : 0;
            }
            values[6] = temp;
        }
        if (info.Ward != null)
        {
            int temp = 0;
            if (info.Ticks > 10)
            {
                temp += info.Ward.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 20)
            {
                temp += info.Ward.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 40)
            {
                temp += info.Ward.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 70)
            {
                temp += info.Ward.Equals("Primary") ? 1 : 0;
            }
            if (info.Ticks > 100)
            {
                temp += info.Ward.Equals("Primary") ? 1 : 0;
            }
            values[7] = temp;
        }
        return values;
    }

    public static List<int> ConvertSupportToGrowths(Support info)
    {
        //initialize a list with 8 values of 0
        List<int> values = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };

        //we check to see if the value for each stat is null
        if (info.HP != null)
        {
            //if it's not null, we increment the value according to the chart, it's a bit weird sorry
            int temp = 0;
            if (info.Ticks > 10)
            {
                temp += info.HP.Equals("Primary") ? 10 : 5;
            }
            if (info.Ticks > 20)
            {
                temp += info.HP.Equals("Primary") ? 0 : 5;
            }
            if (info.Ticks > 40)
            {
                temp += info.HP.Equals("Primary") ? 10 : 0;
            }
            if (info.Ticks > 70)
            {
                temp += info.HP.Equals("Primary") ? 0 : 5;
            }
            if (info.Ticks > 100)
            {
                temp += info.HP.Equals("Primary") ? 10 : 0;
            }
            values[0] = temp;
        }
        if (info.Might != null)
        {
            int temp = 0;
            if (info.Ticks > 10)
            {
                temp += info.Might.Equals("Primary") ? 10 : 5;
            }
            if (info.Ticks > 20)
            {
                temp += info.Might.Equals("Primary") ? 0 : 5;
            }
            if (info.Ticks > 40)
            {
                temp += info.Might.Equals("Primary") ? 10 : 0;
            }
            if (info.Ticks > 70)
            {
                temp += info.Might.Equals("Primary") ? 0 : 5;
            }
            if (info.Ticks > 100)
            {
                temp += info.Might.Equals("Primary") ? 10 : 0;
            }
            values[1] = temp;
        }
        if (info.Focus != null)
        {
            int temp = 0;
            if (info.Ticks > 10)
            {
                temp += info.Focus.Equals("Primary") ? 10 : 5;
            }
            if (info.Ticks > 20)
            {
                temp += info.Focus.Equals("Primary") ? 0 : 5;
            }
            if (info.Ticks > 40)
            {
                temp += info.Focus.Equals("Primary") ? 10 : 0;
            }
            if (info.Ticks > 70)
            {
                temp += info.Focus.Equals("Primary") ? 0 : 5;
            }
            if (info.Ticks > 100)
            {
                temp += info.Focus.Equals("Primary") ? 10 : 0;
            }
            values[2] = temp;
        }
        if (info.Skill != null)
        {
            int temp = 0;
            if (info.Ticks > 10)
            {
                temp += info.Skill.Equals("Primary") ? 10 : 5;
            }
            if (info.Ticks > 20)
            {
                temp += info.Skill.Equals("Primary") ? 0 : 5;
            }
            if (info.Ticks > 40)
            {
                temp += info.Skill.Equals("Primary") ? 10 : 0;
            }
            if (info.Ticks > 70)
            {
                temp += info.Skill.Equals("Primary") ? 0 : 5;
            }
            if (info.Ticks > 100)
            {
                temp += info.Skill.Equals("Primary") ? 10 : 0;
            }
            values[3] = temp;
        }
        if (info.Speed != null)
        {
            int temp = 0;
            if (info.Ticks > 10)
            {
                temp += info.Speed.Equals("Primary") ? 10 : 5;
            }
            if (info.Ticks > 20)
            {
                temp += info.Speed.Equals("Primary") ? 0 : 5;
            }
            if (info.Ticks > 40)
            {
                temp += info.Speed.Equals("Primary") ? 10 : 0;
            }
            if (info.Ticks > 70)
            {
                temp += info.Speed.Equals("Primary") ? 0 : 5;
            }
            if (info.Ticks > 100)
            {
                temp += info.Speed.Equals("Primary") ? 10 : 0;
            }
            values[4] = temp;
        }
        if (info.Favor != null)
        {
            int temp = 0;
            if (info.Ticks > 10)
            {
                temp += info.Favor.Equals("Primary") ? 10 : 5;
            }
            if (info.Ticks > 20)
            {
                temp += info.Favor.Equals("Primary") ? 0 : 5;
            }
            if (info.Ticks > 40)
            {
                temp += info.Favor.Equals("Primary") ? 10 : 0;
            }
            if (info.Ticks > 70)
            {
                temp += info.Favor.Equals("Primary") ? 0 : 5;
            }
            if (info.Ticks > 100)
            {
                temp += info.Favor.Equals("Primary") ? 10 : 0;
            }
            values[5] = temp;
        }
        if (info.Armor != null)
        {
            int temp = 0;
            if (info.Ticks > 10)
            {
                temp += info.Armor.Equals("Primary") ? 10 : 5;
            }
            if (info.Ticks > 20)
            {
                temp += info.Armor.Equals("Primary") ? 0 : 5;
            }
            if (info.Ticks > 40)
            {
                temp += info.Armor.Equals("Primary") ? 10 : 0;
            }
            if (info.Ticks > 70)
            {
                temp += info.Armor.Equals("Primary") ? 0 : 5;
            }
            if (info.Ticks > 100)
            {
                temp += info.Armor.Equals("Primary") ? 10 : 0;
            }
            values[6] = temp;
        }
        if (info.Ward != null)
        {
            int temp = 0;
            if (info.Ticks > 10)
            {
                temp += info.Ward.Equals("Primary") ? 10 : 5;
            }
            if (info.Ticks > 20)
            {
                temp += info.Ward.Equals("Primary") ? 0 : 5;
            }
            if (info.Ticks > 40)
            {
                temp += info.Ward.Equals("Primary") ? 10 : 0;
            }
            if (info.Ticks > 70)
            {
                temp += info.Ward.Equals("Primary") ? 0 : 5;
            }
            if (info.Ticks > 100)
            {
                temp += info.Ward.Equals("Primary") ? 10 : 0;
            }
            values[7] = temp;
        }
        return values;
    }

    public static int GetRankFromTicks(int ticks)
    {
        int temp = 0;
        if(ticks > 10)
        {
            temp++;
        }
        if (ticks > 20)
        {
            temp++;
        }
        if (ticks > 40)
        {
            temp++;
        }
        if (ticks > 70)
        {
            temp++;
        }
        if (ticks > 100)
        {
            temp++;
        }
        return temp;
    }

    public static int GetTicksFromRank(int rank)
    {
        int temp = 0;
        switch (rank)
        {
            case 1:
                temp = 10;
                break;
            case 2:
                temp = 20;
                break;
            case 3:
                temp = 40;
                break;
            case 4:
                temp = 70;
                break;
            case 5:
                temp = 100;
                break;
        }
        return temp;
    }

    public static List<string> GetSupportList(string person) //uses the revised CSV helper wording.  this should be the new standard.
    {
        List<string> supports = new List<string>();

        //read in from Supports
        StreamReader sr = new StreamReader(Application.dataPath + "/DataFiles/Supports.csv");
        CsvReader csv = new CsvReader(sr);
        csv.Read();
        csv.ReadHeader();

        while(csv.Read())
        {
            string user = csv.GetField<string>("Primary");
            string friend = csv.GetField<string>("Secondary");
            if (user.Equals(person) && friend != "")//if the user matches and the friend column exists
            {
                supports.Add(friend);
            }
        }

        csv.Dispose();

        return supports;
    }

    public static Support GetSupportInfo(string user, string friend)
    {
        //set up the Support object
        GrowthManager gm = GameObject.Find(user).GetComponent<GrowthManager>();
        Support info = new Support
        {
            User = user,
            Friend = friend,
            Ticks = gm.growthData.ticks[gm.growthData.friends.IndexOf(friend)]
        };

        //read in from Supports
        StreamReader sr = new StreamReader(Application.dataPath + "/DataFiles/Supports.csv");
        CsvReader csv = new CsvReader(sr);
        csv.Read();
        csv.ReadHeader();

        while (csv.Read())
        {
            string col1 = csv.GetField<string>("Primary");
            string col2 = csv.GetField<string>("Secondary");
            if (col1.Equals(user) && col2.Equals(friend))//if the user and friend column both match
            {
                info.HP = csv.GetField<string>("HP");
                info.Might = csv.GetField<string>("Might");
                info.Focus = csv.GetField<string>("Focus");
                info.Skill = csv.GetField<string>("Skill");
                info.Speed = csv.GetField<string>("Speed");
                info.Favor = csv.GetField<string>("Favor");
                info.Armor = csv.GetField<string>("Armor");
                info.Ward = csv.GetField<string>("Ward");
            }
        }

        //now we finish up
        csv.Dispose();
        
        return info;
    }

    public static List<string> GetTalentList(string charname)
    {
        //read in from NaturalTalents to get talent lists
        List<string> info = new List<string>();
        //import from csv
        StreamReader sr = new StreamReader(Application.dataPath + "/DataFiles/NaturalTalents.csv");
        CsvParser parser = new CsvParser(sr);
        string[] record;
        do
        {
            try //attempt to read a new record
            {
                record = parser.Read();
                if (record[0].Equals(charname)) //compare the name to passed in value
                {
                    for(int i=1; i<record.Length; i++) //if the name matches, iterate through each value in the line
                    {
                        string temp = record[i];
                        //if the value is separated by commas, add each value
                        if(temp.Split(',').Length > 1)
                        {
                            foreach(string splitter in temp.Split(','))
                            {
                                info.Add(splitter.Trim());
                            }
                        } //otherwise, we just add the value normally
                        else if (!temp.Equals(""))
                        {
                            info.Add(temp);
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
                break; //on the off chance it doesn't work, break out so the code doesn't crash
            }
        } while (info.ToArray().Length < 1); //go until we find the right item


        parser.Dispose();


        return info;
    }

    public static Talent GetTalentInfo(string tname) //clone from GetItemInfo
    {
        Talent info = new Talent();

        //import from csv
        StreamReader sr = new StreamReader(Application.dataPath + "/DataFiles/Talents.csv");
        CsvParser parser = new CsvParser(sr);
        string[] temp;
        do
        {

            try //attempt to read a new record
            {
                temp = parser.Read();
                if (temp[0].Equals(tname)) //compare the record to passed in value
                {
                    info.TalentName = temp[0];
                    info.Prerequisites = new List<string>(temp[1].Split(','));
                    info.Cost = int.Parse(temp[2]);
                    info.Description = temp[3];
                    info.Tags = new List<string>(temp[4].Split(','));
                    info.Notes = new List<string>(temp[5].Split(','));
                    info.Effects = new List<string>(temp[6].Split(','));
                    //native currently doesn't have a value
                }
            }
            catch(System.Exception e)
            {
                Debug.Log(e);
                break; //on the off chance it doesn't work, break out so the code doesn't crash
            }
        } while (info.TalentName == null); //go until we find the right item


        parser.Dispose();

        return info;
    }

    public void CreateItems() //charges need fixing
    {
        StreamReader reader = new StreamReader(Application.dataPath + "/DataFiles/Items.csv");
        CsvReader csv = new CsvReader(reader);
        //csv.Configuration.RegisterClassMap<ItemMap>();

        Item item;

        csv.Read();
        csv.ReadHeader();
        while (csv.Read())
        {
            if(!csv.GetField<string>("Damage").Equals("--"))
            {
                item = new Weapon
                {
                    Damage = csv.GetField<string>("Damage"),
                    Mastery = csv.GetField<int>("Mastery"),
                    Ranged = csv.GetField<string>("Range").Equals("Ranged") ? true : false //ternary operator is cool
                };

            }
            else
            {
                item = new Item();
            }
            item.ItemName = csv.GetField<string>("ItemName");
            item.Type = csv.GetField<string>("Type"); //process this one?
            item.Subtype = csv.GetField<string>("Subtype");
            //item.Charges = csv.GetField<int>("Charges").Equals("--") ? -1 : csv.GetField<int>("Charges");
            item.Attack = csv.GetField<int>("Attack");
            item.Defense = csv.GetField<int>("Defense");
            item.Followup = csv.GetField<int>("Followup");
            item.Accuracy = csv.GetField<int>("Accuracy");
            item.Evasion = csv.GetField<int>("Evasion");
            item.Critical = csv.GetField<int>("Critical");
            item.Guard = csv.GetField<int>("Guard");
            item.Description = csv.GetField<string>("Description");
            item.Effects = csv.GetField<string>("Effects").Split(',');
            item.Cost = csv.GetField<int>("Cost");

            AssetDatabase.CreateAsset(item, "Assets/DataFiles/Items/" + item.ItemName + ".asset");
            EditorUtility.SetDirty(item);

        }

    }

    public static Item GetItemInfo(string iname) //needs to be updated with actual data values
    {
        Item info = new Item();

        //import from csv
        StreamReader sr = new StreamReader(Application.dataPath + "/DataFiles/Items.csv");
        CsvParser parser = new CsvParser(sr);
        string[] temp;
        do
        {
            try //attempt to read a new record
            {
                temp = parser.Read();
                if (temp[0].Equals(iname)) //compare the record to passed in value
                {
                    info.ItemName = temp[0]; //if true, set info's values
                    info.ItemName = temp[1];
                    info.ItemName = temp[2];
                    info.ItemName = temp[0];
                    info.ItemName = temp[0];
                    info.ItemName = temp[0];
                    info.ItemName = temp[0];
                    info.ItemName = temp[0];
                    info.ItemName = temp[0];
                    info.ItemName = temp[0];
                    info.ItemName = temp[0];
                    info.ItemName = temp[0];
                }
            }
            catch
            {
                break; //on the off chance it doesn't work, break out so the code doesn't crash
            }
        } while (info.ItemName == null); //go until we find the right item


        parser.Dispose();

        return info;
    }


    public static Weapon GetWeaponInfo(string iname)
    {
        Weapon info = new Weapon();

        //import from csv
        StreamReader sr = new StreamReader(Application.dataPath + "/DataFiles/Items.csv");
        CsvParser parser = new CsvParser(sr);
        string[] temp;
        do
        {
            try //attempt to read a new record
            {
                temp = parser.Read();
                if (temp[0].Equals(iname)) //compare the record to passed in value
                {
                    info.ItemName = temp[0]; //if true, set info's values
                    info.ItemName = temp[1];
                    info.ItemName = temp[2];
                    info.ItemName = temp[0];
                    info.ItemName = temp[0];
                    info.ItemName = temp[0];
                    info.ItemName = temp[0];
                    info.ItemName = temp[0];
                    info.ItemName = temp[0];
                    info.ItemName = temp[0];
                    info.ItemName = temp[0];
                    info.ItemName = temp[0];
                }
            }
            catch
            {
                break; //on the off chance it doesn't work, break out so the code doesn't crash
            }
        } while (info.ItemName == null); //go until we find the right item


        parser.Dispose();

        return info;
    }

    //    

    /*
     * 
    public void CreateWeapons()
    {
        //create streamreaders to get the data
        StreamReader sr = new StreamReader(Application.dataPath + "/DataFiles/Weapons.csv");
        CsvParser parser = new CsvParser(sr);
        string[] temp = parser.Read();

        Weapon weapon;
        for (int i = 0; i < 21; i++)
        {
            temp = parser.Read();
            weapon = ScriptableObject.CreateInstance<Weapon>();  //so we'll probably have to save the Manager classes

            weapon.ItemName = temp[0];
            weapon.Type = temp[1];
            weapon.Mastery = int.Parse(temp[2]);
            string[] stats = temp[3].Split(',');
            foreach(string stat in stats)
            {
                int plus = stat.IndexOf("+");
                plus++;
                switch (stat.Substring(0, stat.IndexOf(" ")))
                {
                    case "Attack":
                        //it's fetching the + in this as well
                        weapon.statMods[0] = int.Parse(stat.Substring(stat.Length - 1, 1));
                        break;
                    case "Defense":
                        weapon.statMods[1] = int.Parse(stat.Substring(stat.Length - 1, 1));
                        break;
                    case "Followup":
                        weapon.statMods[2] = int.Parse(stat.Substring(stat.Length - 1, 1));
                        break;
                    case "Accuracy":
                        weapon.statMods[3] = int.Parse(stat.Substring(stat.Length - 1, 1));
                        break;
                    case "Evasion":
                        weapon.statMods[4] = int.Parse(stat.Substring(stat.Length - 1, 1));
                        break;
                    case "Critical":
                        weapon.statMods[5] = int.Parse(stat.Substring(stat.Length - 1, 1));
                        break;

                    case "Guard":
                        weapon.statMods[6] = int.Parse(stat.Substring(stat.Length - 1, 1));
                        break;
                        //weapon.statMods[5] = int.Parse(stat.Substring(stat.IndexOf("+"), stat.Length - stat.IndexOf("+") - 1));
                }
            }
            //set range
            weapon.description = temp[5];
            weapon.Cost = int.Parse(temp[6]);

            AssetDatabase.CreateAsset(weapon, "Assets/DataFiles/Items/" + temp[0] + ".asset");
            EditorUtility.SetDirty(weapon);
        }
        parser.Dispose();
    }
     * 
     * 
     * */
}

public sealed class ItemMap : ClassMap<Item>
{
    public ItemMap()
    {
        Map(m => m.ItemName);
        Map(m => m.Type);
        Map(m => m.Subtype);
        Map(m => m.Charges);
        Map(m => m.Attack);
        Map(m => m.Defense);
        Map(m => m.Followup);
        Map(m => m.Accuracy);
        Map(m => m.Evasion);
        Map(m => m.Critical);
        Map(m => m.Guard);
        Map(m => m.Description);
        Map(m => m.Cost);
    }
}
