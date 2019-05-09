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

    void StartScene()
    {
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
            switch (talent)
            {
                case "Lifetaker":
                    gameObject.AddComponent<Lifetaker>();
                    break;

                    //etc
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
