using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterruptQuick : AttackAction
{
    public override void ExecuteCombat()
    {
        //this needs to hook into an existing combat
    }

    public override void PreviewCombat()
    {
        //since this is the interrupt attack for things like Parry/Riposte, do not have any preview interaction
    }

    public override void UndoAction()
    {
        throw new System.NotImplementedException();
    }

}
