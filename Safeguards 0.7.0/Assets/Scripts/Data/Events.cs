using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UltEvents;


//so actually, we can just create generic classes of events and then instance them in the classes we need.

public sealed class StatsManagerEvent : UltEvent<StatsManager> { }

public sealed class GameObjectEvent : UltEvent<GameObject> { }


public sealed class HPEvent : UltEvent<HealthManager, StatsManager, int> { } //maybe try to find a workaround for this






//actually this would need a unique event for every action, unless I had some way to differentiate them.
//I'll definitely need to have more Events for Squads at least since they have different parameter needs.
//but maybe I could leverage Inheritance here.  Specific events could inherit from CombatEvent?

//I wonder if I could make a "StatsManager" for Squads? then I could just fire the same event but pass in the Squad's manager

public class EquipEvent : UltEvent<int>
{

}

public class DownEvent : UnityEvent<HealthManager>
{

}
public class DamageEvent : UnityEvent<HealthManager, int>
{

}
public class HealEvent : UnityEvent<HealthManager, int>
{

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