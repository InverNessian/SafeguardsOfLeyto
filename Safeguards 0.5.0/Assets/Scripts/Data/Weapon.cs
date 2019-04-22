using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Weapon",menuName ="Weapon")]
public class Weapon : Item
{
    public string damageType;
    public int mastery; //we'll need to adjust this for hybrid weapons
    public bool ranged;
}
