using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AttackAction", fileName ="AttackAction")]
public class Action_Attack : Action
{
    //somehow this is going to need to check against field modifiers but we'll come back to that
    public Stats_Unit attacker;
    public Stats_Unit defender;

    public void StartCombat()
    {

    }

    public void GenerateAttack()
    {

    }

    public void EndCombat()
    {

    }
}
