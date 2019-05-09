using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquip 
{
    //BeginEquip
    void ItemSlots(); //certain talents will add more slot panel prefabs

    //Check Item Type
    void ItemType();



    //End Equip
    void FinishEquip(); //use this to delete extra slots for those who added them

}
