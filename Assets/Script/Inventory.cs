﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    GameObject inventoryPanel;
    public GameObject slotPanel;
    public ItemDataBase database;  //public 빼
    ItemData itemdata;
    Item item;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    public Text current_item_name;

    public int selected_item_id = -1;

    int slotAmount;


    public List<Item> Items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    private static Inventory instance;

    public static Inventory Instance
    {
        get{
            if(instance == null)
            {
                instance = FindObjectOfType<Inventory>();

                if(instance == null)
                {
                    GameObject container = new GameObject("Inventory");

                    instance = container.AddComponent<Inventory>();
                }
            }
            return instance;
        }
    }

    void Start()
    {
        database = GetComponent<ItemDataBase>();
        itemdata = GetComponent<ItemData>();
        slotAmount = 18;
        //inventoryPanel = GameObject.Find("Inventory_Panel");
        //slotPanel = inventoryPanel.transform.Find("Slot_Panel").gameObject;
        for(int i = 0; i < slotAmount; i++)
        {
            Items.Add(new Item());
            slots.Add(Instantiate(inventorySlot)); //
            slots[i].GetComponent<Slot>().id = i;
            slots[i].transform.SetParent(slotPanel.transform);
            slots[i].transform.localScale = Vector3.one; //생성시 스케일이 이상하게 되서 부모 스케일을 따라함;
                                                //스케일을 1로 설정
        }
        InitItem();
        LoadItem();
       
  
    }

    public void InitItem()
    {
        //Debug.Log("Items.Count is " + Items.Count);
        for(int i = 0; i < database.database.Count; i++) //Items.Count
        {
            Item itemToAdd = database.FetchItemByID(i);
            
            string key = itemToAdd.Title;

                Items[i] = itemToAdd;
                GameObject itemObj = Instantiate(inventoryItem); //아이템 생성
                itemObj.GetComponent<ItemData>().item = itemToAdd;
                itemObj.GetComponent<ItemData>().slot = i;
                itemObj.transform.SetParent(slots[i].transform); //부모를 [i]번째 슬릇으로 설정
                itemObj.transform.position = Vector2.zero;
                itemObj.transform.localScale = Vector3.one;
                itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite; //아이템 이미지 가져오기
                itemObj.name = itemToAdd.Title;
                //itemObj.gameObject.SetActive(false);
                ItemData data = slots[i].transform.GetChild(1).GetComponent<ItemData>();
                //data.amount++;
                data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
            if(PlayerPrefs.GetInt(key + "_amount") < 1) //아이템의 amount수가 1이하가 될시
            {
                itemObj.gameObject.SetActive(false);
            }
        }
    }    
    
    public void LoadItem()
    { 
        
        for(int i = 0; i < database.database.Count; i++) //Items.Count
        {
            Item itemToAdd = database.FetchItemByID(i);
            
            string key = itemToAdd.Title;
            if(PlayerPrefs.GetInt(key + "_amount") != 0)
            {
                for(int a = 0; a < PlayerPrefs.GetInt(key + "_amount"); a++)
                {
                    /* 
                    Items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem); //아이템 생성
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().slot = i;
                    itemObj.transform.SetParent(slots[i].transform); //부모를 [i]번째 슬릇으로 설정
                    itemObj.transform.position = Vector2.zero;
                    itemObj.transform.localScale = Vector3.one;
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite; //아이템 이미지 가져오기
                    itemObj.name = itemToAdd.Title;
                    */
                    ItemData data = slots[i].transform.GetChild(1).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                }
            }
            
        }    
 
        

    }

    public void debug_only()
    {

            for(int i = 0; i < Items.Count; i++)
            {
                Item itemToAdd = database.FetchItemByID(i);
                string key = itemToAdd.Title;
                Debug.Log(i + "is " + PlayerPrefs.GetInt(key + "_amount"));
            }
          
        
    }

    public void SaveItem()
    {

    }

    public void AddItem(int id)
    {
        Item itemToAdd = database.FetchItemByID(id);
        //Debug.Log("itemToAdd.ID[1] : " + itemToAdd.ID);
        if(itemToAdd.Stackable&& CheckIfItemIsInventory(itemToAdd) == true) //인벤토리에 아이템이 이미 존재하면
        {
            for(int i = 0; i < Items.Count; i++)
            {
                if(Items[i].ID == id)
                {
                    string key = itemToAdd.Title; //현재 아이템 이름 
                    ItemData data = slots[i].transform.GetChild(1).GetComponent<ItemData>();
                    //0번쨰가 아닌 1번쨰에 Item이 들어가므로 getChild(1)로 설정 0은 shader가 차지;
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    data.gameObject.SetActive(true);
                    PlayerPrefs.SetInt(key + "_amount", data.amount); //아이템 amount 저장
                    break;
                }
            }
        }
        else //인벤토리에 아이템이 존재하지 않으면
        {
            for(int i =0; i < Items.Count; i++)
            {
                if(Items[i].ID == -1) //조건: 
                {
                    string key = itemToAdd.Title;
                    Items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem); //아이템 생성
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().slot = i;
                    itemObj.transform.SetParent(slots[i].transform); //부모를 [i]번째 슬릇으로 설정
                    itemObj.transform.position = Vector3.zero;
                    itemObj.transform.localScale = Vector3.one;
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite; //아이템 이미지 가져오기
                    itemObj.name = itemToAdd.Title;
                    ItemData data = slots[i].transform.GetChild(1).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    
                    itemObj.transform.position = Vector3.zero;
                    PlayerPrefs.SetInt(key + "_amount", data.amount);
                    break;
                }
                
            }
        }
    }


    bool CheckIfItemIsInventory(Item item)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if(Items[i].ID == item.ID) //Items[i].ID == item.ID
            {
                return true;
            }
            
        }
        return false;
    }

    public void useItem()
    {
        Item selected_item = database.FetchItemByID(selected_item_id);

        if(selected_item.Stackable && CheckIfItemIsInventory(selected_item) == true) //stackable이 true일때
        {
            ItemData data = slots[selected_item_id].transform.GetChild(1).GetComponent<ItemData>();


            if(data.amount >= 1)
            {
                use_item_effect(selected_item_id); //아이템 id에 따른 아이템 효과 사용
                data.amount--; 
                data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                PlayerPrefs.SetInt(selected_item.Title + "_amount", data.amount);
            }
           
            if(data.amount < 1) //아이템의 amount수가 1이하가 될시
            {

                Transform this_item = slots[selected_item_id].transform.GetChild(1); //transform형식으로 아이템을 받아옴
                this_item.gameObject.SetActive(false);
                //data.amount--;
                PlayerPrefs.SetInt(selected_item.Title + "_amount", data.amount);
                //Items[selected_item_id].ID = -2;      //ID를 -1로 바꿔야 Slot.cs에서 다 쓴 아이템 자리를 빈 자리(-1)로 인식한다.
                //Destroy(this_item.gameObject); //받아온 transform형식의 아이템을 gameobject형식으로 바꾸고 제거
            }
        }

    
        
    }

    public void use_item_effect(int id)
    {
        switch(id)
        {
            case 0: //사과
                DataController.Instance.health += 5;
                break;
            case 1: //고기
                DataController.Instance.attack += 5;
                break;
            case 2: //호박
                DataController.Instance.mana += 5;
                break;
            case 3: //버섯
                DataController.Instance.special += 5;
                break;
            case 4: //버섯
                DataController.Instance.current_exp += DataController.Instance.max_exp;
                break;

        }
    }
}