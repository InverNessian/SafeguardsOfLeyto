using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.IO;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController master;
    public static InputController unitController;

    public Save_State save_file;
    //private enum character_list : int { Avery, Chalice, Crastos, Dondi, Eve, Faber, Fant, Ghar, Gradio, Hadrian, Jor, Kallen, Lena, Leonardo, Liliane, Litugenos, Lyra, Nora, Saph, Trinity, Vyka };

    //Awake is called even before Start()
    void Awake()
    {
        //Instantiate(selected, new Vector3(0, 0, 0), Quaternion.identity);
        if (master == null)
        {
            DontDestroyOnLoad(gameObject);
            master = this;
        }
        else if(master != this)
        {
            Destroy(gameObject);
        }
        if(save_file == null)
        {
            //when we want to load a save file, we can just create a menu with options to pick from existing .asset SaveStates.
            //this allows us to dynamically create saves as well as dynamically choose which one to load.
        }
    }


    public void OpenScene(int sceneIndex)
    {
        //basically, it will read in the info about each scene and which characters the player has access to.
        //after it has a list of all the characters it needs, it will loop through and create an instance of the character object for each one.
        //then it will assign some basic info to that object's DataManager component.  This will allow DataManager to handle importing the character's data when it loads.
        //maybe I should store the info in a csv?  that makes it easier to modify after the fact?

        //actually, this is much easier now.  we can just put objects of the characters who belong in each scene, into that scene.
        //literally all we're doing is loading the scene.
        SceneManager.LoadScene(sceneIndex);
    }




    //basically we need to make a new folder in the asset menu and then fill it with Character objects and a SaveState

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

            weapon.itemName = temp[0];
            weapon.type = temp[1];
            weapon.mastery = int.Parse(temp[2]);
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
            weapon.effect = temp[5];
            weapon.marketPrice = int.Parse(temp[6]);

            AssetDatabase.CreateAsset(weapon, "Assets/DataFiles/Items/" + temp[0] + ".asset");
            EditorUtility.SetDirty(weapon);
        }
        parser.Dispose();
    }

    public void CreateNewSaveFile()
    {
        //create streamreaders to get the data
        StreamReader sr = new StreamReader(Application.dataPath + "/DataFiles/Bases.csv");
        CsvParser parser = new CsvParser(sr);
        string[] temp = parser.Read();

        StreamReader sr2 = new StreamReader(Application.dataPath + "/DataFiles/Growths.csv");
        CsvParser parser2 = new CsvParser(sr2);
        string[] temp2 = parser2.Read();


        StatsData stats;
        GrowthData growths;
        for (int i = 0; i < 21; i++)
        {
            temp = parser.Read();
            temp2 = parser2.Read();
            stats = ScriptableObject.CreateInstance<StatsData>();  //so we'll probably have to save the Manager classes
            growths = ScriptableObject.CreateInstance<GrowthData>(); //they can serve as wrappers
            //create instances of our data files and iterate over them

            stats.displayName = temp[0];
            stats.healthValue = int.Parse(temp[1]);
            stats.mightValue = int.Parse(temp[2]);
            stats.focusValue = int.Parse(temp[3]);
            stats.skillValue = int.Parse(temp[4]);
            stats.speedValue = int.Parse(temp[5]);
            stats.favorValue = int.Parse(temp[6]);
            stats.armorValue = int.Parse(temp[7]);
            stats.wardValue = int.Parse(temp[8]);

            growths.healthGrowth = int.Parse(temp2[1]);
            growths.mightGrowth = int.Parse(temp2[2]);
            growths.focusGrowth = int.Parse(temp2[3]);
            growths.skillGrowth = int.Parse(temp2[4]);
            growths.speedGrowth = int.Parse(temp2[5]);
            growths.favorGrowth = int.Parse(temp2[6]);
            growths.armorGrowth = int.Parse(temp2[7]);
            growths.wardGrowth = int.Parse(temp2[8]);

            //add them as asset files and set dirty
            AssetDatabase.CreateAsset(stats, "Assets/DataFiles/Units/" + temp[0] + " Stats.asset");
            AssetDatabase.CreateAsset(growths, "Assets/DataFiles/Units/" + temp[0] + " Growths.asset");
            EditorUtility.SetDirty(stats);
            EditorUtility.SetDirty(growths);

            //ideally we'd like to have a way to generate a new folder in here.


            //so apparently this actually doesn't work.  ugh.
        }
        parser.Dispose();
        parser2.Dispose();

    }



    /* Code used to create the UnitData asset objects and read from CSV
     * 
     * 
     * 
     * GUI.Label(new Rect(10, 10, 100, 30), "Active Player: " + active_player);

        if (GUI.Button(new Rect(10, 40, 150, 30), "Save and Quit"))
        {
            int temp = 1;
            if(temp != 0)
            {
                save_file.test = "blah";
                save_file.id = "blue";
                save_file.bonusEXP = temp;
                EditorUtility.SetDirty(save_file); //this is necessary to force the Editor to see that this has changed.
            }
            
        }
     * 
     */
}
