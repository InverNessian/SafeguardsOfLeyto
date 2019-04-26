using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action
{
    public string aName = "Action name";
    public Sprite aIcon;
    //public List<UnitData> user;
    //public List<Vector3> movePath;
    //public AudioClip aSound;
    //public float aCooldown = 1f;

    //public abstract void Initialize(GameObject obj);
    public abstract void ProcessAction(string target); //this method will be the generic one that each individual uses to call its processes.
    public abstract void UndoAction(); //for use with the Action Log and undoing things with Caduceus
}
