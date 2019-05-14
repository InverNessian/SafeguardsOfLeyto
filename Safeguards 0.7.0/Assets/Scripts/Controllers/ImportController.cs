using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.IO;
using UnityEditor;
using System.Text;
using CsvHelper.Configuration;

public class ImportController : MonoBehaviour
{

    public static StringBuilder GetEventDialogue(string ename)
    {
        //check the list of events & supports for matching name, then return dialogue
        StringBuilder sb = new StringBuilder();
        return sb;
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
            catch
            {
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
