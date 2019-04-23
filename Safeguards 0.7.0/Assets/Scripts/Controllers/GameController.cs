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
    public static UnitController unitController;

    public int active_player = 0;
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

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 30), "Active Player: " + active_player);

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


    /* Code used to create the UnitData asset objects and read from CSV
     * 
     * 
     * 
     * 
     * 
     */

    //basically we need to make a new folder in the asset menu and then fill it with Character objects and a SaveState


    public UnitData modify;
    public Save_State CreateNewSaveFile()
    {
        
        StreamReader sr = new StreamReader(Application.dataPath + "/DataFiles/Bases.csv");
        CsvParser parser = new CsvParser(sr);
        string[] temp = parser.Read();

        StreamReader sr2 = new StreamReader(Application.dataPath + "/DataFiles/Growths.csv");
        CsvParser parser2 = new CsvParser(sr2);
        string[] temp2 = parser2.Read();

        //string[] guids = AssetDatabase.FindAssets("t:UnitData", new[] { "Assets/DataFiles/Characters" });

        Debug.Log(temp2);
        for (int i = 0; i < 21; i++)
        {
            temp = parser.Read();
            temp2 = parser2.Read();
            modify = ScriptableObject.CreateInstance<UnitData>();
            Debug.Log(modify);
            //modify = (UnitData)AssetDatabase.LoadAssetAtPath(guids[i], typeof(UnitData));

            modify.unit_name = temp[0];
            modify.health[0] = int.Parse(temp[1]);
            modify.health[1] = int.Parse(temp[1]);
            modify.might[1] = int.Parse(temp[2]);
            modify.focus[1] = int.Parse(temp[3]);
            modify.skill[1] = int.Parse(temp[4]);
            modify.speed[1] = int.Parse(temp[5]);
            modify.favor[1] = int.Parse(temp[6]);
            modify.armor[1] = int.Parse(temp[7]);
            modify.ward[1] = int.Parse(temp[8]);

            modify.health[2] = int.Parse(temp2[1]);
            modify.might[2] = int.Parse(temp2[2]);
            modify.focus[2] = int.Parse(temp2[3]);
            modify.skill[2] = int.Parse(temp2[4]);
            modify.speed[2] = int.Parse(temp2[5]);
            modify.favor[2] = int.Parse(temp2[6]);
            modify.armor[2] = int.Parse(temp2[7]);
            modify.ward[2] = int.Parse(temp2[8]);

            AssetDatabase.CreateAsset(modify, "Assets/DataFiles/Characters/" + temp[0] + ".asset");
            EditorUtility.SetDirty(modify);
        }
        parser.Dispose();
        parser2.Dispose();
        //Debug.Log(parser.Read());
        //EditorUtility.SetDirty(save_file); //this is necessary to force the Editor to see that this has changed.

        return new Save_State(); 
    }
}
