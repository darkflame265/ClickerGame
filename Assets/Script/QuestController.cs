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
        update_quest_inform();
        check_quest_and_change_image();
    }


    void insert_quest_name_toList()
    {
        quest_name.Add("초보자 방어구 구매");
        quest_name.Add("초보자 무기 구매");
        quest_name.Add("모험가 부츠 구매");
        quest_name.Add("모험가 지식 배우기");

        quest_name.Add("수상해보이는 골동품 구매");

        quest_name.Add("기사단의 투구 구매하기");
        quest_name.Add("기사단의 검 구매하기");
        quest_name.Add("그림자 망토 구매");
        quest_name.Add("마법 주문서 구매하기");

        quest_name.Add("황금 사과 구매하기");

        quest_name.Add("전직 용사에게 조언듣기(1)");
        quest_name.Add("전직 용사에게 조언듣기(2)");
        quest_name.Add("전직 용사에게 조언듣기(3)");
        quest_name.Add("전직 용사에게 조언듣기(4)");

        quest_name.Add("신이 용사가 기특해서 주는 권능");
        quest_name.Add("경매장에서 유물 구매하기");
        //new
        quest_name.Add("체력 영약 구매");
        quest_name.Add("분노의 영약 구매");
        quest_name.Add("민첩 영약 구매");
        quest_name.Add("마나 영약 구매");

        quest_name.Add("신이 제안한 등가교환(1)");
        quest_name.Add("광물 신의 가호 구매");

        quest_name.Add("성직자의 가호 받기");
        quest_name.Add("경매장에서 용사의 성검 구매");
        quest_name.Add("무효화의 코트 구매");
        quest_name.Add("성검에 공격력 인챈트하기");

        quest_name.Add("신이 제안한 등가교환(2)");
        quest_name.Add("신이 제안한 등가교환(3)");
    }
    void insert_quest_cost_toList()
    {
        quest_cost.Add((10000));
        quest_cost.Add((50000));
        quest_cost.Add((100000));
        quest_cost.Add((150000));

        quest_cost.Add((250000));

        quest_cost.Add((500000));
        quest_cost.Add((750000));
        quest_cost.Add((1000000));
        quest_cost.Add((1500000));

        quest_cost.Add((2000000));

        quest_cost.Add((5000000));
        quest_cost.Add((5000000));
        quest_cost.Add((5000000));
        quest_cost.Add((5000000));

        quest_cost.Add((10000000));
        quest_cost.Add((20000000));
        //new
        quest_cost.Add((40000000));
        quest_cost.Add((60000000));
        quest_cost.Add((80000000));
        quest_cost.Add((100000000));

        quest_cost.Add((500000000));
        quest_cost.Add((1000000000));

        quest_cost.Add((10000000000));
        quest_cost.Add((10000000000));
        quest_cost.Add((50000000000));
        quest_cost.Add((50000000000));

        quest_cost.Add((100000000000));
        quest_cost.Add((300000000000));
    }

    void insert_quest_effect_toList()
    {
        quest_effect.Add("체력 +30");
        quest_effect.Add("공격력 +40");
        quest_effect.Add("민첩 +50");
        quest_effect.Add("스킬공격력 +60");

        quest_effect.Add("유물뽑기권 +1\n 클릭골드 2배");

        quest_effect.Add("체력 +70");
        quest_effect.Add("공격력 +80");
        quest_effect.Add("민첩 +90");
        quest_effect.Add("스킬공격력 +100");

        quest_effect.Add("현자의돌 +1\n 클릭골드 2배");

        quest_effect.Add("체력 +200");
        quest_effect.Add("공격력 +200");
        quest_effect.Add("민첩 +200");;
        quest_effect.Add("스킬공격력 +200");

        quest_effect.Add("권능뽑기권 +1\n 클릭골드 2배");
        quest_effect.Add("유물뽑기권 +1\n 클릭골드 2배");
        //new
        quest_effect.Add("체력 +300");
        quest_effect.Add("공격력 +300");
        quest_effect.Add("민첩 +300");;
        quest_effect.Add("스킬공격력 +300");

        quest_effect.Add("루비 10000개 지급\n 클릭골드 2배");
        quest_effect.Add("모든 광물 종류별로 30개 지급\n 클릭골드 2배");

        quest_effect.Add("체력 +400");
        quest_effect.Add("공격력 +400");;
        quest_effect.Add("체력 +500");
        quest_effect.Add("공격력 +500");

        quest_effect.Add("권능뽑기권 +1\n 클릭골드 2배");
        quest_effect.Add("권능뽑기권 +3\n 클릭골드 2배");
    }

    IEnumerator Auto()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.3f);
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
        item_image = null;
        if(index == 0) {
            DataController.Instance.health += 30;
        } else if(index == 1) {
            DataController.Instance.attack += 40;
        } else if(index == 2) {
            DataController.Instance.mana += 50;
        } else if(index == 3) {
            DataController.Instance.special += 60;
        } 
        
            else if(index == 4) {
            DataController.Instance.artifact_ticket += 1;
            DataController.Instance.MultiplyGoldPerClick(2);
        } else if(index == 5) {
            DataController.Instance.health += 70;
        } else if(index == 6) {
            DataController.Instance.attack += 80;
        } else if(index == 7) {
            DataController.Instance.mana += 90;
        }else if(index == 8) {
            DataController.Instance.special += 100;
        } else if(index == 9) {
            DataController.Instance.attack += 3000;           //황금 사과 가 없음
            int i = 5;
            inventory.AddItem(i);
            item_image = inventory.database.FetchItemByID(i).Sprite;
            DataController.Instance.MultiplyGoldPerClick(2);
        } else if(index == 10) {
            DataController.Instance.health += 200;
        } else if(index == 11) {       
            DataController.Instance.attack += 200;
        } else if(index == 12) {
            DataController.Instance.mana += 200;
        } else if(index == 13) {
            DataController.Instance.special += 200;
        } else if(index == 14) {
            DataController.Instance.power_ticket += 1;
            DataController.Instance.MultiplyGoldPerClick(2);
        } else if(index == 15) {
            DataController.Instance.health += 300;
        } else if(index == 16) {
            DataController.Instance.attack += 300;
        } else if(index == 17) {
            DataController.Instance.mana += 300;
        } else if(index == 18) {
            DataController.Instance.special += 300;
        } else if(index == 19) {
            DataController.Instance.diamond += 10000;
            DataController.Instance.MultiplyGoldPerClick(2);
        } else if(index == 20) {
            for(int i = 1; i < 6; i++)
            {
                int current_booty = PlayerPrefs.GetInt("booty_" + i);
                PlayerPrefs.SetInt("booty_" + i, current_booty + 30);
            }
            DataController.Instance.MultiplyGoldPerClick(2);
            
        } else if(index == 21) {
            DataController.Instance.health += 400;
        } else if(index == 22) {
            DataController.Instance.attack += 400;
        } else if(index == 23) {
            DataController.Instance.health += 500;
        } else if(index == 24) {
            DataController.Instance.attack += 400;
        } else if(index == 25) {
            DataController.Instance.power_ticket += 1;
            DataController.Instance.MultiplyGoldPerClick(2);
        } else if(index == 26) {
            DataController.Instance.power_ticket += 3;
            DataController.Instance.MultiplyGoldPerClick(2);
        }else Debug.Log("퀘스트 알맞는 이름이 없습니다.");
        //prefab_floating_text.GetComponent<Text>().text = 
    }

    void update_quest_inform()
    {
        for(int i = 0; i < quest.Length; i++)
        {
            quest[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = UiManager.ToStringKR(quest_cost[i]);
            quest[i].transform.GetChild(1).GetComponent<Text>().text = quest_name[i] + "\n\n" + quest_effect[i];
        }
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
