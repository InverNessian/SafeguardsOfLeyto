using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData
{
    public bool executed = false;
    public bool hit = false;
    public bool crit = false; //these are for logging purposes.
    public int HitRate;
    public int CriticalRate;
    public int GuardRate;
    public int DamageDealt;
    public string[] OnHitEffects;
}
