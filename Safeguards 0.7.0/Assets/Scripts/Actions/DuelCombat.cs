using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DuelCombat : AttackAction
{


    public override void ProcessAction(string target)
    {
        throw new System.NotImplementedException();
    }

    public override void PreviewCombat()
    {
        SetAttackerDelegates(user);
        SetDefenderDelegates(target);
        attacks.Add(GenerateHit());

        SetAttackerDelegates(target);
        SetDefenderDelegates(user);
        attacks.Add(GenerateHit());

        //now check followup and stuff
    }

    public override void ExecuteCombat()
    {
        foreach(AttackData attack in attacks)
        {
            ExecuteHit(attack);
        }
    }

    public DuelCombat(StatsManager initiate, StatsManager counter)
    {
        user = initiate;
        target = counter;
        attacks = new List<AttackData>();

    }

}
