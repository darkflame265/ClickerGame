using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
//using System.Collections.Generic;
using System.IO;

public class ItemDataBase : MonoBehaviour
{
    public List<Item> database = new List<Item>();
    private JsonData itemData;

    void Start()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        ConstructItemDatabase();
    }

    public Item FetchItemByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if(database[i].ID == id)
            {
                return database[i];
            }
            
        }
        return null;
        
    }

    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            database.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["value"],
            itemData[i]["description"].ToString(), (bool)itemData[i]["stackable"], (int)itemData[i]["rarity"],
            itemData[i]["slug"].ToString(), itemData[i]["effect"].ToString()));
        }

    }

}

public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set ;}
    public string Description { get; set; }
    public bool Stackable {get; set; }
    public int Rarity { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }
    public string effect { get; set; }


    public Item(int id, string title, int value, string Description, bool Stackable, int Rarity, string Slug, string effect)
    {
        this.ID = id;
        this.Title = title;
        this.Value = value;
        this.Description = Description;
        this.Stackable = Stackable;
        this.Rarity = Rarity;
        this.Slug = Slug;
        this.Sprite = Resources.Load<Sprite>("Image/Item/Icons/" + Slug);
                                            //이미지는 slug를 통해 가져옴
        this.effect = effect;
    }

    public Item()
    {
        this.ID = -1;
    }
    
    

    


}
