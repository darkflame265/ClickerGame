using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject FloatingText;
    
    public Inventory inventory;

    int result = 0;

     public void OnClick()
     {
         long goldPerClick = DataController.Instance.goldPerClick;
         DataController.Instance.gold += goldPerClick;
         //SoundManager.Instance.click_sound();
        choose_power();
        DataController.Instance.clickCount++;
     }

     public void float_text()
     {
        Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition); 

        Vector2 touchPos = new Vector2(wp.x, wp.y); 

        GameObject child = Instantiate(FloatingText);
        child.SetActive(true);
        child.transform.parent = Canvas.transform;
        child.transform.position = wp;//transform.position;
        child.GetComponent<Text>().text = "+" + UiManager.ToStringKR(DataController.Instance.goldPerClick);
     }

     public void select_rank()
    {
        int number = Random.Range(1, 10001); //1~10000 사이의 숫자
         // 0=에러 1=C 2=B 3=A 4=S 5=??


        if(number > 0 && number <=9800) //98% = 꽝
        {
            result = 1; // C등급
        }
        else if(number > 0 && number <=9900) // 1% = 아이템
        {
            result = 2; // B등급
        }
        else if(number > 0 && number <=9950) // 0.5% = 아이템(대)
        {
            result = 3; // A등급
        }
        else if(number > 0 && number <=9990) // 0.4% = 다이아
        {
            result = 4; // A등급
        }
        else if(number > 0 && number <=10000) // 0.1% = 뽑기권
        {
            result = 5; // S등급
        }
    }

    public void choose_power()
    {
        select_rank();
        Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition); 

        Vector2 touchPos = new Vector2(wp.x, wp.y); 

        
        if(result == 1)
        {
            //Debug.Log("꽝");
            SoundManager.Instance.click_sound();
        }
        if(result == 2)
        {
            SoundManager.Instance.click_get_item_sound();
            int i =Random.Range(0,4); // 0~3
            inventory.AddItem(i);

            GameObject child = Instantiate(FloatingText);
            child.SetActive(true);
            child.transform.parent = Canvas.transform;
            child.transform.position = wp;//transform.position;
            string item = inventory.database.FetchItemByID(i).Title;
            Sprite image = inventory.database.FetchItemByID(i).Sprite;

            child.GetComponent<Text>().text = "  " + item;
            child.transform.GetChild(0).gameObject.SetActive(true);
            child.GetComponentInChildren<Image>().sprite = image;
        }
        if(result == 3)
        {
            SoundManager.Instance.click_get_item_sound();
            int i = Random.Range(0,4);
            int amount = Random.Range(3, 51); // 3~50
            for(int a = 0; a < amount; a++)
            {
                inventory.AddItem(i);
            }
            GameObject child = Instantiate(FloatingText);
            child.SetActive(true);
            child.transform.parent = Canvas.transform;
            child.transform.position = wp;//transform.position;
            string item = inventory.database.FetchItemByID(i).Title;
            child.GetComponent<Text>().text = "  X" + amount;

            Sprite image = inventory.database.FetchItemByID(i).Sprite;
            child.transform.GetChild(0).gameObject.SetActive(true);
            child.GetComponentInChildren<Image>().sprite = image;
        }
        if(result == 4)
        {
            SoundManager.Instance.click_get_item_sound();
            int amount = Random.Range(3, 51); // 3~50
            DataController.Instance.diamond += amount;

            GameObject child = Instantiate(FloatingText);
            child.SetActive(true);
            child.transform.parent = Canvas.transform;
            child.transform.position = wp;//transform.position;
            child.GetComponent<Text>().text = "   +" + amount;

            child.transform.GetChild(0).gameObject.SetActive(true);
            child.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Image/UI/Freeui/ZOSMA/Main/Cristal");
            //child.GetComponentInChildren<Image>().transform.localScale = new Vector3(0.6f, 0.6f, 1);
        }

    }
            

    
}
