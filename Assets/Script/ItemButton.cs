using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{

    public Text itemDisplayer;
    public Text currentCostDisplayer;

    public GameObject UpgradeButton;
    public GameObject GetGoldButton;
    public GameObject NoticePanel;

    public string itemName;

    public int level;

    public long currentCost;

    

    public long startCurrentCost = 1;
    public long startcurrentQuestGold;
    public long startMaxQuestGold;

    [HideInInspector]
    public long goldPerSec;
    public long showgoldPerSec;
    public long startGoldPerSec;

    public float costPow;
    public float upgradePow;
    public float MaxQuestPow;

    public long appearGoldPerSec;

    public long currentQuestGold;
    public long MaxQuestGold;

    public bool QuestGoldIsFull = false;
    public bool canPurchased = true;

    public GameObject prefab_floating_text;

    //[HideInInspector]
    public bool isPurchased = false; //아이템 구매 유무
    bool isPaused = false;

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

    void Start()
    {
        //시작시 데이터 불러오기
        //DataController.Instance.LoadItemButton(this);
        //StartCoroutine("AddGoldLoop");
        UpdateUI();
    }

    

    public void PurchaseItem()
    {
        canPurchased = true;

        if(isPurchased == false)
        {
            if(DataController.Instance.gold <= currentCost)
            {
                NoticePanel.GetComponentInChildren<Text>().text = "금액이 부족합니다.";
                NoticePanel.SetActive(true);
                canPurchased = false; //구매 불가능
            }
            if(this.itemName == "마을 주민 돕기" && GetBool("ch1" + 5) == false) //조건 챕터1-5 클리어
            {
                NoticePanel.GetComponentInChildren<Text>().text = "스테이지 1-5를 클리어 해주세요";
                NoticePanel.SetActive(true);
                canPurchased = false; //구매 불가능
            }
            if(this.itemName == "동네 양아치 퇴치" && GetBool("ch1" + 10) == false) //조건 챕터1-10 클리어
            {
                NoticePanel.GetComponentInChildren<Text>().text = "스테이지 1-10를 클리어 해주세요";
                NoticePanel.SetActive(true);
                canPurchased = false; //구매 불가능
            }
            if(this.itemName == "왕국기사 훈련" && GetBool("ch1" + 15) == false) //조건 챕터1-15 클리어
            {
                NoticePanel.GetComponentInChildren<Text>().text = "스테이지 1-15를 클리어 해주세요";
                NoticePanel.SetActive(true);
                canPurchased = false; //구매 불가능
            }
        }

        if(DataController.Instance.gold >= currentCost && canPurchased == true)
        {
            
            if(currentQuestGold >= MaxQuestGold)
            {
                currentQuestGold = currentQuestGold / 2;
            }
            isPurchased = true;
            DataController.Instance.gold -= currentCost;
           // level = level + 1
            level++; 

            
            UpdateItem();

            DataController.Instance.SaveItemButton(this);
            DataController.Instance.LoadItemButton(this);
            UpdateUI();
            
            
            
            //구매할시 데이터 저장
        }
    }

    public void GetQuestGold()
    {
         
        var clone = Instantiate(prefab_floating_text, new Vector3(0, 2), Quaternion.Euler(Vector3.zero));

        clone.GetComponent<FloatingText>().text.text = UiManager.ToStringKR(currentQuestGold);

        
        clone.transform.SetParent(this.transform);
        clone.transform.GetChild(0).gameObject.SetActive(true);
        clone.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Image/UI/Freeui/ZOSMA/Main/Coin");

        DataController.Instance.gold += currentQuestGold;
        currentQuestGold = 0;
        UpdateUI();
    }

    public void getAllQuestGold()
    {
        var clone = Instantiate(prefab_floating_text, new Vector3(0, 2), Quaternion.Euler(Vector3.zero));

        clone.GetComponent<FloatingText>().text.text = UiManager.ToStringKR(DataController.Instance.countgold);
        
        clone.transform.SetParent(this.transform);
        clone.transform.GetChild(0).gameObject.SetActive(true);

        clone.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Image/UI/Freeui/ZOSMA/Main/Coin");
    }

    IEnumerator AddGoldLoop()
    {  //1초마다 골드가 들어옴 (자동골드흭득기능)
        while(true)
        {
            //Debug.Log("Auto is working!!");
            if(isPurchased)  //아이템이 구매(활성화)됐을 시
            {
                UpgradeButton.GetComponent<CanvasGroup>().alpha = 1f;
                GetGoldButton.GetComponent<CanvasGroup>().alpha = 1f;
                
                currentQuestGold += goldPerSec;
                if(currentQuestGold >= MaxQuestGold)  //currentQuestGold가 꽉찰시
                {
                    currentQuestGold = 2 * MaxQuestGold;
                    GetGoldButton.GetComponentInChildren<Text>().text = "수령하기";
                    GetGoldButton.GetComponentInChildren<Text>().fontSize = 43;
                    GetGoldButton.GetComponent<Image>().color = Color.cyan;
                    QuestGoldIsFull = true;
                }
                else
                {
                    GetGoldButton.GetComponentInChildren<Text>().text = "수령하기";
                    GetGoldButton.GetComponentInChildren<Text>().fontSize = 50;
                    GetGoldButton.GetComponent<Image>().color = Color.white;
                    QuestGoldIsFull = false;
                }
            }
            else
            {
                goldPerSec = 0;
                currentCostDisplayer.text = "" + startCurrentCost;
                UpgradeButton.GetComponent<CanvasGroup>().alpha = 0.5f;
                GetGoldButton.GetComponent<CanvasGroup>().alpha = 0.5f;
            }
            DataController.Instance.SaveItemButton(this);
            DataController.Instance.LoadItemButton(this);
            UpdateUI();
            
            yield return new WaitForSeconds(1.0f);
            //1초 대기
        }
    }

    public void InitItem()
    {
        DataController.Instance.InitItem(this);
        DataController.Instance.LoadItemButton(this);
    }

    public void UpdateItem()
    {
        goldPerSec = goldPerSec + startGoldPerSec * (long)Mathf.Pow(upgradePow, level);
        currentCost = startCurrentCost * (long)Mathf.Pow(costPow, level);
        MaxQuestGold = startMaxQuestGold * (long)Mathf.Pow(MaxQuestPow, level);
    }

    public void UpdateUI()
    {
        string key = this.itemName;


        itemDisplayer.text = itemName + "  LV." + level + "\n초당골드: " + UiManager.ToStringKR(goldPerSec) + "\n" + UiManager.ToStringKR(currentQuestGold) + " / \n" + UiManager.ToStringKR(MaxQuestGold);
        //itemDisplayer.text = itemName + "  LV." + level + "\n " + UiManager.ToStringKR(goldPerSec) + " " + UiManager.ToStringKR(currentQuestGold) + " / \n" + UiManager.ToStringKR(MaxQuestGold);
        //currentCostDisplayer.text = "" + UiManager.ToStringKR(currentCost);
        currentCostDisplayer.text = "" + UiManager.ToStringKR(long.Parse(PlayerPrefs.GetString(key + "_currentCost")));
        this.GetComponentInChildren<Text>().text = itemName;
    }

}
