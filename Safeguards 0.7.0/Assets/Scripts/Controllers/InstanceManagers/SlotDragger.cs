using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotDragger : MonoBehaviour, IDropHandler
{
    public GameObject ItemElement
    { get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        //we have to work with this one because if we don't, then the way we return ItemElement gets fucked because we have 2 children
        ItemElement.transform.position = ItemDragger.startPosition;
        ItemElement.transform.SetParent(ItemDragger.startParent);

        //now we can set new parent and position
        ItemDragger.drag.transform.SetParent(gameObject.transform);
        ItemDragger.drag.transform.position = ItemDragger.drag.transform.parent.position;
    }
}
