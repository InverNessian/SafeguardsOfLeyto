using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackAction : Action
{

    //data
    public StatsManager initiator;
    public StatsManager receiver;
    public List<AttackData> attacks;
    //public AttackData recent; // may notbe needed since we pass it as parameter

    //delegates
    public delegate void TriggerCombatEffect(AttackData combat);
    //public delegate int TriggerCombatBonus(AttackData combat);
    //public delegate double TriggerCombatMultiplier(AttackData combat);

    TriggerCombatEffect BeginAttack; //maybe use this to add to the attackData's list of onhit strings, but not actual stuff
    TriggerCombatEffect FindAdvantage;
    TriggerCombatEffect HitRate;
    TriggerCombatEffect CritRate;
    TriggerCombatEffect AttackStepB;
    TriggerCombatEffect AttackStepM;
    TriggerCombatEffect DefenseStepB;
    TriggerCombatEffect DefenseStepM;
    TriggerCombatEffect DamageStepB;
    TriggerCombatEffect DamageStepM;
    TriggerCombatEffect FinishAttack;
    TriggerCombatEffect CombatEnd;

    public abstract void PreviewCombat();
    public abstract void ExecuteCombat();

    protected void SetAttackerDelegates(StatsManager person)
    {
        AbilityTrigger[] triggers = person.gameObject.GetComponents<AbilityTrigger>();
        foreach (AbilityTrigger trigger in triggers)
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
        AbilityTrigger[] triggers = person.gameObject.GetComponents<AbilityTrigger>();
        foreach (AbilityTrigger trigger in triggers)
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
        SetAttackerDelegates(attacker);
        SetDefenderDelegates(defender);
        //create an AttackData object and invoke all our delegates
        //except for the FinishAttack delegate, since that one will likely call actual effects
        AttackData attackData = new AttackData
        {
            attacker = attacker.gameObject.name,
            defender = defender.gameObject.name
        };


        //now create attack data based on talent calls and unit stats
        BeginAttack?.Invoke(attackData);
        FindAdvantage.Invoke(attackData);

        //Hit Rate
        attackData.HitRate += attacker.statsData.DeriveAccuracy();
        attackData.HitRate -= defender.statsData.DeriveEvasion();
        HitRate?.Invoke(attackData);

        //Crit Rate
        attackData.CriticalRate += attacker.statsData.DeriveCritical();
        attackData.CriticalRate -= defender.statsData.DeriveGuard();
        CritRate?.Invoke(attackData);

        //Attack Step
        attackData.DamageMultiplier = 1.0;
        attackData.DamageDealt += attacker.statsData.DeriveAttack(attacker.statsData.weapons[0]);
        AttackStepB?.Invoke(attackData);
        AttackStepM?.Invoke(attackData);
        attackData.DamageDealt = (int)Mathf.Ceil((float)(attackData.DamageDealt * attackData.DamageMultiplier));

        //Defense Step
        attackData.DamageMultiplier = 1.0;
        attackData.DamageDealt -= defender.statsData.DeriveDefense(attacker.statsData.weapons[0]);
        DefenseStepB?.Invoke(attackData);
        DefenseStepM?.Invoke(attackData);
        attackData.DamageDealt = (int)Mathf.Ceil((float)(attackData.DamageDealt * attackData.DamageMultiplier));

        //Damage Step
        attackData.DamageMultiplier = 1.0;
        DamageStepB?.Invoke(attackData);
        DamageStepM?.Invoke(attackData);
        attackData.DamageDealt = (int)Mathf.Ceil((float)(attackData.DamageDealt * attackData.DamageMultiplier));

        //now return data
        return attackData;
        
    }

    public AttackData ExecuteHit(StatsManager attacker, StatsManager defender)
    {
        SetAttackerDelegates(attacker);
        SetDefenderDelegates(defender);
        //create an AttackData object and invoke all our delegates
        //except for the FinishAttack delegate, since that one will likely call actual effects
        AttackData attackData = new AttackData
        {
            attacker = attacker.gameObject.name,
            defender = defender.gameObject.name
        };


        //now create attack data based on talent calls and unit stats
        BeginAttack?.Invoke(attackData);
        FindAdvantage?.Invoke(attackData);

        //Hit Rate
        attackData.HitRate += attacker.statsData.DeriveAccuracy();
        attackData.HitRate -= defender.statsData.DeriveEvasion();
        HitRate?.Invoke(attackData);


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


            //Crit Rate
            attackData.CriticalRate += attacker.statsData.DeriveCritical();
            attackData.CriticalRate -= defender.statsData.DeriveGuard();
            CritRate?.Invoke(attackData);

            //check Crit RNG and set starting multiplier based on that
            int CritRNG = Random.Range(1, 100);
            if (attackData.advantage < 1)
            {
                CritRNG = (int)Mathf.Ceil((float)(CritRNG * attackData.advantage));
            }
            else if (attackData.advantage > 1)
            {
                CritRNG = (int)Mathf.Floor((float)(CritRNG * attackData.advantage));
            }
            if (attackData.CriticalRate >= 0 && attackData.CriticalRate <= CritRNG)
            {
                //successful crit
                attackData.crit = true;
                attackData.DamageMultiplier = 1.5;
            }
            else if(attackData.CriticalRate < 0 && Mathf.Abs(attackData.CriticalRate) <= CritRNG)
            {
                //successful guard
                attackData.guard = true;
                attackData.DamageMultiplier = 0.5;
            }
            else
            {
                //no crit OR guard
                attackData.DamageMultiplier = 1.0;
            }

            //now we calculate damage by calling methods

            //Attack Step
            attackData.DamageDealt += attacker.statsData.DeriveAttack(attacker.statsData.weapons[0]);
            AttackStepB?.Invoke(attackData);
            AttackStepM?.Invoke(attackData);
            attackData.DamageDealt = (int)Mathf.Ceil((float)(attackData.DamageDealt * attackData.DamageMultiplier));

            //Defense Step
            attackData.DamageMultiplier = 1.0;
            attackData.DamageDealt -= defender.statsData.DeriveDefense(attacker.statsData.weapons[0]);
            DefenseStepB?.Invoke(attackData);
            DefenseStepM?.Invoke(attackData);
            attackData.DamageDealt = (int)Mathf.Ceil((float)(attackData.DamageDealt * attackData.DamageMultiplier));

            //Damage Step
            attackData.DamageMultiplier = 1.0;
            DamageStepB?.Invoke(attackData);
            DamageStepM?.Invoke(attackData);
            attackData.DamageDealt = (int)Mathf.Ceil((float)(attackData.DamageDealt * attackData.DamageMultiplier));


            //once attack is done, call finishers.
            defender.GetComponentInParent<HealthManager>().TakeDamage(attackData.DamageDealt);
            FinishAttack?.Invoke(attackData); //we call the take damage function first so that if the on hit happens to check HP, it's updated
            //theoretically we could add the take damage thing to the delegate but that'd be weird and doesn't guarantee proper order.
        }

        return attackData;
    }


    /*
    old code, here for reference just in case



        if(FindAdvantage != null)
        {
            foreach (TriggerCombatMultiplier del in FindAdvantage.GetInvocationList())
                {
                    attackData.advantage += del.Invoke(attackData);
                }
        }
        
        attackData.HitRate += attacker.statsData.DeriveAccuracy();
        attackData.HitRate -= defender.statsData.DeriveEvasion();
        if(HitRate != null)
        {
            foreach (TriggerCombatBonus del in HitRate.GetInvocationList())
            {
                attackData.HitRate += del.Invoke(attackData);
            }
        }

        attackData.CriticalRate += attacker.statsData.DeriveCritical();
        attackData.CriticalRate -= defender.statsData.DeriveGuard();
        if(CritRate != null)
        {
            foreach (TriggerCombatBonus del in CritRate.GetInvocationList())
            {
                attackData.CriticalRate += del.Invoke(attackData);
            }
        }

        attackData.DamageDealt += attacker.statsData.DeriveAttack(attacker.statsData.weapons[0]);
        if(AttackStepB != null)
        {
            foreach (TriggerCombatBonus del in AttackStepB.GetInvocationList())
            {
                attackData.DamageDealt += del.Invoke(attackData);
            }
        }

        double temp = 1.0;
        if(AttackStepM != null)
        {
            foreach (TriggerCombatMultiplier del in AttackStepM.GetInvocationList())
            {
                temp += del.Invoke(attackData);
            }
        }

        attackData.DamageDealt = (int)Mathf.Ceil((float)(attackData.DamageDealt * temp));
        //do the multiplication outside of the loop

        attackData.DamageDealt -= defender.statsData.DeriveAttack(attacker.statsData.weapons[0]);
        if(DefenseStepB != null)
        {
            foreach (TriggerCombatBonus del in DefenseStepB.GetInvocationList())
            {
                try
                {
                     attackData.DamageDealt -= del.Invoke(attackData); //may need to check how this goes with the standard
               }
               catch { }
            }
        }
        
        temp = 1.0;
        foreach (TriggerCombatMultiplier del in DefenseStepM.GetInvocationList())
        {
            try
            {
                temp += del.Invoke(attackData);
            }
            catch { }
        }
        attackData.DamageDealt = (int)Mathf.Ceil((float)(attackData.DamageDealt * temp));

        foreach (TriggerCombatBonus del in DamageStepB.GetInvocationList())
        {
            try
            {
                attackData.DamageDealt -= del.Invoke(attackData); //may need to check how attackData goes with the standard
            }
            catch { }
        }
        temp = 1.0;
        foreach (TriggerCombatMultiplier del in DamageStepM.GetInvocationList())
        {
            try
            {
                temp += del.Invoke(attackData);
            }
            catch { }
        }
        attackData.DamageDealt = (int)Mathf.Ceil((float)(attackData.DamageDealt * temp)); //experimenting with rounding up
        */
}
