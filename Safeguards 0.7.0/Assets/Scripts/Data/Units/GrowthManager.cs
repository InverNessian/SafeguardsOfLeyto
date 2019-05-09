using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GrowthManager : MonoBehaviour
{
    public GrowthData growthData;


    private void OnEnable()
    {
        GameController.BeginScene.dynamicCalls += StartScene;
        GameController.EndScene.dynamicCalls += EndScene;
    }


    void StartScene()
    {
        if(growthData == null)
        {
            //load from memory
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + growthData.displayName + "_growths.dat", FileMode.Open);

            GrowthSaver growthSaver = (GrowthSaver)bf.Deserialize(file);
            file.Close();

            growthData = growthSaver.GetGrowthData();
        }
    }


    public void EndScene()
    {
        //write Data to memory
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + growthData.displayName + "_growths.dat");

        GrowthSaver growthSaver = new GrowthSaver(growthData);

        bf.Serialize(file, growthSaver);
        file.Close();

        Debug.Log(growthSaver);
    }
}

//wrapper class
[System.Serializable]
public struct GrowthSaver
{
    [SerializeField]
    private GrowthData gd;

    public GrowthSaver(GrowthData growthData)
    {
        gd = growthData;
    }
    
    public GrowthData GetGrowthData()
    {
        return gd;
    }
}
