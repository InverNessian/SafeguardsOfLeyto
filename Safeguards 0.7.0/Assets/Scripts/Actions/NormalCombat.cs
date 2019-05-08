using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NormalCombat : AttackAction
{

    public override void PreviewCombat()
    {
        //two standard hits
        attacks.Add(GenerateHit(initiator, receiver));
        attacks.Add(GenerateHit(receiver, initiator));

        //now check followup and stuff
        for(int i = 1; i <= initiator.statsData.maxFollowUpAttacks; i++)
        {
            //check difference compared to threshold times number of attack
            if (initiator.statsData.DeriveFollowup() - receiver.statsData.DeriveFollowup() > initiator.statsData.followUpThreshold * i)
            {
                attacks.Add(GenerateHit(initiator, receiver));
                //actually the problem with this is that the follow-up hits will be out of order?  
                //then again it's not very common to have more than 1 person following up
            }
        }

        //check for extra followups
        for (int i = 1; i <= receiver.statsData.maxFollowUpAttacks; i++)
        {
            if (receiver.statsData.DeriveFollowup() - receiver.statsData.DeriveFollowup() > receiver.statsData.followUpThreshold * i)
            {
                attacks.Add(GenerateHit(receiver, initiator));
            }
        }
        foreach (AttackData temp in attacks)
        {
            Debug.Log(temp.ToString()); //in actuality this should be a UI
        }
    }

    public override void ExecuteCombat()
    {
        //first we clear out the attacks list so we can do fresh
        attacks = new List<AttackData>();

        //two standard hits
        attacks.Add(ExecuteHit(initiator, receiver));
        attacks.Add(ExecuteHit(receiver, initiator));

        //now check followup and stuff
        for (int i = 1; i <= initiator.statsData.maxFollowUpAttacks; i++)
        {
            if (initiator.statsData.DeriveFollowup() - receiver.statsData.DeriveFollowup() > initiator.statsData.followUpThreshold * i)
            {
                attacks.Add(ExecuteHit(initiator, receiver));
                //actually the problem with this is that the follow-up hits will be out of order?  
                //then again it's not very common to have more than 1 person following up
            }
        }

        //check for extra followups
        for (int i = 1; i <= receiver.statsData.maxFollowUpAttacks; i++)
        {
            if (receiver.statsData.DeriveFollowup() - receiver.statsData.DeriveFollowup() > receiver.statsData.followUpThreshold * i)
            {
                attacks.Add(ExecuteHit(receiver, initiator));
            }
        }
    }

    public override void UndoAction()
    {
        throw new System.NotImplementedException();
    }

    public NormalCombat(StatsManager initiate, StatsManager counter)
    {
        initiator = initiate;
        receiver = counter;
        attacks = new List<AttackData>();

    }

}
