using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquip 
{
    //BeginEquip
    void ItemSlots(EquipData equipData); //certain talents will add more slot panel prefabs

    //ItemSlot
    void ItemType(EquipData equipData);

    //TrainingType
    void TrainingType(EquipData equipData);

    //End Equip
    void Cleanup(EquipData equipData); //use this to delete extra slots for those who added them

}
