using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombat
{

    //all methods are void because of return bullshit with delegates.  
    //talents that boost combat stats should apply their value to the combat parameter they receive in the call

    //EvBeginAttack
    void BeginAttack(AttackData combat);
    void FindAdvantage(AttackData combat);

    //EvHitRate
    void Accuracy(AttackData combat);
    void Evasion(AttackData combat); 
    //because of how the combat will manage these, return Evasion and anything that drops Hit rate as negative

    //EvCritRate
    void Critical(AttackData combat);
    void Guard(AttackData combat);

    //EvAttackStep
    void AttackStepBonus(AttackData combat);
    void AttackStepMultiplier(AttackData combat);

    //EvDefenseStep
    void DefenseStepBonus(AttackData combat);
    void DefenseStepMultiplier(AttackData combat);

    //EvDamageStep
    void DamageStepBonus(AttackData combat);
    void DamageStepMultiplier(AttackData combat);

    //EvFinishAttack
    void OnHitEffect(AttackData combat); //in combat controller, these events are invoked only if the attack hits
    void TakeDamage(AttackData combat); //and will be invoked even if no damage is actually dealt
    //for Parry/Riposte, you can check the data here for damage dealt.  if it flags, maybe create a new combat object?
    //this one could have special rules that 


    //EvCombatEnd
    void CombatEnd(AttackData combat);
}