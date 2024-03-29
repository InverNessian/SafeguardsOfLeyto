﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName ="Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string type;
    public int marketPrice;
    public int[] statMods = new int[7]; //order is Attack, Defense, Follow-up, Accuracy, Evasion, Critical, Guard
    public string effect;
}
