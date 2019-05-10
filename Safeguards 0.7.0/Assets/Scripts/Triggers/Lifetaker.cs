using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetaker : AbilityTrigger, ICombat, IConditional
{
    public void Accuracy(AttackData combat)
    {
        
    }

    public void AttackStepBonus(AttackData combat)
    {

    }

    public void AttackStepMultiplier(AttackData combat)
    {

    }

    public void BeginAttack(AttackData combat)
    {
        combat.OnHitEffects.Add("Lifetaker");
    }

    public bool CheckCondition(StatsManager user = null, StatsManager opponent = null, int value = -1)
    {
        if(value <= 0 && !opponent.statsData.CheckTalents("Nihil"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CombatEnd(AttackData combat)
    {

    }

    public void Critical(AttackData combat)
    {

    }

    public void DamageStepBonus(AttackData combat)
    {

    }

    public void DamageStepMultiplier(AttackData combat)
    {

    }

    public void DefenseStepBonus(AttackData combat)
    {

    }

    public void DefenseStepMultiplier(AttackData combat)
    {

    }

    public void Evasion(AttackData combat)
    {

    }

    public void FindAdvantage(AttackData combat)
    {

    }

    public void Guard(AttackData combat)
    {

    }

    public void OnHitEffect(AttackData combat)
    {
        HealthManager target = GameObject.Find(combat.defender).GetComponent<HealthManager>();
        HealthManager user = GameObject.Find(combat.attacker).GetComponent<HealthManager>();
        if(CheckCondition(GameObject.Find(combat.defender).GetComponent<StatsManager>(), value:target.CheckHP())) //named parameters!!
        {
            user.HealDamage((int)Mathf.Ceil((float)(target.GetComponentInParent<StatsManager>().statsData.healthValue * 0.1)));
        }
        Debug.Log("lifetaker!");

        //alternatively, maybe we use this to set a listener for the target's DownEvent?
    }

    public void TakeDamage(AttackData combat)
    {

    }

    public override void UndoTrigger()
    {
        throw new System.NotImplementedException();
    }

}
