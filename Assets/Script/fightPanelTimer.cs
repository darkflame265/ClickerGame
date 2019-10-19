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
                return 0;
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
                return 1;
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

    void Start()
    {
        damage_test_LimitTime = damage_test_limit_time - DataController.Instance.timeAfterLastPlay;
        current_heart = damage_test_current_heart;
        max_heart = damage_test_max_heart;
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        while(true)
        {
            while(damage_test_LimitTime < 0)
                {
                    //int count = (int)LimitTime / DataController.Instance.timeAfterLastPlay;
                    //current_heart += count;
                   // count = 0;
                    damage_test_LimitTime += 7200;  //LifeTime - 지나간 시간 라이프타임 = 1200;  
                    current_heart += 1;
                }
            if(current_heart < max_heart) //충전 1200초 = 20분
            {
                Test_damage_btn.GetComponent<Button>().interactable = false;
                damage_test_LimitTime -= 1;
                //text_Timer.text = "충전시간 : " + Mathf.Round(LimitTime)/60 + "분 " + Mathf.Round(LimitTime)/60%60 + "초";
                text_Timer.text = Math.Truncate(Mathf.Round(damage_test_LimitTime)/60)  + ":" + Math.Truncate(Mathf.Round(damage_test_LimitTime)%60%60);
                
            }
            else //하트가 충전되면
            {
                Test_damage_btn.GetComponent<Button>().interactable = true;
                current_heart = max_heart;
                damage_test_LimitTime = 7200;
            }

            damage_test_limit_time = damage_test_LimitTime;
            damage_test_current_heart = current_heart;
            damage_test_max_heart = max_heart;
            yield return new WaitForSeconds(1f);
        }
    }


}
