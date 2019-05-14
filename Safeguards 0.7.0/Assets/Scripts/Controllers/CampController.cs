using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UltEvents;

public class CampController : MonoBehaviour
{
    //Finite State Machine
    public enum PlayStates { ACTIVE_PLAYER, INACTIVE, UNIT_SELECT } 
    public static PlayStates playState;
    
    //other static fields
    public static string selected = "none";

    //events
    public static GameObjectEvent SelectEvent = new GameObjectEvent();
    public static UltEvent DeselectEvent = new UltEvent();
    public static GameObjectEvent MovementEvent = new GameObjectEvent();



    private void OnEnable()
    {
        //this method is intended to be used with adding event listeners.
        //GameController.unitController = this;

    }

    void Awake()
    {
        //semi-singleton.  we don't keep them between scenes, but there is only one per scene.  set a static reference for other classes.
        //if(controller == null)
        //{
        //    controller = this;
        //}

        playState = PlayStates.ACTIVE_PLAYER;
    }

    public void SetStateInstance(int statecode)
    {
        //this is gross but it works for an instance method
        //only the buttons call this
        if (statecode == (int)PlayStates.ACTIVE_PLAYER)
        {
            playState = PlayStates.ACTIVE_PLAYER;
        }
        if (statecode == (int)PlayStates.INACTIVE)
        {
            playState = PlayStates.INACTIVE;
        }
        if (statecode == (int)PlayStates.UNIT_SELECT)
        {
            playState = PlayStates.UNIT_SELECT;
        }
    }
    public static void SetState(PlayStates statecode)
    {
        playState = statecode;
    }


    public void ReceiveAction(string action)
    {
        switch (action)
        {
            case "NormalMove":
                NormalMove move = new NormalMove(GameObject.Find(selected));
                move.PreviewMove();
                break;
            case "Equip":
                Equip equip = new Equip(GameObject.Find(selected).GetComponent<StatsManager>());
                equip.PreviewLoadout();
                break;
        }
        playState = PlayStates.UNIT_SELECT;
        //I think I want the actions themselves to return control back when they are finished
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Act2") && (playState == PlayStates.ACTIVE_PLAYER || playState == PlayStates.UNIT_SELECT))
        { // if right button pressed...
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //the script will only update the selected value if the target has a StatsManager component
                string name = hit.collider.gameObject.name;
                if (hit.collider.GetComponent<StatsManager>() != null)
                {
                    if (name.Equals(selected))
                    {
                        selected = "none";
                        DeselectEvent.Invoke();
                        playState = PlayStates.ACTIVE_PLAYER;
                        Debug.Log("Deselected " + name);
                    }
                    else
                    {
                        //by firing a deselect event, we clear out any lingering UI or whatnot
                        DeselectEvent.Invoke();

                        selected = name;
                        SelectEvent.Invoke(hit.collider.gameObject);
                        //mSelect.Invoke(hit.collider.gameObject);
                        playState = PlayStates.UNIT_SELECT;
                        Debug.Log("Selected " + selected);
                    }

                }

            }
        }
    }
}
