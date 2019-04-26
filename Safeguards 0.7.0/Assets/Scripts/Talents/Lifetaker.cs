using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetaker : TalentTrigger, ICombat
{
    public int Accuracy(AttackData combat)
    {
        throw new System.NotImplementedException();
    }

    public int AttackStepBonus(AttackData combat)
    {
        throw new System.NotImplementedException();
    }

    public double AttackStepMultiplier(AttackData combat)
    {
        throw new System.NotImplementedException();
    }

    public void BeginAttack(AttackData combat)
    {
        combat.OnHitEffects.Add("Lifetaker");
    }

    public void CombatEnd(AttackData combat)
    {
        throw new System.NotImplementedException();
    }

    public int Critical(AttackData combat)
    {
        throw new System.NotImplementedException();
    }

    public int DamageStepBonus(AttackData combat)
    {
        throw new System.NotImplementedException();
    }

    public double DamageStepMultiplier(AttackData combat)
    {
        throw new System.NotImplementedException();
    }

    public int DefenseStepBonus(AttackData combat)
    {
        throw new System.NotImplementedException();
    }

    public double DefenseStepMultiplier(AttackData combat)
    {
        throw new System.NotImplementedException();
    }

    public int Evasion(AttackData combat)
    {
        throw new System.NotImplementedException();
    }

    public double FindAdvantage(AttackData combat)
    {
        throw new System.NotImplementedException();
    }

    public int Guard(AttackData combat)
    {
        throw new System.NotImplementedException();
    }

    public void OnHitEffect(AttackData combat)
    {
        HealthManager target = GameObject.Find(combat.defender).GetComponent<HealthManager>();
        HealthManager user = GameObject.Find(combat.attacker).GetComponent<HealthManager>();
        if(target.CheckHP() <= 0)
        {
            user.HealDamage((int)Mathf.Ceil((float)(target.GetComponentInParent<StatsManager>().statsData.healthValue * 0.1)));
        }
        Debug.Log("lifetaker!");
    }

    public void TakeDamage(AttackData combat)
    {
        throw new System.NotImplementedException();
    }

    public override void UndoTrigger()
    {
        throw new System.NotImplementedException();
    }

}
