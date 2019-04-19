using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    void Start()
    {
        UnitController.mDuelCombat.AddListener(DuelCombat);
    }
    //do we want to have events just for combat?  might make things easier since we could have each special combat really just call some Events.
    
    
    public void DuelCombat(UnitData attacker, UnitData defender)
    {
        List<AttackData> attacks = new List<AttackData>();
        attacks.Add(GenerateHit(attacker, defender, true));
        attacks.Add(GenerateHit(defender, attacker, true)); //we'll have to add some conditional checks here, or do an event system or something.  this is a temp measure.
        if(attacker.DeriveFollowup() - defender.DeriveFollowup() >= 5)
        {
            attacks.Add(GenerateHit(attacker, defender, true));
        }
        if (defender.DeriveFollowup() - attacker.DeriveFollowup() >= 5)
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

    public AttackData GenerateHit(UnitData attacker, UnitData defender, bool preview)
    {
        AttackData attackData = new AttackData();
        attackData.attacker = attacker.unit_name;
        attackData.HitRate = 60 + attacker.DeriveAccuracy() - defender.DeriveEvasion();
        attackData.CriticalRate = attacker.DeriveCritical() - defender.DeriveGuard();
        attackData.DamageDealt = attacker.DeriveAttack(attacker.weapon) - defender.DeriveDefense(attacker.weapon);
        //update this to interact with Dual Wield later on.

        if (!preview)
        {
            //roll RNG and call defender.takeDamage(dmg)
        }
        return attackData;
    }
}
