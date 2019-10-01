using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour, IDropHandler
{
    public int id;
    private Inventory inv;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
        if(inv.Items[id].ID == -1)
        {
            inv.Items[droppedItem.slot] = new Item(); //원래 자리에 다시 놓을수있다.
            inv.Items[id] = droppedItem.item;
            droppedItem.slot = id;
        }
        else
        {
            Transform item = this.transform.GetChild(1);
            //getChild(1)인 이유는 0번쨰에는 shade가 들어있기 때문;
            item.GetComponent<ItemData>().slot = droppedItem.slot;
            item.transform.SetParent(inv.slots[droppedItem.slot].transform);
            item.transform.position = inv.slots[droppedItem.slot].transform.position;
        
            droppedItem.slot = id;
            droppedItem.transform.SetParent(this.transform);
            droppedItem.transform.position = this.transform.position;

            inv.Items[droppedItem.slot] = item.GetComponent<ItemData>().item;
            inv.Items[id] = droppedItem.item;
        }
       
    } 
}
