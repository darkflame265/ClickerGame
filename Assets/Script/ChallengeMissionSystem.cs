﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChallengeMissionSystem : MonoBehaviour
{

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

    //mission Panel List
    public GameObject[] missionList;

    long ms1;
    long ms2;
    long ms3;
    long ms4;
    long ms5;
    long ms6;

    //클릭, 레벨, 골드레벨, 스탯, 스테이지, 데미지측정
    public long[] ms_1 = {100, 500, 1000, 2000, 5000, 10000, 15000, 30000, 50000, 75000, 100000, 200000, 300000, 400000, 500000, 750000, 100000, 125000, 150000, 175000, 200000, 300000, 400000, 500000, 750000, 1000000, 1250000, 1500000, 2000000};
    public long[] ms_2 = {3, 5, 7, 10, 13, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120, 125, 130, 135, 140, 145, 150, 155, 160, 165, 170, 175, 180, 185, 190, 195, 200};
    public long[] ms_3 = {5, 10, 30, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200, 210, 220, 230, 240, 250, 260, 270, 280, 290, 300, 310, 320, 330, 340, 350, 360, 370, 380, 390, 400, 410, 420, 430, 440, 450, 460, 470, 480, 490, 500};
    public long[] ms_4 = {10, 30, 50, 75, 100, 150, 200, 300, 500, 750, 1000, 1250, 1500, 1750, 2000, 2250, 2500, 2750, 3000, 3250, 3500, 3750, 4000, 4250, 4500, 4750, 5000, 5250, 5500, 5750, 6000, 6250, 6500, 6750, 7000, 7250, 7500, 7750, 8000, 8250, 8500, 8750, 9000, 9250, 9500, 9750, 10000, 10250, 10500, 10750, 11000, 11250, 11500, 11750, 12000, 12250, 12500, 12750, 13000};
    public long[] ms_5 = {3, 5, 7, 10, 13, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120, 125, 130, 135, 140, 145, 150, 155, 160, 165, 170, 175, 180, 185, 190, 195, 200};
    public long[] ms_6 = {100, 500, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 10000, 12500, 15000, 17500, 20000, 22500, 25000, 27500, 30000, 32500, 35000, 37500, 40000, 42500, 45000, 47500, 50000, 52500, 55000, 57500, 60000, 62500, 65000, 67500, 70000, 72500, 75000, 77500, 80000, 82500, 85000, 87500, 90000, 92500, 95000, 97500, 100000, 200000, 300000, 400000, 500000, 600000};

    public long[] reward_list = { 100, 200, 300, 500, 600, 700, 1000, 1100, 1200, 1500, 1600, 1700, 2000, 2100, 2200, 2500, 2600, 2700, 3000, 3100, 3200, 5000, 5100, 5200, 5300, 5400, 5500, 5600, 5700, 5800, 5900, 6000, 6100, 6200, 6300, 6400, 6500, 6600, 6700, 6800, 6900, 7000, 7100, 7200, 7300, 7400, 7500, 7600, 7700, 7800, 7900, 8000, 8100, 8200, 8300, 8400, 8500, 8600, 8700, 8800, 8900, 9000, 9100, 9200, 9300, 9400, 9500, 9600, 9700, 9800, 9900, 99999999};
    
    public string[] ms_t = {"  애송이1", "  애송이2", "  애송이3", " 초보자1", " 초보자2", " 초보자3", " 숙련자1", " 숙련자2", " 숙련자3", " 영웅1", " 영웅2", " 영웅3", " 대영웅1", " 대영웅2", " 대영웅3", " 초인1", " 초인2", " 초인3", " 신1", " 신2", " 신3", " 의 끝"};
    
    public int current1_i;
    public int current2_i;
    public int current3_i;
    public int current4_i;
    public int current5_i;
    public int current6_i;

    long reward_crystal;

    public GameObject point_to_spawn_text;
    public GameObject prefab_floating_text;
    public BattleManager battleManager;
    public Inventory inventory;

    public GameObject probability_panel;


    void Start()
    {
        //StartCoroutine("Auto");
        ms1 = ms_1[0];
        ms2 = ms_2[0];
        ms3 = ms_3[0];
        ms4 = ms_4[0];
        ms5 = ms_5[0];
        ms6 = ms_6[0];
        checkMission();
        StartCoroutine("Auto");
    }

    IEnumerator Auto()
    {
        while(true)
        {
            yield return new WaitForSeconds(10f);
            checkMission();
        }
    } 

    public void show_probability()
    {
        probability_panel.SetActive(!probability_panel.active);
    }

    public void initclickCount()
    {
        DataController.Instance.clickCount = 0;
        DataController.Instance.maxDps = 0;

        current1_i = 0;
        current2_i = 0;
        current3_i = 0;
        current4_i = 0;
        current5_i = 0;
        current6_i = 0;
        for(int i =0; i < 100; i++) 
        {
            SetBool("ms1" + i, false);
            SetBool("ms2" + i, false);
            SetBool("ms3" + i, false);
            SetBool("ms4" + i, false);
            SetBool("ms5" + i, false);
            SetBool("ms6" + i, false);
        }
    }

    public void Debugsss()
    {
        Debug.Log("current1_i is " + current1_i);
        Debug.Log("current2_i is " + current2_i);
        Debug.Log("current3_i is " + current3_i);
        Debug.Log("current4_i is " + current4_i);
        Debug.Log("current5_i is " + current5_i);
        Debug.Log("current6_i is " + current6_i);
        for(int i =0; i<ms_1.Length; i++)
        {
            Debug.Log("ms1 " + i + " is " + GetBool("ms1" + i));
        }
    }
    

    void checkMission()
    {
        //mission 1
        // ms1 = ms_1[0];
        // ms2 = ms_2[0];
        // ms3 = ms_3[0];
        // ms4 = ms_4[0];
        // ms5 = ms_5[0];
        // ms6 = ms_6[0];

        // for(int i =0; i < reward_list.Length; i++)    //for 목적지가 ms_1.length는 좀...
        // {
        //     int count = 0;
        //     if(GetBool("ms1" + i) == true)
        //     {
        //         ms1 = ms_1[i+1];
        //         current1_i = i+1;
        //         count++;
        //     } 
        //     if(GetBool("ms2" + i) == true)
        //     {
        //         ms2 = ms_2[i+1];
        //         current2_i = i+1;
        //         count++;
        //     }
        //     if(GetBool("ms3" + i) == true)
        //     {
        //         ms3 = ms_3[i+1];
        //         current3_i = i+1;
        //         count++;
        //     }
        //     if(GetBool("ms4" + i) == true)
        //     {
        //         ms4 = ms_4[i+1];
        //         current4_i = i+1;
        //         count++;
        //     }
        //     if(GetBool("ms5" + i) == true)
        //     {
        //         ms5 = ms_5[i+1];
        //         current5_i = i+1;
        //         count++;
        //     }
        //     if(GetBool("ms6" + i) == true)
        //     {
        //         ms6 = ms_6[i+1];
        //         current6_i = i+1;
        //         count++;
        //     }
        //     if(count == 0)
        //     {
        //         Debug.Log("i is " + i);
        //         break;
        //     }
        // }
        int i = 0;
        while(true)
        {   
            if(GetBool("ms1" + i) == false)
            {
                break;
            } else {
                ms1 = ms_1[i+1];
                current1_i = i+1;
            }
            i++;
        }
        i = 0;
        while(true)
        {   
            if(GetBool("ms2" + i) == false)
            {
                break;
            } else {
                ms2 = ms_2[i+1];
                current2_i = i+1;
            }
            i++;
        }
        i = 0;
        while(true)
        {   
            if(GetBool("ms3" + i) == false)
            {
                break;
            } else {
                ms3 = ms_3[i+1];
                current3_i = i+1;
            }
            i++;
        }
        i = 0;
        while(true)
        {   
            if(GetBool("ms4" + i) == false)
            {
                break;
            } else {
                ms4 = ms_4[i+1];
                current4_i = i+1;
            }
            i++;
        }
        i = 0;
        while(true)
        {   
            if(GetBool("ms5" + i) == false)
            {
                break;
            } else {
                ms5 = ms_5[i+1];
                current5_i = i+1;
            }
            i++;
        }
        i = 0;
        while(true)
        {   
            if(GetBool("ms6" + i) == false)
            {
                break;
            } else {
                ms6 = ms_6[i+1];
                current6_i = i+1;
            }
            i++;
        }
        missionList[0].GetComponentInChildren<Text>().text = UiManager.ToStringKR(DataController.Instance.clickCount) + " / " + ms1;
        missionList[0].transform.GetChild(2).GetComponent<Text>().text = "클릭" + ms_t[current1_i];

        missionList[1].GetComponentInChildren<Text>().text = UiManager.ToStringKR(DataController.Instance.char_level) + " / " + ms2;
        missionList[1].transform.GetChild(2).GetComponent<Text>().text = "레벨" + ms_t[current2_i];

        missionList[2].GetComponentInChildren<Text>().text = UiManager.ToStringKR(DataController.Instance.level) + " / " + ms3;
        missionList[2].transform.GetChild(2).GetComponent<Text>().text = "골드레벨" + ms_t[current3_i];

        missionList[3].GetComponentInChildren<Text>().text = UiManager.ToStringKR(DataController.Instance.stateTotal) + " / " + ms4;
        missionList[3].transform.GetChild(2).GetComponent<Text>().text = "스탯" + ms_t[current4_i];

        missionList[4].GetComponentInChildren<Text>().text = UiManager.ToStringKR(battleManager.require_max_stage()) + " / " + ms5;
        missionList[4].transform.GetChild(2).GetComponent<Text>().text = "스테이지" + ms_t[current5_i];
        
        missionList[5].GetComponentInChildren<Text>().text = UiManager.ToStringKR(DataController.Instance.maxDps) + " / " + ms6;
        missionList[5].transform.GetChild(2).GetComponent<Text>().text = "데미지측정" + ms_t[current6_i];

        if(DataController.Instance.clickCount >= ms1) 
        {
            missionList[0].transform.GetChild(0).GetComponent<Button>().interactable = true;
        } 
        else
        {
            missionList[0].transform.GetChild(0).GetComponent<Button>().interactable = false;
        }

        if(DataController.Instance.char_level >= ms2) 
        {
            missionList[1].transform.GetChild(0).GetComponent<Button>().interactable = true;
        
        } 
        else
        {
            missionList[1].transform.GetChild(0).GetComponent<Button>().interactable = false;
        }

        if(DataController.Instance.level >= ms3) 
        {
            missionList[2].transform.GetChild(0).GetComponent<Button>().interactable = true;
        } 
        else
        {
            missionList[2].transform.GetChild(0).GetComponent<Button>().interactable = false;
        }

        if(DataController.Instance.stateTotal >= ms4) 
        {
            missionList[3].transform.GetChild(0).GetComponent<Button>().interactable = true;
        } 
        else
        {
            missionList[3].transform.GetChild(0).GetComponent<Button>().interactable = false;
        }

        if(battleManager.require_max_stage() >= ms5) 
        {
            missionList[4].transform.GetChild(0).GetComponent<Button>().interactable = true;
        } 
        else
        {
            missionList[4].transform.GetChild(0).GetComponent<Button>().interactable = false;
        }

        if(DataController.Instance.maxDps >= ms6) 
        {
            missionList[5].transform.GetChild(0).GetComponent<Button>().interactable = true;
        } 
        else
        {
            missionList[5].transform.GetChild(0).GetComponent<Button>().interactable = false;
        }
    }

    void gold_text(long currentQuestGold)
    {
        GameObject clone = Instantiate(prefab_floating_text);
        clone.transform.position = Vector3.zero;

        clone.transform.SetParent(point_to_spawn_text.transform);
        clone.transform.GetChild(0).gameObject.SetActive(true);
        clone.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Image/UI/Freeui/ZOSMA/Main/Cristal");
        if(UiManager.ToStringKR(currentQuestGold).Length >= 10)
        {
            clone.GetComponent<FloatingText>().text.text = "                   " + UiManager.ToStringKR(currentQuestGold);
        }
        else if(UiManager.ToStringKR(currentQuestGold).Length >= 8)
        {
            clone.GetComponent<FloatingText>().text.text = "                " + UiManager.ToStringKR(currentQuestGold);
        }
        else if(UiManager.ToStringKR(currentQuestGold).Length >= 6)
        {
            clone.GetComponent<FloatingText>().text.text = "             " + UiManager.ToStringKR(currentQuestGold);
        }
        else if(UiManager.ToStringKR(currentQuestGold).Length >= 4)
        {
            clone.GetComponent<FloatingText>().text.text = "       " + UiManager.ToStringKR(currentQuestGold);
        }
        else
        {
            clone.GetComponent<FloatingText>().text.text = "        " + UiManager.ToStringKR(currentQuestGold);
        }
    }

    public void getMissionReward() 
    {

        //string name =  EventSystem.current.currentSelectedGameObject.name;
        string name = EventSystem.current.currentSelectedGameObject.transform.parent.name;

        if(name == "clickMission(ms1)" && GetBool("ms1" + current1_i) == false)
        {   //코드 간소화함 => 보상이 골드면 간소화 간단한데 보상이 아이템이면?
            SoundManager.Instance.get_challenge_reward();
            SetBool("ms1" + current1_i, true);   
            reward_crystal = reward_list[current1_i];
            DataController.Instance.diamond += reward_crystal;
            gold_text(reward_crystal);
        }

        else if(name == "levelMission(ms2)" && GetBool("ms2" + current2_i) == false)
        {
            SoundManager.Instance.get_challenge_reward();
            SetBool("ms2" + current2_i, true);
            reward_crystal = reward_list[current2_i];
            DataController.Instance.diamond += reward_crystal;
            gold_text(reward_crystal);
        }

        else if(name == "goldLevelMission(ms3)" && GetBool("ms3" + current3_i) == false)
        {
            SoundManager.Instance.get_challenge_reward();
            SetBool("ms3" + current3_i, true);
            reward_crystal = reward_list[current3_i];
            DataController.Instance.diamond += reward_crystal;
            gold_text(reward_crystal);
        }

        else if(name == "stateTotalMission(ms4)" && GetBool("ms4" + current4_i) == false)
        {
            SoundManager.Instance.get_challenge_reward();
            SetBool("ms4" + current4_i, true);
            reward_crystal = reward_list[current4_i];
            DataController.Instance.diamond += reward_crystal;
            gold_text(reward_crystal);
        }

        else if(name == "maxStageMission(ms5)" && GetBool("ms5" + current5_i) == false)
        {   //코드 간소화함 => 보상이 골드면 간소화 간단한데 보상이 아이템이면?
            SoundManager.Instance.get_challenge_reward();
            SetBool("ms5" + current5_i, true);
            reward_crystal = reward_list[current5_i];
            DataController.Instance.diamond += reward_crystal;
            gold_text(reward_crystal);
        }

        else if(name == "maxDpsMission(ms6)" && GetBool("ms6" + current6_i) == false)
        {   //코드 간소화함 => 보상이 골드면 간소화 간단한데 보상이 아이템이면?
            SoundManager.Instance.get_challenge_reward();
            SetBool("ms6" + current6_i, true);
            reward_crystal = reward_list[current6_i];
            DataController.Instance.diamond += reward_crystal;
            gold_text(reward_crystal);
        }
        checkMission();
    }
}
