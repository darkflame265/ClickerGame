using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BattleManager : MonoBehaviour
{

    bool IsPause;
    bool getResult = false;
    bool getMoney = false;
    bool getExp = false;
    bool getCrystal = false;
    bool getItem = false;
    bool timer_operate = false;

    public Inventory inventory;
    int ItemNum = 0;

    public long random_get_crystal = 0;

    private string notice_string;
    private int reward_item_id;
    private int reward_item_count;

    public GameObject result_crystal_panel;

    //ui
    public GameObject Fight_SelectFrame;
    public Text show_current_stage;
    public Text Stage_explain;
    public GameObject dps_probability_panel;
    public GameObject infinity_probability_panel;
    public GameObject tower_btn;

    public Text battle_result_title_text;
    public Text stage_gold_result;
    public Text stage_exp_result;
    public Text stage_crystal_result;
    public Text stage_resource_result;

    public Text gold_result_text;
    public Text exp_result_text;
    public Image Item_img;
    public Text Item_result_text;

    public Transform BattleScene_panel;

    public Transform hero_spawn_point0;
    public Transform hero_spawn_point1;
    public Transform hero_spawn_point2;

    public Transform Spawn_point0;
    public Transform Spawn_point1;
    public Transform Spawn_point2;

    public GameObject Map1;
    public GameObject mapImage;

    public GameObject Front;
    public GameObject Mid;
    public GameObject Back;

    public Image hero_0_img;
    public Text hero_0_hp_text;
    public Text hero_0_atk_text;

    public Image hero_1_img;
    public Text hero_1_hp_text;
    public Text hero_1_atk_text;

    public Image hero_2_img;
    public Text hero_2_hp_text;
    public Text hero_2_atk_text;

    public Image monster_0_img;
    public Text monster_0_hp_text;
    public Text monster_0_atk_text;

    public Image monster_1_img;
    public Text monster_1_hp_text;
    public Text monster_1_atk_text;

    public Image monster_2_img;
    public Text monster_2_hp_text;
    public Text monster_2_atk_text;

    public Text infinity_grade_text;

    //hero
    public GameObject dummy;

    public GameObject knight;
    public GameObject archer;
    public GameObject wizard;

    public GameObject[] knight_pack = new GameObject[0];
    public GameObject[] archer_pack = new GameObject[0];
    public GameObject[] wizard_pack = new GameObject[0];

    public GameObject[] monster_pack = new GameObject[0];

    //Enemy
    public GameObject Scarecrow;     //dpstest   
    public GameObject Bat_level1;
    public GameObject Bat_level2;
    public GameObject Spider;
    public GameObject Gorem;
    public GameObject Big_Gorem;
    public GameObject Troll;
    public GameObject goblin_general;
    public GameObject summon_portal;
    public GameObject plant_monster;
    public GameObject ice_gorem;
    public GameObject warm_monster;
    public GameObject stoneFighter;
    public GameObject blueDragonic;
    public GameObject meduza;
    public GameObject minotaurse;
    public GameObject Bat_Bose;
    public GameObject crazy_portal_Bose;
    public GameObject Bose_scareCrow;


    public float LimitTime;
    public Text text_Timer;
    public Text text_damage;
    public long total_damage;

    public long tower_previous_stage;

    //
    GameObject hero_0 = null;
    GameObject hero_1 = null;
    GameObject hero_2 = null;
    GameObject monster_0 = null;
    GameObject monster_1 = null;
    GameObject monster_2 = null;
    long hero_0_name;
    long hero_1_name;
    long hero_2_name;
    string monster_0_name;
    string monster_1_name;
    string monster_2_name;

    //sprite
    public Sprite x_img;
    public Sprite[] booty_pack = new Sprite[0];
    public Sprite[] map_pack = new Sprite[0];

    public FightController fightController;

    private static BattleManager instance;

    public static BattleManager Instance
    {
        get{
            if(instance == null)
            {
                instance = FindObjectOfType<BattleManager>();

                if(instance == null)
                {
                    GameObject container = new GameObject("BattleManager");

                    instance = container.AddComponent<BattleManager>();
                }
            }
            return instance;
        }
    }
 
    // Use this for initialization
    void Start () {
        IsPause = false;
        StartCoroutine("Auto");
        StartCoroutine("Timer");
        if(DataController.Instance.current_stage == -2)
        {
            StartCoroutine("infinity_mode_set");
        }

        getResult = false;
        ItemNum = DataController.Instance.itemNum;
        SetBool("getResult", false);

    }

    public static void SetBool(string name, bool booleanValue)
    {
        PlayerPrefs.SetInt(name, booleanValue ? 1 : 0);
    }

    public static bool GetBool(string name)
    {
        if(!PlayerPrefs.HasKey(name))
        {
            return false;
        }
        return PlayerPrefs.GetInt(name) == 1 ? true : false;
    }

    public static bool GetBool(string name, bool defaultValue)
    {
        if(PlayerPrefs.HasKey(name)) 
    {
            return GetBool(name);
        }
    
        return defaultValue;
    }

    public void InitStage()
    {
        for(int i = 0; i < FightController.Instance.stageCh1.Length; i++) // 21을 max_current_Stage추가해서 교체
            {
                SetBool("ch1" + i, false);
            }
        SetBool("ch1" + 0, true);    //ch1-1만 활성화
        PlayerPrefs.SetInt("userMaxStage", 1);
        DataController.Instance.tower_stage = 1;
        FightController.Instance.check_stage();
        Debug.Log("usemaxstage : " + PlayerPrefs.GetInt("userMaxStage", FightController.Instance.stageCh1.Length));
    }

    public void clear_all_stage()
    {
        for(int i = 0; i < FightController.Instance.stageCh1.Length; i++) // 21을 max_current_Stage추가해서 교체
            {
                SetBool("ch1" + i, true);
            }
        SetBool("ch1" + 0, true);    //ch1-1만 활성화
        FightController.Instance.check_stage();
        PlayerPrefs.SetInt("userMaxStage", FightController.Instance.stageCh1.Length);
        Debug.Log("usemaxstage : " + PlayerPrefs.GetInt("userMaxStage", FightController.Instance.stageCh1.Length));
    }

    public void debug_stage()
    {
        for(int i = 0; i < FightController.Instance.stageCh1.Length; i++) // 21을 max_current_Stage추가해서 교체
            {
                Debug.Log("ch1 " + i + " is " + GetBool("ch1" + i));
            }
        FightController.Instance.check_stage();
    }

    public void debug_monster_despose()
    {
        if(DataController.Instance.monster_0_ID != null)
        {
            Debug.Log("spawn 0 is " + DataController.Instance.monster_0_ID);
        }

        if(DataController.Instance.monster_1_ID != null)
        {
            Debug.Log("spawn 1 is " + DataController.Instance.monster_1_ID);
        }

        if(DataController.Instance.monster_2_ID != null)
        {
            Debug.Log("spawn 2 is " + DataController.Instance.monster_2_ID);
        }

    }

    // public int require_max_stage()
    // {
    //     int a = 0;
    //     for(int i = 0; i < FightController.Instance.stageCh1.Length; i++) // 21을 max_current_Stage추가해서 교체
    //     {
    //         if(GetBool("ch1" + i) == false)
    //         {
    //             a = i;
    //             break;
    //         }
    //     }
    //     if(a == 0) a = FightController.Instance.stageCh1.Length;
    //     return a;
    // }     //stage의 interactive담당 스크립트는 어디?

    public void set_reward_item(int id, int count, string notice_text)
    {
        reward_item_id = id;
        goToPanel.Instance.BigNoticePanel.GetComponentInChildren<Text>().text = notice_string;
    }

    public void reward_item()
    {
        goToPanel.Instance.BigNoticePanel.SetActive(true);
        for(int a = 0; a < reward_item_count; a++)
        {
            Inventory.Instance.AddItem(reward_item_id);
        }
    }

    

    void set_fight_reward()
    {
        if(DataController.Instance.current_stage <= 20)
        {
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 1000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 1;
        }
        else if(DataController.Instance.current_stage <= 40)
        {
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 10000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 5;
        }
        else if(DataController.Instance.current_stage <= 60)
        {
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 100000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 10;
        }
        else if(DataController.Instance.current_stage <= 80)
        {
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 250000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 30;
        }
        else if(DataController.Instance.current_stage <= 100)
        {
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 500000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 50;
        }
        else if(DataController.Instance.current_stage <= 120)
        {
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 1000000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 70;
        }
        else if(DataController.Instance.current_stage <= 140)
        {
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 2500000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 90;
        }
        else if(DataController.Instance.current_stage <= 160)
        {
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 5000000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 100;
        }
        else if(DataController.Instance.current_stage <= 180)
        {
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 7500000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 110;
        }
        else if(DataController.Instance.current_stage <= 200)
        {
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 10000000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 120;
        }
        else if(DataController.Instance.current_stage <= 220)
        {
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 30000000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 130;
        }
        else if(DataController.Instance.current_stage <= 240)
        {
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 50000000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 140;
        }
        else if(DataController.Instance.current_stage <= 260)
        {
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 75000000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 150;
        }
        else if(DataController.Instance.current_stage <= 280)
        {
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 100000000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 160;
        }
        else if(DataController.Instance.current_stage <= 300)
        {
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 250000000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 170;
        }

        if(GetBool("ch1" + DataController.Instance.current_stage) == true)       //이미 클리어한 스테이지면 보상이 10분의 1
        {
            DataController.Instance.get_stage_gold = DataController.Instance.get_stage_gold / 10;
            DataController.Instance.get_stage_exp = DataController.Instance.get_stage_exp / 10;
        }
        //Debug.Log("DataController.Instance.current_stage : " + DataController.Instance.current_stage);
        //Debug.Log("DataController.Instance.get_stage_gold : " + DataController.Instance.get_stage_gold);

        
        //ItemNum = 0;
    }



    void set_background_image()
    {
        //Debug.Log("DataController.Instance.current_stage : " + DataController.Instance.current_stage);
        if(DataController.Instance.current_stage > 300)
        {
            Map1.SetActive(false);
            mapImage.SetActive(true);
            mapImage.GetComponent<Image>().sprite = map_pack[0];
        }
        else if(DataController.Instance.current_stage > 280)
        {
            Map1.SetActive(false);
            mapImage.SetActive(true);
            mapImage.GetComponent<Image>().sprite = map_pack[17];
        }
        else if(DataController.Instance.current_stage > 260)
        {
            Map1.SetActive(false);
            mapImage.SetActive(true);
            mapImage.GetComponent<Image>().sprite = map_pack[16];
        }
        else if(DataController.Instance.current_stage > 240)
        {
            Map1.SetActive(false);
            mapImage.SetActive(true);
            mapImage.GetComponent<Image>().sprite = map_pack[15];
        }
        else if(DataController.Instance.current_stage > 220)
        {
            Map1.SetActive(false);
            mapImage.SetActive(true);
            mapImage.GetComponent<Image>().sprite = map_pack[14];
        }
        else if(DataController.Instance.current_stage > 200)
        {
            Map1.SetActive(false);
            mapImage.SetActive(true);
            mapImage.GetComponent<Image>().sprite =  map_pack[13];
        }
        else if(DataController.Instance.current_stage > 180)
        {
            Map1.SetActive(false);
            mapImage.SetActive(true);
            mapImage.GetComponent<Image>().sprite = map_pack[10];
        }
        else if(DataController.Instance.current_stage > 160)
        {
            Map1.SetActive(false);
            mapImage.SetActive(true);
            mapImage.GetComponent<Image>().sprite = map_pack[9];
        }
        else if(DataController.Instance.current_stage > 140)
        {
            Map1.SetActive(false);
            mapImage.SetActive(true);
            mapImage.GetComponent<Image>().sprite = map_pack[8];
        }
        else if(DataController.Instance.current_stage > 120)
        {
            Map1.SetActive(false);
            mapImage.SetActive(true);
            mapImage.GetComponent<Image>().sprite = map_pack[7];
        }
        else if(DataController.Instance.current_stage > 100)
        {
            Map1.SetActive(false);
            mapImage.SetActive(true);
            mapImage.GetComponent<Image>().sprite = map_pack[6];
        }
        else if(DataController.Instance.current_stage > 80)
        {
            Map1.SetActive(false);
            mapImage.SetActive(true);
            mapImage.GetComponent<Image>().sprite = map_pack[5];
        }
        
        else if(DataController.Instance.current_stage > 60)
        {
            Map1.SetActive(false);
            mapImage.SetActive(true);                           
            mapImage.GetComponent<Image>().sprite = map_pack[4];
        }
        else if(DataController.Instance.current_stage > 40)
        {
            Map1.SetActive(false);
            mapImage.SetActive(true);           
            mapImage.GetComponent<Image>().sprite = map_pack[3];
        }
        
        else if(DataController.Instance.current_stage > 20)
        {
            Map1.SetActive(false);
            mapImage.SetActive(true);
            mapImage.GetComponent<RectTransform>().offsetMax = new Vector2(mapImage.GetComponent<RectTransform>().offsetMax.x, 330f);
            mapImage.GetComponent<Image>().sprite = map_pack[2];
        }
        else if(DataController.Instance.current_stage >= 1)
        {
            Map1.SetActive(true);
            mapImage.SetActive(false);
        }
        
        else if(DataController.Instance.current_stage == 0)
        {
            Map1.SetActive(false);
            mapImage.SetActive(true);
            mapImage.GetComponent<Image>().sprite = map_pack[1];
        }
        else if(DataController.Instance.current_stage == -1)
        {
            Map1.SetActive(false);
            mapImage.SetActive(true);
            mapImage.GetComponent<RectTransform>().offsetMax = new Vector2(mapImage.GetComponent<RectTransform>().offsetMax.x, 330f);
            mapImage.GetComponent<Image>().sprite = map_pack[0];
        }
        else if(DataController.Instance.current_stage == -2)        //무한모드
        {
            Map1.SetActive(false);
            mapImage.SetActive(true);
            mapImage.GetComponent<RectTransform>().offsetMax = new Vector2(mapImage.GetComponent<RectTransform>().offsetMax.x, 330f);
            mapImage.GetComponent<Image>().sprite = map_pack[1];
        }
    }

    void show_state_in_windows()
    {
        //hero
        if(Front.transform.childCount != 0)
        {
            hero_2_img.GetComponent<Image>().sprite = Front.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite;
        } else  hero_2_img.GetComponent<Image>().sprite =x_img;

        if(Mid.transform.childCount != 0)
        {
            hero_1_img.GetComponent<Image>().sprite = Mid.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite;
        } else  hero_1_img.GetComponent<Image>().sprite = x_img;
        if(Back.transform.childCount != 0)
        {
            hero_0_img.GetComponent<Image>().sprite = Back.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite;
        } else  hero_0_img.GetComponent<Image>().sprite = x_img;

        if(hero_spawn_point0.transform.childCount != 0)
        {
            if(PowerController.Instance.return_power_list(18) == 1)
            {
                hero_0_hp_text.GetComponent<Text>().text = "HP: " + hero_spawn_point0.GetComponentInChildren<Character>().current_HP.ToString() + "<color=#8b33dd>" + "(+" + hero_spawn_point0.GetComponentInChildren<Character>().current_Shield.ToString() + ")" + "</color>";
            }
            else {
                hero_0_hp_text.GetComponent<Text>().text = "HP: " + hero_spawn_point0.GetComponentInChildren<Character>().current_HP.ToString();
            }
            hero_0_atk_text.GetComponent<Text>().text = "ATK: " + hero_spawn_point0.GetComponentInChildren<Character>().striking_power.ToString();
            if(hero_spawn_point0.GetComponentInChildren<Character>().current_HP <= 0)
            {
                hero_0_hp_text.GetComponent<Text>().text = "HP: " + "<color=#ff0000>" + 0 + "</color>";
            }
        }
        //Debug.Log("hero_spawn_point1.transform.childCount is " + hero_spawn_point1.transform.childCount);
        if(hero_spawn_point1.transform.childCount != 0)
        {
            //hero_1_img.GetComponent<Image>().sprite = Mid.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite;
            if(PowerController.Instance.return_power_list(18) == 1)
            {
                hero_1_hp_text.GetComponent<Text>().text = "HP: " + hero_spawn_point1.GetComponentInChildren<Character>().current_HP.ToString() + "<color=#8b33dd>" + "(+" + hero_spawn_point1.GetComponentInChildren<Character>().current_Shield.ToString() + ")" + "</color>";
            }
            else {
                hero_1_hp_text.GetComponent<Text>().text = "HP: " + hero_spawn_point1.GetComponentInChildren<Character>().current_HP.ToString();
            }
            hero_1_atk_text.GetComponent<Text>().text = "ATK: " + hero_spawn_point1.GetComponentInChildren<Character>().striking_power.ToString();
            if(hero_spawn_point1.GetComponentInChildren<Character>().current_HP <= 0)
            {
                hero_1_hp_text.GetComponent<Text>().text = "HP: " + "<color=#ff0000>" + 0 + "</color>";
            }
        }
        if(hero_spawn_point2.transform.childCount != 0)
        {
        

            if(PowerController.Instance.return_power_list(18) == 1)
            {
                hero_2_hp_text.GetComponent<Text>().text = "HP: " + hero_spawn_point2.GetComponentInChildren<Character>().current_HP.ToString() + "<color=#8b33dd>" + "(+" + hero_spawn_point2.GetComponentInChildren<Character>().current_Shield.ToString() + ")" + "</color>";
            }
            else {
                hero_2_hp_text.GetComponent<Text>().text = "HP: " + hero_spawn_point2.GetComponentInChildren<Character>().current_HP.ToString();
            }
            hero_2_atk_text.GetComponent<Text>().text = "ATK: " + hero_spawn_point2.GetComponentInChildren<Character>().striking_power.ToString();
            if(hero_spawn_point2.GetComponentInChildren<Character>().current_HP <= 0)
            {
                hero_2_hp_text.GetComponent<Text>().text = "HP: " + "<color=#ff0000>" + 0 + "</color>";
            }
        }



        //monster
        //monster_2_img.GetComponent<Image>().sprite == null && 
        if(Spawn_point0.transform.childCount != 0)
        {
            monster_0_img.GetComponent<Image>().sprite = Spawn_point0.GetComponentInChildren<SpriteRenderer>().sprite;
        } else  monster_0_img.GetComponent<Image>().sprite = x_img;
        if(Spawn_point1.transform.childCount != 0)
        {
            monster_1_img.GetComponent<Image>().sprite = Spawn_point1.GetComponentInChildren<SpriteRenderer>().sprite;
        } else  monster_1_img.GetComponent<Image>().sprite = x_img;
        if(Spawn_point2.transform.childCount != 0)
        {
            monster_2_img.GetComponent<Image>().sprite = Spawn_point2.GetComponentInChildren<SpriteRenderer>().sprite;
        } else  monster_2_img.GetComponent<Image>().sprite = x_img;

        if(Spawn_point0.transform.childCount != 0)
        {
            //hero_0_img.GetComponent<Image>().sprite = Back.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite;
            monster_0_hp_text.GetComponent<Text>().text = "HP: " + Spawn_point0.GetComponentInChildren<EnemyController>().current_HP.ToString();
            monster_0_atk_text.GetComponent<Text>().text = "ATK: " + Spawn_point0.GetComponentInChildren<EnemyController>().damage.ToString();
            if(Spawn_point0.GetComponentInChildren<EnemyController>().current_HP <= 0)
            {
                monster_0_hp_text.GetComponent<Text>().text = "HP: " + "<color=#ff0000>" + 0 + "</color>";
            }
        }

        if(Spawn_point1.transform.childCount != 0)
        {
            //hero_0_img.GetComponent<Image>().sprite = Back.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite;
            monster_1_hp_text.GetComponent<Text>().text = "HP: " + Spawn_point1.GetComponentInChildren<EnemyController>().current_HP.ToString();
            monster_1_atk_text.GetComponent<Text>().text = "ATK: " + Spawn_point1.GetComponentInChildren<EnemyController>().damage.ToString();
            if(Spawn_point1.GetComponentInChildren<EnemyController>().current_HP <= 0)
            {
                monster_1_hp_text.GetComponent<Text>().text = "HP: " + "<color=#ff0000>" + 0 + "</color>";
            }
        }

        if(Spawn_point2.transform.childCount != 0)
        {
            //hero_0_img.GetComponent<Image>().sprite = Back.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite;
            monster_2_hp_text.GetComponent<Text>().text = "HP: " + Spawn_point2.GetComponentInChildren<EnemyController>().current_HP.ToString();
            monster_2_atk_text.GetComponent<Text>().text = "ATK: " + Spawn_point2.GetComponentInChildren<EnemyController>().damage.ToString();
            if(Spawn_point2.GetComponentInChildren<EnemyController>().current_HP <= 0)
            {
                monster_2_hp_text.GetComponent<Text>().text = "HP: " + "<color=#ff0000>" + 0 + "</color>";
            }
        }


    }

    
    IEnumerator Timer()
    {   
        timer_operate = true;
        LimitTime = 30;
        while(true)
        {
            if(DataController.Instance.current_stage == 0) //dps
            {

                if(LimitTime > 0) //충전 1200초 = 20분
                {
                    
                    LimitTime -= 1;
                    //text_Timer.text = "충전시간 : " + Mathf.Round(LimitTime)/60 + "분 " + Mathf.Round(LimitTime)/60%60 + "초";
                    text_Timer.text = Math.Truncate(Mathf.Round(LimitTime)%60%60) + "초";
                    
                }
                else //시간이 다 지나면
                {
                    if(PlayerPrefs.GetInt("dpsResultBool") == 1)
                    {
                        if(total_damage >= 1000)              //
                        {
                            DataController.Instance.get_stage_gold = 30000;
                            DataController.Instance.get_stage_exp = 300;
                            DataController.Instance.get_stage_crystal = 10;
                        }
                        if(total_damage >= 10000)
                        {
                            DataController.Instance.get_stage_gold = 200000;
                            DataController.Instance.get_stage_exp = 2000;
                            DataController.Instance.get_stage_crystal = 50;
                        }
                        if(total_damage >= 100000)
                        {
                            DataController.Instance.get_stage_gold = 20000000;
                            DataController.Instance.get_stage_exp = 20000;
                            DataController.Instance.get_stage_crystal = 100;
                        }
                        if(total_damage >= 1000000)
                        {
                            DataController.Instance.get_stage_gold = 500000000;
                            DataController.Instance.get_stage_exp = 500000;
                            DataController.Instance.get_stage_crystal = 300;
                        }
                        if(total_damage >= 10000000)
                        {
                            DataController.Instance.get_stage_gold = 3500000000;
                            DataController.Instance.get_stage_exp = 3000000;
                            DataController.Instance.get_stage_crystal = 500;
                        }
                        if(total_damage >= 100000000)
                        {
                            DataController.Instance.get_stage_gold = 300000000000;
                            DataController.Instance.get_stage_exp = 20000000;
                            DataController.Instance.get_stage_crystal = 1000;
                        }

                    } else {
                        Debug.Log("it's set result 0");
                        DataController.Instance.get_stage_gold = 0;
                        DataController.Instance.get_stage_exp = 0;
                        DataController.Instance.get_stage_crystal = 0;
                    }
                    //PlayerPrefs.SetInt("dpsResultBool", 0);

                    
                    timer_operate = false;
                    yield return new WaitForSeconds(0.2f);
                    goToPanel.Instance.show_result_panel();       //결과창에 데미지 몇 돌파했는지 표시하기: 자신이 왜 이 만큼의 보상을 얻었는가 설명
                    StopCoroutine("Timer");
                }

            }
            yield return new WaitForSeconds(1f);
        }
    }

    bool activate_one = false;

    int current_timer_second = 0;
    Transform spawn_location;
    IEnumerator infinity_mode_set()     //무한모드
    {
        StartCoroutine("infinity_timer");
        int summon_time = 0;
        int current_grade = 1;
        int monster_num = 0;
        int spawn_num = 0;
        GameObject clone;
        GameObject mob;

        //
        clone = Instantiate(Scarecrow, Spawn_point2);
        //clone.transform.position = new Vector3(clone.transform.position.x, transform.position.y, transform.position.z);
        
        while(true)           //몬스터 생성 조건 : 맵에 몬스터가 없을 떄, 생성 후 3초가 지났을 때
        {
            yield return new WaitForSeconds(0.1f);
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if(this.gameObject.activeSelf == true)
            {
                if(DataController.Instance.current_stage == -2)  
                {
                    current_grade = current_timer_second / 10;
                    if(enemies.Length == 1)    //맵에 몬스터가 없을 떄
                    {   
                        
                        if(current_grade == 0) { current_grade = 1; }
                        summon_time = current_timer_second;
                        //몬스터 생성

                        
                        monster_num = UnityEngine.Random.Range(0, 14);
                        spawn_num = UnityEngine.Random.Range(0, 3);

                        mob = monster_pack[monster_num];
                        if(spawn_num == 0) { spawn_location = Spawn_point0; }
                        else if(spawn_num == 1) { spawn_location = Spawn_point1; }
                        else if(spawn_num == 2) { spawn_location = Spawn_point2; }
                        else { spawn_location = Spawn_point2; }


                        clone = Instantiate(mob, spawn_location);
                        clone.GetComponent<EnemyController>().Max_HP = 5 * ((long)Mathf.Pow(3, current_grade));
                        clone.GetComponent<EnemyController>().damage = 5 * ((long)Mathf.Pow(3, current_grade));
                    }
                    if((float)summon_time + (2f/current_grade) < (float)current_timer_second)      //생성 후 3초가 지났을 때
                    {
                        
                        if(current_grade == 0) { current_grade = 1; }
                        summon_time = current_timer_second;
                        //몬스터 생성

                        monster_num = UnityEngine.Random.Range(0, 14);
                        spawn_num = UnityEngine.Random.Range(0, 3);

                        mob = monster_pack[monster_num];
                        if(spawn_num == 0) { spawn_location = Spawn_point0; }
                        else if(spawn_num == 1) { spawn_location = Spawn_point1; }
                        else if(spawn_num == 2) { spawn_location = Spawn_point2; }
                        else { spawn_location = Spawn_point2; }


                        clone = Instantiate(mob, spawn_location);
                        clone.GetComponent<EnemyController>().Max_HP = 5 * ((long)Mathf.Pow(3, current_grade));
                        clone.GetComponent<EnemyController>().damage = 5 * ((long)Mathf.Pow(3, current_grade));
                    }
                    infinity_grade_text.text = current_grade + "단계";
                }
            }
            if(DataController.Instance.current_stage != -2)
            {
                StopCoroutine("infinity_mode_set");
            }
            //무한모드 단계에 따른 보상설정
            if(PlayerPrefs.GetInt("infinityResultBool") == 1)
            {
                DataController.Instance.get_stage_gold = 100 * (long)Mathf.Pow(5, current_grade);
                DataController.Instance.get_stage_exp = 20 * (long)Mathf.Pow(2, current_grade);
                DataController.Instance.get_stage_crystal = 1 * (long)Mathf.Pow(2, current_grade);
            } else {
                DataController.Instance.get_stage_gold = 0;
                DataController.Instance.get_stage_exp = 0;
                DataController.Instance.get_stage_crystal = 0;
            }
             
        }
    }

    IEnumerator infinity_timer()
    {
        
        while(true)
        {
            yield return new WaitForSeconds(1f);
            current_timer_second++;
            text_Timer.text = current_timer_second + "초";
            if(DataController.Instance.current_stage != -2)
            {
                StopCoroutine("infinity_timer");
            }
        }
    }

    IEnumerator Auto()
    {
        DataController.Instance.get_stage_resource = 0;
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if(this.gameObject.activeSelf == true)
            {
                show_state_in_windows();

                if(DataController.Instance.current_stage == 0)  //dps측정
                {
                    total_damage = enemies[0].GetComponent<EnemyController>().Max_HP - enemies[0].GetComponent<EnemyController>().current_HP;
                    text_damage.text = "" + total_damage;
                    if(DataController.Instance.maxDps <= total_damage)
                    {
                        DataController.Instance.maxDps = total_damage;
                    }
                    if(getMoney == false && timer_operate == false)
                    {
                        getMoney = true;
                        DataController.Instance.gold += DataController.Instance.get_stage_gold;
                        stage_gold_result.text = "" + UiManager.ToStringKR(DataController.Instance.get_stage_gold);
                    }  
                    if(getExp == false && timer_operate == false)
                    {
                        getExp = true;
                        DataController.Instance.current_exp += DataController.Instance.get_stage_exp;
                        stage_exp_result.text = "" + DataController.Instance.get_stage_exp;
                    }
                    if(getCrystal == false && timer_operate == false)
                    {
                        getCrystal = true;
                        DataController.Instance.diamond += DataController.Instance.get_stage_crystal;
                        stage_crystal_result.text = "" + DataController.Instance.get_stage_crystal;
                    }
                    stage_resource_result.text = "" + DataController.Instance.get_stage_resource;
                    Debug.Log("1");
                }
                
                if(enemies.Length == 0 && getResult == false)  //승리할시
                {
                    getResult = true;
                    SoundManager.Instance.play_victory_sound();
                    battle_result_title_text.text = "승리";
                    if(DataController.Instance.get_stage_crystal == 0 )
                    {
                        result_crystal_panel.SetActive(false);
                    }

                    SetBool("ch1" + DataController.Instance.current_stage, true);
                    if(DataController.Instance.current_stage >= 1)
                    {
                        PlayerPrefs.SetInt("userMaxStage", (int)DataController.Instance.current_stage);
                    }
                    
                    if(GetBool("getResult") == false)
                    {//결과창이 중복해서 뜨는걸 방지
                        goToPanel.Instance.show_result_panel();
                    }
                    
                    if(GetBool("ch1" + 19) == true && GetBool("ch1" + 20) == false)  //궁수 잠금해제
                    {
                        PlayerPrefs.GetInt("archer", 1);
                    }
                    Debug.Log("2");

                }
                else if(player.Length == 0 && getResult == false)  //패배할 시
                {
                    SoundManager.Instance.play_defeat_sound();
                    getResult = true;
                    battle_result_title_text.text = "패배";

                    //일반 스토리 진행에서 패배할시
                    if(DataController.Instance.current_stage != 0 && DataController.Instance.current_stage != -1 && DataController.Instance.current_stage != -2)
                    {
                        DataController.Instance.get_stage_gold = 0;
                        DataController.Instance.get_stage_exp = 0;
                        DataController.Instance.get_stage_crystal = 0;
                        DataController.Instance.get_stage_resource = 0;
                    }
                    
                    //결과 문구를 "승리"에서 "패배"로 바꾸기
                    if(GetBool("getResult") == false)
                    {//결과창이 중복해서 뜨는걸 방지
                        goToPanel.Instance.show_result_panel();
                    }
                    // stage = 0(Dps) stage =-1(시련의 탑) stage = -2(무한모드)
                    if(DataController.Instance.current_stage == -2)    
                    {
                        DataController.Instance.gold += DataController.Instance.get_stage_gold;
                        DataController.Instance.current_exp += DataController.Instance.get_stage_exp;
                        DataController.Instance.diamond += DataController.Instance.get_stage_crystal;
                    }  
                    stage_gold_result.text = "" + UiManager.ToStringKR(DataController.Instance.get_stage_gold);
                    stage_exp_result.text = "" + DataController.Instance.get_stage_exp;
                    stage_crystal_result.text = "" + DataController.Instance.get_stage_crystal;
                    stage_resource_result.text = "" + DataController.Instance.get_stage_resource;
                    Debug.Log("3");
                }

                if(getMoney == false && enemies.Length == 0)    
                {
                    getMoney = true;
                    DataController.Instance.gold += DataController.Instance.get_stage_gold;
                    stage_gold_result.text = "" + UiManager.ToStringKR(DataController.Instance.get_stage_gold);
                    Debug.Log("4");
                }  
                if(getExp == false && enemies.Length == 0)
                {
                    getExp = true;
                    DataController.Instance.current_exp += DataController.Instance.get_stage_exp;
                    stage_exp_result.text = "" + DataController.Instance.get_stage_exp;
                    Debug.Log("5");
                }
                if(getCrystal == false && enemies.Length == 0)
                {
                    getCrystal = true;
                    DataController.Instance.diamond += DataController.Instance.get_stage_crystal;
                    stage_crystal_result.text = "" + DataController.Instance.get_stage_crystal; 
                    Debug.Log("6");              
                }
                if(getItem == false && enemies.Length == 0)
                {
                    getItem = true;
                    if(DataController.Instance.current_stage == -1)
                    {
                        DataController.Instance.tower_stage++; //tower_previous_stage + 1;
                        DataController.Instance.current_stage = -4;
                        tower_of_diamond();
                    }
                    //if(ItemNum != 0)
                    //{
                        get_booty();
                    //}
                    stage_resource_result.text = "" + DataController.Instance.get_stage_resource;
                    Debug.Log("7");
                }
                if(( enemies.Length == 0 && activate_one == false) || (player.Length == 0 && activate_one == false))
                {
                    FightController.Instance.check_stage();
                    
                    
                    activate_one = true;
                    Debug.Log("8");
                }
                
                
            }
        }
    }

    public void get_booty()
    {
        DataController.Instance.get_stage_resource = 0;
        for(int i = 0; i < 20; i++)
        {
            if(DataController.Instance.current_stage / 20 == i)
            {
                int booty_index = (int)(DataController.Instance.current_stage / 20) % 5;
                Item_img.sprite = booty_pack[booty_index];
                //Debug.Log("booty_is " + booty_pack[booty_index].name);
                break;
            }
        }
        
        ItemNum = (int)((DataController.Instance.current_stage / 20) % 5) + 1;
        int rant = UnityEngine.Random.Range(1, 101); //1~!00
        if(rant > 40)
        {
            Debug.Log("it's work!!");
            int current_booty = PlayerPrefs.GetInt("booty_" + ItemNum);
            int max = BlessingExchange.Instance.blessing_get_booty_ratio[PlayerPrefs.GetInt("bls_7")];
            int get_booty_value = UnityEngine.Random.Range(1, max);
            current_booty = current_booty + get_booty_value;
            DataController.Instance.get_stage_resource = get_booty_value;
            PlayerPrefs.SetInt("booty_" + ItemNum, current_booty);
            
        }
    }

    public void show_dps_probability()
    {
        dps_probability_panel.SetActive(!dps_probability_panel.active);
    }

    public void show_infinity_probability()
    {
        infinity_probability_panel.SetActive(!infinity_probability_panel.active);
    }

    String item_name = null;

    public void show_fight_result_to_Text()
    {
        
        // if(DataController.Instance.current_stage < 20)
        // {
        //     item_name = "납 주괴";
        //     ItemNum = 1;
        //     Item_img.GetComponent<Image>().sprite = booty_pack[0];
        // } else if(DataController.Instance.current_stage < 40) {
        //     item_name = "철 주괴";  //스켈레톤 써서 스켈레톤 뼈 가자
        //     ItemNum = 2;
        //     Item_img.GetComponent<Image>().sprite = booty_pack[1];
        // } else if(DataController.Instance.current_stage < 60) {
        //     item_name = "금 주괴";
        //     ItemNum = 3;
        //     Item_img.GetComponent<Image>().sprite = booty_pack[2];
        // } else if(DataController.Instance.current_stage < 80) {
        //     item_name = "다이아몬드";
        //     ItemNum = 4;
        //     Item_img.GetComponent<Image>().sprite =booty_pack[3];
        // } else if(DataController.Instance.current_stage < 100) {
        //     item_name = "오리하르콘";
        //     ItemNum = 5;
        //     Item_img.GetComponent<Image>().sprite = booty_pack[4];
        // }
        // else {
        //     item_name = "없음";
        //     ItemNum = 0;
        //     Item_img.GetComponent<Image>().sprite = null;
        // }
        // DataController.Instance.itemNum = ItemNum;
        // gold_result_text.text = "<color=#fffb00>" + "Gold: " + UiManager.ToStringKR(DataController.Instance.get_stage_gold) + "</Color>";
        // exp_result_text.text = "<color=#00ff3a>" + "Exp: " + UiManager.ToStringKR(DataController.Instance.get_stage_exp) + "</Color>";
        
        // Item_result_text.text = "<color=#ff8300>" + "Item: " + item_name + "</Color>";
    }

    public void pause_button()
    {
        if (IsPause == false)
            {
                Time.timeScale = 0;
                IsPause = true;
                //Debug.Log("It's Stop!!");
                return;
                
            }
 
            /*일시정지 비활성화*/
            if (IsPause == true)
            {
                Time.timeScale = 1;
                IsPause = false;
                //Debug.Log("It's Moveing!!");
                return;
            }
    }

    public void restart_stage()
    {
        for(int i = 0; i < 50; i++)
        {
            if(DataController.Instance.current_stage == i)
            {
                Invoke("stage1_" + i, 0.3f);
            }
        }
    }

    void check_hero_level()
    {
        knight = knight_pack[DataController.Instance.knight_level-1];
        archer = archer_pack[DataController.Instance.archer_level-1];
        wizard = wizard_pack[DataController.Instance.wizard_level-1];
    }

    public void set_hero_monster()
    {
        hero_0_name = DataController.Instance.hero_0_ID;
        hero_1_name = DataController.Instance.hero_1_ID;
        hero_2_name = DataController.Instance.hero_2_ID;
        monster_0_name = DataController.Instance.monster_0_ID;
        monster_1_name = DataController.Instance.monster_1_ID;
        monster_2_name = DataController.Instance.monster_2_ID;
        //히어로
        check_hero_level();
        switch(hero_0_name){
            case 0: 
                 hero_0 = null;
                break;
            case 1:
                hero_0 = knight;
                break;
            case 2:
                hero_0 = archer;
                break;
            case 3:
                hero_0 = wizard;
                break;
            default:
                 hero_0 = null;
                break;
        }
        switch(hero_1_name){
            case 0:
                 hero_1 =null;
                break;
            case 1:
                hero_1 = knight;
                break;
            case 2:
                hero_1 = archer;
                break;
            case 3:
                hero_1 = wizard;
                break;
            default:
                 hero_1 = null;
                break;
        }
        switch(hero_2_name){
            case 0:
                 hero_2 = null;
                break;
            case 1:
                hero_2 = knight;
                break;
            case 2:
                hero_2 = archer;
                break;
            case 3:
                hero_2 = wizard;
                break;
            default:
                 hero_2 = null;
                break;
        }
        
        //몬스터
        switch(monster_0_name)
        {
            case "Scarecrow":
                monster_0 = Scarecrow;
                break;
            case "Bat":
                monster_0 = Bat_level1;
                break;
            case "Bat_2":
                monster_0 = Bat_level2;
                break;
            case "Spider":
                monster_0 = Spider;
                break;
            case "Gorem":
                monster_0 = Gorem;
                break;
            case "BigGorem":
                monster_0 = Big_Gorem;
                break;
            case "Troll":
                monster_0 = Troll;
                break;
            case "goblin_general":
                monster_0 = goblin_general;
                break;
            case "summon_portal":
                monster_0 = summon_portal;
                break;
            case "plant_monster":
                monster_0 = plant_monster;
                break;
            case "ice_gorem":
                monster_0 = ice_gorem;
                break;
            case "warm_monster":
                monster_0 = warm_monster;
                break;
            case "stoneFighter":
                monster_0 = stoneFighter;
                break;
            case "blueDragonic":
                monster_0 = blueDragonic;
                break;
            case "meduza":
                monster_0 = meduza;
                break;
            case "minotaurse":
                monster_0 = minotaurse;
                break;
            case "Bat_Bose":
                monster_0 = Bat_Bose;
                break;
            case "crazy_portal_Bose":
                monster_0 = crazy_portal_Bose;
                break;
            case "Bose_scareCrow":
                monster_0 = Bose_scareCrow;
                break;
            
            default:
                monster_0 = null;
                break;
        }
        switch(monster_1_name)
        {
            case "Scarecrow":
                monster_1 = Scarecrow;
                break;
            case "Bat":
                monster_1 = Bat_level1;
                break;
            case "Bat_2":
                monster_1 = Bat_level2;
                break;
            case "Spider":
                monster_1 = Spider;
                break;
            case "Gorem":
                monster_1 = Gorem;
                break;
            case "BigGorem":
                monster_1 = Big_Gorem;
                break;
            case "Troll":
                monster_1 = Troll;
                break;
            case "goblin_general":
                monster_1 = goblin_general;
                break;
            case "summon_portal":
                monster_1 = summon_portal;
                break;
            case "plant_monster":
                monster_1 = plant_monster;
                break;
            case "ice_gorem":
                monster_1 = ice_gorem;
                break;
            case "warm_monster":
                monster_1 = warm_monster;
                break;
            case "stoneFighter":
                monster_1 = stoneFighter;
                break;
            case "blueDragonic":
                monster_1 = blueDragonic;
                break;
            case "meduza":
                monster_1 = meduza;
                break;
            case "minotaurse":
                monster_1 = minotaurse;
                break;
            case "Bat_Bose":
                monster_1 = Bat_Bose;
                break;
            case "crazy_portal_Bose":
                monster_1 = crazy_portal_Bose;
                break;
            case "Bose_scareCrow":
                monster_1 = Bose_scareCrow;
                break;

            default:
                monster_1 = null;
                break;
           
        }
        switch(monster_2_name)
        {
            case "Scarecrow":
                monster_2 = Scarecrow;
                break;
            case "Bat":
                monster_2 = Bat_level1;
                break;
            case "Bat_2":
                monster_2 = Bat_level2;
                break;
            case "Spider":
                monster_2 = Spider;
                break;
            case "Gorem":
                monster_2 = Gorem;
                break;
            case "BigGorem":
                monster_2 = Big_Gorem;
                break;
            case "Troll":
                monster_2 = Troll;
                break;
            case "goblin_general":
                monster_2= goblin_general;
                break;
            case "summon_portal":
                monster_2 = summon_portal;
                break;
            case "plant_monster":
                monster_2 = plant_monster;
                break;
            case "ice_gorem":
                monster_2 = ice_gorem;
                break;
            case "warm_monster":
                monster_2 = warm_monster;
                break;
            case "stoneFighter":
                monster_2 = stoneFighter;
                break;
            case "blueDragonic":
                monster_2 = blueDragonic;
                break;
            case "meduza":
                monster_2 = meduza;
                break;
            case "minotaurse":
                monster_2 = minotaurse;
                break;
            case "Bat_Bose":
                monster_2 = Bat_Bose;
                break;
            case "crazy_portal_Bose":
                monster_2 = crazy_portal_Bose;
                break;
            case "Bose_scareCrow":
                monster_2 = Bose_scareCrow;
                break;

             default:
                monster_2 = null;
                break;
            
        }
    }

    public void Set_Hero_Base()
    {
        set_background_image();
        set_hero_monster();

        goToPanel.Instance.turn_on_and_off_dispose_panel();

        GameObject clone_player0 = null;
        GameObject clone_player1 = null;
        GameObject clone_player2 = null;
        
        GameObject clone_enemy0 = null;
        GameObject clone_enemy1 = null;
        GameObject clone_enemy2 = null;

        if(DataController.Instance.current_stage >= 280 && DataController.Instance.current_stage <= 300)
        {
            //모든 스폰 포인트 아래로
        }
        
        if(hero_0 != null)
        {
            clone_player0 = Instantiate(hero_0, hero_spawn_point0);
            
        }

        if(hero_1 != null)
        {
            clone_player1 = Instantiate(hero_1, hero_spawn_point1);
            
        }

        if(hero_2 != null)
        {
            clone_player2 = Instantiate(hero_2, hero_spawn_point2);
            
        }

        if(monster_0 != null)
        {
            clone_enemy0 = Instantiate(monster_0, Spawn_point0);
            clone_enemy0.GetComponent<EnemyController>().Max_HP = (long)(DataController.Instance.monster_hp * (long)clone_enemy0.GetComponent<EnemyController>().health_ratio);
            clone_enemy0.GetComponent<EnemyController>().damage = (long)(DataController.Instance.monster_damage * clone_enemy0.GetComponent<EnemyController>().attack_ratio);
        }

        if(monster_1 != null)
        {
            clone_enemy1 = Instantiate(monster_1, Spawn_point1);
            clone_enemy1.GetComponent<EnemyController>().Max_HP = (long)(DataController.Instance.monster_hp * (long)clone_enemy1.GetComponent<EnemyController>().health_ratio);
            clone_enemy1.GetComponent<EnemyController>().damage = (long)(DataController.Instance.monster_damage * clone_enemy1.GetComponent<EnemyController>().attack_ratio);
            
        }

        if(monster_2 != null)
        {
            clone_enemy2 = Instantiate(monster_2, Spawn_point2);
            clone_enemy2.GetComponent<EnemyController>().Max_HP = (long)(DataController.Instance.monster_hp * (long)clone_enemy2.GetComponent<EnemyController>().health_ratio);
            clone_enemy2.GetComponent<EnemyController>().damage =(long)(DataController.Instance.monster_damage * clone_enemy2.GetComponent<EnemyController>().attack_ratio);
            
        }

        goToPanel.Instance.show_battle_scene_panel();

        Destroy(clone_player0);
        Destroy(clone_player1);
        Destroy(clone_player2);
        Destroy(clone_enemy0);
        Destroy(clone_enemy1);
        Destroy(clone_enemy2);

        

    }
    public void monster_init()
    {
        DataController.Instance.monster_0_ID = "dummy";
        DataController.Instance.monster_0_damage = 0;
        DataController.Instance.monster_0_hp = 0;
        DataController.Instance.monster_1_ID = "dummy";
        DataController.Instance.monster_1_damage = 0;
        DataController.Instance.monster_1_hp = 0;
        DataController.Instance.monster_2_ID = "dummy";
        DataController.Instance.monster_2_damage = 0;
        DataController.Instance.monster_2_hp = 0;

        DataController.Instance.get_stage_gold = 0;
        DataController.Instance.get_stage_exp = 0;
        DataController.Instance.get_stage_crystal = 0;
        // random_get_crystal = UnityEngine.Random.Range(0, 6); //0~5
        // DataController.Instance.get_stage_crystal = random_get_crystal;
    }

    public void only_debug()
    {
        Debug.Log("DataController.Instance.monster_2_ID " + DataController.Instance.monster_2_ID);
        Debug.Log("DataController.Instance.monster_2_damage " + DataController.Instance.monster_2_damage);
        Debug.Log("DataController.Instance.monster_2_hp " + DataController.Instance.monster_2_hp);
        Time.timeScale = 1;
    }
    //스테이지마다 나오는 몬스터의 능력치 수치를 조정하기 보단
    //능력치 수치가 다른 몬스터를 여러개 만들어서 prefab에서 가져오기

    
    public void test_dps_stage()         //허수아비 필요함
    {
        if(DataController.Instance.current_stage == 0)
        {
                      //하트 1개 사용하기
            if(fightPanelTimer.Instance.current_heart >= 1)
            {
                Debug.Log("heart is minus");
                PlayerPrefs.SetInt("dpsResultBool", 1);
                fightPanelTimer.Instance.current_heart--;
            } else {
                PlayerPrefs.SetInt("dpsResultBool", 0);
            }
            Set_Hero_Base();
        }
        SoundManager.Instance.play_fight_panel_btn_sound();
            monster_init();  //get_stage_gold, exp도 초기화
            DataController.Instance.monster_0_ID = "Scarecrow";
            DataController.Instance.monster_damage = 0;      //3
            DataController.Instance.monster_hp = 100000000000;       //100

        DataController.Instance.current_stage = 0;
    }


    public void stage1_1()
    {
        if(DataController.Instance.current_stage == 1)
        {
            monster_init();  //get_stage_gold, exp도 초기화
            DataController.Instance.monster_2_ID = "minotaurse";  //Bat
            DataController.Instance.monster_damage = 2;      //3
            DataController.Instance.monster_hp = 10000;       //100
            Set_Hero_Base();
            //보상
            DataController.Instance.get_stage_gold = 500;
            DataController.Instance.get_stage_exp = 3;
            DataController.Instance.get_stage_crystal = 3;
        }
        DataController.Instance.current_stage = 1;
    }


    public void stage1_60()
    {
        monster_init();
        DataController.Instance.monster_0_ID = "goblin_general"; 
        DataController.Instance.monster_1_ID = "plant_monster";
        DataController.Instance.monster_2_ID = "Spider";
        DataController.Instance.monster_hp = 200;
        DataController.Instance.monster_damage = 200;
        set_hero_monster();

        if(DataController.Instance.current_stage == 60)
        {  
            Set_Hero_Base();
            set_fight_reward();
        }
        DataController.Instance.current_stage = 60;
        set_explanation_panel();
    }

    private long[] ms_2 = {3, 4};

    public enum monster_list
    {
        Bat,
        Bat_2,
        Spider,
        Gorem,
        BigGorem,
        Troll,
        summon_portal,
        goblin_general,
        plant_monster,
        ice_gorem,
        warm_monster,
        stoneFighter,
        blueDragonic,
        meduza,
        minotaurse,
    }


    public void over_60()
    {
        

        for(int i = 0; i < FightController.Instance.stageCh1.Length; i++)
        {

            if(fightController.stageCh1[i] == EventSystem.current.currentSelectedGameObject
            && DataController.Instance.current_stage == i+1)  //일치하면 전투화면
            {
                Set_Hero_Base();
                break;
            }    

            if(fightController.stageCh1[i] == EventSystem.current.currentSelectedGameObject 
            && DataController.Instance.current_stage != i+1)
            {
                monster_init();
                DataController.Instance.current_stage = i+1;
                long x = ((DataController.Instance.current_stage / 20)*3) % 15;
                monster_list list_0 = (monster_list)x;
                monster_list list_1 = (monster_list)x+1;
                monster_list list_2 = (monster_list)x+2;
                DataController.Instance.monster_0_ID = list_0.ToString();
                if(DataController.Instance.current_stage % 2 == 0)
                {
                    DataController.Instance.monster_0_ID = list_1.ToString();
                }
                if(DataController.Instance.current_stage % 3 == 0)
                {
                    DataController.Instance.monster_0_ID = list_2.ToString();
                }
                if(DataController.Instance.current_stage % 20 > 5)
                {
                    DataController.Instance.monster_1_ID = list_1.ToString();
                    if(DataController.Instance.current_stage % 2 == 0)
                    {
                        string tmp = DataController.Instance.monster_0_ID;
                        DataController.Instance.monster_0_ID = DataController.Instance.monster_1_ID;
                        DataController.Instance.monster_1_ID = tmp;
                        
                    }
                }
                if(DataController.Instance.current_stage % 20 > 15)
                {
                    DataController.Instance.monster_2_ID = list_2.ToString();
                    if(DataController.Instance.current_stage % 3 == 0)
                    {
                        string tmp = DataController.Instance.monster_1_ID;
                        DataController.Instance.monster_1_ID = DataController.Instance.monster_2_ID;
                        DataController.Instance.monster_2_ID = tmp;
                    }
                }
                if(DataController.Instance.current_stage % 20 > 5 && DataController.Instance.monster_2_ID == "dummy")
                {
                    if(DataController.Instance.current_stage % 3 == 0)
                    {
                        string tmp = DataController.Instance.monster_2_ID;
                        DataController.Instance.monster_2_ID = DataController.Instance.monster_1_ID;
                        DataController.Instance.monster_1_ID = tmp;
                    }
                }
                if (x == 0) x = 1;
                DataController.Instance.monster_hp = DataController.Instance.current_stage * 10;
                DataController.Instance.monster_damage = DataController.Instance.current_stage * 10;
                if(DataController.Instance.current_stage % 20 == 0)
                {
                    DataController.Instance.monster_hp = DataController.Instance.current_stage * 30;
                    DataController.Instance.monster_damage = DataController.Instance.current_stage * 30;
                }
                set_hero_monster();
                set_fight_reward();
                //set_explanation_panel();
                //show_fight_result_to_Text();
                break;
            }
        }
    }

    public void over_100()
    {
        

        for(int i = 100; i < FightController.Instance.stageCh1.Length; i++)
        {

            if(fightController.stageCh1[i] == EventSystem.current.currentSelectedGameObject
            && DataController.Instance.current_stage == i+1)  //일치하면 전투화면
            {
                Set_Hero_Base();
                break;
            }    

            if(fightController.stageCh1[i] == EventSystem.current.currentSelectedGameObject 
            && DataController.Instance.current_stage != i+1)
            {
                monster_init();
                DataController.Instance.current_stage = i+1;
                long x = ((DataController.Instance.current_stage / 20)*3) % 15;
                monster_list list_0 = (monster_list)x;
                monster_list list_1 = (monster_list)x+1;
                monster_list list_2 = (monster_list)x+2;
                DataController.Instance.monster_0_ID = list_0.ToString();
                if(DataController.Instance.current_stage % 2 == 0)
                {
                    DataController.Instance.monster_0_ID = list_1.ToString();
                }
                if(DataController.Instance.current_stage % 3 == 0)
                {
                    DataController.Instance.monster_0_ID = list_2.ToString();
                }
                if(DataController.Instance.current_stage % 20 > 5)
                {
                    DataController.Instance.monster_1_ID = list_1.ToString();
                    if(DataController.Instance.current_stage % 2 == 0)
                    {
                        string tmp = DataController.Instance.monster_0_ID;
                        DataController.Instance.monster_0_ID = DataController.Instance.monster_1_ID;
                        DataController.Instance.monster_1_ID = tmp;
                    }
                }
                if(DataController.Instance.current_stage % 20 > 15)
                {
                    DataController.Instance.monster_2_ID = list_2.ToString();
                    if(DataController.Instance.current_stage % 3 == 0)
                    {
                        string tmp = DataController.Instance.monster_1_ID;
                        DataController.Instance.monster_1_ID = DataController.Instance.monster_2_ID;
                        DataController.Instance.monster_2_ID = tmp;
                    }
                }
                if(DataController.Instance.current_stage % 20 > 5 && DataController.Instance.monster_2_ID == "dummy")
                {
                    if(DataController.Instance.current_stage % 3 == 0)
                    {
                        string tmp = DataController.Instance.monster_2_ID;
                        DataController.Instance.monster_2_ID = DataController.Instance.monster_1_ID;
                        DataController.Instance.monster_1_ID = tmp;
                    }
                }
                if (x == 0) x = 1;
                DataController.Instance.monster_hp = DataController.Instance.current_stage * 30;
                DataController.Instance.monster_damage = DataController.Instance.current_stage * 30;
                set_hero_monster();
                set_fight_reward();
                //set_explanation_panel();
                //show_fight_result_to_Text();
                break;
            }
        }
    }

    public void over_200()
    {
        

        for(int i = 200; i < FightController.Instance.stageCh1.Length; i++)
        {

            if(fightController.stageCh1[i] == EventSystem.current.currentSelectedGameObject
            && DataController.Instance.current_stage == i+1)  //일치하면 전투화면
            {
                Set_Hero_Base();
                break;
            }    

            if(fightController.stageCh1[i] == EventSystem.current.currentSelectedGameObject 
            && DataController.Instance.current_stage != i+1)
            {
                monster_init();
                DataController.Instance.current_stage = i+1;
                long x = ((DataController.Instance.current_stage / 20)*3) % 15;
                monster_list list_0 = (monster_list)x;
                monster_list list_1 = (monster_list)x+1;
                monster_list list_2 = (monster_list)x+2;
                DataController.Instance.monster_0_ID = list_0.ToString();
                if(DataController.Instance.current_stage % 2 == 0)
                {
                    DataController.Instance.monster_0_ID = list_1.ToString();
                }
                if(DataController.Instance.current_stage % 3 == 0)
                {
                    DataController.Instance.monster_0_ID = list_2.ToString();
                }
                if(DataController.Instance.current_stage % 20 > 5)
                {
                    DataController.Instance.monster_1_ID = list_1.ToString();
                    if(DataController.Instance.current_stage % 2 == 0)
                    {
                        string tmp = DataController.Instance.monster_0_ID;
                        DataController.Instance.monster_0_ID = DataController.Instance.monster_1_ID;
                        DataController.Instance.monster_1_ID = tmp;
                    }
                }
                if(DataController.Instance.current_stage % 20 > 15)
                {
                    DataController.Instance.monster_2_ID = list_2.ToString();
                    if(DataController.Instance.current_stage % 3 == 0)
                    {
                        string tmp = DataController.Instance.monster_1_ID;
                        DataController.Instance.monster_1_ID = DataController.Instance.monster_2_ID;
                        DataController.Instance.monster_2_ID = tmp;
                    }
                }
                if(DataController.Instance.current_stage % 20 > 5 && DataController.Instance.monster_2_ID == "dummy")
                {
                    if(DataController.Instance.current_stage % 3 == 0)
                    {
                        string tmp = DataController.Instance.monster_2_ID;
                        DataController.Instance.monster_2_ID = DataController.Instance.monster_1_ID;
                        DataController.Instance.monster_1_ID = tmp;
                    }
                }
                if (x == 0) x = 1;
                DataController.Instance.monster_hp = DataController.Instance.current_stage * 50;
                DataController.Instance.monster_damage = DataController.Instance.current_stage * 50;
                set_hero_monster();
                set_fight_reward();
                set_explanation_panel();
                show_fight_result_to_Text();
                break;
            }
        }
    }

    public void stage1_100()
    {
        

        monster_init();
        DataController.Instance.monster_0_ID = "Bat_Bose"; 
        DataController.Instance.monster_hp = 5000;
        DataController.Instance.monster_damage = 500;
        set_hero_monster();

        if(DataController.Instance.current_stage == 100)
        {  
            Set_Hero_Base();
        }
        DataController.Instance.current_stage = 100;
        set_fight_reward();
        set_explanation_panel();
        show_fight_result_to_Text();
    }

    public void stage1_200()   //200스테이지 보스
    {
        

        monster_init();
        DataController.Instance.monster_0_ID = "crazy_portal_Bose"; 
        DataController.Instance.monster_1_ID = "crazy_portal_Bose"; 
        DataController.Instance.monster_2_ID = "crazy_portal_Bose"; 
        DataController.Instance.monster_hp = 10000;
        DataController.Instance.monster_damage = 3000;
        set_hero_monster();

        if(DataController.Instance.current_stage == 200)
        {  
            Set_Hero_Base();
        }
        DataController.Instance.current_stage = 200;
        set_fight_reward();
        set_explanation_panel();
        show_fight_result_to_Text();
    }

     public void stage1_300()   //200스테이지 보스
    {
        

        monster_init();
        DataController.Instance.monster_2_ID = "Bose_scareCrow"; 
        DataController.Instance.monster_hp = 100000;
        DataController.Instance.monster_damage = 1000;
        set_hero_monster();

        if(DataController.Instance.current_stage == 300)
        {  
            Set_Hero_Base();
        }
        DataController.Instance.current_stage = 300;
        set_fight_reward();
        set_explanation_panel();
        show_fight_result_to_Text();
    }

    public String avg_hp;
    public String avg_dm;

    void set_explanation_panel()
    {
        // long m_0_hp = 0;
        // long m_0_dm = 0;
        // long m_1_hp = 0;
        // long m_1_dm = 0;
        // long m_2_hp = 0;
        // long m_2_dm = 0;
        // int count = 0;

        // if(monster_0 != null)
        // {
        //     //m_0_hp = DataController.Instance.monster_hp * (long)monster_0.GetComponent<EnemyController>().health_ratio;
        //     m_0_hp = (long)(DataController.Instance.monster_hp * monster_0.GetComponent<EnemyController>().health_ratio);
        //     m_0_dm = (long)(DataController.Instance.monster_damage * monster_0.GetComponent<EnemyController>().attack_ratio);
        //     count++;
        // }
        // if(monster_1 != null)
        // {
        //     m_1_hp = (long)(DataController.Instance.monster_hp * monster_1.GetComponent<EnemyController>().health_ratio);
        //     m_1_dm = (long)(DataController.Instance.monster_damage * monster_1.GetComponent<EnemyController>().attack_ratio);
        //     count++;
        // }
        // if(monster_2 != null)
        // {
        //     m_2_hp = (long)(DataController.Instance.monster_hp * monster_2.GetComponent<EnemyController>().health_ratio);
        //     m_2_dm = (long)(DataController.Instance.monster_damage * monster_2.GetComponent<EnemyController>().attack_ratio);
        //     count++;
        // }

        // avg_hp = "" + ((m_0_hp + m_1_hp + m_2_hp) / count);
        // avg_dm = "" + ((m_0_dm + m_1_dm + m_2_dm) / count);
        // DataController.Instance.avg_hp = avg_hp;
        // DataController.Instance.avg_dg = avg_dm;
    }


    //stage-3 마법사 해방!!

    String[] monster_string = {"Bat",  "Bat_2", "Spider", "Gorem", "BigGorem", "Troll", "goblin_general", "summon_portal", "plant_monster", "ice_gorem"
    , "warm_monster", "stoneFighter", "blueDragonic", "meduza", "minotaurse"};

    public void tower_of_diamond()
    {
        int monster_kindCount = 13;
            long tower_stage = DataController.Instance.tower_stage;
            long repeatCount = tower_stage / monster_kindCount;
            if(repeatCount == 0)
            {
                repeatCount = 1;
            }
            DataController.Instance.tower_repeatCount = repeatCount;
            tower_previous_stage = DataController.Instance.tower_stage;
        if(DataController.Instance.current_stage == -1)
        { 
            Set_Hero_Base();
        }
        if(DataController.Instance.current_stage != -4)
        {
            SoundManager.Instance.play_fight_panel_btn_sound();
        }
        
            monster_init();
            int i = UnityEngine.Random.Range(0, 15);
            if(tower_stage >= 10)
            {
                DataController.Instance.monster_1_ID = monster_string[i];
                if(tower_stage >= 20)
                {
                    int k = UnityEngine.Random.Range(0, 15);
                    DataController.Instance.monster_0_ID = monster_string[k];
                }
                else { DataController.Instance.monster_0_ID = "dummy"; }
            } else { DataController.Instance.monster_1_ID = "dummy"; }

            if(tower_stage % monster_kindCount == 1)
            {
                DataController.Instance.monster_2_ID = "Bat"; 
            }
            else if(tower_stage % monster_kindCount == 2)
            {
                DataController.Instance.monster_2_ID = "Bat_2"; 
            }
            else if(tower_stage % monster_kindCount == 3)
            {
                DataController.Instance.monster_2_ID = "Spider"; 
            }
            else if(tower_stage % monster_kindCount == 4)
            {
                DataController.Instance.monster_2_ID = "Gorem"; 
            }
            else if(tower_stage % monster_kindCount == 5)
            {
                DataController.Instance.monster_2_ID = "BigGorem"; 
            }
            else if(tower_stage % monster_kindCount == 6)
            {
                DataController.Instance.monster_2_ID = "Troll"; 
            }
            else if(tower_stage % monster_kindCount == 7)
            {
                DataController.Instance.monster_2_ID = "goblin_general"; 
            }
            else if(tower_stage % monster_kindCount == 8)
            {
                DataController.Instance.monster_2_ID = "summon_portal"; 
            }
            else if(tower_stage % monster_kindCount == 9)
            {
                DataController.Instance.monster_2_ID = "plant_monster"; 
            }
            else if(tower_stage % monster_kindCount == 10)
            {
                DataController.Instance.monster_2_ID = "ice_gorem"; 
            }
            else if(tower_stage % monster_kindCount == 11)
            {
                DataController.Instance.monster_2_ID = "warm_monster"; 
            }
            else if(tower_stage % monster_kindCount == 12)
            {
                DataController.Instance.monster_2_ID = "stoneFighter"; 
            }
            else if(tower_stage % monster_kindCount == 13)
            {
                DataController.Instance.monster_2_ID = "blueDragonic"; 
            }
            else if(tower_stage % monster_kindCount == 14)
            {
                DataController.Instance.monster_2_ID = "meduza"; 
            }
            else if(tower_stage % monster_kindCount == 14)
            {
                DataController.Instance.monster_2_ID = "minotaurse"; 
            }
            else {
                DataController.Instance.monster_2_ID = "Bat"; 
            }
            set_hero_monster();

            DataController.Instance.monster_hp = tower_stage * 500;
            DataController.Instance.monster_damage = tower_stage * 500;

            DataController.Instance.get_stage_gold = 10000 * (long)Mathf.Pow(2, (tower_stage/2));
            DataController.Instance.get_stage_exp = tower_stage * 100;
            DataController.Instance.get_stage_crystal = (tower_stage * 25);
            DataController.Instance.current_stage = -1;

            //goToPanel.Instance.stage_explain_panel.SetActive(true);
            //goToPanel.Instance.stage_reward_explain_panel.SetActive(true);
            set_explanation_panel();
            show_fight_result_to_Text();
            
        
    }

    public void infinity_mode()
    {

       

        if(DataController.Instance.current_stage == -2)
        { 
             if(fightPanelTimer.Instance.infinity_mode_heart > 0)
            {
                PlayerPrefs.SetInt("infinityResultBool", 1);
                fightPanelTimer.Instance.infinity_mode_heart--;
            } else {
                PlayerPrefs.SetInt("infinityResultBool", 0);
                //fightPanelTimer.Instance.infinity_mode_heart--;
            }
            Set_Hero_Base();
            
        }
        SoundManager.Instance.play_fight_panel_btn_sound();

        if(DataController.Instance.current_stage != -2)
        {
            
            monster_init();
            DataController.Instance.current_stage = -2;

            //goToPanel.Instance.stage_explain_panel.SetActive(true);
            //goToPanel.Instance.stage_reward_explain_panel.SetActive(true);
            show_fight_result_to_Text();
        }
    }
    



    public void MyPosition (Transform transform)
    {
        var tra = transform;
        Fight_SelectFrame.SetActive(true);
        Fight_SelectFrame.transform.position = tra.position;
        Fight_SelectFrame.transform.parent = tra.transform;
        //goToPanel.Instance.stage_explain_panel.SetActive(true);
        //goToPanel.Instance.stage_reward_explain_panel.SetActive(true);
    }

    

    

    
}
