using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


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

    public GameObject[]knight = new GameObject[0];
    public GameObject[]archer = new GameObject[0];
    public GameObject[]wizard = new GameObject[0];

    public Sprite[] skill_img_pack = new Sprite[0];

    int index = 0;
    public GameObject SelectFrame;


    //각성에 필요한 능력치 설정
    public long[] require_public = {50, 100, 150, 200, 300, 500, 600, 800, 1000, 1200, 1400, 1700, 2000};
    public long require_health;
    public long require_attack;
    public long require_mana;
    public long require_special;

    string text_health;
    string text_attack;
    string text_mana;
    string text_special;

    public long power_up_cost;

    private long[] knight_req = {100};

    public GameObject skill_panel;
    public Image skill_image;
    public Text skill_text;


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
                if(current_select_hero == "wizard")
                {
                    if(DataController.Instance.wizard_level == 10)
                    {
                        goToPanel.Instance.show_noticePanel();
                        goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "영웅이 최고레벨입니다.";
                    }
                    else {
                        DataController.Instance.wizard_level += 1;  
                    }
                    select_wizard();
                }
                SoundManager.Instance.upgrade_button_sound();
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
         DataController.Instance.wizard_level = 1;
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

    public void show_ratio_increase()
    {
        if(current_select_hero == "knight")
        {
            if(DataController.Instance.knight_level < 3)
            {
                skill_panel.SetActive(true);
                skill_image.sprite = skill_img_pack[0];
                skill_text.text = "강타\n(3레벨 흭득)";
            }
            else {
                skill_panel.SetActive(false);
            }
            if(DataController.Instance.knight_level != 10)
            {
                show_how_increase_ability_text.text = 
                "레벨:" + knight[index].GetComponent<Character>().level + " -> " + knight[index+1].GetComponent<Character>().level
                + "\n체력:" + knight[index].GetComponent<Character>().health_ratio + " -> " + knight[index+1].GetComponent<Character>().health_ratio
                + "\n공격력: " + knight[index].GetComponent<Character>().attack_ratio + " -> " + knight[index+1].GetComponent<Character>().attack_ratio
                + "\n민첩: " + knight[index].GetComponent<Character>().mana_ratio + " -> " + knight[index+1].GetComponent<Character>().mana_ratio
                + "\n스킬공격력: " + knight[index].GetComponent<Character>().special_ratio + " -> " + knight[index+1].GetComponent<Character>().special_ratio;

                char_inform_text.text =  "기사 " + " LV." + DataController.Instance.knight_level + "\n" + "체력: " + DataController.Instance.health + "    (" + text_health +")"
                + "\n공격력: " + DataController.Instance.attack + "    (" + text_attack +")"
                + "\n민첩: " + DataController.Instance.mana + "    (" + text_mana +")"
                + "\n스킬공격력: " + DataController.Instance.special + "    (" + text_special +")";
                buy_text.text = "각성    " + UiManager.ToStringKR(power_up_cost) + "  골드";
            }   else {
                show_how_increase_ability_text.text = 
                "레벨:" + knight[index].GetComponent<Character>().level
                + "\n체력:" + knight[index].GetComponent<Character>().health_ratio
                + "\n공격력: " + knight[index].GetComponent<Character>().attack_ratio
                + "\n민첩: " + knight[index].GetComponent<Character>().mana_ratio
                + "\n스킬공격력: " + knight[index].GetComponent<Character>().special_ratio;

                char_inform_text.text =  "기사 " + " LV." + "MAX(" + DataController.Instance.knight_level + ")";
                buy_text.text = "각성 최고레벨";
            }
            
        }

        else if(current_select_hero == "archer")
        {
            if(DataController.Instance.archer_level < 3)
            {
                skill_panel.SetActive(true);
                skill_image.sprite = skill_img_pack[1];
                skill_text.text = "속사\n(3레벨 흭득)";
            }
            else {
                skill_panel.SetActive(false);
            }
            if(DataController.Instance.archer_level != 10)
            {
                show_how_increase_ability_text.text = 
                "레벨:" + archer[index].GetComponent<Character>().level + " -> " + archer[index+1].GetComponent<Character>().level
                +"\n체력:" + archer[index].GetComponent<Character>().health_ratio + " -> " + archer[index+1].GetComponent<Character>().health_ratio
                + "\n공격력: " + archer[index].GetComponent<Character>().attack_ratio + " -> " + archer[index+1].GetComponent<Character>().attack_ratio
                + "\n민첩: " + archer[index].GetComponent<Character>().mana_ratio + " -> " + archer[index+1].GetComponent<Character>().mana_ratio
                + "\n스킬공격력: " + archer[index].GetComponent<Character>().special_ratio + " -> " + archer[index+1].GetComponent<Character>().special_ratio;

                char_inform_text.text =  "궁수 " + " LV." + DataController.Instance.archer_level + "\n" + "체력: " + DataController.Instance.health + "    (" + text_health +")"
                + "\n공격력: " + DataController.Instance.attack + "    (" + text_attack +")"
                + "\n민첩: " + DataController.Instance.mana + "    (" + text_mana +")"
                + "\n스킬공격력: " + DataController.Instance.special + "    (" + text_special +")";
                buy_text.text = "각성    " + UiManager.ToStringKR(power_up_cost) + "  골드";
            } else {
                show_how_increase_ability_text.text = 
                "레벨:" + archer[index].GetComponent<Character>().level
                +"\n체력:" + archer[index].GetComponent<Character>().health_ratio
                + "\n공격력: " + archer[index].GetComponent<Character>().attack_ratio
                + "\n민첩: " + archer[index].GetComponent<Character>().mana_ratio
                + "\n스킬공격력: " + archer[index].GetComponent<Character>().special_ratio;

                char_inform_text.text =  "궁수 " + " LV." + "MAX(" + DataController.Instance.archer_level + ")";
                buy_text.text = "각성 최고레벨";
            }
        }

        else if(current_select_hero == "wizard")
        {
            if(DataController.Instance.wizard_level < 3)
            {
                skill_panel.SetActive(true);
                skill_image.sprite = skill_img_pack[2];
                skill_text.text = "익스플로전\n(3레벨 흭득)";
            }
            else {
                skill_panel.SetActive(false);
            }
            if(DataController.Instance.wizard_level != 10)
            {
                show_how_increase_ability_text.text = 
                "레벨:" + wizard[index].GetComponent<Character>().level + " -> " + wizard[index+1].GetComponent<Character>().level
                +"\n체력:" + wizard[index].GetComponent<Character>().health_ratio + " -> " + wizard[index+1].GetComponent<Character>().health_ratio
                + "\n공격력: " + wizard[index].GetComponent<Character>().attack_ratio + " -> " + wizard[index+1].GetComponent<Character>().attack_ratio
                + "\n민첩: " + wizard[index].GetComponent<Character>().mana_ratio + " -> " + wizard[index+1].GetComponent<Character>().mana_ratio
                + "\n스킬공격력: " + wizard[index].GetComponent<Character>().special_ratio + " -> " + wizard[index+1].GetComponent<Character>().special_ratio;

                char_inform_text.text =  "마법사 " + " LV." + DataController.Instance.wizard_level + "\n" + "체력: " + DataController.Instance.health + "    (" + text_health +")"
                + "\n공격력: " + DataController.Instance.attack + "    (" + text_attack +")"
                + "\n민첩: " + DataController.Instance.mana + "    (" + text_mana +")"
                + "\n스킬공격력: " + DataController.Instance.special + "    (" + text_special +")";
                buy_text.text = "각성    " + UiManager.ToStringKR(power_up_cost) + "  골드";
            } else {
                show_how_increase_ability_text.text = 
                "레벨:" + wizard[index].GetComponent<Character>().level
                +"\n체력:" + wizard[index].GetComponent<Character>().health_ratio
                + "\n공격력: " + wizard[index].GetComponent<Character>().attack_ratio
                + "\n민첩: " + wizard[index].GetComponent<Character>().mana_ratio
                + "\n스킬공격력: " + wizard[index].GetComponent<Character>().special_ratio;

                char_inform_text.text =  "마법사 " + " LV." + "MAX(" + DataController.Instance.wizard_level + ")";
                buy_text.text = "각성 최고레벨";
            }   
        }

        //int skill1_id = CharacterStateController.Instance.
        if(DataController.Instance.knight_level >= 3) //전사 스킬 조건:전사 3레벨
                {
                    PlayerPrefs.SetInt(CharacterStateController.Instance.skill1_id[0], 1);
                } else {
                    PlayerPrefs.SetInt(CharacterStateController.Instance.skill1_id[0], 0);
                }

                if(DataController.Instance.archer_level >= 3) 
                {
                    PlayerPrefs.SetInt(CharacterStateController.Instance.skill1_id[1], 1);
                } else {
                    PlayerPrefs.SetInt(CharacterStateController.Instance.skill1_id[1], 0);
                }

                if(DataController.Instance.wizard_level >= 3) 
                {
                    PlayerPrefs.SetInt(CharacterStateController.Instance.skill1_id[2], 1);
                } else {
                    PlayerPrefs.SetInt(CharacterStateController.Instance.skill1_id[2], 0);
                }
        
    }

    public void select_knight()
    {
        current_select_hero = "knight";  
        for(index = 0; index < knight.Length; index++)
        {
            if(DataController.Instance.knight_level == index+1)
            {
                power_up_cost = (long)Mathf.Pow(10, index) * 1000;
                require_health = require_public[index];
                require_attack = require_public[index] / 4;
                require_mana = require_public[index] / 4;
                require_special = require_public[index] / 4;
                check_ability_and_set_color();
                show_ratio_increase();
                break;
            }
        }
        GameObject currentChar = EventSystem.current.currentSelectedGameObject.transform.gameObject;
    }

    public void select_archer()
    {
        current_select_hero = "archer";
        for(index = 0; index < archer.Length; index++)
        {
            if(DataController.Instance.archer_level == index+1)
            {
                power_up_cost = (long)Mathf.Pow(10, index) * 1000;
                require_health = require_public[index] / 4;
                require_attack = require_public[index];
                require_mana = require_public[index] / 4;
                require_special = require_public[index] / 4;
                check_ability_and_set_color();
                show_ratio_increase();
                break;
            }
        }
        GameObject currentChar = EventSystem.current.currentSelectedGameObject.transform.gameObject;

    }

    public void select_wizard()
    {
        current_select_hero = "wizard";
        for(index = 0; index < wizard.Length; index++)
        {
            if(DataController.Instance.wizard_level == index+1)
            {
                power_up_cost = (long)Mathf.Pow(10, index) * 1000;
                require_health = require_public[index] / 4;
                require_attack = require_public[index] / 4;
                require_mana = require_public[index] / 4;
                require_special = require_public[index];
                check_ability_and_set_color();
                show_ratio_increase();
                break;
            }
        }
        GameObject currentChar = EventSystem.current.currentSelectedGameObject.transform.gameObject;

    }

    public void setT_char_selectFrame(Transform transform)
    {
        var tra = transform;
        SelectFrame.SetActive(true);
        SelectFrame.transform.position = tra.position;
        SelectFrame.transform.parent = tra.transform;
    }

}
