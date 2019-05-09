using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMenu : MonoBehaviour
{
    public static GameObject unitMenu;
    //this is literally just making sure the unit menu only has 1 instance
    //maybe I should make this "enforce singleton" class name
    void Awake()
    {
        if(unitMenu != null)
        {
            Debug.Log(unitMenu.gameObject.name);
            Destroy(unitMenu.gameObject);
        }
        unitMenu = this.gameObject;
    }

}
