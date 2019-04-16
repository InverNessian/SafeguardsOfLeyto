using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : Action, IAttack
{

    public override void TriggerAction()
    {
        throw new System.NotImplementedException();
    }
    
    public void PreviewCombat(UnitData attacker, UnitData defender)
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
    }

    public void DoCombat(UnitData attacker, UnitData defender)
    {
        throw new System.NotImplementedException();
    }

    public AttackData GenerateHit(UnitData attacker, UnitData defender, bool preview)
    {
        AttackData attackData = new AttackData();
        attackData.HitRate = 60 + attacker.DeriveAccuracy() - defender.DeriveEvasion();
        //etc

        if (!preview)
        {
            //roll RNG and call defender.takeDamage(dmg)
        }
        return attackData;
    }
}
