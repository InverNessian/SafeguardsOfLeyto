using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementEvent : UnityEvent<GameObject, Vector3>
{
    //, Vector3[]
    //the vector3 is an array so we can have the paths with waypoints
}
