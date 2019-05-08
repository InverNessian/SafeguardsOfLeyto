using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.IO;
using UnityEditor;
using System.Text;

public class ImportController : MonoBehaviour
{

    public static StringBuilder GetEventDialogue(string ename)
    {
        //check the list of events & supports for matching name, then return dialogue
        StringBuilder sb = new StringBuilder();
        return sb;
    }

    public static Talent GetTalentInfo(string tname) //clone this for items after its done
    {
        Talent info = new Talent();

        //import from csv
        StreamReader sr = new StreamReader(Application.dataPath + "/DataFiles/Weapons.csv");
        CsvParser parser = new CsvParser(sr);
        string[] temp = parser.Read();

        //iterate on parser until we find the talent with matching name
        //then set info to have its data

        parser.Dispose();

        return info;
    }
}
