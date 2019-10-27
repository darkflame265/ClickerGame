using System.Collections;
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


    public long[] ms_1 = {100, 500, 1000, 2000, 5000, 10000, 15000, 30000, 50000, 75000, 100000};
    public long[] ms_2 = {3, 5, 7, 10, 13, 15, 20, 30, 35, 40, 50};
    public long[] ms_3 = {5, 10, 30, 50, 75, 100, 150, 200, 300, 400, 500};
    public long[] ms_4 = {10, 30, 50, 75, 100, 150, 200, 300, 500, 750, 1000};
    public long[] ms_5 = {5, 10, 15, 20, 25, 30, 35, 40, 45 ,50, 55};
    public long[] ms_6 = {100, 500, 1000, 2000, 5000, 10000, 15000, 30000, 50000, 75000, 100000};

    public long[] ms_1_reward = { 1000, 3000, 10000, 30000, 50000, 100000, 300000,500000, 1000000};
    
    public string[] ms_t = {" 시작하는 사람", " 초보", " 좀 하는 사람", " 좀 해본 사람", " 숙련자", " 전문가", "의 천재", "의 달인", "마스터", "의 신"};
    
    public int current1_i;
    public int current2_i;
    public int current3_i;
    public int current4_i;
    public int current5_i;
    public int current6_i;

    public GameObject point_to_spawn_text;
    public GameObject prefab_floating_text;
    public BattleManager battleManager;
    public Inventory inventory;


    void Start()
    {
        StartCoroutine("Auto");
    }

    IEnumerator Auto()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.2f);
            checkMission();
            
        }
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
        for(int i =0; i < ms_1.Length; i++) 
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
        ms1 = ms_1[0];
        ms2 = ms_2[0];
        ms3 = ms_3[0];
        ms4 = ms_4[0];
        ms5 = ms_5[0];
        ms6 = ms_5[0];

        for(int i =0; i < ms_1.Length; i++)    //for 목적지가 ms_1.length는 좀...
        {
            if(GetBool("ms1" + i) == true)
            {
                ms1 = ms_1[i+1];
                current1_i = i+1;
            } 
            if(GetBool("ms2" + i) == true)
            {
                ms2 = ms_2[i+1];
                current2_i = i+1;
            }
            if(GetBool("ms3" + i) == true)
            {
                ms3 = ms_3[i+1];
                current3_i = i+1;
            }
            if(GetBool("ms4" + i) == true)
            {
                ms4 = ms_4[i+1];
                current4_i = i+1;
            }
            if(GetBool("ms5" + i) == true)
            {
                ms5 = ms_5[i+1];
                current5_i = i+1;
            }
            if(GetBool("ms6" + i) == true)
            {
                ms6 = ms_6[i+1];
                current6_i = i+1;
            }
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
        clone.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Image/UI/Freeui/ZOSMA/Main/Coin");
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
        Debug.Log("this btn parent is " + name);

        if(name == "clickMission(ms1)" && GetBool("ms1" + current1_i) == false)
        {   //코드 간소화함 => 보상이 골드면 간소화 간단한데 보상이 아이템이면?
            SoundManager.Instance.get_challenge_reward();
            SetBool("ms1" + current1_i, true);
            long reward_gold;
            reward_gold = ms_1_reward[current1_i];
            DataController.Instance.gold += reward_gold;
            gold_text(reward_gold);
        }

        if(name == "levelMission(ms2)" && GetBool("ms2" + current2_i) == false)
        {
            SoundManager.Instance.get_challenge_reward();
            SetBool("ms2" + current2_i, true);
            long reward_gold;
            if(current2_i == 0) //보상
            {
                reward_gold = 10000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current2_i == 1) //보상
            {
                reward_gold = 30000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current2_i == 2) //보상
            {
                reward_gold = 100000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current2_i == 3) //보상
            {
                reward_gold = 300000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current2_i == 4) //보상
            {
                reward_gold = 500000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current2_i == 5) //보상
            {
                reward_gold = 1000000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current2_i == 6) //보상
            {
                reward_gold = 3000000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
            
        }

        if(name == "goldLevelMission(ms3)" && GetBool("ms3" + current3_i) == false)
        {
            SoundManager.Instance.get_challenge_reward();
            SetBool("ms3" + current3_i, true);
            long reward_gold;
            if(current3_i == 0) //보상
            {
                reward_gold = 1000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current3_i == 1) //보상
            {
                reward_gold = 3000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current3_i == 2) //보상
            {
                reward_gold = 100000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current3_i == 3) //보상
            {
                reward_gold = 300000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current3_i == 4) //보상
            {
                reward_gold = 500000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current3_i == 5) //보상
            {
                reward_gold = 1000000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current3_i == 6) //보상
            {
                reward_gold = 3000000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
        }

        if(name == "stateTotalMission(ms4)" && GetBool("ms4" + current4_i) == false)
        {
            SoundManager.Instance.get_challenge_reward();
            SetBool("ms4" + current4_i, true);
            long reward_gold;
            if(current4_i == 0) //보상
            {
                reward_gold = 1000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current4_i == 1) //보상
            {
                reward_gold = 3000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current4_i == 2) //보상
            {
                reward_gold = 100000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current4_i == 3) //보상
            {
                reward_gold = 300000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current4_i == 4) //보상
            {
                reward_gold = 500000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current4_i == 5) //보상
            {
                reward_gold = 1000000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current4_i == 6) //보상
            {
                reward_gold = 3000000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
        }

        if(name == "maxStageMission(ms5)" && GetBool("ms5" + current5_i) == false)
        {   //코드 간소화함 => 보상이 골드면 간소화 간단한데 보상이 아이템이면?
            SoundManager.Instance.get_challenge_reward();
            SetBool("ms5" + current5_i, true);
            long reward_gold;
            reward_gold = ms_1_reward[current5_i];
            DataController.Instance.gold += reward_gold;
            gold_text(reward_gold);
        }

        if(name == "maxDpsMission(ms6)" && GetBool("ms6" + current6_i) == false)
        {   //코드 간소화함 => 보상이 골드면 간소화 간단한데 보상이 아이템이면?
            SoundManager.Instance.get_challenge_reward();
            SetBool("ms6" + current6_i, true);
            long reward_gold;
            reward_gold = ms_1_reward[current6_i];
            DataController.Instance.gold += reward_gold;
            gold_text(reward_gold);
        }
        
        

    }
}
