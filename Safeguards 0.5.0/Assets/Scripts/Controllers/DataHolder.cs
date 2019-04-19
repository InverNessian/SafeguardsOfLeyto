using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class DataHolder : MonoBehaviour
{
    public UnitData unitData;
    //somehow we need a way to get individual unit HP for enemies.

    // Start is called before the first frame update
    void Awake()
    {
        if(unitData == null)
        {
            //for the most part, we can have enemies already created in the scene, so that they won't need to be created.
            //this means we can assume that only Characters will reach this point with a null value for unitData
            //and, since Characters will have their objects in the scene as well, we can just not put a stat block in there for anyone who is returning.
            //LoadData(gameObject.name);
            //characters will always have their object named after them.

            //for best design practicies, the starter stats object for each character should only be in the first mission where they appear
        }
    }

    //Unity automatically serializes scriptable objects, so we don't even need to do this.
    //each character will always have their stats attached.
    //this is actually easy.


    /*
    private void LoadData(string id)
    {
        //check in the persistent data path for the file name based on id
        if (File.Exists(Application.persistentDataPath + "/" + gameObject.name + "_data.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + unitData.unit_name + "_data.dat", FileMode.Open);
            
            unitData = (UnitData)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            throw new Exception("Data for " + gameObject.name + " not found!");
        }
    }

    public void SaveData() //maybe we should set this as a listener to the "GameQuit" Event
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/chars/" + unitData.unit_name + "_data.dat", FileMode.Open);
        bf.Serialize(file, unitData);
        file.Close();
    }
    */
}
