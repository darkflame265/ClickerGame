using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CharacterStateController : MonoBehaviour
{
    private static CharacterStateController instance;

    public static CharacterStateController Instance
    {
        get{
            if(instance == null)
            {
                instance = FindObjectOfType<CharacterStateController>();

                if(instance == null)
                {
                    GameObject container = new GameObject("CharacterStateController");

                    instance = container.AddComponent<CharacterStateController>();
                }
            }
            return instance;
        }
    }

    public Text gold_text;
    public Text crystal_text;

    public GameObject skill_panel;

    public Text get_skill_text;
    public Image get_skill_image;
    public Text get_skill_name;
    public Text get_skill_explain;

    public Image Exp_Bar;

    //UI
    public GameObject skillContent0;
    public GameObject skillContent1;
    public GameObject skillContent2;


    public Text Exp_Percent_text;
    public GameObject SelectFrame;  //skill
    public GameObject charSelectFrame;  //char

    public GameObject knightBtn;
    public GameObject archerBtn;
    public GameObject wizardBtn;

    public GameObject[]knight = new GameObject[0];
    public GameObject[]archer = new GameObject[0];
    public GameObject[]wizard = new GameObject[0];

    //skill 목록
    public Text skill_explain;

    //List<string> skill_name = new List<string>();
    public string[] skill_name = new string[0];
    public string[] skill_effect = new string[0];

    List<string> skill1_name = new List<string>();
    List<string> skill1_effect = new List<string>();


    public static int skill_size = 15;
    public GameObject[]skill = new GameObject[skill_size];
    public GameObject[]skill_empty = new GameObject[0];
    public GameObject[]skill1 = new GameObject[2];
    public GameObject[]skill1_empty = new GameObject[0];

    public static void SetBool(string key, bool value)
    {
        if(value)
            PlayerPrefs.SetInt(key, 1);
        else
            PlayerPrefs.SetInt(key, 0);
    }

    public string []skill_id = new string[skill_size];
    public string []skill1_id = new string[2];

    public GameObject select_character_prefabs;  //empty

    public GameObject knight01;
    public GameObject knight02;

    public GameObject archer01;
    public GameObject archer02;

    public GameObject probability_panel;

    public GameObject skill_upgrade_button;

    

    void Start()
    {
        for(int index = 0; index < 3; index++)
        {
            skill_box_pack[index].GetComponent<Image>().sprite = Skill_Img_pack[PlayerPrefs.GetInt("select_skill" + index)]; //스킬 이미지
            skill_box_pack[index].GetComponentInChildren<Text>().text = PlayerPrefs.GetString("skill_name_text" + index); //PlayerPrefs.GetString("skill_name_text")
            Debug.Log("PlayerPrefs.GetString(skill_name_text + index) : " + PlayerPrefs.GetString("skill_name_text" + index));
            if(PlayerPrefs.GetString("skill_name_text" + index) == "구매 완료")
            {
                price_text[index].transform.parent.GetComponent<Button>().interactable = false;
            }
            
            price_text[index].text = ""+ PlayerPrefs.GetInt("skill_Price_diamond"+ index);
        }

        Debug.Log("price_text[0].text : " + price_text[0].text);
        if(price_text[0].text == "0")     //맨처음 1번만 실행
        {
            DataController.Instance.diamond += 500;
            change_skill_in_shop();
        } else {
        }
        

        select_knight();
        insert_skill1_name_toList();
        insert_skill1_effect_toList();
        StartCoroutine("SomeCoroutine");
        StartCoroutine("Auto");
    }

    IEnumerator Auto()
    {
        while(true)
        {   
            yield return new WaitForSeconds(0.1f);
            gold_text.text = UiManager.ToStringKR(DataController.Instance.gold);
            crystal_text.text = UiManager.ToStringKR(DataController.Instance.diamond);
        }
    }

    

    void insert_skill1_name_toList()
    {
        skill1_name.Add("강타(전사 3레벨 흭득)");
        skill1_name.Add("속사(궁수 3레벨 흭득)");
        skill1_name.Add("익스플로전(마법사 3레벨 흭득)");
    }

    void insert_skill1_effect_toList()
    {
        skill1_effect.Add("자신의 공격력의 200%의 일격을 2번 입힙니다.");
        skill1_effect.Add("스킬화살 10개를 적에게 날립니다.");
        skill1_effect.Add("폭발하는 화염구를 발사해 적에게 스킬공격력의 10배의 데미지를 입힙니다.");
    }

    void hide_all_skill_content() {
        skillContent0.SetActive(false);
        skillContent1.SetActive(false);
        skillContent2.SetActive(false);
    }

    public void show_skill_content0() {
        hide_all_skill_content();
        skillContent0.SetActive(true);
    }

    public void show_skill_content1() {
        hide_all_skill_content();
        skillContent1.SetActive(true);
    }

    public void show_skill_content2() {
        hide_all_skill_content();
        skillContent2.SetActive(true);
    }

    public void show_probability()
    {
        probability_panel.SetActive(!probability_panel.active);
    }



    public void setT_char_selectFrame(Transform transform)
    {
        var tra = transform;
        charSelectFrame.SetActive(true);
        charSelectFrame.transform.position = tra.position;
        charSelectFrame.transform.parent = tra.transform;
    }

    public void get_free_state_dev()
    {
        DataController.Instance.freestate = 10000;
    }

    public void check_current_char()
    {
        if(charSelectFrame.transform.parent == knightBtn)
        {
            select_knight();
        }
        if(charSelectFrame.transform.parent == archerBtn)
        {
            select_archer();
        }
        if(charSelectFrame.transform.parent == wizardBtn)
        {
            select_wizard();
        }
        else
        {
            select_knight();
        }
    }

    public void select_knight()
    {
        for(int i = 0; i < knight.Length; i++)
        {
            if(DataController.Instance.knight_level == i+1)
            {
                select_character_prefabs = knight[i];
                break;
            }
        }
        UiManager.Instance.char_ui();
    }

    public void select_archer()
    {
        for(int i = 0; i < archer.Length; i++)
        {
            if(DataController.Instance.archer_level == i+1)
            {
                select_character_prefabs = archer[i];
                break;
            }
        }
        UiManager.Instance.char_ui();
    }

    public void select_wizard()
    {
        for(int i = 0; i < wizard.Length; i++)
        {
            if(DataController.Instance.wizard_level == i+1)
            {
                select_character_prefabs = wizard[i];
                break;
            }
        }
        UiManager.Instance.char_ui();
    }

    
    

    IEnumerator SomeCoroutine() //artifact_bool값을 프리팹으로 저장하기 
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            
            check_exp();
            // if(selected_skill_inform != -1)
            // {
            //     if(PlayerPrefs.GetInt("skill_card_value"+selected_skill_inform) >= 
            //     max_card_value[PlayerPrefs.GetInt("skill_level_" + selected_skill_inform)])
            //     {
            //         skill_upgrade_button.SetActive(true);
            //     } else {
            //         skill_upgrade_button.SetActive(false);
            //     }
            // }

            for(int i = 0; i < skill.Length; i++) //skill.Length = 16
            {
                if(PlayerPrefs.GetInt(skill_id[i]) == 1)
                {
                    skill[i].SetActive(true);
                    skill_empty[i].SetActive(false);
                    //skill[i].GetComponent<Image>().color = new Color(1f, 1f,1f);
                }
                else
                {
                    skill[i].SetActive(false);
                    skill_empty[i].SetActive(true);
                   // skill[i].GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
                }
            }

            for(int i = 0; i < skill1.Length; i++) //checkskill_and_show
            {
                if(PlayerPrefs.GetInt(skill1_id[i]) == 1)
                {
                    skill1[i].SetActive(true);
                    skill1_empty[i].SetActive(false);
                    //skill1[i].GetComponent<Image>().color = new Color(1f, 1f,1f);
                }
                else
                {
                    skill1[i].SetActive(false);
                    skill1_empty[i].SetActive(true);
                    //skill1[i].GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
                }
            }
            
        }
    }

    public Sprite[] Skill_Img_pack = new Sprite[0];
    public GameObject[] skill_box_pack = new GameObject[0];
    public Text[] price_text = new Text[0];

    public void change_skill_in_shop()     //상점에 스킬이 없을 떄 => 설치 후 초기에만 실행 => 이후에는 광고보고 실행 가능
    {
        if(DataController.Instance.diamond >= 500)
        {
            for(int index = 0; index < 3; index++)
            {
                int i = Random.Range(0, skill.Length);
                
                price_text[index].transform.parent.GetComponent<Button>().interactable = true;
                PlayerPrefs.SetInt("select_skill"+index, i);

                int pr = Random.Range(1, 20);

                PlayerPrefs.SetInt("skill_Price_diamond"+index, pr*100);  //100 ~ 5000까지
                //if(PlayerPrefs.GetInt("select_skill") == 8)

                skill_box_pack[index].GetComponent<Image>().sprite = Skill_Img_pack[i]; //스킬 이미지
                skill_box_pack[index].GetComponentInChildren<Text>().text = skill_name[i];
                price_text[index].text = ""+ PlayerPrefs.GetInt("skill_Price_diamond"+ index);
                PlayerPrefs.SetString("skill_name_text"+index, skill_name[i]);
            }
            DataController.Instance.diamond -= 500;
        }
        
        
    }

    private long skill_price_diamond = 0;

    public void buy_skill()   //스킬 구매할 시
    {
        //스킬 얻었다고 prefab 수정
        //스킬 얻었으니 스킬 효과 부여 : 조건 => 스킬을 소지 && 레벨이 올랐을 떄
         for(int index = 0; index < 3; index++)
        {
            if(EventSystem.current.currentSelectedGameObject.name == price_text[index].transform.parent.name)
            {
                int select_skill = PlayerPrefs.GetInt("select_skill"+index);
                long skill_price_diamond = PlayerPrefs.GetInt("skill_Price_diamond"+index);
                Debug.Log("select skill : " + select_skill);
                Debug.Log("skill_price_diamond : " + skill_price_diamond);
                    if(DataController.Instance.diamond >= skill_price_diamond)
                    {
                        
                        int current_card = PlayerPrefs.GetInt("skill_card_value"+select_skill);
                        PlayerPrefs.SetInt("skill_card_value"+select_skill, current_card+1);     //카드 개수 증가
                        if(PlayerPrefs.GetInt(skill_id[select_skill]) == 0) //스킬을 처음 얻을 때만 효과 부여
                        {
                            if(select_skill == 0)
                            {
                                DataController.Instance.health += 100;
                            } else if(select_skill == 1) {
                                DataController.Instance.attack += 100;
                            } else if(select_skill == 2) {
                                DataController.Instance.mana += 100;
                            } else if(select_skill == 3) {
                                DataController.Instance.special += 100;
                            } else if(select_skill == 4) {     //치명타
                                float current_critical = PlayerPrefs.GetFloat("current_critical");
                                PlayerPrefs.SetFloat("current_critical", current_critical + 2f);
                            } else if(select_skill == 5) {        //회피    
                                float current_avoid = PlayerPrefs.GetFloat("current_avoid");
                                PlayerPrefs.SetFloat("current_avoid", current_avoid + 2f);
                            } else if(select_skill == 6) {      //골드 기본수치
                                int current_plus_gold = PlayerPrefs.GetInt("current_plus_gold");
                                PlayerPrefs.SetInt("current_plus_gold", current_plus_gold + 5);
                            } else if(select_skill == 7) {      //상점 최대 구매횟수
                                int current_plus_maxheart = PlayerPrefs.GetInt("current_plus_maxheart");
                                PlayerPrefs.SetInt("current_plus_maxheart", current_plus_maxheart + 1);
                            }
                        }
                        Debug.Log("success to buy skill");
                        PlayerPrefs.SetInt(skill_id[select_skill], 1);  //스킬 얻음
                        DataController.Instance.diamond -= skill_price_diamond;

                        price_text[index].transform.parent.GetComponent<Button>().interactable = false;
                        skill_box_pack[index].GetComponentInChildren<Text>().text = "구매 완료";
                        PlayerPrefs.SetString("skill_name_text"+index, "구매 완료");
                    } else {
                        goToPanel.Instance.show_noticePanel();
                        goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "금액이 부족합니다";
                    }
                    break;
            }
        }
        
       
        
    }

    public void upgrade_skill()
    {
        
        if(PlayerPrefs.GetInt("skill_card_value"+selected_skill_inform) >= 
        max_card_value[PlayerPrefs.GetInt("skill_level_" + selected_skill_inform)])
        {
            SoundManager.Instance.upgrade_button_sound();
            //카드 지출
            int current_card_value = PlayerPrefs.GetInt("skill_card_value"+selected_skill_inform);
            PlayerPrefs.SetInt("skill_card_value"+selected_skill_inform, current_card_value-max_card_value[PlayerPrefs.GetInt("skill_level_" + selected_skill_inform)]);

            //레벨업, 효과 부여
            int current_level = PlayerPrefs.GetInt("skill_level_" + selected_skill_inform);
            PlayerPrefs.SetInt("skill_level_" + selected_skill_inform, current_level+1);

            if(selected_skill_inform == 0)
                    {
                        DataController.Instance.health += 100;
                    } else if(selected_skill_inform == 1) {
                        DataController.Instance.attack += 100;
                    } else if(selected_skill_inform == 2) {
                        DataController.Instance.mana += 100;
                    } else if(selected_skill_inform == 3) {
                        DataController.Instance.special += 100;
                    } else if(selected_skill_inform == 4) {     //치명타
                        float current_critical = PlayerPrefs.GetFloat("current_critical");
                        PlayerPrefs.SetFloat("current_critical", current_critical + 2f);
                    } else if(selected_skill_inform == 5) {        //회피    
                        float current_avoid = PlayerPrefs.GetFloat("current_avoid");
                        PlayerPrefs.SetFloat("current_avoid", current_avoid + 2f);
                    } else if(selected_skill_inform == 6) {      //골드 기본수치
                        int current_plus_gold = PlayerPrefs.GetInt("current_plus_gold");
                        PlayerPrefs.SetInt("current_plus_gold", current_plus_gold + 5);
                    } else if(selected_skill_inform == 7) {      //상점 최대 구매횟수
                        int current_plus_maxheart = PlayerPrefs.GetInt("current_plus_maxheart");
                        PlayerPrefs.SetInt("current_plus_maxheart", current_plus_maxheart + 1);
                    }

            skill_explain.text = skill_name[selected_skill_inform] + "   LV." + PlayerPrefs.GetInt("skill_level_" + selected_skill_inform) + 
                "       " + PlayerPrefs.GetInt("skill_card_value"+selected_skill_inform) + "/" + max_card_value[PlayerPrefs.GetInt("skill_level_" + selected_skill_inform)] 
                +"\n효과: " + set_skill_effect_string(selected_skill_inform);

            if(PlayerPrefs.GetInt("skill_card_value"+selected_skill_inform) >= 
                max_card_value[PlayerPrefs.GetInt("skill_level_" + selected_skill_inform)])
                {
                    skill_upgrade_button.SetActive(true);
                    //가격설정
                    skill_upgrade_button.GetComponentInChildren<Text>().text = "" + UiManager.ToStringKR(skill_upgrade_price[PlayerPrefs.GetInt("skill_level_" + selected_skill_inform)]); 
                } else {
                    skill_upgrade_button.SetActive(false);
                }
        }
    }

    public void check_exp()
    {
        float exp_percent = (float)DataController.Instance.current_exp / (float)DataController.Instance.max_exp;

        if(DataController.Instance.current_exp >= DataController.Instance.max_exp)
        {
            DataController.Instance.char_level += 1;
            DataController.Instance.current_exp -= DataController.Instance.max_exp;
            DataController.Instance.max_exp = 1 * (long) Mathf.Pow(1.14f, DataController.Instance.char_level);
            DataController.Instance.freestate += (5 * DataController.Instance.freestate_besu);
        }
        else{

            Exp_Bar.fillAmount = exp_percent;
            Exp_Percent_text.text = Mathf.Round(exp_percent * 100) + "%";
        }
    }

    public int[] skill_0_3_value = new int[0];
    public float[] skill_4_5_value = new float[0];
    public long[] skill_6_value = new long[0];
    public int[] skill_7_value = new int[0];

    public string set_skill_effect_string(int i)
    {
        string inform = "";
        if(i == 0)
        {
            inform = "체력이 " + "<Color=#00FF00>" + skill_0_3_value[PlayerPrefs.GetInt("skill_level_0")] +"</color>"+ "증가합니다.";
        }
        else if(i == 1)
        {
            inform = "공격력이 " + "<Color=#00FF00>" + skill_0_3_value[PlayerPrefs.GetInt("skill_level_1")] +"</color>"+ "증가합니다.";
        }
        else if(i == 2)
        {
            inform = "민첩이 " + "<Color=#00FF00>" + skill_0_3_value[PlayerPrefs.GetInt("skill_level_2")] +"</color>"+ "증가합니다.";
        }
        else if(i == 3)
        {
            inform = "스킬공격력이 " + "<Color=#00FF00>" + skill_0_3_value[PlayerPrefs.GetInt("skill_level_3")] +"</color>"+ "증가합니다.";
        }
        else if(i == 4)
        {
            inform = "치명타 확률이 " + "<Color=#00FF00>" + skill_4_5_value[PlayerPrefs.GetInt("skill_level_4")] +"</color>"+ "증가합니다.";
        }
        else if(i == 5)
        {
            inform = "회피 확률이 " + "<Color=#00FF00>" + skill_4_5_value[PlayerPrefs.GetInt("skill_level_5")] +"</color>"+ "증가합니다.";
        }
        else if(i == 6)
        {
            inform = "클릭 기본 골드가 " + "<Color=#00FF00>" + skill_6_value[PlayerPrefs.GetInt("skill_level_6")] +"</color>"+ "증가합니다.";
        }
        else if(i == 7)
        {
            inform = "상점 최대 구매 횟수가 " + "<Color=#00FF00>" + skill_7_value[PlayerPrefs.GetInt("skill_level_7")] +"</color>"+ "증가합니다.";
        }
        return inform;
    }
    public int[] max_card_value = new int[0];
    int selected_skill_inform = -1;
    public long[] skill_upgrade_price = new long[0];

    public void show_skill_inform()
    {
        for(int i = 0; i <= skill.Length-1; i++)
        {
            if(EventSystem.current.currentSelectedGameObject == skill[i])
            {
                skill_explain.text = skill_name[i] + "   LV." + PlayerPrefs.GetInt("skill_level_" + i) + 
                "       " + PlayerPrefs.GetInt("skill_card_value"+i) + "/" + max_card_value[PlayerPrefs.GetInt("skill_level_" + i)] 
                +"\n효과: " + set_skill_effect_string(i);
                
                selected_skill_inform = i;

                //업글 버튼 활성화
                if(PlayerPrefs.GetInt("skill_card_value"+selected_skill_inform) >= 
                max_card_value[PlayerPrefs.GetInt("skill_level_" + selected_skill_inform)])
                {
                    skill_upgrade_button.SetActive(true);
                    //가격설정
                    skill_upgrade_button.GetComponentInChildren<Text>().text = "" + UiManager.ToStringKR(skill_upgrade_price[PlayerPrefs.GetInt("skill_level_" + selected_skill_inform)]); 
                } else {
                    skill_upgrade_button.SetActive(false);
                }
                
            }
            else if(EventSystem.current.currentSelectedGameObject == skill_empty[i])
            {
                //업글 버튼 비활성화
                skill_upgrade_button.SetActive(false);
                skill_explain.text = skill_name[i] + "\n효과: " + set_skill_effect_string(i);
            }
        }
        for(int i = 0; i <= skill1.Length-1; i++)
        {
            if(EventSystem.current.currentSelectedGameObject == skill1[i])
            {
                skill_explain.text = skill1_name[i] + "\n효과: " + skill1_effect[i];
            }
            else if(EventSystem.current.currentSelectedGameObject == skill1_empty[i])
            {
                skill_explain.text = skill1_name[i] + "\n효과: " + skill1_effect[i];
            }
        }
        
        
    }

    public void clear_panel()
    {
        skill_explain.text = "";
        SelectFrame.SetActive(false);
        skill_upgrade_button.SetActive(false);
    }



   
    public void MyPosition (Transform transform)
    {
        var tra = transform;
        SelectFrame.SetActive(true);
        SelectFrame.transform.position = tra.position;
        SelectFrame.transform.parent = tra.transform;
        //클릭하고 아이템창을 움직이면 프레임이 따라서 안움직이는 버그가 나타남
        //해결1 프레임의 위치를 실시간으로 아이템의 위치로 변경한다.
        //해결2 그냥 아이템마다 새로운 프레임을 넣어서 클릭시만 나타나게 한다.
    }

    public void init_level_exp()
    {
        DataController.Instance.char_level = 1;
        DataController.Instance.current_exp = 1;
        DataController.Instance.max_exp = 2;
    }

    public void init_all_state()
    {
        DataController.Instance.health = 5;
        DataController.Instance.attack = 5;
        DataController.Instance.mana = 5;
        DataController.Instance.special = 5;
        DataController.Instance.freestate = 5;
        for(int i = 0; i < skill.Length; i++)
        {
            PlayerPrefs.SetInt(skill_id[i], 0);
        }

    }

    

}
