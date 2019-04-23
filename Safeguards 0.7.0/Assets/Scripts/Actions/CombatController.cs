using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    //public static event DamageDealt;
    //this event has two parameters, the person taking damage and the damage dealer (gameObjects)

    void Start()
    {
        UnitController.mDuelCombat.AddListener(DuelCombat);
    }
    //do we want to have events just for combat?  might make things easier since we could have each special combat really just call some Events.
        //these could be non-static events!
        //I think, considering the issues with deriveAttack, etc, this will need to be the solution.  it's just too awkward to have these methods with split info.
    
    
    public void DuelCombat(GameObject attacker, GameObject defender)
    {
        StatsData atkr = attacker.GetComponent<StatsManager>().statsData;
        StatsData dfndr = attacker.GetComponent<StatsManager>().statsData;


        List<AttackData> attacks = new List<AttackData>();
        attacks.Add(GenerateHit(attacker, defender, true));
        attacks.Add(GenerateHit(defender, attacker, true)); //we'll have to add some conditional checks here, or do an event system or something.  this is a temp measure.
        if(atkr.DeriveFollowup() - dfndr.DeriveFollowup() >= 5)
        {
            attacks.Add(GenerateHit(attacker, defender, true));
        }
        if (dfndr.DeriveFollowup() - atkr.DeriveFollowup() >= 5)
        {
            attacks.Add(GenerateHit(defender, attacker, true));
        }

        foreach(AttackData ad in attacks)
        {
            Debug.Log(ad.ToString()); //once UI is up, have this display attacks in the UI.
        }

        //then 
    }

    public void DoCombat(UnitData attacker, UnitData defender)
    {
        throw new System.NotImplementedException();
    }

    public AttackData GenerateHit(GameObject attacker, GameObject defender, bool preview)
    {
        StatsData atkr = attacker.GetComponent<StatsManager>().statsData;
        StatsData dfndr = attacker.GetComponent<StatsManager>().statsData;
        AttackData attackData = new AttackData();
        attackData.attacker = attacker.name;
        attackData.HitRate = 60 + atkr.DeriveAccuracy() - dfndr.DeriveEvasion();
        attackData.CriticalRate = atkr.DeriveCritical() - dfndr.DeriveGuard();
        attackData.DamageDealt = atkr.DeriveAttack(GetWeapon(attacker)) - dfndr.DeriveDefense(GetWeapon(attacker));
        //update this to interact with Dual Wield later on.

        if (!preview)
        {
            //roll RNG and then Invoke(DamageDealt(dfndr, atkr))
        }
        return attackData;
    }

    private Weapon GetWeapon(GameObject guy)
    {
        Equip equipped = guy.GetComponent<Equip>();
        if (equipped.item)
        {
            //check for weapon 
        }
        return (Weapon)equipped.item;
    }

}
