using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleManager : MonoBehaviour
{
    // private static BattleManager instance;

    // public static BattleManager Instance
    // {
    //     get{
    //         if(instance == null)
    //         {
    //             instance = FindObjectOfType<BattleManager>();

    //             if(instance == null)
    //             {
    //                 GameObject container = new GameObject("BattleManager");

    //                 instance = container.AddComponent<BattleManager>();
    //             }
    //         }
    //         return instance;
    //     }
    // }

    bool IsPause;
    bool getResult = false;
    bool getMoney = false;
    bool getExp = false;
    bool getCrystal = false;

    public long random_get_crystal = 0;

    private string notice_string;
    private int reward_item_id;
    private int reward_item_count;

    public GameObject result_crystal_panel;

    //ui
    public GameObject Fight_SelectFrame;
    public Text show_current_stage;
    public Text Stage_explain;

    public Transform BattleScene_panel;

    public Transform hero_spawn_point0;
    public Transform hero_spawn_point1;
    public Transform hero_spawn_point2;

    public Transform Spawn_point0;
    public Transform Spawn_point1;
    public Transform Spawn_point2;

    public GameObject Map1;
    public GameObject mapImage;

    //hero
    public GameObject dummy;
    public GameObject knight;
    public GameObject archer;

    public GameObject knight_01;
    public GameObject knight_02;

    public GameObject archer_01;
    public GameObject archer_02;

    //Enemy
    public GameObject Scarecrow;     //dpstest   
    public GameObject Bat_level1;
    public GameObject Bat_level2;
    public GameObject Spider;
    public GameObject Gorem;
    public GameObject T_Rex;
    public GameObject Big_Gorem;
    public GameObject skeleton_rancer;
    public GameObject goblin_general;
    public GameObject summon_portal;
    public GameObject plant_monster;
    public GameObject ice_gorem;
    public GameObject warm_monster;
    public GameObject stoneFighter;
    public GameObject blueDragonic;

    //timer
    long current_heart = 2;
    long max_heart = 3;

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
 
    // Use this for initialization
    void Start () {
        IsPause = false;
        StartCoroutine("Auto");
        StartCoroutine("Timer");

        getResult = false;
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
        DataController.Instance.tower_stage = 1;
    }

    public void clear_all_stage()
    {
        for(int i = 0; i < FightController.Instance.stageCh1.Length; i++) // 21을 max_current_Stage추가해서 교체
            {
                SetBool("ch1" + i, true);
            }
        SetBool("ch1" + 0, true);    //ch1-1만 활성화
    }

    public void debug_stage()
    {
        for(int i = 0; i < FightController.Instance.stageCh1.Length; i++) // 21을 max_current_Stage추가해서 교체
            {
                Debug.Log("ch1 " + i + " is " + GetBool("ch1" + i));
            }
    }

    public int require_max_stage()
    {
        int a = 0;
        for(int i = 0; i < FightController.Instance.stageCh1.Length; i++) // 21을 max_current_Stage추가해서 교체
        {
            if(GetBool("ch1" + i) == false)
            {
                a = i;
                break;
            }
        }
        if(a == 0) a = FightController.Instance.stageCh1.Length;
        return a;
    }     //stage의 interactive담당 스크립트는 어디?

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
        DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 10000;
        DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 5;
    }



    void set_background_image()
    {
        if(DataController.Instance.current_stage >= 1)
        {
            Map1.SetActive(true);
            mapImage.SetActive(false);
        }
        if(DataController.Instance.current_stage >= 20)
        {
            Map1.SetActive(false);
            mapImage.SetActive(true);
            mapImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Background/fightMap/ForestDayLight") as Sprite;
        }
        if(DataController.Instance.current_stage >= 40)
        {
            Map1.SetActive(false);
            mapImage.SetActive(true);
            mapImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/Background/fightMap/DistantCity") as Sprite;
        }
    }

    
    IEnumerator Timer()
    {   
        LimitTime = 30;
        while(true)
        {
            if(DataController.Instance.current_stage == 0)
            {

                if(LimitTime > 0) //충전 1200초 = 20분
                {
                    
                    LimitTime -= 1;
                    //text_Timer.text = "충전시간 : " + Mathf.Round(LimitTime)/60 + "분 " + Mathf.Round(LimitTime)/60%60 + "초";
                    text_Timer.text = Math.Truncate(Mathf.Round(LimitTime)%60%60) + "초";
                    
                }
                else //시간이 다 지나면
                {
                    if(total_damage >= 1000)              //
                    {
                        DataController.Instance.get_stage_gold = 30000;
                        DataController.Instance.get_stage_exp = 300;
                    }
                    if(total_damage >= 10000)
                    {
                        DataController.Instance.get_stage_gold = 2000000;
                        DataController.Instance.get_stage_exp = 2000;
                    }
                    if(total_damage >= 10000000)
                    {
                        DataController.Instance.get_stage_gold = 9999999;
                        DataController.Instance.get_stage_exp = 9999;
                    }
                    Debug.Log("it's DPS STAGE!!");
                    goToPanel.Instance.show_result_panel();       //결과창에 데미지 몇 돌파했는지 표시하기: 자신이 왜 이 만큼의 보상을 얻었는가 설명
                }

            }
            yield return new WaitForSeconds(1f);
        }
    }



    IEnumerator Auto()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);

            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if(this.gameObject.activeSelf == true)
            {
                if(DataController.Instance.current_stage == 0)  //dps측정
                {
                    total_damage = enemies[0].GetComponent<EnemyController>().Max_HP - enemies[0].GetComponent<EnemyController>().current_HP;
                    text_damage.text = "" + total_damage;
                    if(DataController.Instance.maxDps <= total_damage)
                    {
                        DataController.Instance.maxDps = total_damage;
                    }
                }

                
                if(enemies.Length == 0 && getResult == false)  //승리할시
                {
                    getResult = true;
                    Debug.Log("Enemies are dead!!");
                    if(DataController.Instance.get_stage_crystal == 0 )
                    {
                        result_crystal_panel.SetActive(false);
                    }
                    for(int i = 0; i < 41; i++) // 21을 max_current_Stage추가해서 교체
                    {
                        if(DataController.Instance.current_stage == i) //1스테이지를 꺠면 1번쨰 요소(2스테이지)가 true
                        {
                            SetBool("ch1" + i, true);
                        }
                    }
                    if(GetBool("getResult") == false)
                    {//결과창이 중복해서 뜨는걸 방지
                        goToPanel.Instance.show_result_panel();
                    }
                    
                    if(GetBool("ch1" + 19) == true && GetBool("ch1" + 20) == false)  //궁수 잠금해제
                    {
                        PlayerPrefs.GetInt("archer", 1);
                    }
                    if(DataController.Instance.current_stage == -1)
                    {
                        DataController.Instance.tower_stage = tower_previous_stage + 1;
                    }

                }
                else if(player.Length == 0 && getResult == false)  //패배할 시
                {
                    getResult = true;
                    DataController.Instance.get_stage_gold = 0;
                    DataController.Instance.get_stage_exp = 0;
                    //결과 문구를 "승리"에서 "패배"로 바꾸기
                    if(GetBool("getResult") == false)
                    {//결과창이 중복해서 뜨는걸 방지
                        goToPanel.Instance.show_result_panel();
                    }
                }
                if(getMoney == false && enemies.Length == 0)
                {
                    getMoney = true;
                    DataController.Instance.gold += DataController.Instance.get_stage_gold;
                    
                }  
                if(getExp == false && enemies.Length == 0)
                {
                    getExp = true;
                    DataController.Instance.current_exp += DataController.Instance.get_stage_exp;
                }
                if(getCrystal == false && enemies.Length == 0)
                {
                    getCrystal = true;
                    DataController.Instance.diamond += DataController.Instance.get_stage_crystal;
                    Debug.Log("You Got Crystal is " + DataController.Instance.get_stage_crystal);
                }
            }
            
            
        }
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
        //knight
        if(DataController.Instance.knight_level == 1)
        {
            knight = knight_01;
        }
        else if(DataController.Instance.knight_level == 2)
        {
            knight = knight_02;
        }

        //archer
        if(DataController.Instance.archer_level == 1)
        {
            archer = archer_01;
        }
        else if(DataController.Instance.archer_level == 2)
        {
            archer = archer_02;
        }
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
                hero_0 = dummy;
                break;
            case 1:
                hero_0 = knight;
                break;
            case 2:
                hero_0 = archer;
                break;
            default:
                hero_0 = dummy;
                break;
        }
        switch(hero_1_name){
            case 0:
                hero_1 = dummy;
                break;
            case 1:
                hero_1 = knight;
                break;
            case 2:
                hero_1 = archer;
                break;
            default:
                hero_1 = dummy;
                break;
        }
        switch(hero_2_name){
            case 0:
                hero_2 = dummy;
                break;
            case 1:
                hero_2 = knight;
                break;
            case 2:
                hero_2 = archer;
                break;
            default:
                hero_2 = dummy;
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
            case "skeleton_rancer":
                monster_0 = skeleton_rancer;
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
            
            default:
                monster_0 = dummy;
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
            case "skeleton_rancer":
                monster_1 = skeleton_rancer;
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

            default:
                monster_1 = dummy;
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
            case "skeleton_rancer":
                monster_2 = skeleton_rancer;
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

            default:
                monster_2 = dummy;
                break;
            
        }
    }

    public void Set_Hero_Base()
    {
        set_background_image();
        set_hero_monster();

        GameObject clone_player0 = Instantiate(hero_0, hero_spawn_point0);
        GameObject clone_player1 = Instantiate(hero_1, hero_spawn_point1);
        GameObject clone_player2 = Instantiate(hero_2, hero_spawn_point2);
        
        GameObject clone_enemy0 = Instantiate(monster_0, Spawn_point0);
        GameObject clone_enemy1 = Instantiate(monster_1, Spawn_point1);
        GameObject clone_enemy2 = Instantiate(monster_2, Spawn_point2);
        clone_enemy0.GetComponent<EnemyController>().Max_HP = (long)(DataController.Instance.monster_hp * (long)clone_enemy0.GetComponent<EnemyController>().health_ratio);
        clone_enemy0.GetComponent<EnemyController>().damage = (long)(DataController.Instance.monster_damage * clone_enemy0.GetComponent<EnemyController>().attack_ratio);
        clone_enemy1.GetComponent<EnemyController>().Max_HP = (long)(DataController.Instance.monster_hp * (long)clone_enemy1.GetComponent<EnemyController>().health_ratio);
        clone_enemy1.GetComponent<EnemyController>().damage = (long)(DataController.Instance.monster_damage * clone_enemy1.GetComponent<EnemyController>().attack_ratio);
        clone_enemy2.GetComponent<EnemyController>().Max_HP = (long)(DataController.Instance.monster_hp * (long)clone_enemy2.GetComponent<EnemyController>().health_ratio);
        clone_enemy2.GetComponent<EnemyController>().damage =(long)(DataController.Instance.monster_damage * clone_enemy2.GetComponent<EnemyController>().attack_ratio);

    
        
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
    }
    //스테이지마다 나오는 몬스터의 능력치 수치를 조정하기 보단
    //능력치 수치가 다른 몬스터를 여러개 만들어서 prefab에서 가져오기

    
    public void test_dps_stage()         //허수아비 필요함
    {
        fightPanelTimer.Instance.current_heart--;
            monster_init();  //get_stage_gold, exp도 초기화
            DataController.Instance.monster_0_ID = "Scarecrow";
            DataController.Instance.monster_damage = 0;      //3
            DataController.Instance.monster_hp = 10000000;       //100
            Set_Hero_Base();
            //보상        //dps_test는 입힌 데미지량에 따라 보상이 겨렁됨
            DataController.Instance.get_stage_gold = 500;
            DataController.Instance.get_stage_exp = 3;
            DataController.Instance.get_stage_crystal = 3;
        
        
        DataController.Instance.current_stage = 0;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n100\n몹 평균 공격력\n3";
    }


    public void stage1_1()
    {
        if(DataController.Instance.current_stage == 1)
        {
            monster_init();  //get_stage_gold, exp도 초기화
            DataController.Instance.monster_2_ID = "blueDragonic";  //Bat
            DataController.Instance.monster_damage = 2;      //3
            DataController.Instance.monster_hp = 10000;       //100
            Set_Hero_Base();
            //보상
            DataController.Instance.get_stage_gold = 500;
            DataController.Instance.get_stage_exp = 3;
            DataController.Instance.get_stage_crystal = 3;
        }
        
        DataController.Instance.current_stage = 1;
        Debug.Log("current_Stage is " + DataController.Instance.current_stage);
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n10\n몹 평균 공격력\n2";
    }

    public void stage1_2()
    {
        if(DataController.Instance.current_stage == 2)
        {
            monster_init();
            DataController.Instance.monster_0_ID = "Bat";
            DataController.Instance.monster_hp = 12;
            DataController.Instance.monster_damage = 3;

            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 1000;
            DataController.Instance.get_stage_exp = 6;

        }
        DataController.Instance.current_stage = 2;
        Debug.Log("current_Stage is " + DataController.Instance.current_stage);
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n12\n몹 평균 공격력\n3";
    }

    public void stage1_3()
    {
        if(DataController.Instance.current_stage == 3)
        {
            monster_init();
            DataController.Instance.monster_2_ID = "Bat";
            DataController.Instance.monster_hp = 15;
            DataController.Instance.monster_damage = 5;
            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 2000;
            DataController.Instance.get_stage_exp = 9;
        
        }
        DataController.Instance.current_stage = 3;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n15\n몹 평균 공격력\n5";
    }

    public void stage1_4()
    {
        if(DataController.Instance.current_stage == 4)
        {
            monster_init();

            DataController.Instance.monster_1_ID = "Bat";
            DataController.Instance.monster_hp = 17;
            DataController.Instance.monster_damage = 7;
            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 2500;
            DataController.Instance.get_stage_exp = 12;

        }
        DataController.Instance.current_stage = 4;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n17\n몹 평균 공격력\n7";
    }

    public void stage1_5()
    {
        if(DataController.Instance.current_stage == 5)
        {
            monster_init();
            DataController.Instance.monster_0_ID = "Spider";
            DataController.Instance.monster_hp = 20;
            DataController.Instance.monster_damage = 10;
            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 5000;
            DataController.Instance.get_stage_exp = 15;

        }
        DataController.Instance.current_stage = 5;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n20\n몹 평균 공격력\n10";
    }

    public void stage1_6()
    {
        if(DataController.Instance.current_stage == 6)
        {
            monster_init();
            DataController.Instance.monster_0_ID = "Spider";
            DataController.Instance.monster_hp = 23;
            DataController.Instance.monster_damage = 12;

            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 6000;
            DataController.Instance.get_stage_exp = 20;

        }
        DataController.Instance.current_stage = 6;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n23\n몹 평균 공격력\n12";
    }

    public void stage1_7()
    {
        if(DataController.Instance.current_stage == 7)
        {
            monster_init();

            DataController.Instance.monster_1_ID = "Spider";
            DataController.Instance.monster_hp = 800;
            DataController.Instance.monster_damage = 30;

            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 7000;
            DataController.Instance.get_stage_exp = 25;

        }
        DataController.Instance.current_stage = 7;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n25\n몹 평균 공격력\n15";
    }

    public void stage1_8()
    {
        if(DataController.Instance.current_stage == 8)
        {
            monster_init();
            DataController.Instance.monster_2_ID = "Spider";
            DataController.Instance.monster_hp = 27;
            DataController.Instance.monster_damage = 17;

            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 8000;
            DataController.Instance.get_stage_exp = 30;

        }
        DataController.Instance.current_stage = 8;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n27\n몹 평균 공격력\n17";
    }

    public void stage1_9()
    {
        if(DataController.Instance.current_stage == 9)
        {
            monster_init();
            DataController.Instance.monster_2_ID = "Spider";
            DataController.Instance.monster_hp = 30;
            DataController.Instance.monster_damage = 20;

            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 10000;
            DataController.Instance.get_stage_exp = 35;
        }
        DataController.Instance.current_stage = 9;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n30\n몹 평균 공격력\n20";
    }

    public void stage1_10()
    {
        if(DataController.Instance.current_stage == 10)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();

            DataController.Instance.monster_0_ID = "Gorem";
            DataController.Instance.monster_hp = 100;
            DataController.Instance.monster_damage = 7;
            //DataController.Instance.summon_monster_ID = "Bat";
            //DataController.Instance.summon_monster_hp = 1000;
            //DataController.Instance.summon_monster_damage = 40;

            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 20000;
            DataController.Instance.get_stage_exp = 50;
        }
        DataController.Instance.current_stage = 10;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n100\n몹 평균 공격력\n7";
    }

    public void stage1_11()
    {
        if(DataController.Instance.current_stage == 11)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_1_ID = "Gorem";
            DataController.Instance.monster_hp = 110;
            DataController.Instance.monster_damage = 10;
            //DataController.Instance.summon_monster_ID = "Bat";
            //DataController.Instance.summon_monster_hp = 1000;
            //DataController.Instance.summon_monster_damage = 40;

            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 30000;
            DataController.Instance.get_stage_exp = 60;
        }
        DataController.Instance.current_stage = 11;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n110\n몹 평균 공격력\n10";
    }

    public void stage1_12()
    {
        if(DataController.Instance.current_stage == 12)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_2_ID = "Gorem";
            DataController.Instance.monster_hp = 120;
            DataController.Instance.monster_damage = 15;
            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 50000;
            DataController.Instance.get_stage_exp = 70;
        }
        DataController.Instance.current_stage = 12;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n120\n몹 평균 공격력\n15";
    }

    public void stage1_13()
    {
        if(DataController.Instance.current_stage == 13)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();

            DataController.Instance.monster_1_ID = "Gorem";
            DataController.Instance.monster_hp = 1000;
            DataController.Instance.monster_damage = 70;

            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 80000;
            DataController.Instance.get_stage_exp = 70;
        }
        DataController.Instance.current_stage = 13;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n130\n몹 평균 공격력\n18";
    }

    public void stage1_14()
    {
        if(DataController.Instance.current_stage == 14)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_2_ID = "Gorem";
            DataController.Instance.monster_hp = 1300;
            DataController.Instance.monster_damage = 70;

            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 100000;
            DataController.Instance.get_stage_exp = 750;
        }
        DataController.Instance.current_stage = 14;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n140\n몹 평균 공격력\n18";
    }

    public void stage1_15()
    {
        if(DataController.Instance.current_stage == 15)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_0_ID = "Bat_2";
            DataController.Instance.monster_hp = 100;
            DataController.Instance.monster_damage = 35;


            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 100000;
            DataController.Instance.get_stage_exp = 75;
        }
        DataController.Instance.current_stage = 15;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n100\n몹 평균 공격력\n35";
    }

    public void stage1_16()
    {
        if(DataController.Instance.current_stage == 16)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_0_ID = "Bat_2";
            DataController.Instance.monster_hp = 105;
            DataController.Instance.monster_damage = 38;

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 150000;
            DataController.Instance.get_stage_exp = 80;
        }
        DataController.Instance.current_stage = 16;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n105\n몹 평균 공격력\n38";
    }

    public void stage1_17()
    {
        if(DataController.Instance.current_stage == 17)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();

            DataController.Instance.monster_2_ID = "Bat_2";
            DataController.Instance.monster_hp = 110;
            DataController.Instance.monster_damage = 42;

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 150000;
            DataController.Instance.get_stage_exp = 80;
        }
        DataController.Instance.current_stage = 17;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n110\n몹 평균 공격력\n42";
    }

    public void stage1_18()
    {
        if(DataController.Instance.current_stage == 18)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_0_ID = "Bat_2";
            DataController.Instance.monster_hp = 120;
            DataController.Instance.monster_damage = 45;


            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 180000;
            DataController.Instance.get_stage_exp = 90;
        }
        DataController.Instance.current_stage = 18;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n120\n몹 평균 공격력\n45";
    }

    public void stage1_19()
    {
        if(DataController.Instance.current_stage == 19)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_0_ID = "Bat_2";
            DataController.Instance.monster_hp = 125;
            DataController.Instance.monster_damage = 50;


            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 180000;
            DataController.Instance.get_stage_exp = 90;
        }
        DataController.Instance.current_stage = 19;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n125\n몹 평균 공격력\n50";
    }

    public void stage1_20()
    {
        if(DataController.Instance.current_stage == 20)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_1_ID = "Bat";
            DataController.Instance.monster_2_ID = "Bat_2";
            DataController.Instance.monster_2_hp = 100;
            DataController.Instance.monster_2_damage = 50;

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 200000;
            DataController.Instance.get_stage_exp = 100;
        }
        DataController.Instance.current_stage = 20;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n82\n몹 평균 공격력\n35";
    }

    //stage-2 궁수개방!!
    public void stage1_21()
    {
        if(DataController.Instance.current_stage == 21)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_1_ID = "Bat";
            DataController.Instance.monster_2_ID = "Bat_2";
            DataController.Instance.monster_hp = 110;
            DataController.Instance.monster_damage = 55;

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 210000;
            DataController.Instance.get_stage_exp = 100;
        }
        DataController.Instance.current_stage = 21;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n87\n몹 평균 공격력\n40";
    }

    public void stage1_22()
    {
        if(DataController.Instance.current_stage == 22)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_0_ID = "Bat";
            DataController.Instance.monster_1_ID = "Bat_2";
            DataController.Instance.monster_1_hp = 80;
            DataController.Instance.monster_1_damage = 80;

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 210000;
            DataController.Instance.get_stage_exp = 100;
        }
        DataController.Instance.current_stage = 22;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n89\n몹 평균 공격력\n42";
    }

    public void stage1_23()
    {
        if(DataController.Instance.current_stage == 23)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_0_ID = "Bat";
            DataController.Instance.monster_1_ID = "Bat_2";
            DataController.Instance.monster_hp = 85;
            DataController.Instance.monster_damage = 85;

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 210000;
            DataController.Instance.get_stage_exp = 100;
        }
        DataController.Instance.current_stage = 23;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n92\n몹 평균 공격력\n45";
    }

    public void stage1_24()
    {
        if(DataController.Instance.current_stage == 24)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_0_ID = "Bat";
            DataController.Instance.monster_2_ID = "Bat_2";
            DataController.Instance.monster_hp = 90;
            DataController.Instance.monster_damage = 90;

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 210000;
            DataController.Instance.get_stage_exp = 100;
        }
        DataController.Instance.current_stage = 24;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n97\n몹 평균 공격력\n50";
    }

    public void stage1_25()
    {
        if(DataController.Instance.current_stage == 25)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_0_ID = "Spider";
            DataController.Instance.monster_1_ID = "Bat";
            DataController.Instance.monster_hp = 90;
            DataController.Instance.monster_damage = 90;

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 250000;
            DataController.Instance.get_stage_exp = 120;
        }
        DataController.Instance.current_stage = 25;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n66\n몹 평균 공격력\n76";
    }

    public void stage1_26()
    {
        if(DataController.Instance.current_stage == 26)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_0_ID = "Bat";
            DataController.Instance.monster_1_ID = "Spider";
            DataController.Instance.monster_hp = 95;
            DataController.Instance.monster_damage = 95;
            

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 250000;
            DataController.Instance.get_stage_exp = 120;
        }
        DataController.Instance.current_stage = 26;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n69\n몹 평균 공격력\n79";
    }

    public void stage1_27()
    {
        if(DataController.Instance.current_stage == 27)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            
            DataController.Instance.monster_0_ID = "Spider";
            DataController.Instance.monster_1_ID = "Bat_2"; 
            DataController.Instance.monster_hp = 95;
            DataController.Instance.monster_damage = 95;
            

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 260000;
            DataController.Instance.get_stage_exp = 125;
        }
        DataController.Instance.current_stage = 27;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n87\n몹 평균 공격력\n92";
    }

    public void stage1_28()
    {
        if(DataController.Instance.current_stage == 28)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            
            DataController.Instance.monster_0_ID = "Spider";
            DataController.Instance.monster_2_ID = "Bat_2";  
            DataController.Instance.monster_hp = 100;
            DataController.Instance.monster_damage = 100;
            

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 260000;
            DataController.Instance.get_stage_exp = 125;
        }
        DataController.Instance.current_stage = 28;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n89\n몹 평균 공격력\n94";
    }

    public void stage1_29()
    {
        if(DataController.Instance.current_stage == 29)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            
            DataController.Instance.monster_0_ID = "Bat_2"; 
            DataController.Instance.monster_1_ID = "Spider";
            DataController.Instance.monster_1_hp = 105;
            DataController.Instance.monster_1_damage = 105;
            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 280000;
            DataController.Instance.get_stage_exp = 130;
        }
        DataController.Instance.current_stage = 29;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n91\n몹 평균 공격력\n96";
    }

    public void stage1_30()
    {
        if(DataController.Instance.current_stage == 30)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            
            DataController.Instance.monster_0_ID = "Bat_2"; 
            DataController.Instance.monster_1_ID = "Bat_2";
            DataController.Instance.monster_hp = 105;
            DataController.Instance.monster_damage = 105;
            
            

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 300000;
            DataController.Instance.get_stage_exp = 140;
        }
        DataController.Instance.current_stage = 30;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n104\n몹 평균 공격력\n74";
    }

    public void stage1_31()
    {
        if(DataController.Instance.current_stage == 31)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            
            DataController.Instance.monster_0_ID = "Bat_2"; 
            DataController.Instance.monster_1_ID = "Bat_2";
            DataController.Instance.monster_hp = 110;
            DataController.Instance.monster_damage = 110;
            
            

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 300000;
            DataController.Instance.get_stage_exp = 140;
        }
        DataController.Instance.current_stage = 31;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n106\n몹 평균 공격력\n76";
    }

    public void stage1_32()
    {
        if(DataController.Instance.current_stage == 32)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            
            DataController.Instance.monster_0_ID = "Bat_2"; 
            DataController.Instance.monster_1_ID = "Bat_2";
            DataController.Instance.monster_hp = 115;
            DataController.Instance.monster_damage = 115;
            
            

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 320000;
            DataController.Instance.get_stage_exp = 150;
        }
        DataController.Instance.current_stage = 32;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n108\n몹 평균 공격력\n78";
    }

    public void stage1_33()
    {
        if(DataController.Instance.current_stage == 33)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            
            DataController.Instance.monster_0_ID = "Bat_2"; 
            DataController.Instance.monster_2_ID = "Bat_2";
            DataController.Instance.monster_hp = 120;
            DataController.Instance.monster_damage = 120;
            
            

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 10000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 5;
        }
        DataController.Instance.current_stage = 33;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n150\n몹 평균 공격력\n76";
    }

    public void stage1_34()
    {
        if(DataController.Instance.current_stage == 34)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            
            DataController.Instance.monster_0_ID = "Bat_2"; 
            DataController.Instance.monster_2_ID = "Bat_2";
            DataController.Instance.monster_hp = 125;
            DataController.Instance.monster_damage = 125;
            
            

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 10000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 5;
        }
        DataController.Instance.current_stage = 34;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n300\n몹 평균 공격력\n50";
    }

    public void stage1_35()
    {
        if(DataController.Instance.current_stage == 35)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            
            DataController.Instance.monster_0_ID = "Bat_2"; 
            DataController.Instance.monster_1_ID = "Gorem";
            DataController.Instance.monster_hp = 125;
            DataController.Instance.monster_damage = 125;
            
            

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 10000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 5;
        }
        DataController.Instance.current_stage = 35;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n220\n몹 평균 공격력\n47";
    }

    public void stage1_36()
    {
        if(DataController.Instance.current_stage == 36)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            
            DataController.Instance.monster_0_ID = "Bat_2"; 
            DataController.Instance.monster_2_ID = "Gorem";
            DataController.Instance.monster_hp = 130;
            DataController.Instance.monster_damage = 130;
            
            

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 10000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 5;
        }
        DataController.Instance.current_stage = 36;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n223\n몹 평균 공격력\n50";
    }

    public void stage1_37()
    {
        if(DataController.Instance.current_stage == 37)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            
            DataController.Instance.monster_0_ID = "Bat_2"; 
            DataController.Instance.monster_1_ID = "Gorem";
            DataController.Instance.monster_hp = 135;
            DataController.Instance.monster_damage = 135;
            
            

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 10000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 5;
        }
        DataController.Instance.current_stage = 37;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n250\n몹 평균 공격력\n45";
    }

    public void stage1_38()
    {
        if(DataController.Instance.current_stage == 38)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            
            DataController.Instance.monster_0_ID = "Bat_2"; 
            DataController.Instance.monster_1_ID = "Gorem";
            DataController.Instance.monster_hp = 135;
            DataController.Instance.monster_damage = 135;
            
            

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 10000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 5;
        }
        DataController.Instance.current_stage = 38;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n1\n몹 평균 공격력\n100";
    }

    public void stage1_39()
    {
        if(DataController.Instance.current_stage == 39)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            
            DataController.Instance.monster_0_ID = "Gorem";
            DataController.Instance.monster_2_ID = "Bat_2"; 
            DataController.Instance.monster_hp = 140;
            DataController.Instance.monster_damage = 140;
            
            
            

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 10000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 5;
        }
        DataController.Instance.current_stage = 39;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n600\n몹 평균 공격력\n40";
    }

    public void stage1_40()
    {
        if(DataController.Instance.current_stage == 40)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            
            
            DataController.Instance.monster_0_ID = "Bat_2"; 
            DataController.Instance.monster_2_ID = "Gorem";
            DataController.Instance.monster_hp = 150;
            DataController.Instance.monster_damage = 150;

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = DataController.Instance.current_stage * 10000;
            DataController.Instance.get_stage_exp = DataController.Instance.current_stage * 5;
        }
        DataController.Instance.current_stage = 40;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n325\n몹 평균 공격력\n65";
    }

    public void stage1_41()
    {
        monster_init();
        DataController.Instance.monster_0_ID = "plant_monster"; 
        DataController.Instance.monster_2_ID = "Spider";
        DataController.Instance.monster_hp = 155;
        DataController.Instance.monster_damage = 155;
        set_hero_monster();

        if(DataController.Instance.current_stage == 41)
        {  
            Set_Hero_Base();
            set_fight_reward();
        }
        DataController.Instance.current_stage = 41;
        set_explanation_panel();
    }

    public void stage1_42()
    {
        monster_init();
        DataController.Instance.monster_0_ID = "plant_monster"; 
        DataController.Instance.monster_1_ID = "Spider";
        DataController.Instance.monster_hp = 160;
        DataController.Instance.monster_damage = 160;
        set_hero_monster();

        if(DataController.Instance.current_stage == 42)
        {  
            Set_Hero_Base();
            set_fight_reward();
        }
        DataController.Instance.current_stage = 42;
        set_explanation_panel();
    }

    public void stage1_43()
    {
        monster_init();
        DataController.Instance.monster_0_ID = "Spider"; 
        DataController.Instance.monster_1_ID = "plant_monster";
        DataController.Instance.monster_hp = 165;
        DataController.Instance.monster_damage = 165;
        set_hero_monster();

        if(DataController.Instance.current_stage == 43)
        {  
            Set_Hero_Base();
            set_fight_reward();
        }
        DataController.Instance.current_stage = 43;
        set_explanation_panel();
    }

    public void stage1_44()
    {
        monster_init();
        DataController.Instance.monster_0_ID = "Spider"; 
        DataController.Instance.monster_2_ID = "plant_monster";
        DataController.Instance.monster_hp = 165;
        DataController.Instance.monster_damage = 165;
        set_hero_monster();

        if(DataController.Instance.current_stage == 44)
        {  
            Set_Hero_Base();
            set_fight_reward();
        }
        DataController.Instance.current_stage = 44;
        set_explanation_panel();
    }

    public void stage1_45()
    {
        monster_init();
        DataController.Instance.monster_0_ID = "Spider"; 
        DataController.Instance.monster_2_ID = "plant_monster";
        DataController.Instance.monster_hp = 170;
        DataController.Instance.monster_damage = 170;
        set_hero_monster();

        if(DataController.Instance.current_stage == 45)
        {  
            Set_Hero_Base();
            set_fight_reward();
        }
        DataController.Instance.current_stage = 45;
        set_explanation_panel();
    }

    public void stage1_46()
    {
        monster_init();
        DataController.Instance.monster_0_ID = "Spider"; 
        DataController.Instance.monster_1_ID = "goblin_general";
        DataController.Instance.monster_hp = 170;
        DataController.Instance.monster_damage = 170;
        set_hero_monster();

        if(DataController.Instance.current_stage == 46)
        {  
            Set_Hero_Base();
            set_fight_reward();
        }
        DataController.Instance.current_stage = 46;
        set_explanation_panel();
    }

    public void stage1_47()
    {
        monster_init();
        DataController.Instance.monster_0_ID = "Spider"; 
        DataController.Instance.monster_2_ID = "Spider";
        DataController.Instance.monster_hp = 175;
        DataController.Instance.monster_damage = 175;
        set_hero_monster();

        if(DataController.Instance.current_stage == 47)
        {  
            Set_Hero_Base();
            set_fight_reward();
        }
        DataController.Instance.current_stage = 47;
        set_explanation_panel();
    }

    public void stage1_48()
    {
        monster_init();
        DataController.Instance.monster_0_ID = "Spider"; 
        DataController.Instance.monster_2_ID = "goblin_general";
        DataController.Instance.monster_hp = 180;
        DataController.Instance.monster_damage = 180;
        set_hero_monster();

        if(DataController.Instance.current_stage == 48)
        {  
            Set_Hero_Base();
            set_fight_reward();
        }
        DataController.Instance.current_stage = 48;
        set_explanation_panel();
    }

    public void stage1_49()
    {
        monster_init();
        DataController.Instance.monster_0_ID = "goblin_general"; 
        DataController.Instance.monster_2_ID = "goblin_general";
        DataController.Instance.monster_hp = 180;
        DataController.Instance.monster_damage = 180;
        set_hero_monster();

        if(DataController.Instance.current_stage == 49)
        {  
            Set_Hero_Base();
            set_fight_reward();
        }
        DataController.Instance.current_stage = 49;
        set_explanation_panel();
    }

    public void stage1_50()
    {
        monster_init();
        DataController.Instance.monster_0_ID = "goblin_general"; 
        DataController.Instance.monster_2_ID = "goblin_general";
        DataController.Instance.monster_hp = 180;
        DataController.Instance.monster_damage = 180;
        set_hero_monster();

        if(DataController.Instance.current_stage == 50)
        {  
            Set_Hero_Base();
            set_fight_reward();
        }
        DataController.Instance.current_stage = 50;
        set_explanation_panel();
    }

    public void stage1_51()
    {
        monster_init();
        DataController.Instance.monster_0_ID = "plant_monster"; 
        DataController.Instance.monster_2_ID = "goblin_general";
        DataController.Instance.monster_hp = 185;
        DataController.Instance.monster_damage = 185;
        set_hero_monster();

        if(DataController.Instance.current_stage == 51)
        {  
            Set_Hero_Base();
            set_fight_reward();
        }
        DataController.Instance.current_stage = 51;
        set_explanation_panel();
    }

    public void stage1_52()
    {
        monster_init();
        DataController.Instance.monster_0_ID = "plant_monster"; 
        DataController.Instance.monster_1_ID = "goblin_general";
        DataController.Instance.monster_hp = 190;
        DataController.Instance.monster_damage = 190;
        set_hero_monster();

        if(DataController.Instance.current_stage == 52)
        {  
            Set_Hero_Base();
            set_fight_reward();
        }
        DataController.Instance.current_stage = 52;
        set_explanation_panel();
    }

    public void stage1_53()
    {
        monster_init();
        DataController.Instance.monster_0_ID = "plant_monster"; 
        DataController.Instance.monster_1_ID = "Spider";
        DataController.Instance.monster_hp = 195;
        DataController.Instance.monster_damage = 195;
        set_hero_monster();

        if(DataController.Instance.current_stage == 53)
        {  
            Set_Hero_Base();
            set_fight_reward();
        }
        DataController.Instance.current_stage = 53;
        set_explanation_panel();
    }

    public void stage1_54()
    {
        monster_init();
        DataController.Instance.monster_0_ID = "plant_monster"; 
        DataController.Instance.monster_1_ID = "plant_monster";
        DataController.Instance.monster_hp = 200;
        DataController.Instance.monster_damage = 200;
        set_hero_monster();

        if(DataController.Instance.current_stage == 54)
        {  
            Set_Hero_Base();
            set_fight_reward();
        }
        DataController.Instance.current_stage = 54;
        set_explanation_panel();
    }

    public void stage1_55()
    {
        monster_init();
        DataController.Instance.monster_0_ID = "plant_monster"; 
        DataController.Instance.monster_1_ID = "plant_monster";
        DataController.Instance.monster_hp = 205;
        DataController.Instance.monster_damage = 205;
        set_hero_monster();

        if(DataController.Instance.current_stage == 55)
        {  
            Set_Hero_Base();
            set_fight_reward();
        }
        DataController.Instance.current_stage = 55;
        set_explanation_panel();
    }

    public void stage1_56()
    {
        monster_init();
        DataController.Instance.monster_0_ID = "plant_monster"; 
        DataController.Instance.monster_1_ID = "plant_monster";
        DataController.Instance.monster_2_ID = "plant_monster";
        DataController.Instance.monster_hp = 170;
        DataController.Instance.monster_damage = 170;
        set_hero_monster();

        if(DataController.Instance.current_stage == 56)
        {  
            Set_Hero_Base();
            set_fight_reward();
        }
        DataController.Instance.current_stage = 56;
        set_explanation_panel();
    }

    public void stage1_57()
    {
        monster_init();
        DataController.Instance.monster_0_ID = "goblin_general"; 
        DataController.Instance.monster_1_ID = "plant_monster";
        DataController.Instance.monster_2_ID = "plant_monster";
        DataController.Instance.monster_hp = 175;
        DataController.Instance.monster_damage = 175;
        set_hero_monster();

        if(DataController.Instance.current_stage == 57)
        {  
            Set_Hero_Base();
            set_fight_reward();
        }
        DataController.Instance.current_stage = 57;
        set_explanation_panel();
    }

    public void stage1_58()
    {
        monster_init();
        DataController.Instance.monster_0_ID = "goblin_general"; 
        DataController.Instance.monster_1_ID = "plant_monster";
        DataController.Instance.monster_2_ID = "plant_monster";
        DataController.Instance.monster_hp = 180;
        DataController.Instance.monster_damage = 180;
        set_hero_monster();

        if(DataController.Instance.current_stage == 58)
        {  
            Set_Hero_Base();
            set_fight_reward();
        }
        DataController.Instance.current_stage = 58;
        set_explanation_panel();
    }

    public void stage1_59()
    {
        monster_init();
        DataController.Instance.monster_0_ID = "goblin_general"; 
        DataController.Instance.monster_1_ID = "goblin_general";
        DataController.Instance.monster_2_ID = "Spider";
        DataController.Instance.monster_hp = 185;
        DataController.Instance.monster_damage = 185;
        set_hero_monster();

        if(DataController.Instance.current_stage == 59)
        {  
            Set_Hero_Base();
            set_fight_reward();
        }
        DataController.Instance.current_stage = 59;
        set_explanation_panel();
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

    void set_explanation_panel()
    {
        long m_0_hp = DataController.Instance.monster_hp * (long)monster_0.GetComponent<EnemyController>().health_ratio;
        long m_1_hp = DataController.Instance.monster_hp * (long)monster_1.GetComponent<EnemyController>().health_ratio;
        long m_2_hp = DataController.Instance.monster_hp * (long)monster_2.GetComponent<EnemyController>().health_ratio;

        long m_0_dm = DataController.Instance.monster_damage * (long)monster_0.GetComponent<EnemyController>().attack_ratio;
        long m_1_dm = DataController.Instance.monster_damage * (long)monster_1.GetComponent<EnemyController>().attack_ratio;
        long m_2_dm = DataController.Instance.monster_damage * (long)monster_2.GetComponent<EnemyController>().attack_ratio;

        
        String avg_hp = "" + ((m_0_hp + m_1_hp + m_2_hp) / 3);
        String avg_dm = "" + ((m_0_dm + m_1_dm + m_2_dm) / 3);

        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n" + avg_hp + "\n몹 평균 공격력\n" + avg_dm;
    }


    //stage-3 마법사 해방!!

    public void tower_of_diamond()
    {
        int monster_kindCount = 20;
        long tower_stage = DataController.Instance.tower_stage;
        long repeatCount = tower_stage / monster_kindCount;
        if(repeatCount == 0)
        {
            repeatCount = 1;
        }
        DataController.Instance.tower_repeatCount = repeatCount;
        tower_previous_stage = DataController.Instance.tower_stage;
        

        monster_init();
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
            DataController.Instance.monster_2_ID = "Gorem"; 
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
            DataController.Instance.monster_2_ID = "skeleton_rancer"; 
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
        else {
            DataController.Instance.monster_2_ID = "Bat"; 
        }
        
        DataController.Instance.monster_hp = repeatCount * 100;
        DataController.Instance.monster_damage = repeatCount * 100;
        Set_Hero_Base();
        
        DataController.Instance.get_stage_gold = tower_stage * 10000000 * repeatCount;
        DataController.Instance.get_stage_exp = tower_stage * 5000 * repeatCount;
        DataController.Instance.get_stage_crystal = (tower_stage * 10) * repeatCount;
        DataController.Instance.current_stage = -1;

    }
    



    public void MyPosition (Transform transform)
    {
        var tra = transform;
        Fight_SelectFrame.SetActive(true);
        Fight_SelectFrame.transform.position = tra.position;
        Fight_SelectFrame.transform.parent = tra.transform;
        goToPanel.Instance.stage_explain_panel.SetActive(true);
    }

    

    

    
}
