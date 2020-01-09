using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class fightPanelTimer : MonoBehaviour
{
    private static fightPanelTimer instance;

    public static fightPanelTimer Instance
    {
        get{
            if(instance == null)
            {
                instance = FindObjectOfType<fightPanelTimer>();

                if(instance == null)
                {
                    GameObject container = new GameObject("fightPanelTimer");

                    instance = container.AddComponent<fightPanelTimer>();
                }
            }
            return instance;
        }
    }

    public long damage_test_current_heart   
    {
        get
        {
            if(!PlayerPrefs.HasKey("damage_test_current_heart")) // 골드가 없을떄
            {
                return 3;
            }
            string tmpdamage_test_current_heart = PlayerPrefs.GetString("damage_test_current_heart");
            return long.Parse(tmpdamage_test_current_heart);
        }
        set
        {
            PlayerPrefs.SetString("damage_test_current_heart", value.ToString()); 
        }
    }

    public long damage_test_max_heart
    {
        get
        {
            if(!PlayerPrefs.HasKey("damage_test_max_heart")) // 골드가 없을떄
            {
                return 3;
            }
            string tmpdamage_test_max_heart = PlayerPrefs.GetString("damage_test_max_heart");
            return long.Parse(tmpdamage_test_max_heart);
        }
        set
        {
            PlayerPrefs.SetString("damage_test_max_heart", value.ToString()); 
        }
    }

    public float damage_test_limit_time
    {
        get
        {
            if(!PlayerPrefs.HasKey("damage_test_limit_time")) // 골드가 없을떄
            {
                return 7200;
            }
            string tmpdamage_test_limit_time = PlayerPrefs.GetString("damage_test_limit_time");
            return long.Parse(tmpdamage_test_limit_time);
        }
        set
        {
            PlayerPrefs.SetString("damage_test_limit_time", value.ToString()); 
        }
    }

    public long current_heart = 1;
    long max_heart = 1;
    public float damage_test_LimitTime;
    public Text text_Timer;

    //btn
    public GameObject Test_damage_btn;
    public Text dps_heart_text;
    public Text infinity_heart_text;

    //infinity
    public long infinity_mode_heart = 1;
    public float infinity_mode_LimitTime;
    public Text infinity_text_timer;

    //영웅 진영 반복체크
    public GameObject[] hero_cardDropZone = new GameObject[0];
    public GameObject[] hero_card = new GameObject[0];

    void Start()
    {
        //dps
        damage_test_LimitTime = damage_test_limit_time - DataController.Instance.timeAfterLastPlay;
        current_heart = damage_test_current_heart;
        max_heart = damage_test_max_heart;
        StartCoroutine("Timer");

        infinity_mode_LimitTime = PlayerPrefs.GetFloat("infinity_mode_LimitTime") - DataController.Instance.timeAfterLastPlay;
        if(!PlayerPrefs.HasKey("infinity_mode_heart")) // 골드가 없을떄
        {
            PlayerPrefs.SetInt("infinity_mode_heart", 3);
        }
        infinity_mode_heart = PlayerPrefs.GetInt("infinity_mode_heart");
        StartCoroutine("Timer2");


        //StartCoroutine("repeat_hero_dispose_check");
    }

    public void charge_heart()
    {
        current_heart = 3;
        damage_test_LimitTime = 7200;
        text_Timer.text = "보상 흭득가능";

        infinity_mode_heart = 3;
        infinity_mode_LimitTime = 7200;
        infinity_text_timer.text = "보상 흭득가능";
    }

    IEnumerator Timer()
    {
        while(true)
        {
            //Debug.Log("timer is working");
            //Debug.Log("damage_test_LimitTime : " + damage_test_LimitTime);
            while(damage_test_LimitTime < 0)
            {
                damage_test_LimitTime = 7200;  //LifeTime - 지나간 시간 라이프타임 = 1200;  
                current_heart += 1;
                if(current_heart > 3)
                {
                    current_heart = 3;
                }
            }
            if(current_heart < 3) //충전 1200초 = 20분
            {
                damage_test_LimitTime -= 1;
                text_Timer.text = Math.Truncate(Mathf.Round(damage_test_LimitTime)/60)  + ":" + Math.Truncate(Mathf.Round(damage_test_LimitTime)%60%60);
                dps_heart_text.text = "" + current_heart + "/" + 3;
            }
            else //하트가 충전되면
            {
                if(current_heart < 3)
                {
                    current_heart += 1;
                    damage_test_LimitTime = 7200;
                }
                text_Timer.text = "";
                dps_heart_text.text = "" + current_heart + "/" + 3;
            }

            damage_test_limit_time = damage_test_LimitTime;
            damage_test_current_heart = current_heart;
            damage_test_max_heart = max_heart;
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator Timer2()
    {
        while(true)
        {
            while(infinity_mode_LimitTime < 0)
            {
                infinity_mode_LimitTime = 7200;  //LifeTime - 지나간 시간 라이프타임 = 1200;  
                infinity_mode_heart += 1;
                if(infinity_mode_heart > 3)
                {
                    infinity_mode_heart = 3;
                }
            }
            if(infinity_mode_heart < 3) //충전 1200초 = 20분
            {
                infinity_mode_LimitTime --;
                infinity_text_timer.text = Math.Truncate(Mathf.Round(infinity_mode_LimitTime)/60)  + ":" + Math.Truncate(Mathf.Round(infinity_mode_LimitTime)%60%60);
                infinity_heart_text.text = "" + infinity_mode_heart + "/" + 3;
            }
            else //하트가 충전되면
            {
                if(infinity_mode_heart < 3)
                {
                    infinity_mode_heart += 1;
                    infinity_mode_LimitTime = 7200;
                }
                infinity_text_timer.text = "";
                infinity_heart_text.text = "" + infinity_mode_heart + "/" + 3;
                
            }

            PlayerPrefs.SetFloat("infinity_mode_LimitTime", infinity_mode_LimitTime);
            PlayerPrefs.SetInt("infinity_mode_heart", (int)infinity_mode_heart);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator repeat_hero_dispose_check()
    {
        while(true)
        {
            for(int i = 0; i<3; i++)
            {
                hero_cardDropZone[i].GetComponent<CardDropZone>().checkBase();
                
            }
            yield return new WaitForSeconds(10f);
        }
    }

    public void location_debug()
    {
        for(int i = 0; i<3; i++)
        {
            Debug.Log((hero_card[i].GetComponent<CharacterCard>().hero_name) + " is " + PlayerPrefs.GetInt(hero_card[i].GetComponent<CharacterCard>().hero_name));
        }
    }

    public void set_location_all()
    {
        for(int i = 0; i<3; i++)
        {
                hero_card[i].GetComponent<CharacterCard>().SetLocation();
        }
    }
    


}
