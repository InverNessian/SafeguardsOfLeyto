using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CombatEvent : UnityEvent<UnitData, UnitData> //this will need updating for Squads
{
    //actually this would need a unique event for every action, unless I had some way to differentiate them.
    //I'll definitely need to have more Events for Squads at least since they have different parameter needs.
    //but maybe I could leverage Inheritance here.  Specific events could inherit from CombatEvent?
}
