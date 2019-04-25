using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Equip : MonoBehaviour
{
    public Item item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool IsWeapon()
    {
        if (item.GetType().ToString().Equals("Weapon"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
