using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UltEvents;

public class InputController : MonoBehaviour
{
    //Finite State Machine
    public enum PlayStates { ACTIVE_AI, ACTIVE_PLAYER, INACTIVE, UNIT_SELECT, UNIT_TARGET }
    public static PlayStates playState;

    //player data
    public static ActivePlayers activePlayer = ActivePlayers.PLAYER1;
    public enum ActivePlayers { AI1, AI2, AI3, PLAYER1, PLAYER2, PLAYER3 }

    //other static fields
    public static string selected = "none";
    public static string target = "none";

    //events
    public static SelectionEvent mSelect = new SelectionEvent();
    public static DeselectionEvent mDeselect = new DeselectionEvent();
    public static CombatEvent mDuelCombat = new CombatEvent();
    public static MovementEvent mNormalMove = new MovementEvent();


    public static GameObjectEvent SelectEvent = new GameObjectEvent();
    public static UltEvent DeselectEvent = new UltEvent();

    //experiment with non-static Events?


    // Start is called before the first frame update
    private void OnEnable()
    {
        //this method is intended to be used with adding event listeners.
        GameController.unitController = this;

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
        if (statecode == (int)PlayStates.ACTIVE_AI)
        {
            playState = PlayStates.ACTIVE_AI;
        }
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
        if (statecode == (int)PlayStates.UNIT_TARGET)
        {
            playState = PlayStates.UNIT_TARGET;
        }
    }
    public static void SetState(PlayStates statecode)
    {
        playState = statecode;
    }

    // Update is called once per frame
    void Update()
    {
        //if the player clicks while they are active player.
        if (Input.GetButtonDown("Act1") && playState == PlayStates.UNIT_TARGET) //for multiplayer, add a check for which player is active.
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {

                if (selected != "none") //this is worded this way so that there are no triggers here except if there's a selected unit. 
                {
                    //if the targeted object has stats, and isn't the selected unit, initiate combat against it
                    //otherwise, move to the point
                    if (hit.collider.GetComponent<StatsManager>() != null && !hit.collider.gameObject.name.Equals(selected)) // && mDuelCombat != null
                    {
                        GameObject user = GameObject.Find(selected);
                        NormalCombat combat = new NormalCombat(user.GetComponent<StatsManager>(), hit.collider.GetComponent<StatsManager>());
                        combat.PreviewCombat();
                        combat.ExecuteCombat();
                        //log combat, then delete it from RAM
                        playState = PlayStates.UNIT_SELECT;
                    }
                    else
                    {
                        mNormalMove.Invoke(GameObject.Find(selected), hit.point);
                    }

                }


            }
        }

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
                        mDeselect.Invoke();
                        SelectEvent.InvokeSafe(hit.collider.gameObject);
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
