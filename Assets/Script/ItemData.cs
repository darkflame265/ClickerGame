using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler//, IBeginDragHandler, IDragHandler,IEndDragHandler, 
{
    public Item item;
    public int amount;
    public int slot;
    

    private GameObject item_text;
    public Image item_slot_color;
    Color original_color;
    

    private Inventory inv;
    private ItemData itemdata;
    private ToolTip tooltip;
    private Vector3 offset;

    

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        
        tooltip = inv.GetComponent<ToolTip>();
        //StartCoroutine("check_item_amount");
    }

    /*
    public void OnBeginDrag(PointerEventData eventData)
    { 
        if(item != null)
        {
            
            Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition); 

            Vector2 touchPos = new Vector2(wp.x, wp.y); 

            transform.position = touchPos;
            offset = touchPos - new Vector2(this.transform.position.x, this.transform.position.y);
            this.transform.SetParent(this.transform.parent.parent);
            //this.transform.position = transform.position;//-offset;
            

            
            
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        if(item != null)
        {
            Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition); 

            Vector2 touchPos = new Vector2(wp.x, wp.y); 

            transform.position = touchPos;
            
            this.transform.position = transform.position-offset; // - offset
            
        }
        
    
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(inv.slots[slot].transform);
        this.transform.position = inv.slots[slot].transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    */

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }
     
    public void show_item_name()
    {
        GameObject[] s = GameObject.FindGameObjectsWithTag("slot");
        Color m_colopr = new Color(0.6f, 0.6f, 0.6f, 1);
        
        
        //if(item_slot_color != null)
        //{
            for(int i =0; i < inv.Items.Count; i++)
            {
                s[i].GetComponent<Image>().color = m_colopr;
            }
            //s[inv.selected_item_id].GetComponent<Image>().color = m_colopr;
        //}
         

        item_slot_color = this.transform.parent.GetComponent<Image>();
        original_color = item_slot_color.color;

        itemdata = GetComponent<ItemData>();
        item_text = GameObject.Find("Item_name_text");
        item_text.GetComponent<Text>().text = this.transform.name;
        inv.selected_item_id = itemdata.slot;
        
        item_slot_color.color = Color.gray;
        
    }

    public string return_item_name()
    {
        return item_text.GetComponent<Text>().text;
    }

    

}
