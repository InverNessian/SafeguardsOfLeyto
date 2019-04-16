using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using CsvHelper;

public class GameController : MonoBehaviour
{
    public static GameController master;

    public int active_player = 0;
    public string selected = "none";
    public Save_State save_file;

    //Awake is called even before Start()
    void Awake()
    {
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
            //check PlayerPrefs for a key of some sort that tells us which Save_State to load here
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 30), "Active Player: " + active_player);

        if(GUI.Button(new Rect(10, 40, 150, 30), "Save and Quit"))
        {
            int temp = 1;
            //Stats_Character temp = GameObject.Find(selected).GetComponent<Data_Manager>().character_data;
            if(temp != 0)
            {
                SaveGameData();
            }
            
        }
        
    }

    public void openScene(string sceneName)
    {
        //basically, it will read in the info about each scene and which characters the player has access to.
            //after it has a list of all the characters it needs, it will loop through and create an instance of the character object for each one.
                //then it will assign some basic info to that object's DataManager component.  This will allow DataManager to handle importing the character's data when it loads.
            //maybe I should store the info in a csv?  that makes it easier to modify after the fact?
    }

    //I need to implement a "Save State" class that will hold all the values for the game such as completion flags and such.
    public void SaveGameData()
    {
        //somewhere in here we need a way to key which save file the player is loading.  something about the main menu maybe?

        /*BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/"+person.unit_name+"_data.dat", FileMode.Open); //playerinfo is the file name

        bf.Serialize(file, person);
        file.Close();*/
    }

    public void LoadGameData()
    {
        
    }
}
