using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalMove : Movement
{
    public MoveData moveData;

    public NormalMove(GameObject user)
    {
        moveData = new MoveData
        {
            user = user
        };
    }


    public void CommitMove(GameObject user, Vector3 target)
    {
        //maybe remove this once movedata works
        user.GetComponent<NavMeshAgent>().destination = target;
    }

    public override void CommitMove(MoveData md)
    {

    }

    public override void PreviewMove()
    {
        //in the course of this method, we should show movement range and stuff
        //basically do the waypoint selecting thing.
    }

    public override void UndoAction()
    {
        throw new System.NotImplementedException();
    }
}
