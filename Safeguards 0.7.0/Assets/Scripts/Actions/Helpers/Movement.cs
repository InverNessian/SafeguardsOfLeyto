using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Movement : Action
{
    public GameObject user;

    public abstract void PreviewMove();
    //public abstract void CommitMove(GameObject user, Vector3 destination);
    public abstract void CommitMove(MoveData md);

    public Vector3 SetWayPoint()
    {
        //flesh this out at some point
        return new Vector3();
    }

}
