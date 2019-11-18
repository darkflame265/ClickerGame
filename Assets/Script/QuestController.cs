using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestController : MonoBehaviour
{

    private static QuestController instance;

    public static QuestController Instance
    {
        get{
            if(instance == null)
            {
                instance = FindObjectOfType<QuestController>();

                if(instance == null)
                {
                    GameObject container = new GameObject("QuestController");

                    instance = container.AddComponent<QuestController>();
                }
            }
            return instance;
        }
    }


    public GameObject[] quest = new GameObject[3];

    public GameObject prefab_floating_text;


    List<string> quest_name = new List<string>();
    List<long> quest_cost = new List<long>();
    List<string> quest_effect = new List<string>();

    int index;

    public Scrollbar scrollbarr;

    float questProgress;

    public Inventory inventory;
    public Transform tranform_canvas;

    Sprite item_image;

    
    // Start is called before the first frame update
    void Start()
    {
        set_start_position();
        insert_quest_name_toList();
        insert_quest_cost_toList();
        insert_quest_effect_toList();
        //DataController.Instance.LoadQuestButton(this);


        //StartCoroutine("Auto");
        check_quest_and_change_image();
    }


    void insert_quest_name_toList()
    {
        quest_name.Add("초보자 방어구 구매");
        quest_name.Add("초보자 무기 구매");
        quest_name.Add("모험가 지식 배우기");
        quest_name.Add("수상해보이는 골동품 구매");
        quest_name.Add("기사단의 투구 구매하기");
        quest_name.Add("기사단의 검 구매하기");
        quest_name.Add("마법 주문서 구매하기");
        quest_name.Add("황금 사과 구매하기");
        quest_name.Add("전직 S급 모험가와 수련하기(1)");
        quest_name.Add("전직 S급 모험가와 수련하기(2)");
        quest_name.Add("전직 S급 모험가와 수련하기(3)");
        quest_name.Add("천신에게 기도드리기");
        quest_name.Add("경매장에서 유물 구매하기");
    }
    void insert_quest_cost_toList()
    {
        quest_cost.Add((10000));
        quest_cost.Add((50000));
        quest_cost.Add((100000));
        quest_cost.Add((250000));
        quest_cost.Add((500000));
        quest_cost.Add((750000));
        quest_cost.Add((1000000));
        quest_cost.Add((2000000));
        quest_cost.Add((5000000));
        quest_cost.Add((5000000));
        quest_cost.Add((5000000));
        quest_cost.Add((10000000));
        quest_cost.Add((20000000));
    }

    void insert_quest_effect_toList()
    {
        quest_effect.Add("체력 +30");
        quest_effect.Add("공격력 +30");
        quest_effect.Add("스킬공격력 +30");
        quest_effect.Add("유물뽑기권 +1");
        quest_effect.Add("체력 +50");
        quest_effect.Add("공격력 +50");
        quest_effect.Add("스킬공격력 +50");
        quest_effect.Add("현자의돌 +1");
        quest_effect.Add("체력 +100");
        quest_effect.Add("공격력 +100");
        quest_effect.Add("스킬공격력 +100");
        quest_effect.Add("권능뽑기권 +1");
        quest_effect.Add("유물뽑기권 +1");
    }

    IEnumerator Auto()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
                for(int i = 0; i < quest.Length - 1; i++)
                {
                    if(UiManager.GetBool("QuestPurchased" + 0) == false)
                    {
                        quest[0].transform.GetChild(0).GetComponentInChildren<Text>().text = UiManager.ToStringKR(quest_cost[0]);
                        quest[0].GetComponentInChildren<Button>().interactable = true;
                    }
                    if(UiManager.GetBool("QuestPurchased" + i)  == true)
                    {
                        quest[i].transform.GetChild(0).GetComponentInChildren<Text>().text = "구매 완료";
                        quest[i].GetComponentInChildren<Button>().interactable = false;
                    }

                        if(UiManager.GetBool("QuestPurchased" + i) == false && UiManager.GetBool("QuestPurchased" + (i+1)) == false)
                        {
                            //quest[i+1].transform.GetChild(0).GetComponentInChildren<Text>().text = "구매 불가";
                            quest[i+1].GetComponentInChildren<Button>().interactable = false;
                        }
                        else if(UiManager.GetBool("QuestPurchased" + i) == true && UiManager.GetBool("QuestPurchased" + (i+1)) == false)
                        {
                            quest[i+1].transform.GetChild(0).GetComponentInChildren<Text>().text = UiManager.ToStringKR(quest_cost[i]);;
                            quest[i+1].GetComponentInChildren<Button>().interactable = true;
                            
                        }
                }
        }
    }

    void check_quest_and_change_image()
    {
        for(int i = 0; i < quest.Length - 1; i++)
                {
                    if(UiManager.GetBool("QuestPurchased" + 0) == false)
                    {
                        //quest[0].transform.GetChild(0).GetComponentInChildren<Text>().text = UiManager.ToStringKR(quest_cost[0]);
                        quest[0].GetComponentInChildren<Button>().interactable = true;
                    }
                    if(UiManager.GetBool("QuestPurchased" + i)  == true)
                    {
                        quest[i].transform.GetChild(0).GetComponentInChildren<Text>().text = "구매 완료";
                        quest[i].GetComponentInChildren<Button>().interactable = false;
                    }

                        if(UiManager.GetBool("QuestPurchased" + i) == false && UiManager.GetBool("QuestPurchased" + (i+1)) == false)
                        {
                            //quest[i+1].transform.GetChild(0).GetComponentInChildren<Text>().text = "구매 불가";
                            quest[i+1].GetComponentInChildren<Button>().interactable = false;
                        }
                        else if(UiManager.GetBool("QuestPurchased" + i) == true && UiManager.GetBool("QuestPurchased" + (i+1)) == false)
                        {
                            //quest[i+1].transform.GetChild(0).GetComponentInChildren<Text>().text = UiManager.ToStringKR(quest_cost[i]);;
                            quest[i+1].GetComponentInChildren<Button>().interactable = true;
                            
                        }
                }
    }

    public void init() 
    {
        for(int i = 0; i < quest.Length-1; i++)
        {
            UiManager.SetBool("QuestPurchased" + i, false);
            //DataController.Instance.SaveQuestButton(quest[i].GetComponent<QuestController>());
        }
    }

    public void dddebug()
    {
        for(int i = 0; i < quest.Length-1; i++)
        {
            Debug.Log(i + " is " + UiManager.GetBool("QuestPurchased" + i));

        }
    }

    public void set_start_position()
    {
        int max_quest_length = quest.Length;
        int clear_quest_length = 0;
        for(int i = 0; i < quest.Length - 1; i++)
        {
            if(UiManager.GetBool("QuestPurchased" + i)  == true)
            {
                clear_quest_length++;
            }
        }
        questProgress = (float)clear_quest_length / max_quest_length;
        scrollbarr.GetComponent<Scrollbar>().value = 0;
        scrollbarr.GetComponent<Scrollbar>().value = questProgress;
        Debug.Log(questProgress + 0.05f);
    }

    public void PurchaseItem()
    {
        for(index =0; index < quest.Length - 1; index++)
        {
            GameObject currentQuest = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
            if(currentQuest == quest[index] )
            {
                if(DataController.Instance.gold >= quest_cost[index])
                {
                    if(UiManager.GetBool("QuestPurchased" + index) == false)
                    {
                        SoundManager.Instance.get_challenge_reward();
                        DataController.Instance.gold -= quest_cost[index];
                        get_effect();
                        show_floating_text();
                        UiManager.SetBool("QuestPurchased" + index, true);
                        break;
                    }
                }
            }
        }
        check_quest_and_change_image();
    }

    public void get_effect()
    {
        if(index == 0) {
            DataController.Instance.health += 30;
        } else if(index == 1) {
            DataController.Instance.attack += 30;
        } else if(index == 2) {
            DataController.Instance.special += 30;
        } else if(index == 3) {
            DataController.Instance.artifact_ticket += 1;
        } else if(index == 4) {
            DataController.Instance.health += 50;
        } else if(index == 5) {
            DataController.Instance.attack += 50;
        } else if(index == 6) {
            DataController.Instance.special += 50;
        } else if(index == 7) {
            DataController.Instance.attack += 3000;           //황금 사과 가 없음
            int i = 5;

            inventory.AddItem(i);
            item_image = inventory.database.FetchItemByID(i).Sprite;

        } else if(index == 8) {
            DataController.Instance.health += 100;
        } else if(index == 9) {       
            DataController.Instance.attack += 100;
        } else if(index == 10) {
            DataController.Instance.special += 100;
        } else if(index == 11) {
            DataController.Instance.power_ticket += 1;
        } else if(index == 12) {
            DataController.Instance.artifact_ticket += 1;
        } else Debug.Log("퀘스트 알맞는 이름이 없습니다.");
        //prefab_floating_text.GetComponent<Text>().text = 
    }

    void show_floating_text()
    {
        var clone = Instantiate(prefab_floating_text, new Vector3(0, 2), Quaternion.Euler(Vector3.zero));
        clone.GetComponent<Text>().text = quest_effect[index];
        clone.transform.SetParent(quest[index].transform.parent.parent.parent);
        if(item_image != null)
        {
            clone.transform.GetChild(0).gameObject.SetActive(true); 
            //clone.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Image/UI/Freeui/ZOSMA/Main/Coin");
            clone.GetComponentInChildren<Image>().sprite = item_image;
        }
    }

}
