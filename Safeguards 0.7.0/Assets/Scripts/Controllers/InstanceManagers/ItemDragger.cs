using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragger : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Item item; //on instantiating this prefab, set its item value
    public static GameObject drag; //itemBeingDragged 
    public static Vector3 startPosition;
    public static Transform startParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        drag = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }


    public void OnEndDrag(PointerEventData eventData)
    {

        if (transform.parent == startParent) // || transform.parent == transform.root
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        //drag = null;
    }

    public void SetItem(Item thing)
    {
        item = thing;
        gameObject.GetComponentInChildren<Text>().text = thing.itemName;
    }


    //check against location to try and validate equip
    //if equip validates, set new location and move old one to list
    //otherwise, snap location back

    /*
    RectTransform parent = GetComponentInParent<Transform>() as RectTransform;
        ItemDragger target = null;
        foreach (GameObject go in eventData.hovered)
        {
            if (go.GetComponentInChildren<ItemDragger>() != null)
            {
                target = go.GetComponentInChildren<ItemDragger>();
            }
        }

        Debug.Log(RectTransformUtility.RectangleContainsScreenPoint(parent, Input.mousePosition));
        if (target != null) //!RectTransformUtility.RectangleContainsScreenPoint(parent, Input.mousePosition) &&
        {
            //fire the equip validation events?
        }
        else
        {
            transform.localPosition = new Vector3(45, -5, 0);
        }
        

    } */
}


