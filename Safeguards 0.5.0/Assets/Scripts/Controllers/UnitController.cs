﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{

    public static string selected = "none";

    //set up a clickaction event that fires the attack method from CombatController
    //public delegate void ClickAction(string target);
    //public static event ClickAction OnClicked;
    public static CombatEvent m_testEvent;


    //NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        if (m_testEvent == null)
            m_testEvent = new CombatEvent();

        //m_testEvent.AddListener(Ping);
        //agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {

                if (selected != "none")
                {
                    //this is worded this way so that there are no triggers here except if there's a selected unit.  

                    //for right now, I'm going to hijack this to test the combat stuff
                    DataHolder target = hit.collider.GetComponent<DataHolder>();
                    if (target != null && !target.unitData.unit_name.Equals(selected) && m_testEvent != null)
                    {
                        UnitData user = GameObject.Find(selected).GetComponent<DataHolder>().unitData;
                        //OnClicked(target.unit_name);
                        m_testEvent.Invoke(target.unitData, user);
                        //grab the user's data based on the "Selected" property
                        //;
                        //GameObject.Find("ActionHolder").GetComponent<CombatController>().PreviewCombat(user,target);
                    }
                    else
                    {
                        GameObject.Find(selected).GetComponent<NavMeshAgent>().destination = hit.point;
                    }

                }


            }
        }
        if (Input.GetMouseButtonDown(1))
        { // if right button pressed...
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {

                DataHolder target = hit.collider.GetComponent<DataHolder>();
                if (target != null)
                {

                    //this way, the script will only update the selected value if the target has a UnitData component
                    if (target.unitData.unit_name.Equals(selected))
                    {
                        selected = "none";
                        Debug.Log("Deselected " + target.unitData.unit_name);
                    }
                    else
                    {
                        selected = target.unitData.unit_name;
                        Debug.Log("Selected " + selected);
                    }

                }

            }
        }
    }
}