using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName ="Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    public string ItemName;
    public string Type;
    public string Subtype;
    public int Charges;
    public int Attack;
    public int Defense;
    public int Followup;
    public int Accuracy;
    public int Evasion;
    public int Critical;
    public int Guard;
    public string Description;
    public string[] Effects;
    public int Cost;
}
