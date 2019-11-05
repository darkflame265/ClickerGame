using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MagicTowerController : MonoBehaviour
{
    //마탑: 캐릭터를 각성시켜주는 시설
    //캐릭터의 스탯이 조건을 충족하면 각성할수있는 자격이 생김
    //각성에는 골드가 소모
    private static MagicTowerController instance;

    public static MagicTowerController Instance
    {
        get{
            if(instance == null)
            {
                instance = FindObjectOfType<MagicTowerController>();

                if(instance == null)
                {
                    GameObject container = new GameObject("MagicTowerController");

                    instance = container.AddComponent<MagicTowerController>();
                }
            }
            return instance;
        }
    }

    public Text gold_text;
    public Text crystal_text;

    public CharacterStateController characterStateController;

    public Text char_inform_text;
    public Text show_how_increase_ability_text;

    public Text buy_text;


    public string current_select_hero;

    public GameObject knight01;
    public GameObject knight02;

    public GameObject archer01;
    public GameObject archer02;


    //각성에 필요한 능력치 설정
    public long require_health;
    public long require_attack;
    public long require_mana;
    public long require_special;

    string text_health;
    string text_attack;
    string text_mana;
    string text_special;

    public long power_up_cost;

    void Start()
    {
        StartCoroutine("Auto");
    }

    IEnumerator Auto()
    {
        while(true)
        {   
            yield return new WaitForSeconds(0.1f);
            gold_text.text = UiManager.ToStringKR(DataController.Instance.gold);
            crystal_text.text = UiManager.ToStringKR(DataController.Instance.diamond);
        }
    }

    public void power_up()
    {
        if(DataController.Instance.gold >= power_up_cost)
        {
            if(DataController.Instance.health >= require_health && 
            DataController.Instance.attack >= require_attack && DataController.Instance.mana >= require_mana && 
            DataController.Instance.special >= require_special)
            {
                if(current_select_hero == "knight")
                {
                    if(DataController.Instance.knight_level == 10)//knight가 최고레벨인지 확인
                    {
                        goToPanel.Instance.show_noticePanel();
                        goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "영웅이 최고레벨입니다.";
                    }
                    else {
                        DataController.Instance.knight_level += 1;  //knight레벨 1증가
                    }
                    select_knight();
                }
                if(current_select_hero == "archer")
                {
                    if(DataController.Instance.archer_level == 10)
                    {
                        goToPanel.Instance.show_noticePanel();
                        goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "영웅이 최고레벨입니다.";
                    }
                    else {
                        DataController.Instance.archer_level += 1;  
                    }
                    select_archer();
                }
            }
            else {
                goToPanel.Instance.show_noticePanel();
                goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "능력치가 부족합니다.";
            }
        }
        else{
            goToPanel.Instance.show_noticePanel();
            goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "골드가 부족합니다.";
        }
    }

    public void init()
    {
        DataController.Instance.knight_level = 1;
        DataController.Instance.archer_level = 1;
    }

    void check_ability_and_set_color()
    {
        if(DataController.Instance.health >= require_health)
        {
            text_health = "<color=#00ff00>"+ require_health + "</color>";
        }
        else{
            text_health = "<color=#ff0000>" + require_health + "</color>";
        }

        if(DataController.Instance.attack >= require_attack)
        {
            text_attack = "<color=#00ff00>"+ require_attack + "</color>";
        }
        else{
            text_attack = "<color=#ff0000>" + require_attack + "</color>";
        }

        if(DataController.Instance.mana >= require_mana)
        {
            text_mana = "<color=#00ff00>"+ require_mana + "</color>";
        }
        else{
            text_mana = "<color=#ff0000>" + require_mana + "</color>";
        }

        if(DataController.Instance.special >= require_special)
        {
            text_special = "<color=#00ff00>"+ require_special + "</color>";
        }
        else{
            text_special = "<color=#ff0000>" + require_special + "</color>";
        }
    }

    public void select_knight()
    {
        current_select_hero = "knight";
        if(DataController.Instance.knight_level == 1)  //knight 1차 각성
        {
            power_up_cost = 100000;

            require_health = 150;
            require_attack = 100;
            require_mana = 90;
            require_special = 80;
            check_ability_and_set_color();
            
            show_how_increase_ability_text.text ="체력:" + knight01.GetComponent<Character>().health_ratio + " ->" + knight02.GetComponent<Character>().health_ratio
            + "\n공격력: " + knight01.GetComponent<Character>().attack_ratio + " ->" + knight02.GetComponent<Character>().attack_ratio
            + "\n마나: " + knight01.GetComponent<Character>().mana_ratio + " ->" + knight02.GetComponent<Character>().mana_ratio
            + "\n특성: " + knight01.GetComponent<Character>().special_ratio + " ->" + knight02.GetComponent<Character>().special_ratio;

            char_inform_text.text = "LV.1 \n" + "체력: " + DataController.Instance.health + "    (" + text_health +")"
            + "\n공격력: " + DataController.Instance.attack + "    (" + text_attack +")"
            + "\n마나: " + DataController.Instance.mana + "    (" + text_mana +")"
            + "\n특성: " + DataController.Instance.special + "    (" + text_special +")";
            buy_text.text = "각성    " + UiManager.ToStringKR(power_up_cost) + "  골드";
        }
        else if(DataController.Instance.knight_level == 2)
        {
            power_up_cost = 1000000;

            require_health = 500;
            require_attack = 300;
            require_mana = 200;
            require_special = 250;
            check_ability_and_set_color();
            
            show_how_increase_ability_text.text ="체력:" + knight02.GetComponent<Character>().health_ratio + " ->" + knight02.GetComponent<Character>().health_ratio
            + "\n공격력: " + knight02.GetComponent<Character>().attack_ratio + " ->" + knight02.GetComponent<Character>().attack_ratio
            + "\n마나: " + knight02.GetComponent<Character>().mana_ratio + " ->" + knight02.GetComponent<Character>().mana_ratio
            + "\n특성: " + knight02.GetComponent<Character>().special_ratio + " ->" + knight02.GetComponent<Character>().special_ratio;

            char_inform_text.text = "LV.2 \n" + "체력: " + DataController.Instance.health + "    (" + text_health +")"
            + "\n공격력: " + DataController.Instance.attack + "    (" + text_attack +")"
            + "\n마나: " + DataController.Instance.mana + "    (" + text_mana +")"
            + "\n특성: " + DataController.Instance.special + "    (" + text_special +")";
            buy_text.text = "각성    " + UiManager.ToStringKR(power_up_cost) + "  골드";
        }
        
    }

    public void select_archer()
    {
        current_select_hero = "archer";
        if(DataController.Instance.archer_level == 1)  //knight 1차 각성
        {
            power_up_cost = 100000;

            require_health = 100;
            require_attack = 200;
            require_mana = 120;
            require_special = 100;
            check_ability_and_set_color();
            
            show_how_increase_ability_text.text ="체력:" + archer01.GetComponent<Character>().health_ratio + " ->" + archer02.GetComponent<Character>().health_ratio
            + "\n공격력: " + archer01.GetComponent<Character>().attack_ratio + " ->" + archer02.GetComponent<Character>().attack_ratio
            + "\n마나: " + archer01.GetComponent<Character>().mana_ratio + " ->" + archer02.GetComponent<Character>().mana_ratio
            + "\n특성: " + archer01.GetComponent<Character>().special_ratio + " ->" + archer02.GetComponent<Character>().special_ratio;

            char_inform_text.text = "LV.1 \n" + "체력: " + DataController.Instance.health + "    (" + text_health +")"
            + "\n공격력: " + DataController.Instance.attack + "    (" + text_attack +")"
            + "\n마나: " + DataController.Instance.mana + "    (" + text_mana +")"
            + "\n특성: " + DataController.Instance.special + "    (" + text_special +")";
            buy_text.text = "각성    " + UiManager.ToStringKR(power_up_cost) + "  골드";
        }
        else if(DataController.Instance.archer_level == 2)
        {
            power_up_cost = 1000000;

            require_health = 300;
            require_attack = 600;
            require_mana = 300;
            require_special = 240;
            check_ability_and_set_color();
            
            show_how_increase_ability_text.text ="체력:" + archer02.GetComponent<Character>().health_ratio + " ->" + archer02.GetComponent<Character>().health_ratio
            + "\n공격력: " + archer02.GetComponent<Character>().attack_ratio + " ->" + archer02.GetComponent<Character>().attack_ratio
            + "\n마나: " + archer02.GetComponent<Character>().mana_ratio + " ->" + archer02.GetComponent<Character>().mana_ratio
            + "\n특성: " + archer02.GetComponent<Character>().special_ratio + " ->" + archer02.GetComponent<Character>().special_ratio;

            char_inform_text.text = "LV.2 \n" + "체력: " + DataController.Instance.health + "    (" + text_health +")"
            + "\n공격력: " + DataController.Instance.attack + "    (" + text_attack +")"
            + "\n마나: " + DataController.Instance.mana + "    (" + text_mana +")"
            + "\n특성: " + DataController.Instance.special + "    (" + text_special +")";
            buy_text.text = "각성    " + UiManager.ToStringKR(power_up_cost) + "  골드";
        }
    }

}
