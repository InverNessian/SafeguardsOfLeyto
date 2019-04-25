using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombat
{

    //so I might have found a dumb solution... set the methods all to void, then it doesn't matter if they can't return multiple values.
    //since they take the Attack as a parameter, they can just modify the recent attack field of the Attack.


    //EvBeginAttack
    void BeginAttack(AttackAction combat);
    double FindAdvantage(AttackAction combat);

    //EvHitRate
    int Accuracy(AttackAction combat);
    int Evasion(AttackAction combat); 
    //because of how the combat will manage these, return Evasion and anything that drops Hit rate as negative

    //EvCritRate
    int Critical(AttackAction combat);
    int Guard(AttackAction combat);

    //EvAttackStep
    int AttackStepBonus(AttackAction combat);
    double AttackStepMultiplier(AttackAction combat); //convert these to percents later

    //EvDefenseStep
    int DefenseStepBonus(AttackAction combat);
    double DefenseStepMultiplier(AttackAction combat);

    //EvDamageStep
    int DamageStepBonus(AttackAction combat);
    double DamageStepMultiplier(AttackAction combat);

    //EvFinishAttack
    void OnHitEffect(AttackAction combat); //in combat controller, these events are invoked only if the attack hits
    void TakeDamage(AttackAction combat); //and will be invoked even if no damage is actually dealt
    //for takedamage, grab the "recentdamage" field from the combat.

    //EvCombatEnd
    void CombatEnd(AttackAction combat);
}