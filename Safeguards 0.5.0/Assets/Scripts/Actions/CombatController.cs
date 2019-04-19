using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    void Start()
    {
        UnitController.m_testEvent.AddListener(TriggerAction);
    }

    public void TriggerAction(UnitData atkr, UnitData dfndr)
    {
        //call preview attack, which creates the UI button to proceed into DoCombat
        //UnitData atkr = GameObject.Find(UnitController.selected).GetComponent<DataHolder>().unitData;
        //UnitData dfndr = GameObject.Find(target).GetComponent<DataHolder>().unitData;
        PreviewCombat(atkr, dfndr);
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

        foreach(AttackData ad in attacks)
        {
            Debug.Log(ad.ToString());
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
        attackData.attacker = attacker.unit_name;
        //etc

        if (!preview)
        {
            //roll RNG and call defender.takeDamage(dmg)
        }
        return attackData;
    }
}
