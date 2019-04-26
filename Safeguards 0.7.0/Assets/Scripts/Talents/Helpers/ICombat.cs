using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombat
{

    //so I might have found a dumb solution... set the methods all to void, then it doesn't matter if they can't return multiple values.
    //since they take the Attack as a parameter, they can just modify the recent attack field of the Attack.


    //EvBeginAttack
    void BeginAttack(AttackData combat);
    double FindAdvantage(AttackData combat);

    //EvHitRate
    int Accuracy(AttackData combat);
    int Evasion(AttackData combat); 
    //because of how the combat will manage these, return Evasion and anything that drops Hit rate as negative

    //EvCritRate
    int Critical(AttackData combat);
    int Guard(AttackData combat);

    //EvAttackStep
    int AttackStepBonus(AttackData combat);
    double AttackStepMultiplier(AttackData combat); //convert these to percents later

    //EvDefenseStep
    int DefenseStepBonus(AttackData combat);
    double DefenseStepMultiplier(AttackData combat);

    //EvDamageStep
    int DamageStepBonus(AttackData combat);
    double DamageStepMultiplier(AttackData combat);

    //EvFinishAttack
    void OnHitEffect(AttackData combat); //in combat controller, these events are invoked only if the attack hits
    void TakeDamage(AttackData combat); //and will be invoked even if no damage is actually dealt
    //for Parry/Riposte, you can check the data here for damage dealt.  if it flags, maybe create a new combat object?
    //this one could have special rules that 


    //EvCombatEnd
    void CombatEnd(AttackData combat);
}