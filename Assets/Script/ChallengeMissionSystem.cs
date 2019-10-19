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
    public bool[] ms1bool;

    public long ms1;
    public long ms2;


    public long[] ms_1 = {100, 500, 1000, 2000, 5000, 10000, 15000, 30000, 50000, 75000, 100000};
    public long[] ms_2 = {3, 5, 7, 10, 13, 15, 20, 30, 35, 40, 50};
    
    public string[] ms_t = {" 시작하는 사람", " 초보", " 좀 하는 사람", " 좀 해본 사람", " 숙련자", " 전문가", "의 천재", "의 달인", "마스터", "의 신"};
    int current1_i;
    int current2_i;

    public GameObject point_to_spawn_text;
    public GameObject prefab_floating_text;
    public Inventory inventory;
    public Image Box_image1;
    public Image Box_image2;

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
        DataController.Instance.char_level = 1;
        for(int i =0; i < ms_1.Length; i++) 
        {
            SetBool("ms1" + i, false);
            SetBool("ms2" + i, false);
        }
    }

    public void Debugsss()
    {
        Debug.Log("current1_i is " + current1_i);
        Debug.Log("current2_i is " + current2_i);
        for(int i =0; i<ms_1.Length; i++)
        {
            Debug.Log("ms1 " + i + " is " + GetBool("ms1" + i));
        }
        for(int i =0; i<ms_2.Length; i++)
        {
            Debug.Log("ms2 " + i + " is " + GetBool("ms2" + i));
        }
        
    }
    

    void checkMission()
    {
        //mission 1


        for(int i =0; i < ms_1.Length; i++)
        {
            // if(GetBool("ms1" + i) == false)
            // {
            //     ms1 = ms_1[i];
            //     current1_i = i;
            // }
            // if(GetBool("ms2" + i) == false)
            // {
            //     ms2 = ms_2[i];
            //     current2_i = i;
            // }
            SetBool("ms2" + 1, true);
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
        }
        missionList[0].GetComponentInChildren<Text>().text = UiManager.ToStringKR(DataController.Instance.clickCount) + " / " + ms1;
        missionList[0].transform.GetChild(2).GetComponent<Text>().text = "클릭" + ms_t[current1_i];

        missionList[1].GetComponentInChildren<Text>().text = UiManager.ToStringKR(DataController.Instance.char_level) + " / " + ms2;
        missionList[1].transform.GetChild(2).GetComponent<Text>().text = "레벨" + ms_t[current2_i];

        if(DataController.Instance.clickCount >= ms1) 
        {
            missionList[0].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        } 
        else
        {
            missionList[0].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
        }

        if(DataController.Instance.char_level >= ms2) 
        {
            missionList[1].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        } 
        else
        {
            missionList[1].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
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
        {
            SetBool("ms1" + current1_i, true);
            long reward_gold;
            if(current1_i == 0) //보상
            {
                reward_gold = 1000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current1_i == 1) //보상
            {
                reward_gold = 3000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current1_i == 2) //보상
            {
                reward_gold = 10000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current1_i == 3) //보상
            {
                reward_gold = 30000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current1_i == 4) //보상
            {
                reward_gold = 50000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current1_i == 5) //보상
            {
                reward_gold = 100000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current1_i == 6) //보상
            {
                reward_gold = 300000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
        }

        if(name == "levelMission(ms2)" && GetBool("ms2" + current2_i) == false)
        {
            SetBool("ms2" + current2_i, true);
            long reward_gold;
            if(current2_i == 0) //보상
            {
                reward_gold = 1000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current2_i == 1) //보상
            {
                reward_gold = 3000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current2_i == 2) //보상
            {
                reward_gold = 10000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current2_i == 3) //보상
            {
                reward_gold = 30000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current2_i == 4) //보상
            {
                reward_gold = 50000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current2_i == 5) //보상
            {
                reward_gold = 100000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
             if(current2_i == 6) //보상
            {
                reward_gold = 300000;
                DataController.Instance.gold += reward_gold;
                gold_text(reward_gold);
            }
            
        }
        
        

    }
}
