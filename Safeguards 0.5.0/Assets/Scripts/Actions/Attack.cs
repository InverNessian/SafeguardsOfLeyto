using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    AttackData GenerateHit(UnitData attacker, UnitData defender, bool preview);
    void PreviewCombat(UnitData attacker, UnitData defender);
    void DoCombat(UnitData attacker, UnitData defender);
    //List<UnitData> attackers, List<UnitData> defenders
}
