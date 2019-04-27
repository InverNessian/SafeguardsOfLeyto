using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class DisplayStats : MonoBehaviour
{
    public StatsData stats;
    public GrowthData growths;
    // Start is called before the first frame update
    void Start()
    {
        UpdateDisplay();
    }

    public void ChangeTarget()
    {
        string unit = GameObject.Find("SelectLabel").GetComponent<Text>().text;
        //string guid = AssetDatabase.AssetPathToGUID("Assets/DataFiles/New Units/" + unit + " Growths.asset");
        stats = AssetDatabase.LoadAssetAtPath<StatsData>("Assets/DataFiles/New Units/" + unit + " Stats.asset");
        growths = AssetDatabase.LoadAssetAtPath<GrowthData>("Assets/DataFiles/New Units/" + unit + " Growths.asset");
        Debug.Log(growths.talentPoints);
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {

        gameObject.GetComponent<Text>().text = "hp: " + stats.healthValue;
    }
}
