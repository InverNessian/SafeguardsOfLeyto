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
        SetAttackerDelegates(initiator);
        SetDefenderDelegates(receiver);
        attacks.Add(GenerateHit(initiator, receiver));

        SetAttackerDelegates(receiver);
        SetDefenderDelegates(initiator);
        attacks.Add(GenerateHit(receiver, initiator));

        //now check followup and stuff
        if(initiator.statsData.DeriveFollowup() - receiver.statsData.DeriveFollowup() > initiator.statsData.followUpThreshold)
        {
            SetAttackerDelegates(initiator);
            SetDefenderDelegates(receiver);
            attacks.Add(GenerateHit(initiator, receiver));
        }
        if (initiator.statsData.maxFollowUpAttacks == 2 && initiator.statsData.DeriveFollowup() - receiver.statsData.DeriveFollowup() > initiator.statsData.followUpThreshold * 2)
        {
            SetAttackerDelegates(initiator);
            SetDefenderDelegates(receiver);
            attacks.Add(GenerateHit(initiator, receiver));
        }

        //check for extra followups
        if (receiver.statsData.DeriveFollowup() - initiator.statsData.DeriveFollowup() > receiver.statsData.followUpThreshold)
        {
            SetAttackerDelegates(receiver);
            SetDefenderDelegates(initiator);
            attacks.Add(GenerateHit(receiver, initiator));
        }
        if (receiver.statsData.maxFollowUpAttacks == 2 && receiver.statsData.DeriveFollowup() - initiator.statsData.DeriveFollowup() > receiver.statsData.followUpThreshold * 2)
        {
            SetAttackerDelegates(receiver);
            SetDefenderDelegates(initiator);
            attacks.Add(GenerateHit(receiver, initiator));
        }

    }

    public override void ExecuteCombat()
    {
        //first we clear out the attacks list so we can do fresh
        foreach(AttackData attack in attacks)
        {
            attacks.Remove(attack);
        }

        SetAttackerDelegates(initiator);
        SetDefenderDelegates(receiver);
        attacks.Add(ExecuteHit(initiator, receiver));

        SetAttackerDelegates(receiver);
        SetDefenderDelegates(initiator);
        attacks.Add(ExecuteHit(receiver, initiator));

        //now check followup and stuff
        if (initiator.statsData.DeriveFollowup() - receiver.statsData.DeriveFollowup() > initiator.statsData.followUpThreshold)
        {
            SetAttackerDelegates(initiator);
            SetDefenderDelegates(receiver);
            attacks.Add(ExecuteHit(initiator, receiver));
        }
        if (initiator.statsData.maxFollowUpAttacks == 2 && initiator.statsData.DeriveFollowup() - receiver.statsData.DeriveFollowup() > initiator.statsData.followUpThreshold * 2)
        {
            SetAttackerDelegates(initiator);
            SetDefenderDelegates(receiver);
            attacks.Add(ExecuteHit(initiator, receiver));
        }

        //check for extra followups
        if (receiver.statsData.DeriveFollowup() - initiator.statsData.DeriveFollowup() > receiver.statsData.followUpThreshold)
        {
            SetAttackerDelegates(receiver);
            SetDefenderDelegates(initiator);
            attacks.Add(ExecuteHit(receiver, initiator));
        }
        if (receiver.statsData.maxFollowUpAttacks == 2 && receiver.statsData.DeriveFollowup() - initiator.statsData.DeriveFollowup() > receiver.statsData.followUpThreshold * 2)
        {
            SetAttackerDelegates(receiver);
            SetDefenderDelegates(initiator);
            attacks.Add(ExecuteHit(receiver, initiator));
        }
    }

    public override void UndoAction()
    {
        throw new System.NotImplementedException();
    }

    public DuelCombat(StatsManager initiate, StatsManager counter)
    {
        initiator = initiate;
        receiver = counter;
        attacks = new List<AttackData>();

    }

}
