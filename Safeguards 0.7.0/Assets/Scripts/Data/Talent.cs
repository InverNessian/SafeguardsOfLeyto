using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Talent
{
    public string TalentName;
    public List<string> Prerequisites;
    public int Cost;
    public string Description;
    public List<string> Tags;
    public List<string> Notes;
    public List<string> Effects;
    public bool native; //this allows us to know if it's learned from other units.

}
