using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackAction : Action
{

    //data
    public StatsManager user;
    public StatsManager target;
    public List<AttackData> attacks; // the most recent attack should be the last in the list.
    public AttackData recent;

    //delegates
    public delegate void TriggerEffect(AttackAction combat);
    public delegate int TriggerBonus(AttackAction combat);
    public delegate double TriggerMultiplier(AttackAction combat);

    TriggerEffect BeginAttack; //maybe use this to add to the attackData's list of onhit strings, but not actual stuff
    TriggerMultiplier FindAdvantage;
    TriggerBonus HitRate;
    TriggerBonus CritRate;
    TriggerBonus AttackStepB;
    TriggerMultiplier AttackStepM;
    TriggerBonus DefenseStepB;
    TriggerMultiplier DefenseStepM;
    TriggerBonus DamageStepB;
    TriggerMultiplier DamageStepM;
    TriggerEffect FinishAttack;
    TriggerEffect CombatEnd;

    public abstract void PreviewCombat();
    public abstract void ExecuteCombat();

    protected void SetAttackerDelegates(StatsManager person)
    {
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
                FinishAttack += ((ICombat)trigger).TakeDamage;
                CombatEnd += ((ICombat)trigger).CombatEnd;
            }
        }
    }

    protected void SetDefenderDelegates(StatsManager person)
    {
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
    
    public AttackData GenerateHit() 
    {
        //create an AttackData object and invoke all our delegates
        //except for the FinishAttack delegate, since that one will likely call actual effects
        AttackData attackData = new AttackData();
        recent = attackData;
        attackData.attacker = user.gameObject.name;
        attackData.defender = target.gameObject.name;
        BeginAttack(this);
        foreach (TriggerMultiplier del in FindAdvantage.GetInvocationList())
        {
            attackData.advantage += del.Invoke(this);
        }
        attackData.HitRate += user.statsData.DeriveAccuracy();
        attackData.HitRate -= target.statsData.DeriveEvasion();
        foreach (TriggerBonus del in HitRate.GetInvocationList())
        {
            attackData.HitRate += del.Invoke(this);
        }
        attackData.CriticalRate += user.statsData.DeriveCritical();
        attackData.CriticalRate -= target.statsData.DeriveGuard();
        foreach (TriggerBonus del in CritRate.GetInvocationList())
        {
            attackData.CriticalRate += del.Invoke(this);
        }
        attackData.DamageDealt += user.statsData.DeriveAttack(user.statsData.weapons[0]);
        foreach (TriggerBonus del in AttackStepB.GetInvocationList())
        {
            attackData.DamageDealt += del.Invoke(this);
        }
        double temp = 1.0;
        foreach (TriggerMultiplier del in AttackStepM.GetInvocationList())
        { 
            temp += del.Invoke(this);
            attackData.DamageDealt = (int)Mathf.Ceil((float)(attackData.DamageDealt * temp)); //experimenting with rounding up
        }
        attackData.DamageDealt -= target.statsData.DeriveAttack(user.statsData.weapons[0]);
        foreach (TriggerBonus del in DefenseStepB.GetInvocationList())
        {
            attackData.DamageDealt -= del.Invoke(this); //may need to check how this goes with the standard
        }
        temp = 1.0;
        foreach (TriggerMultiplier del in DefenseStepM.GetInvocationList())
        {
            temp += del.Invoke(this);
            attackData.DamageDealt = (int)Mathf.Ceil((float)(attackData.DamageDealt * temp)); //experimenting with rounding up
        }
        foreach (TriggerBonus del in DamageStepB.GetInvocationList())
        {
            attackData.DamageDealt -= del.Invoke(this); //may need to check how this goes with the standard
        }
        temp = 1.0;
        foreach (TriggerMultiplier del in DamageStepM.GetInvocationList())
        {
            temp += del.Invoke(this);
            attackData.DamageDealt = (int)Mathf.Ceil((float)(attackData.DamageDealt * temp)); //experimenting with rounding up
        }
        return attackData;
    }

    public void ExecuteHit(AttackData attackData)
    {
        int HitRNG = Random.Range(1, 100);
        if(HitRNG <= attackData.HitRate)
        {
            attackData.hit = true;
        }
        int CritRNG = Random.Range(1, 100); //now there's the issue of not being able to modify damage numbers properly
    }
}
