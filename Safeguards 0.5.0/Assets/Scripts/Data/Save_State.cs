using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu(menuName = "SaveState", fileName = "SaveState")]
public class Save_State : ScriptableObject
{
    public string id;
    public string test; //for testing purposes

    public int current_mission = 0; //we're doing it this way so that the game doesn't have to store as many values
    public int[] completed_secondary_objectives = new int[30]; //doing it this way allows us to store the number of completed secondary objectives.  
                                                               //It hopefully won't matter which particular ones are completed?  if it does, though, we can use the below.
                                                               //public Dictionary<string, bool> secondary_objectives = new Dictionary<string, bool>(); //initialize it so that it can be added to later.
    public int bonusEXP = 0;
    public List<Item> convoy = new List<Item>();
}
