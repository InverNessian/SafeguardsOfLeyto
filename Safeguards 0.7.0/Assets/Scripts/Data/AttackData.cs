using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData //make this a struct?
{
    public string attacker = "";
    public string defender = "";
    public bool executed = false;
    public bool hit = false;
    public bool crit = false; //these are for logging purposes.
    public bool guard = false;
    public double advantage = 1.0;

    public int HitRate;
    public int CriticalRate; //set this to a negative value to represent Guard chance.
    public int DamageDealt;
    public List<string> OnHitEffects;

    public AttackData()
    {
        OnHitEffects = new List<string>();
        HitRate = 60;
        CriticalRate = 0;
        DamageDealt = 0;
    }

    public override string ToString()
    {
        string temp = "Attacker: " + attacker + ", Hit Rate: " + HitRate + ", Damage: " + DamageDealt;
        return temp;
    }
}
