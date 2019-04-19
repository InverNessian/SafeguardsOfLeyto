using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public UnitData unitData;
    

    // Start is called before the first frame update
    void Awake()
    {
        if(unitData == null)
        {
            //for the most part, we can have enemies already created in the scene, so that they won't need to be created.
            //this means we can assume that only Characters will reach this point with a null value for unitData
            //and, since Characters will have their objects in the scene as well, we can just not put a stat block in there for anyone who is returning.
            unitData = UnitData.LoadData(gameObject.name);
            //characters will always have their object named after them.

            //for best design practicies, the starter stats object for each character should only be in the first mission where they appear
        }
    }

}
