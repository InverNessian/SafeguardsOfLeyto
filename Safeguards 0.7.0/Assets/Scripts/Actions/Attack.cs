using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackAction : Action
{

    //data
    public StatsManager initiator;
    public StatsManager receiver;
    public List<AttackData> attacks; // the most recent attack should be the last in the list.
    public AttackData recent;

    //delegates
    public delegate void TriggerCombatEffect(AttackData combat);
    public delegate int TriggerCombatBonus(AttackData combat);
    public delegate double TriggerCombatMultiplier(AttackData combat);

    TriggerCombatEffect BeginAttack; //maybe use this to add to the attackData's list of onhit strings, but not actual stuff
    TriggerCombatMultiplier FindAdvantage;
    TriggerCombatBonus HitRate;
    TriggerCombatBonus CritRate;
    TriggerCombatBonus AttackStepB;
    TriggerCombatMultiplier AttackStepM;
    TriggerCombatBonus DefenseStepB;
    TriggerCombatMultiplier DefenseStepM;
    TriggerCombatBonus DamageStepB;
    TriggerCombatMultiplier DamageStepM;
    TriggerCombatEffect FinishAttack;
    TriggerCombatEffect CombatEnd;

    public abstract void PreviewCombat();
    public abstract void ExecuteCombat();

    protected void SetAttackerDelegates(StatsManager person)
    {
        Debug.Log("Test1");
        TalentTrigger[] triggers = person.gameObject.GetComponents<TalentTrigger>();
        foreach (TalentTrigger trigger in triggers)
        {
            if (trigger is ICombat)
            {
                BeginAttack += ((ICombat)trigger).BeginAttack;
                FindAdvantage += ((ICombat)trigger).FindAdvantage;
                HitRate += ((ICombat)trigger).Accuracy;
                CritRate += ((ICombat)trigger).Critical;
                AttackStepB += ((ICombat)trigger).AttackStepBonus;
                AttackStepM += ((ICombat)trigger).AttackStepMultiplier;
                DefenseStepM += ((ICombat)trigger).DefenseStepMultiplier;
                DamageStepB += ((ICombat)trigger).DamageStepBonus;
                DamageStepM += ((ICombat)trigger).DamageStepMultiplier;
                FinishAttack += ((ICombat)trigger).OnHitEffect;
                //FinishAttack += ((ICombat)trigger).TakeDamage; generally only the defender will have these
                CombatEnd += ((ICombat)trigger).CombatEnd;
            }
        }
    }

    protected void SetDefenderDelegates(StatsManager person)
    {
        Debug.Log("Test2");
        TalentTrigger[] triggers = person.gameObject.GetComponents<TalentTrigger>();
        foreach (TalentTrigger trigger in triggers)
        {
            if (trigger is ICombat)
            {
                BeginAttack += ((ICombat)trigger).BeginAttack;
                FindAdvantage += ((ICombat)trigger).FindAdvantage;
                HitRate += ((ICombat)trigger).Evasion;
                CritRate += ((ICombat)trigger).Guard;
                AttackStepM += ((ICombat)trigger).AttackStepMultiplier;
                DefenseStepB += ((ICombat)trigger).DefenseStepBonus;
                //DefenseStepM += ((ICombat)trigger).DefenseStepMultiplier; this is commented out because Pierce is the only one
                DamageStepB += ((ICombat)trigger).DefenseStepBonus;
                DamageStepM += ((ICombat)trigger).DamageStepMultiplier;
                FinishAttack += ((ICombat)trigger).TakeDamage;
                CombatEnd += ((ICombat)trigger).CombatEnd;
            }
        }
    }
    
    public AttackData GenerateHit(StatsManager attacker, StatsManager defender) 
    {
        //create an AttackData object and invoke all our delegates
        //except for the FinishAttack delegate, since that one will likely call actual effects
        AttackData attackData = new AttackData();
        recent = attackData;
        attackData.attacker = attacker.gameObject.name;
        attackData.defender = defender.gameObject.name;
        BeginAttack(attackData);
        foreach (TriggerCombatMultiplier del in FindAdvantage.GetInvocationList())
        {
            attackData.advantage += del.Invoke(attackData);
        }
        attackData.HitRate += attacker.statsData.DeriveAccuracy();
        attackData.HitRate -= defender.statsData.DeriveEvasion();
        foreach (TriggerCombatBonus del in HitRate.GetInvocationList())
        {
            attackData.HitRate += del.Invoke(attackData);
        }
        attackData.CriticalRate += attacker.statsData.DeriveCritical();
        attackData.CriticalRate -= defender.statsData.DeriveGuard();
        foreach (TriggerCombatBonus del in CritRate.GetInvocationList())
        {
            attackData.CriticalRate += del.Invoke(attackData);
        }
        attackData.DamageDealt += attacker.statsData.DeriveAttack(attacker.statsData.weapons[0]);
        foreach (TriggerCombatBonus del in AttackStepB.GetInvocationList())
        {
            attackData.DamageDealt += del.Invoke(attackData);
        }
        double temp = 1.0;
        foreach (TriggerCombatMultiplier del in AttackStepM.GetInvocationList())
        { 
            temp += del.Invoke(attackData);
            attackData.DamageDealt = (int)Mathf.Ceil((float)(attackData.DamageDealt * temp)); //experimenting with rounding up
        }
        attackData.DamageDealt -= defender.statsData.DeriveAttack(attacker.statsData.weapons[0]);
        foreach (TriggerCombatBonus del in DefenseStepB.GetInvocationList())
        {
            attackData.DamageDealt -= del.Invoke(attackData); //may need to check how this goes with the standard
        }
        temp = 1.0;
        foreach (TriggerCombatMultiplier del in DefenseStepM.GetInvocationList())
        {
            temp += del.Invoke(attackData);
            attackData.DamageDealt = (int)Mathf.Ceil((float)(attackData.DamageDealt * temp)); //experimenting with rounding up
        }
        foreach (TriggerCombatBonus del in DamageStepB.GetInvocationList())
        {
            attackData.DamageDealt -= del.Invoke(attackData); //may need to check how attackData goes with the standard
        }
        temp = 1.0;
        foreach (TriggerCombatMultiplier del in DamageStepM.GetInvocationList())
        {
            temp += del.Invoke(attackData);
            attackData.DamageDealt = (int)Mathf.Ceil((float)(attackData.DamageDealt * temp)); //experimenting with rounding up
        }
        return attackData;
    }

    public AttackData ExecuteHit(StatsManager attacker, StatsManager defender)
    {
        //set up variables
        AttackData attackData = new AttackData();
        recent = attackData;
        attackData.attacker = attacker.gameObject.name;
        attackData.defender = defender.gameObject.name;

        //call setup methods
        Debug.Log(attackData);
        BeginAttack(attackData); //for some reason this doesn't exist?  attackData is there, but something in it isn't
        foreach (TriggerCombatMultiplier del in FindAdvantage.GetInvocationList())
        {
            attackData.advantage += del.Invoke(attackData);
        }
        attackData.HitRate += attacker.statsData.DeriveAccuracy();
        attackData.HitRate -= defender.statsData.DeriveEvasion();
        foreach (TriggerCombatBonus del in HitRate.GetInvocationList())
        {
            attackData.HitRate += del.Invoke(attackData);
        }

        //work with RNG to determine Hit or Miss
        int HitRNG = Random.Range(1, 100);
        if(attackData.advantage < 1)
        {
            HitRNG = (int)Mathf.Ceil((float)(HitRNG * attackData.advantage));
        }
        else if(attackData.advantage > 1)
        {
            HitRNG = (int)Mathf.Floor((float)(HitRNG * attackData.advantage));
        }
        if (HitRNG <= attackData.HitRate * attackData.advantage)
        {
            //If attack hits, work with RNG to determine Crit, Guard, or None.
            attackData.hit = true;
            attackData.CriticalRate += attacker.statsData.DeriveCritical();
            attackData.CriticalRate -= defender.statsData.DeriveGuard();
            foreach (TriggerCombatBonus del in CritRate.GetInvocationList())
            {
                attackData.CriticalRate += del.Invoke(attackData);
            }

            int CritRNG = Random.Range(1, 100);
            if (attackData.advantage < 1)
            {
                CritRNG = (int)Mathf.Ceil((float)(CritRNG * attackData.advantage));
            }
            else if (attackData.advantage > 1)
            {
                CritRNG = (int)Mathf.Floor((float)(CritRNG * attackData.advantage));
            }
            double temp;
            if (attackData.CriticalRate >= 0 && attackData.CriticalRate <= CritRNG)
            {
                //successful crit
                attackData.crit = true;
                temp = 1.5;
            }
            else if(attackData.CriticalRate < 0 && Mathf.Abs(attackData.CriticalRate) <= CritRNG)
            {
                //successful guard
                attackData.guard = true;
                temp = 0.5;
            }
            else
            {
                //no crit OR guard
                temp = 1.0;
            }

            //now we calculate damage by calling methods
            attackData.DamageDealt += attacker.statsData.DeriveAttack(attacker.statsData.weapons[0]);
            foreach (TriggerCombatBonus del in AttackStepB.GetInvocationList())
            {
                attackData.DamageDealt += del.Invoke(attackData);
            }
            foreach (TriggerCombatMultiplier del in AttackStepM.GetInvocationList())
            {
                temp += del.Invoke(attackData);
                attackData.DamageDealt = (int)Mathf.Ceil((float)(attackData.DamageDealt * temp)); //experimenting with rounding up
            }
            attackData.DamageDealt -= defender.statsData.DeriveAttack(attacker.statsData.weapons[0]);
            foreach (TriggerCombatBonus del in DefenseStepB.GetInvocationList())
            {
                attackData.DamageDealt -= del.Invoke(attackData); //may need to check how this goes with the standard
            }
            temp = 1.0;
            foreach (TriggerCombatMultiplier del in DefenseStepM.GetInvocationList())
            {
                temp += del.Invoke(attackData);
                attackData.DamageDealt = (int)Mathf.Ceil((float)(attackData.DamageDealt * temp));
            }
            foreach (TriggerCombatBonus del in DamageStepB.GetInvocationList())
            {
                attackData.DamageDealt -= del.Invoke(attackData);
            }
            temp = 1.0;
            foreach (TriggerCombatMultiplier del in DamageStepM.GetInvocationList())
            {
                temp += del.Invoke(attackData);
                attackData.DamageDealt = (int)Mathf.Ceil((float)(attackData.DamageDealt * temp));
            }

            //once attack is done, call finishers.
            defender.GetComponentInParent<HealthManager>().TakeDamage(attackData.DamageDealt);
            FinishAttack(attackData); //we call the take damage function first so that if the on hit happens to check HP, it's updated
            //theoretically we could add the take damage thing to the delegate but that'd be weird and doesn't guarantee proper order.
        }

        return attackData;
    }
}
