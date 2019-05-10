using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Talent
{
    public string TalentName;
    public List<string> Prerequisites;
    public int Cost;
    public string Description;
    public List<string> Tags;
    public List<string> Notes;
    public bool native; //this allows us to know if it's learned from other units.
    public List<string> Effects;

}
