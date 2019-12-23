using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SummonSystem : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject[]hero = new GameObject[0];

    int result = 0;

    public Text gold_text;
    public Text crystal_text;

    public GameObject prefab_floating_text;
    public Transform tranform_canvas;

    public Inventory inventory;

    public long require_diamond;

    long current_heart = 9;
    long max_heart = 10;

    public float LimitTime;
    public Text text_Timer;
    public Text text_Heart;

    public long common_summon_current_heart   
    {
        get
        {
            if(!PlayerPrefs.HasKey("common_summon_current_heart")) // 골드가 없을떄
            {
                return 10;
            }
            string tmpcommon_summon_current_heart = PlayerPrefs.GetString("common_summon_current_heart");
            return long.Parse(tmpcommon_summon_current_heart);
        }
        set
        {
            PlayerPrefs.SetString("common_summon_current_heart", value.ToString()); 
        }
    }

    public long common_summon_max_heart
    {
        get
        {
            if(!PlayerPrefs.HasKey("common_summon_max_heart")) // 골드가 없을떄
            {
                return 10;
            }
            string tmpcommon_summon_max_heart = PlayerPrefs.GetString("common_summon_max_heart");
            return long.Parse(tmpcommon_summon_max_heart);
        }
        set
        {
            PlayerPrefs.SetString("common_summon_max_heart", value.ToString()); 
        }
    }

    public float common_summon_limit_time
    {
        get
        {
            if(!PlayerPrefs.HasKey("common_summon_limit_time")) // 골드가 없을떄
            {
                return 3600;
            }
            string tmpcommon_summon_limit_time = PlayerPrefs.GetString("common_summon_limit_time");
            return long.Parse(tmpcommon_summon_limit_time);
        }
        set
        {
            PlayerPrefs.SetString("common_summon_limit_time", value.ToString()); 
        }
    }

    public void init()
    {
        current_heart = 10;
    }

    void Start()
    {
        LimitTime = common_summon_limit_time - DataController.Instance.timeAfterLastPlay;
        current_heart = common_summon_current_heart;
        max_heart = common_summon_max_heart;
        StartCoroutine("check_char_exist");
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        while(true)
        {
            gold_text.text = UiManager.ToStringKR(DataController.Instance.gold);
            crystal_text.text = UiManager.ToStringKR(DataController.Instance.diamond);
            while(LimitTime < 0)
                {

                    LimitTime += 1200;  //LifeTime - 지나간 시간 라이프타임 = 1200;  
                    current_heart += 1;
                }
            if(current_heart < max_heart) //충전 1200초 = 20분
            {
                
                LimitTime -= 1;
                text_Timer.text = "" + Math.Truncate(Mathf.Round(LimitTime)/60)  + " : " + Math.Truncate(Mathf.Round(LimitTime)%60%60);
                
            }
            else //하트가 충전되면
            {
                text_Timer.text = "Full";
                current_heart = max_heart;
                LimitTime = 3600;
            }
            text_Heart.text = "" + current_heart + "/" + max_heart;
        
            common_summon_limit_time = LimitTime;
            common_summon_current_heart = current_heart;
            common_summon_max_heart = max_heart;
            yield return new WaitForSeconds(1f);
        }
    }

    public IEnumerator check_char_exist()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            for(int i =0; i < hero.Length; i++)
            {//enum형식에 for 적용하려고 하는데 모르겠따.
                hero_list a = (hero_list)i;
                if(PowerController.check_list.Load(a.ToString()) == 1)
                {
                    //Debug.Log("answer is " + a.ToString());
                    hero[i].SetActive(true);
                }
                else
                {
                    hero[i].SetActive(false);
                    //hero[i].transform.SetParent(Hand);
                    //Debug.Log("Instpertor length:  " + power.Length);
                }
            }
        }
    }

    public enum hero_list
    {
        Knight,
        Archer
    }

    public void select_rank()
    {
        int number = UnityEngine.Random.Range(1, 1001); //1~1000 사이의 숫자
         // 0=에러 1=C 2=B 3=A 4=S 5=??

        if(number > 0 && number <=500) //50%
        {
            result = 1; // 꽝
        }
        else if(number > 0 && number <=770) // 27%
        {
            result = 2; // 골드
        }
        else if(number > 0 && number <=970) // 20%
        {
            result = 3; // 음식
        }
        else if(number > 0 && number <=1000) // 1%
        {
            result = 4; // 경험치포션
        }
        else
        {
            Debug.Log("error");
        }
    }

    public void common_summon()
    {
        if(current_heart >= 1)
        {
            select_rank();
        
            if(result == 1)
            {
                SoundManager.Instance.click_get_item_sound();  //꽝

                var clone = Instantiate(prefab_floating_text, new Vector3(-6, 0), Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingText>().text.text = " 꽝 ";
                clone.transform.SetParent(tranform_canvas);
            }

            if(result == 2)     //골드
            {
                SoundManager.Instance.click_get_item_sound();

                long gold = DataController.Instance.goldPerClick * 1000;
                DataController.Instance.gold += gold;

                var clone = Instantiate(prefab_floating_text, new Vector3(-6, 0), Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingText>().text.text = UiManager.ToStringKR(gold);
                clone.transform.SetParent(tranform_canvas);

                clone.transform.GetChild(0).gameObject.SetActive(true);
                clone.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Image/UI/Freeui/ZOSMA/Main/Coin");
            }

            if(result == 3)    //음식
            {
                SoundManager.Instance.click_get_item_sound();
                int i =UnityEngine.Random.Range(0,4); // 0~3

                inventory.AddItem(i);
                string item = inventory.database.FetchItemByID(i).Title;
                Sprite image = inventory.database.FetchItemByID(i).Sprite;

                var clone = Instantiate(prefab_floating_text, new Vector3(-6, 0), Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingText>().text.text = item;
                clone.transform.SetParent(tranform_canvas);

                clone.transform.GetChild(0).gameObject.SetActive(true);
                clone.GetComponentInChildren<Image>().sprite = image;
            }

            if(result == 4) //경험치 포션 => 랜덤 광물 3개
            {
                SoundManager.Instance.click_get_item_sound();

                int i = UnityEngine.Random.Range(1, 6);
                int current_booty = PlayerPrefs.GetInt("booty_" + i);
                PlayerPrefs.SetInt("booty_" + i, current_booty + 3);
                String booty_name = null;
                Sprite booty_img = null;
                if(i == 1) { booty_name = "납 주괴"; booty_img = Resources.Load<Sprite>("Image/Item/Icons/ingots"); }
                else if(i == 2) { booty_name = "철 주괴"; booty_img = Resources.Load<Sprite>("Image/Item/Icons/Iron"); }
                else if(i == 3) { booty_name = "금 주괴"; booty_img = Resources.Load<Sprite>("Image/Item/Icons/Gold"); }
                else if(i == 4) { booty_name = "다이아몬드"; booty_img = Resources.Load<Sprite>("Image/Item/Icons/Gem_03"); }
                else if(i == 5) { booty_name = "오리하르콘"; booty_img = Resources.Load<Sprite>("Image/Item/Icons/Crystal_01"); }

                var clone = Instantiate(prefab_floating_text, new Vector3(-6, 0), Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingText>().text.text = booty_name + " +3";
                clone.transform.SetParent(tranform_canvas);

                clone.transform.GetChild(0).gameObject.SetActive(true);
                clone.GetComponentInChildren<Image>().sprite = booty_img;
            }
            current_heart--;
        }
        else 
        {
            goToPanel.Instance.show_noticePanel();
            goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "뽑기 기회를 모두 소진하셨습니다.";
        }
        gold_text.text = UiManager.ToStringKR(DataController.Instance.gold);
        crystal_text.text = UiManager.ToStringKR(DataController.Instance.diamond);
        
    }

    public void rare_summon()
    {
        require_diamond = 100;
        if(DataController.Instance.diamond >= require_diamond)
        {
            int number = UnityEngine.Random.Range(1, 1001); //1~1000 사이의 숫자
            // 0=에러 1=C 2=B 3=A 4=S 5=??

            if(number > 0 && number <=500) //50%
            {
                result = 1; // 꽝
            }
            else if(number > 0 && number <=780) // 28%
            {
                result = 2; // 음식X10
            }
            else if(number > 0 && number <=980) // 20%
            {
                result = 3; // 경험치포션
            }
            else if(number > 0 && number <=1000) // 2%
            {
                result = 4; // 유물뽑기권
            }
            
            if(result == 1)     //꽝
            {
                SoundManager.Instance.click_get_item_sound();  //꽝
                var clone = Instantiate(prefab_floating_text, new Vector3(-6, 0), Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingText>().text.text = " 꽝 ";
                clone.transform.SetParent(tranform_canvas);
            }

            if(result == 2)    //음식X10
            {
                SoundManager.Instance.click_get_item_sound();
                int i = UnityEngine.Random.Range(0, 4); //0~3

                for(int n =0; n < 3; n++)
                {
                    inventory.AddItem(i);
                }
                
                string item = inventory.database.FetchItemByID(i).Title;
                Sprite image = inventory.database.FetchItemByID(i).Sprite;

                var clone = Instantiate(prefab_floating_text, new Vector3(-6, 0), Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingText>().text.text = item + " X3";
                clone.transform.SetParent(tranform_canvas);

                clone.transform.GetChild(0).gameObject.SetActive(true);
                clone.GetComponentInChildren<Image>().sprite = image;
            }

            if(result == 3)
            {
                SoundManager.Instance.click_get_item_sound();

                int i = UnityEngine.Random.Range(1, 6);
                int current_booty = PlayerPrefs.GetInt("booty_" + i);
                PlayerPrefs.SetInt("booty_" + i, current_booty + 5);
                String booty_name = null;
                Sprite booty_img = null;
                if(i == 1) { booty_name = "납 주괴"; booty_img = Resources.Load<Sprite>("Image/Item/Icons/ingots"); }
                else if(i == 2) { booty_name = "철 주괴"; booty_img = Resources.Load<Sprite>("Image/Item/Icons/Iron"); }
                else if(i == 3) { booty_name = "금 주괴"; booty_img = Resources.Load<Sprite>("Image/Item/Icons/Gold"); }
                else if(i == 4) { booty_name = "다이아몬드"; booty_img = Resources.Load<Sprite>("Image/Item/Icons/Gem_03"); }
                else if(i == 5) { booty_name = "오리하르콘"; booty_img = Resources.Load<Sprite>("Image/Item/Icons/Crystal_01"); }

                var clone = Instantiate(prefab_floating_text, new Vector3(-6, 0), Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingText>().text.text = booty_name + " +5";
                clone.transform.SetParent(tranform_canvas);

                clone.transform.GetChild(0).gameObject.SetActive(true);
                clone.GetComponentInChildren<Image>().sprite = booty_img;
            }

            if(result == 4)
            {
                SoundManager.Instance.click_get_item_sound();
                
                DataController.Instance.artifact_ticket++;
                var clone = Instantiate(prefab_floating_text, new Vector3(-6, 0), Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingText>().text.text = "유물 뽑기권+1";
                clone.transform.SetParent(tranform_canvas);
            }
            DataController.Instance.diamond -= require_diamond;
        }
        else 
        {
            goToPanel.Instance.show_noticePanel();
            goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "돈이 부족합니다.";
        }
        gold_text.text = UiManager.ToStringKR(DataController.Instance.gold);
        crystal_text.text = UiManager.ToStringKR(DataController.Instance.diamond);
        
    }

    public void epic_summon()
    {
        require_diamond = 1000;
        if(DataController.Instance.diamond >= require_diamond)
        {
            int number = UnityEngine.Random.Range(1, 1001); //1~1000 사이의 숫자
            // 0=에러 1=C 2=B 3=A 4=S 5=??

            if(number > 0 && number <=500) //50%
            {
                result = 1; // 음식X30
            }
            else if(number > 0 && number <=800) // 30%
            {
                result = 2; // 경험치포션X10
            }
            else if(number > 0 && number <=970) // 17%
            {
                result = 3; // 유물뽑기권
            }
            else if(number > 0 && number <=1000) // 3%
            {
                result = 4; // 권능해방
            }
            
            if(result == 1)     //음식X10
            {
                SoundManager.Instance.click_get_item_sound();
                int i = UnityEngine.Random.Range(0, 4); //0~3

                for(int n =0; n < 10; n++)
                {
                    inventory.AddItem(i);
                }
                
                string item = inventory.database.FetchItemByID(i).Title;
                Sprite image = inventory.database.FetchItemByID(i).Sprite;

                var clone = Instantiate(prefab_floating_text, new Vector3(-6, 0), Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingText>().text.text = item + " X10";
                clone.transform.SetParent(tranform_canvas);

                clone.transform.GetChild(0).gameObject.SetActive(true);
                clone.GetComponentInChildren<Image>().sprite = image;
            }

            if(result == 2)    //모든광물 10개
            {
                SoundManager.Instance.click_get_item_sound();

                for(int i = 1; i < 6; i++)
                {   
                    int current_booty = PlayerPrefs.GetInt("booty_" + i);
                    PlayerPrefs.SetInt("booty_" + i, current_booty + 10);
                    String booty_name = null;
                    Sprite booty_img = null;
                    if(i == 1) { booty_name = "납 주괴"; booty_img = Resources.Load<Sprite>("Image/Item/Icons/ingots"); }
                    else if(i == 2) { booty_name = "철 주괴"; booty_img = Resources.Load<Sprite>("Image/Item/Icons/Iron"); }
                    else if(i == 3) { booty_name = "금 주괴"; booty_img = Resources.Load<Sprite>("Image/Item/Icons/Gold"); }
                    else if(i == 4) { booty_name = "다이아몬드"; booty_img = Resources.Load<Sprite>("Image/Item/Icons/Gem_03"); }
                    else if(i == 5) { booty_name = "오리하르콘"; booty_img = Resources.Load<Sprite>("Image/Item/Icons/Crystal_01"); }

                    var clone = Instantiate(prefab_floating_text, new Vector3(-6, i-2), Quaternion.Euler(Vector3.zero));
                    clone.GetComponent<FloatingText>().text.text = booty_name + " +10";
                    clone.transform.SetParent(tranform_canvas);

                    clone.transform.GetChild(0).gameObject.SetActive(true);
                    clone.GetComponentInChildren<Image>().sprite = booty_img;
                }
                
            }

            if(result == 3)  //유물뽑기권
            {
                SoundManager.Instance.click_get_item_sound();
                
                DataController.Instance.artifact_ticket++;
                var clone = Instantiate(prefab_floating_text, new Vector3(-6, 0), Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingText>().text.text = "유물 뽑기권+1";
                clone.transform.SetParent(tranform_canvas);
            }

            if(result == 4)
            {
                SoundManager.Instance.click_get_item_sound();
                
                DataController.Instance.power_ticket++;
                var clone = Instantiate(prefab_floating_text, new Vector3(-6, 0), Quaternion.Euler(Vector3.zero));
                clone.GetComponent<FloatingText>().text.text = "권능 해방+1";
                clone.transform.SetParent(tranform_canvas);
            }
            DataController.Instance.diamond -= require_diamond;
        }
        else 
        {
            goToPanel.Instance.show_noticePanel();
            goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "돈이 부족합니다.";
        }
        gold_text.text = UiManager.ToStringKR(DataController.Instance.gold);
        crystal_text.text = UiManager.ToStringKR(DataController.Instance.diamond);
        
    }
    



    public void initiaze_hero()     //캐릭터조각 기능 넣을때 만든건데 캐릭터조각이 없으뮤ㅠ
    {
        for (int i = 0; i < 2; i++) //초기화 기능 넣기
        {
            hero_list a = (hero_list)i;
            if(PowerController.check_list.Load(a.ToString()) == 1)
            {
                if(i == 0)
                {
                    PowerController.check_list.Save(a.ToString(), 0);
                    Debug.Log("knight초기화!!");
                }
            }
            if(PowerController.check_list.Load(a.ToString()) == 1)
            {
                if(i == 1)
                {
                    PowerController.check_list.Save(a.ToString(), 0);
                    Debug.Log("archer초기화!!");
                }
            }
        }
    }

    public void debug()
    {
        for(int i = 0; i < 2; i++)
        {
            hero_list a = (hero_list)i;
            Debug.Log(PowerController.check_list.Load(a.ToString()));
        }
    }

    
}
