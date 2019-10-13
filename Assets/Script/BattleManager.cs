using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleManager : MonoBehaviour
{
    bool IsPause;
    bool getMoney = false;
    bool getExp = false;
    bool getCrystal = false;

    public long random_get_crystal = 0;

    private string notice_string;
    private int reward_item_id;
    private int reward_item_count;

    public GameObject result_crystal_panel;

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

    //hero
    public GameObject dummy;
    public GameObject knight;
    public GameObject archer;

    public GameObject knight_01;
    public GameObject knight_02;

    public GameObject archer_01;
    public GameObject archer_02;

    //Enemy
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

    //stage
 
    // Use this for initialization
    void Start () {
        IsPause = false;
        StartCoroutine("Auto");
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
        for(int i = 0; i < 21; i++) // 21을 max_current_Stage추가해서 교체
            {
                SetBool("ch1" + i, false);
            }
        SetBool("ch1" + 0, true);    //ch1-1만 활성화
    }

    public void clear_all_stage()
    {
        for(int i = 0; i < 21; i++) // 21을 max_current_Stage추가해서 교체
            {
                SetBool("ch1" + i, true);
            }
        SetBool("ch1" + 0, true);    //ch1-1만 활성화
    }

    public void debug_stage()
    {
        for(int i = 0; i < 21; i++) // 21을 max_current_Stage추가해서 교체
            {
                Debug.Log("ch1 " + i + " is " + GetBool("ch1" + i));
            }
    }

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



    IEnumerator Auto()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            if(enemies.Length == 0)  //승리할시
            {
                //Debug.Log("Enemies are dead!!");
                if(DataController.Instance.get_stage_crystal == 0 )
                {
                    result_crystal_panel.SetActive(false);
                }
                goToPanel.Instance.show_result_panel();
                for(int i = 0; i < 21; i++) // 21을 max_current_Stage추가해서 교체
                {
                    if(DataController.Instance.current_stage == i) //1스테이지를 꺠면 1번쨰 요소(2스테이지)가 true
                    {
                        SetBool("ch1" + i, true);
                    }
                }
            }
            else if(player.Length == 0)  //패배할 시
            {
                DataController.Instance.get_stage_gold = 0;
                DataController.Instance.get_stage_exp = 0;
                //결과 문구를 "승리"에서 "패배"로 바꾸기
                goToPanel.Instance.show_result_panel();
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

    public void Set_Hero_Base()
    {
         
        GameObject hero_0 = null;
        GameObject hero_1 = null;
        GameObject hero_2 = null;
        GameObject monster_0 = null;
        GameObject monster_1 = null;
        GameObject monster_2 = null;
        long hero_0_name = DataController.Instance.hero_0_ID;
        long hero_1_name = DataController.Instance.hero_1_ID;
        long hero_2_name = DataController.Instance.hero_2_ID;
        string monster_0_name = DataController.Instance.monster_0_ID;
        string monster_1_name = DataController.Instance.monster_1_ID;
        string monster_2_name = DataController.Instance.monster_2_ID;
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
            
            default:
                monster_0 = dummy;
                break;
        }
        switch(monster_1_name)
        {
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

            default:
                monster_1 = dummy;
                break;
           
        }
        switch(monster_2_name)
        {
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

            default:
                monster_2 = dummy;
                break;
            
        }

        GameObject clone_player0 = Instantiate(hero_0, hero_spawn_point0);
        GameObject clone_player1 = Instantiate(hero_1, hero_spawn_point1);
        GameObject clone_player2 = Instantiate(hero_2, hero_spawn_point2);
        
        GameObject clone_enemy0 = Instantiate(monster_0, Spawn_point0);
        GameObject clone_enemy1 = Instantiate(monster_1, Spawn_point1);
        GameObject clone_enemy2 = Instantiate(monster_2, Spawn_point2);
        clone_enemy0.GetComponent<EnemyController>().Max_HP = DataController.Instance.monster_0_hp;
        clone_enemy0.GetComponent<EnemyController>().damage = DataController.Instance.monster_0_damage;
        clone_enemy1.GetComponent<EnemyController>().Max_HP = DataController.Instance.monster_1_hp;
        clone_enemy1.GetComponent<EnemyController>().damage = DataController.Instance.monster_1_damage;
        clone_enemy2.GetComponent<EnemyController>().Max_HP = DataController.Instance.monster_2_hp;
        clone_enemy2.GetComponent<EnemyController>().damage = DataController.Instance.monster_2_damage;

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
        random_get_crystal = Random.Range(0, 6); //0~5
        DataController.Instance.get_stage_crystal = random_get_crystal;
    }

    public void only_debug()
    {
        Debug.Log("DataController.Instance.monster_2_ID " + DataController.Instance.monster_2_ID);
        Debug.Log("DataController.Instance.monster_2_damage " + DataController.Instance.monster_2_damage);
        Debug.Log("DataController.Instance.monster_2_hp " + DataController.Instance.monster_2_hp);
    }
    //스테이지마다 나오는 몬스터의 능력치 수치를 조정하기 보단
    //능력치 수치가 다른 몬스터를 여러개 만들어서 prefab에서 가져오기
    public void stage1_1()
    {
        if(DataController.Instance.current_stage == 1)
        {
            monster_init();  //get_stage_gold, exp도 초기화
            DataController.Instance.monster_2_ID = "Bat";
            DataController.Instance.monster_2_damage = 5;      //3
            DataController.Instance.monster_2_hp = 200;       //100
            Set_Hero_Base();
            //보상
            DataController.Instance.get_stage_gold = 500;
            DataController.Instance.get_stage_exp = 3;
            DataController.Instance.get_stage_crystal = 3;
        }
        
        DataController.Instance.current_stage = 1;
        Debug.Log("current_Stage is " + DataController.Instance.current_stage);
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n100\n몹 평균 공격력\n3";
    }

    public void stage1_2()
    {
        if(DataController.Instance.current_stage == 2)
        {
            monster_init();
            DataController.Instance.monster_0_ID = "Bat";
            DataController.Instance.monster_0_hp = 200;
            DataController.Instance.monster_0_damage = 5;
            DataController.Instance.monster_1_ID = "Bat";
            DataController.Instance.monster_1_hp = 200;
            DataController.Instance.monster_1_damage = 5;
            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 1000;
            DataController.Instance.get_stage_exp = 6;

        }
        DataController.Instance.current_stage = 2;
        Debug.Log("current_Stage is " + DataController.Instance.current_stage);
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n200\n몹 평균 공격력\n5";
    }

    public void stage1_3()
    {
        if(DataController.Instance.current_stage == 3)
        {
            monster_init();
            DataController.Instance.monster_2_ID = "Spider";
            DataController.Instance.monster_2_hp = 400;
            DataController.Instance.monster_2_damage = 15;
            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 2000;
            DataController.Instance.get_stage_exp = 9;
        
        }
        DataController.Instance.current_stage = 3;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n400\n몹 평균 공격력\n15";
    }

    public void stage1_4()
    {
        if(DataController.Instance.current_stage == 4)
        {
            monster_init();
            DataController.Instance.monster_0_ID = "Bat";
            DataController.Instance.monster_0_hp = 400;
            DataController.Instance.monster_0_damage = 10;
            DataController.Instance.monster_1_ID = "Bat";
            DataController.Instance.monster_1_hp = 400;
            DataController.Instance.monster_1_damage = 10;
            DataController.Instance.monster_2_ID = "Bat";
            DataController.Instance.monster_2_hp = 400;
            DataController.Instance.monster_2_damage = 10;
            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 2500;
            DataController.Instance.get_stage_exp = 12;

        }
        DataController.Instance.current_stage = 4;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n400\n몹 평균 공격력\n10";
    }

    public void stage1_5()
    {
        if(DataController.Instance.current_stage == 5)
        {
            monster_init();
            DataController.Instance.monster_0_ID = "Spider";
            DataController.Instance.monster_0_hp = 500;
            DataController.Instance.monster_0_damage = 20;
            DataController.Instance.monster_1_ID = "Bat";
            DataController.Instance.monster_1_hp = 400;
            DataController.Instance.monster_1_damage = 10;

            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 5000;
            DataController.Instance.get_stage_exp = 15;

        }
        DataController.Instance.current_stage = 5;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n450\n몹 평균 공격력\n15";
    }

    public void stage1_6()
    {
        if(DataController.Instance.current_stage == 6)
        {
            monster_init();
            DataController.Instance.monster_0_ID = "Spider";
            DataController.Instance.monster_0_hp = 700;
            DataController.Instance.monster_0_damage = 25;
            DataController.Instance.monster_1_ID = "Bat";
            DataController.Instance.monster_1_hp = 500;
            DataController.Instance.monster_1_damage = 15;
            DataController.Instance.monster_2_ID = "Bat";
            DataController.Instance.monster_2_hp = 500;
            DataController.Instance.monster_2_damage = 15;

            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 6000;
            DataController.Instance.get_stage_exp = 20;

        }
        DataController.Instance.current_stage = 6;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n550\n몹 평균 공격력\n18";
    }

    public void stage1_7()
    {
        if(DataController.Instance.current_stage == 7)
        {
            monster_init();
            DataController.Instance.monster_0_ID = "Spider";
            DataController.Instance.monster_0_hp = 800;
            DataController.Instance.monster_0_damage = 30;
            DataController.Instance.monster_1_ID = "Spider";
            DataController.Instance.monster_1_hp = 800;
            DataController.Instance.monster_1_damage = 30;
            DataController.Instance.monster_2_ID = "Bat";
            DataController.Instance.monster_2_hp = 600;
            DataController.Instance.monster_2_damage = 20;

            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 7000;
            DataController.Instance.get_stage_exp = 25;

        }
        DataController.Instance.current_stage = 7;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n700\n몹 평균 공격력\n25";
    }

    public void stage1_8()
    {
        if(DataController.Instance.current_stage == 8)
        {
            monster_init();
            DataController.Instance.monster_0_ID = "Spider";
            DataController.Instance.monster_0_hp = 900;
            DataController.Instance.monster_0_damage = 35;
            DataController.Instance.monster_1_ID = "Bat";
            DataController.Instance.monster_1_hp = 900;
            DataController.Instance.monster_1_damage = 35;
            DataController.Instance.monster_2_ID = "Spider";
            DataController.Instance.monster_2_hp = 700;
            DataController.Instance.monster_2_damage = 25;

            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 8000;
            DataController.Instance.get_stage_exp = 30;

        }
        DataController.Instance.current_stage = 8;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n800\n몹 평균 공격력\n28";
    }

    public void stage1_9()
    {
        if(DataController.Instance.current_stage == 9)
        {
            monster_init();
            DataController.Instance.monster_0_ID = "Spider";
            DataController.Instance.monster_0_hp = 1000;
            DataController.Instance.monster_0_damage = 40;
            DataController.Instance.monster_1_ID = "Spider";
            DataController.Instance.monster_1_hp = 1000;
            DataController.Instance.monster_1_damage = 40;
            DataController.Instance.monster_2_ID = "Spider";
            DataController.Instance.monster_2_hp = 1000;
            DataController.Instance.monster_2_damage = 40;

            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 10000;
            DataController.Instance.get_stage_exp = 35;
        }
        DataController.Instance.current_stage = 9;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n1000\n몹 평균 공격력\n40";
    }

    public void stage1_10()
    {
        if(DataController.Instance.current_stage == 10)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_0_ID = "Bat";
            DataController.Instance.monster_0_hp = 1000;
            DataController.Instance.monster_0_damage = 40;
            DataController.Instance.monster_2_ID = "Gorem";
            DataController.Instance.monster_2_hp = 1000;
            DataController.Instance.monster_2_damage = 50;
            //DataController.Instance.summon_monster_ID = "Bat";
            //DataController.Instance.summon_monster_hp = 1000;
            //DataController.Instance.summon_monster_damage = 40;

            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 20000;
            DataController.Instance.get_stage_exp = 50;
        }
        DataController.Instance.current_stage = 10;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n1000\n몹 평균 공격력\n45";
    }

    public void stage1_11()
    {
        if(DataController.Instance.current_stage == 11)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_0_ID = "Bat";
            DataController.Instance.monster_0_hp = 1000;
            DataController.Instance.monster_0_damage = 50;
            DataController.Instance.monster_1_ID = "Bat";
            DataController.Instance.monster_1_hp = 1000;
            DataController.Instance.monster_1_damage = 50;
            DataController.Instance.monster_2_ID = "Gorem";
            DataController.Instance.monster_2_hp = 1000;
            DataController.Instance.monster_2_damage = 50;
            //DataController.Instance.summon_monster_ID = "Bat";
            //DataController.Instance.summon_monster_hp = 1000;
            //DataController.Instance.summon_monster_damage = 40;

            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 30000;
            DataController.Instance.get_stage_exp = 60;
        }
        DataController.Instance.current_stage = 11;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n1000\n몹 평균 공격력\n50";
    }

    public void stage1_12()
    {
        if(DataController.Instance.current_stage == 12)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_0_ID = "Bat";
            DataController.Instance.monster_0_hp = 1000;
            DataController.Instance.monster_0_damage = 60;
            DataController.Instance.monster_1_ID = "Spider";
            DataController.Instance.monster_1_hp = 1300;
            DataController.Instance.monster_1_damage = 60;
            DataController.Instance.monster_2_ID = "Gorem";
            DataController.Instance.monster_2_hp = 1000;
            DataController.Instance.monster_2_damage = 60;
            //DataController.Instance.summon_monster_ID = "Bat";
            //DataController.Instance.summon_monster_hp = 1000;
            //DataController.Instance.summon_monster_damage = 40;

            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 50000;
            DataController.Instance.get_stage_exp = 70;
        }
        DataController.Instance.current_stage = 12;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n1200\n몹 평균 공격력\n60";
    }

    public void stage1_13()
    {
        if(DataController.Instance.current_stage == 13)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_0_ID = "Spider";
            DataController.Instance.monster_0_hp = 1300;
            DataController.Instance.monster_0_damage = 60;
            DataController.Instance.monster_1_ID = "Gorem";
            DataController.Instance.monster_1_hp = 1000;
            DataController.Instance.monster_1_damage = 70;
            DataController.Instance.monster_2_ID = "Spider";
            DataController.Instance.monster_2_hp = 1300;
            DataController.Instance.monster_2_damage = 60;
            //DataController.Instance.summon_monster_ID = "Bat";
            //DataController.Instance.summon_monster_hp = 1000;
            //DataController.Instance.summon_monster_damage = 40;

            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 80000;
            DataController.Instance.get_stage_exp = 70;
        }
        DataController.Instance.current_stage = 13;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n1200\n몹 평균 공격력\n65";
    }

    public void stage1_14()
    {
        if(DataController.Instance.current_stage == 14)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_0_ID = "Spider";
            DataController.Instance.monster_0_hp = 1300;
            DataController.Instance.monster_0_damage = 60;
            DataController.Instance.monster_1_ID = "Spider";
            DataController.Instance.monster_1_hp = 1300;
            DataController.Instance.monster_1_damage = 60;
            DataController.Instance.monster_2_ID = "Gorem";
            DataController.Instance.monster_2_hp = 1300;
            DataController.Instance.monster_2_damage = 70;
            //DataController.Instance.summon_monster_ID = "Bat";
            //DataController.Instance.summon_monster_hp = 1000;
            //DataController.Instance.summon_monster_damage = 40;

            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 100000;
            DataController.Instance.get_stage_exp = 750;
        }
        DataController.Instance.current_stage = 14;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n1200\n몹 평균 공격력\n65";
    }

    public void stage1_15()
    {
        if(DataController.Instance.current_stage == 15)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_0_ID = "Bat_2";
            DataController.Instance.monster_0_hp = 5000;
            DataController.Instance.monster_0_damage = 200;

            //DataController.Instance.summon_monster_ID = "Bat";
            //DataController.Instance.summon_monster_hp = 1000;
            //DataController.Instance.summon_monster_damage = 40;

            Set_Hero_Base();

            DataController.Instance.get_stage_gold = 100000;
            DataController.Instance.get_stage_exp = 75;
        }
        DataController.Instance.current_stage = 15;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n5000\n몹 평균 공격력\n200";
    }

    public void stage1_16()
    {
        if(DataController.Instance.current_stage == 16)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_0_ID = "Bat_2";
            DataController.Instance.monster_0_hp = 5000;
            DataController.Instance.monster_0_damage = 150;
            DataController.Instance.monster_1_ID = "Bat";
            DataController.Instance.monster_1_hp = 2000;
            DataController.Instance.monster_1_damage = 60;

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 150000;
            DataController.Instance.get_stage_exp = 80;
        }
        DataController.Instance.current_stage = 16;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n5000\n몹 평균 공격력\n150";
    }

    public void stage1_17()
    {
        if(DataController.Instance.current_stage == 17)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_0_ID = "Bat";
            DataController.Instance.monster_0_hp = 2000;
            DataController.Instance.monster_0_damage = 60;
            DataController.Instance.monster_0_ID = "Bat";
            DataController.Instance.monster_0_hp = 2000;
            DataController.Instance.monster_0_damage = 60;
            DataController.Instance.monster_2_ID = "Bat_2";
            DataController.Instance.monster_2_hp = 5000;
            DataController.Instance.monster_2_damage = 150;

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 150000;
            DataController.Instance.get_stage_exp = 80;
        }
        DataController.Instance.current_stage = 17;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n5000\n몹 평균 공격력\n150";
    }

    public void stage1_18()
    {
        if(DataController.Instance.current_stage == 18)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_0_ID = "Bat_2";
            DataController.Instance.monster_0_hp = 5000;
            DataController.Instance.monster_0_damage = 150;
            DataController.Instance.monster_1_ID = "Bat";
            DataController.Instance.monster_1_hp = 2000;
            DataController.Instance.monster_1_damage = 60;
            DataController.Instance.monster_2_ID = "Spider";
            DataController.Instance.monster_2_hp = 2500;
            DataController.Instance.monster_2_damage = 70;

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 150000;
            DataController.Instance.get_stage_exp = 80;
        }
        DataController.Instance.current_stage = 18;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n5000\n몹 평균 공격력\n150";
    }

    public void stage1_19()
    {
        if(DataController.Instance.current_stage == 19)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_0_ID = "Bat_2";
            DataController.Instance.monster_0_hp = 5000;
            DataController.Instance.monster_0_damage = 150;
            DataController.Instance.monster_1_ID = "Gorem";
            DataController.Instance.monster_1_hp = 2000;
            DataController.Instance.monster_1_damage = 60;
            DataController.Instance.monster_2_ID = "Spider";
            DataController.Instance.monster_2_hp = 2500;
            DataController.Instance.monster_2_damage = 70;

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 150000;
            DataController.Instance.get_stage_exp = 80;
        }
        DataController.Instance.current_stage = 19;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n5000\n몹 평균 공격력\n150";
    }

    public void stage1_20()
    {
        if(DataController.Instance.current_stage == 20)
        {  //문제점 가끔 골렘의 돌맹이가 영웅에게 안맞음
            monster_init();
            DataController.Instance.monster_0_ID = "Bat_2";
            DataController.Instance.monster_0_hp = 5000;
            DataController.Instance.monster_0_damage = 200;
            DataController.Instance.monster_1_ID = "Bat_2";
            DataController.Instance.monster_1_hp = 5000;
            DataController.Instance.monster_1_damage = 200;
            DataController.Instance.monster_2_ID = "Bat_2";
            DataController.Instance.monster_2_hp = 5000;
            DataController.Instance.monster_2_damage = 200;

            Set_Hero_Base();
            DataController.Instance.get_stage_gold = 150000;
            DataController.Instance.get_stage_exp = 80;
        }
        DataController.Instance.current_stage = 20;
        show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
        Stage_explain.text = "몹 평균 체력\n5000\n몹 평균 공격력\n200";
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
