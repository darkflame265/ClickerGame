using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GeneralStoreController : MonoBehaviour
{
    public Text ProductExplain;
    public Text gold_text;
    public Text crystal_text;

    public GameObject Product_Pay_Panel;
    public GameObject Buy_Button;
    public GameObject Inv;

    long Product_Cost;
    long Product_Crystal;
    int Product_ID;

    long current_heart = 2;
    long max_heart = 3;

    public float LimitTime;
    public Text text_Timer;
    public Text text_Heart;

    public GameObject content0;
    public GameObject content1;

    public static bool GetBool(string name)
    {
        if(!PlayerPrefs.HasKey(name))
        {
            return false;
        }
        return PlayerPrefs.GetInt(name) == 1 ? true : false;
    }

    void Start()
    {
        LimitTime = DataController.Instance.limit_time - DataController.Instance.timeAfterLastPlay;
        current_heart = DataController.Instance.current_heart;
        max_heart = DataController.Instance.max_heart;
        StartCoroutine("check_button_able_to_buy");
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        while(true)
        {
            while(LimitTime < 0)
                {
                    //int count = (int)LimitTime / DataController.Instance.timeAfterLastPlay;
                    //current_heart += count;
                   // count = 0;
                    LimitTime += 1200;  //LifeTime - 지나간 시간 라이프타임 = 1200;  
                    current_heart += 1;
                }
            if(current_heart < max_heart) //충전 1200초 = 20분
            {
                
                LimitTime -= 1;
                //text_Timer.text = "충전시간 : " + Mathf.Round(LimitTime)/60 + "분 " + Mathf.Round(LimitTime)/60%60 + "초";
                text_Timer.text = "충전시간 : " + Math.Truncate(Mathf.Round(LimitTime)/60)  + "분 " + Math.Truncate(Mathf.Round(LimitTime)%60%60) + "초";
                
            }
            else //하트가 충전되면
            {
                text_Timer.text = "20분마다 1회 충전";
                current_heart = max_heart;
                LimitTime = 1200;
            }
            DataController.Instance.limit_time = LimitTime;
            DataController.Instance.current_heart = current_heart;
            DataController.Instance.max_heart = max_heart;
            yield return new WaitForSeconds(1f);
        }
    }

    public void debug_only()
    {
        Debug.Log("LimitTime is " + LimitTime);
        Debug.Log("data_current_heart is " + DataController.Instance.current_heart);
    }

    public void reset()
    {
        current_heart = 3;
        max_heart = 3;
    }

    IEnumerator check_button_able_to_buy()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            text_Heart.text = "구매기회: " + current_heart + "/" + max_heart;
            gold_text.text = UiManager.ToStringKR(DataController.Instance.gold);
            crystal_text.text = UiManager.ToStringKR(DataController.Instance.diamond);
            if(DataController.Instance.gold >= Product_Cost && DataController.Instance.diamond >= Product_Crystal && current_heart > 0)
            {
                Buy_Button.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                Buy_Button.GetComponent<Button>().interactable = true;
            }
            else
            {
                Buy_Button.GetComponent<Image>().color = new Color(1, 1, 1, 255);
                Buy_Button.GetComponent<Button>().interactable = false;
            } 
        }
    }

    public void hide_all_product()
    {
        content0.SetActive(false);
        content1.SetActive(false);
    }

    public void show_another_product() //왼쪽 일반 창
    {
        hide_all_product();
        content0.gameObject.SetActive(true);
    }
    public void show_another_product1() //오른쪾 vip 창
    {
        if(DataController.Instance.char_level >= 50)
        {
            hide_all_product();
            content1.gameObject.SetActive(true);
        } else {
            //사용자 레벨이 50이하라 입장할수 없습니다.
            goToPanel.Instance.show_noticePanel();
            goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "VIP상점은 사용자 레벨 50 이상부터 입장 가능합니다.";
            goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().fontSize = 73;
        }
    }

    public void Buy_Product()
    {
        if(Product_Cost == 0 && Product_Crystal != 0)
        {
            DataController.Instance.diamond -= Product_Crystal; //상품가격만큼 gold 차감
            Inv.GetComponent<Inventory>().AddItem(Product_ID); //인벤토리에 상품 추가
            current_heart -= 1;
        }
        
        else if(DataController.Instance.gold > Product_Cost && current_heart > 0)
        {
            Debug.Log("its'");
            DataController.Instance.gold -= Product_Cost; //상품가격만큼 gold 차감
            Inv.GetComponent<Inventory>().AddItem(Product_ID); //인벤토리에 상품 추가
            current_heart -= 1;
        }
    }

    public void select_product_0()
    {
        Product_Pay_Panel.SetActive(true);
        ProductExplain.text = "사과\n효과: 체력 5 상승\n가격: 5000Gold";
        Product_Cost = 5000;
        Product_Crystal = 0;
        Product_ID = 0;
    }

    public void select_product_1()
    {
        Product_Pay_Panel.SetActive(true);
        ProductExplain.text = "고기\n효과: 공격력 5 상승\n가격: 5000Gold";
        Product_Cost = 5000;
        Product_Crystal = 0;
        Product_ID = 1;
    }

    public void select_product_2()
    {
        Product_Pay_Panel.SetActive(true);
        ProductExplain.text = "호박\n효과: 민첩 5 상승\n가격: 5000Gold";
        Product_Cost = 5000;
        Product_Crystal = 0;
        Product_ID = 2;
    }

    public void select_product_3()
    {
        Product_Pay_Panel.SetActive(true);
        ProductExplain.text = "버섯\n효과: 특성 5 상승\n가격: 5000Gold";
        Product_Cost = 5000;
        Product_Crystal = 0;
        Product_ID = 3;
    }

    public void select_product_4()
    {
        Product_Pay_Panel.SetActive(true);
        ProductExplain.text = "레벨업 포션\n효과: 현재 레벨에 필요한 양 만큼 경험치를 얻습니다.\n가격: 100Crystal";
        Product_Cost = 0;
        Product_Crystal = 100;
        Product_ID = 4;
    }

}
