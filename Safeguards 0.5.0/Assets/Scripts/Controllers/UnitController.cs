using System.Collections;
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
    public static CombatEvent mDuelCombat = new CombatEvent();
    public static MovementEvent mNormalMove = new MovementEvent();

    //experiment with non-static Events?


    // Start is called before the first frame update
    private void OnEnable()
    {
        //this method is intended to be used with adding event listeners.
    }

    void Awake()
    {

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
                    //DataHolder target = hit.collider.GetComponent<DataHolder>();
                    string test = hit.collider.gameObject.name;
                    if (hit.collider.GetComponent<StatsManager>() != null && !test.Equals(selected)) // && mDuelCombat != null
                    {
                        GameObject user = GameObject.Find(selected);//.GetComponent<DataHolder>().unitData;
                        //OnClicked(target.unit_name);
                        mDuelCombat.Invoke(user, hit.collider.gameObject);
                        //grab the user's data based on the "Selected" property
                    }
                    else
                    {
                        mNormalMove.Invoke(GameObject.Find(selected), hit.point);
                    }

                }


            }
        }
        if (Input.GetMouseButtonDown(1))
        { // if right button pressed...
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {

                //StatsManager target = hit.collider.GetComponent<StatsManager>();
                string name = hit.collider.gameObject.name;
                if (hit.collider.GetComponent<StatsManager>() != null)
                {
                    //this way, the script will only update the selected value if the target has a UnitData component
                    if (name.Equals(selected))
                    {
                        selected = "none";
                        Debug.Log("Deselected " + name);
                    }
                    else
                    {
                        selected = name;
                        Debug.Log("Selected " + selected);
                    }

                }

            }
        }
    }
}
