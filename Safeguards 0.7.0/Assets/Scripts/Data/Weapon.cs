using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Weapon",menuName ="Weapon")]
[System.Serializable]
public class Weapon : Item
{
    public string Damage;
    public int Mastery;
    public bool Ranged;
}
