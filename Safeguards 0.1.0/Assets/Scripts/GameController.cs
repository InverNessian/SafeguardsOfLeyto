using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour
{
    public static GameController master;

    public int active_player = 0;
    public string selected = "none";

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
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 30), "Active Player: " + active_player);

        if(GUI.Button(new Rect(10, 40, 150, 30), "Save Selected Character"))
        {
            Stats_Character temp = GameObject.Find(selected).GetComponent<Data_Manager>().character_data;
            if(temp != null)
            {
                SaveData(temp);
            }
            
        }
        if (GUI.Button(new Rect(10, 70, 150, 30), "Load Selected Character"))
        {
            Stats_Character temp = LoadData(selected);
            if (temp != null)
            {
                GameObject.Find(selected).GetComponent<Data_Manager>().character_data = temp;
            }

        }
    }

    public void SaveData(Stats_Character person)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/"+person.unit_name+"_data.dat", FileMode.Open); //playerinfo is the file name

        bf.Serialize(file, person);
        file.Close();
    }

    public Stats_Character LoadData(string name)
    {
        if(File.Exists(Application.persistentDataPath + "/" + name + "_data.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + name + "_data.dat", FileMode.Open); //playerinfo is the file name

            Stats_Character person = (Stats_Character)bf.Deserialize(file);
            file.Close();
            return person;
        }
        else
        {
            return null;
        }
    }
}
