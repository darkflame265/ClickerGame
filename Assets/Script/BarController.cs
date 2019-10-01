using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{

    public Text HeroExplain;
    public Text gold_text;

    public GameObject Hero_Pay_Panel;
    public GameObject Buy_Button;
    public Text Buy_Button_text;
    public GameObject Inv;

    public GameObject NoticePanel;
    public Text condition_text;


    long Hero_Cost;
    int Hero_ID;

    [HideInInspector]
    public int goldByUpgrade; 
    public int startGoldByUpgrade = 1; //맨처음 업그레이드 비용

    [HideInInspector]
    public int startCurrentCost = 1;

    [HideInInspector]
    public float upgradePow = 1.07f; //스탯 업그레이드 폭

    public float costPow = 1.14f;//비용 업그레이드 폭

    public static bool GetBool(string name)
    {
        if(!PlayerPrefs.HasKey(name))
        {
            return false;
        }
        return PlayerPrefs.GetInt(name) == 1 ? true : false;
    }

    void Start() //Awake보다 한박자 느림
    {
        //나 자신을 가져와ㅑ 데이터 load
        //DataController.Instance.LoadUpgradeButton(this);
       StartCoroutine("check_button_able_to_buy");
    }

    IEnumerator check_button_able_to_buy()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            gold_text.text = UiManager.ToStringKR(DataController.Instance.gold);
            if(Hero_ID == 0)
            {
                Buy_Button_text.text = "" + UiManager.ToStringKR(DataController.Instance.archer_current_cost);
                if(DataController.Instance.gold >= DataController.Instance.archer_current_cost)
                {   // 구매 버튼 소지 골드에 따라 활성화 비활성화 기능 넣기
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
    }

    public void PurchaseUpgrade() 
    {
        if(Hero_ID == 0 && DataController.Instance.gold >= DataController.Instance.archer_current_cost)
        {
            if(DataController.Instance.archer_level == 20 && GetBool("ch1" + 10) == false)
            {
                NoticePanel.SetActive(true);
                NoticePanel.GetComponentInChildren<Text>().text = "조건: 1-10을 클리어";
            }
            else if(DataController.Instance.archer_level == 40 && GetBool("ch1" + 15) == false)
            {
                NoticePanel.SetActive(true);
                NoticePanel.GetComponentInChildren<Text>().text = "조건: 1-15을 클리어";
            }
            else
            {   // 구매 버튼 소지 골드에 따라 활성화 비활성화 기능 넣기
                DataController.Instance.gold -= DataController.Instance.archer_current_cost;
                DataController.Instance.archer_level++;
                long plus_hp;
                long plus_damage;
                if(DataController.Instance.archer_level >= 40)
                {
                    plus_hp = 10;
                    plus_damage = 8;
                }
                else if(DataController.Instance.archer_level >= 20)
                {
                   plus_hp = 5;
                   plus_damage = 4;
                }
                else
                {
                    plus_hp = 3;
                    plus_damage = 2;
                }
                DataController.Instance.archer_HP += plus_hp;
                DataController.Instance.archer_damage += plus_damage;
                DataController.Instance.archer_current_cost = startCurrentCost * (long) Mathf.Pow(costPow, DataController.Instance.archer_level);
                //HeroExplain.text = "궁수 " + DataController.Instance.archer_level + "\n체력: " + DataController.Instance.archer_HP + " ->100\n공격력: " + DataController.Instance.archer_damage + "  ->11";
                select_Hero_0();
                Buy_Button_text.text = "" + UiManager.ToStringKR(DataController.Instance.archer_current_cost);
            }
        }
        
    }

    public void Init_all_hero()
    {
        DataController.Instance.archer_HP = 30;
        DataController.Instance.archer_damage = 5;
        DataController.Instance.archer_current_cost = 10;
        DataController.Instance.archer_level = 1;
    }


    public void select_Hero_0()  
    {
        Hero_Pay_Panel.SetActive(true);
        HeroExplain.text = "궁수 " + DataController.Instance.archer_level + "\n체력: " + DataController.Instance.archer_HP + " ->100\n공격력: " + DataController.Instance.archer_damage + "  ->11";
        condition_text.text = "";
        if(DataController.Instance.archer_level == 20)
        {
            condition_text.text = "조건: 1-10 클리어";
            condition_text.color =  Color.black;
            if(GetBool("ch1" + 10) == false)
            {
                condition_text.color =  Color.red;
            }
            else if(GetBool("ch1" + 15) == false)
            {
                condition_text.color =  Color.red;
            }
        }
        else if(DataController.Instance.archer_level == 40)
        {
            condition_text.text = "조건: 1-15 클리어";
            condition_text.color =  Color.black;
            if(GetBool("ch1" + 15) == false)
            {
                condition_text.color =  Color.red;
            }
        }
        Hero_ID = 0;
    }

    public void select_Hero_1()
    {
        Hero_Pay_Panel.SetActive(true);
        HeroExplain.text = "고기\n효과: 공격력 5 상승\n가격: 5000Gold";
        Hero_ID = 1;
    }

    public void select_Hero_2()
    {
        Hero_Pay_Panel.SetActive(true);
        HeroExplain.text = "호박\n효과: 민첩 5 상승\n가격: 5000Gold";
        Hero_ID = 2;
    }

    public void select_Hero_3()
    {
        Hero_Pay_Panel.SetActive(true);
        HeroExplain.text = "버섯\n효과: 특성 5 상승\n가격: 5000Gold";
        Hero_ID = 3;
    }
}
