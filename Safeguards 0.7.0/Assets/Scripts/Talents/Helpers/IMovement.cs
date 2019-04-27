using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement 
{
    //EvBeginMove
    void BonusMove();

    //EvWayPointSet
    void WayPointSet(Vector3 location); //useful for finding Opportunity triggers?
                //rather, just make an invisible collider and have units with Opportunity marked with a hazard icon

    //EvMoveEnd
    void FinishMove();

}
