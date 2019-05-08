using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{


    // Start is called after Awake, so apparently we need that to happen in InputController. :/
    void Awake()
    {
        //InputController.mNormalMove.AddListener(NormalMove);
    }


    public void NormalMove(GameObject user, Vector3 target)
    {
        user.GetComponent<NavMeshAgent>().destination = target;
        InputController.SetState(InputController.PlayStates.UNIT_SELECT);
        Debug.Log(user);
    }

}
