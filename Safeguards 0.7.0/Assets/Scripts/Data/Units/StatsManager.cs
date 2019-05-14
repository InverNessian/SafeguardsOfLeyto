using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public StatsData statsData;

    private void OnEnable()
    {
        GameController.EndScene.dynamicCalls += EndScene;
    }

    void Start() //used to be StartScene, but this honestly seems easier
    {
        //by using editor design to our advantage, we can control when this needs to attempt to load data.
        // all we have to do is not assign a value to statsData for characters who need to load data.
        if (statsData == null)
        {
            //load from memory
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + statsData.displayName + "_stats.dat", FileMode.Open);

            StatSaver growthSaver = (StatSaver)bf.Deserialize(file);
            file.Close();

            statsData = growthSaver.GetStatsData();
        }

        //then add trigger components to gameObject based on statsData.talents
        foreach(string talent in statsData.talents)
        {
            try //as long as we keep the names of talents the same as their trigger classes, we can do a GetType for easy matching.
            { //this method is tested and works, just make sure you call StartScene
                gameObject.AddComponent(Type.GetType(talent));
            }
            catch(TypeLoadException TLE)
            {
                Debug.Log("No trigger exists for " + talent + ": " + TLE);
            }
        }
    }

    //add a method that will transfer over info from a mission to a non-mission?
    //or should it just save, then load every time?
    //that might be more consistent to just have a standard data flow

    public void EndScene()
    {
        //write statsData to memory
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + statsData.displayName + "_stats.dat");

        StatSaver statSaver = new StatSaver(statsData);

        bf.Serialize(file, statSaver);
        file.Close();

        Debug.Log(statSaver);

        GameController.EndScene.dynamicCalls -= EndScene; //then we remove this from the dynamic call list
    }
}

//wrapper class
[System.Serializable]
public struct StatSaver
{
    [SerializeField]
    private StatsData sd;

    public StatSaver(StatsData statsData)
    {
        sd = statsData;
    }

    public StatsData GetStatsData()
    {
        return sd;
    }
}
