using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CombatEvent : UnityEvent<GameObject, GameObject> //this will need updating for Squads
{
    //actually this would need a unique event for every action, unless I had some way to differentiate them.
    //I'll definitely need to have more Events for Squads at least since they have different parameter needs.
    //but maybe I could leverage Inheritance here.  Specific events could inherit from CombatEvent?
}

public class MovementEvent : UnityEvent<GameObject, Vector3>
{
    //, Vector3[]
    //the vector3 is an array so we can have the paths with waypoints
}

[System.Serializable]
public class SelectionEvent : UnityEvent<GameObject> //this will need updating for Squads
{
    
}

[System.Serializable]
public class DeselectionEvent : UnityEvent //this will need updating for Squads
{

}