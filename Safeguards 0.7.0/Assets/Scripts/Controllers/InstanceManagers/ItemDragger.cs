using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragger : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Item item; //on instantiating this prefab, set its item value

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //check against location to try and validate equip
        //if equip validates, set new location and move old one to list
        //otherwise, snap location back
        if (false)
        {
            //swap
        }
        else
        {
            transform.localPosition = Vector3.zero;

        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
