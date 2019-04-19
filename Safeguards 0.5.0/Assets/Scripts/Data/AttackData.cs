using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackData
{
    public string attacker = "";
    public string defender = "";
    public bool executed = false;
    public bool hit = false;
    public bool crit = false; //these are for logging purposes.

    public int HitRate;
    public int CriticalRate;
    public int GuardRate;
    public int DamageDealt;
    public string[] OnHitEffects;

    public override string ToString()
    {
        string temp = "Attacker: " + attacker + ", Hit Rate: " + HitRate + ", Damage: " + DamageDealt;
        return temp;
    }
}
