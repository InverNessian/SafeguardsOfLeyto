using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{


    // Start is called after Awake, so apparently we need that to happen in UnitController. :/
    void Awake()
    {
        UnitController.mNormalMove.AddListener(NormalMove);
    }


    public void NormalMove(GameObject user, Vector3 target)
    {
        user.GetComponent<NavMeshAgent>().destination = target;
        Debug.Log(user);
    }

}
