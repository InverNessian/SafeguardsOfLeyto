using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Talent
{
    public string talentName;
    public string effect;
    public int cost;
    public bool native; //this allows us to know if it's learned from other units.
    public string extraInfo;
    public List<string> tags;
    public List<string> prerequisites;

}
