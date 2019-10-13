using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    

    public GameObject skill_panel;

    public Text get_skill_text;
    public Image get_skill_image;
    public Text get_skill_name;
    public Text get_skill_explain;

    public Image Exp_Bar;

    //UI
    public Text Exp_Percent_text;
    public GameObject SelectFrame;  //skill
    public GameObject charSelectFrame;  //char

    public GameObject knightBtn;
    public GameObject archerBtn;

    //skill 목록
    public Text skill_explain;

    public static int skill_size = 15;
    public GameObject[]skill = new GameObject[skill_size];

    public static void SetBool(string key, bool value)
    {
        if(value)
            PlayerPrefs.SetInt(key, 1);
        else
            PlayerPrefs.SetInt(key, 0);
    }

    public string []skill_id = new string[skill_size];

    public GameObject select_character_prefabs;  //empty

    public GameObject knight01;
    public GameObject knight02;

    public GameObject archer01;
    public GameObject archer02;

    void Start()
    {
        select_knight();
        StartCoroutine("SomeCoroutine");
    }



    public void setT_char_selectFrame(Transform transform)
    {
        var tra = transform;
        charSelectFrame.SetActive(true);
        charSelectFrame.transform.position = tra.position;
        charSelectFrame.transform.parent = tra.transform;
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
        else
        {
            select_knight();
        }
    }

    public void select_knight()
    {
        if(DataController.Instance.knight_level == 1)
        {
            select_character_prefabs = knight01;
        }
        else if(DataController.Instance.knight_level == 2)
        {
            select_character_prefabs = knight02;
        }
    }

    public void select_archer()
    {
        if(DataController.Instance.archer_level == 1)
        {
            select_character_prefabs = archer01;
        }
        else if(DataController.Instance.archer_level == 2)
        {
            select_character_prefabs = archer02;
        }
        
    }

    
    

    IEnumerator SomeCoroutine() //artifact_bool값을 프리팹으로 저장하기 
    {
        while(true)
        {
            yield return new WaitForSeconds(0.2f);
            
            check_exp();
            check_skill();

            for(int i = 0; i <= skill_size; i++) //checkskill_and_show
            {
                if(PlayerPrefs.GetInt(skill_id[i]) == 1)
                {
                    skill[i].SetActive(true);
                    
                }
                else
                {
                    skill[i].SetActive(false);
                }
            }
            
        }
    }

    public void check_skill()
    {
        if(goToPanel.Instance.charPanel.activeSelf == true)
        {
            if(DataController.Instance.health >= 50 && PlayerPrefs.GetInt(skill_id[0]) == 0 && goToPanel.Instance.get_skill_panel.activeSelf == false)
            {
                PlayerPrefs.SetInt(skill_id[0], 1);
                //스킬얻음페널 Text 바꾸기
                get_skill_text.text = "체력 50달성 스킬 흭득";
                get_skill_image.sprite = Resources.Load<Sprite>("Image/Skill/Skill icons Guardian/Icons/Filled/Shoulder_2") as Sprite;
                get_skill_name.text = "경건한 몸";
                get_skill_explain.text = "체력+20, 근력+10, 민첩+10, 마력+10, 클릭골드 2배";
                goToPanel.Instance.get_skill_panel.SetActive(true);
                DataController.Instance.health += 20;
                DataController.Instance.attack += 10;
                DataController.Instance.mana += 10;
                DataController.Instance.special += 10;
                DataController.Instance.MultiplyGoldPerClick(2);
                
               
            }
            if(DataController.Instance.health >= 100 && PlayerPrefs.GetInt(skill_id[1]) == 0 && goToPanel.Instance.get_skill_panel.activeSelf == false)
            {
                PlayerPrefs.SetInt(skill_id[1], 1);
                //스킬얻음페널 Text 바꾸기
                get_skill_text.text = "체력 100달성 스킬 흭득";
                get_skill_image.sprite = Resources.Load<Sprite>("Image/Skill/Skill icons Guardian/Icons/Filled/Shields_2") as Sprite;
                get_skill_name.text = "상급 방패술";
                get_skill_explain.text = "체력+50 근력+30, 클릭골드 3배";
                goToPanel.Instance.get_skill_panel.SetActive(true);
                DataController.Instance.health += 50;
                DataController.Instance.attack += 30;
                DataController.Instance.MultiplyGoldPerClick(3);
                
            }
            
            if(DataController.Instance.health >= 300 && PlayerPrefs.GetInt(skill_id[2]) == 0 && goToPanel.Instance.get_skill_panel.activeSelf == false)
            {
                PlayerPrefs.SetInt(skill_id[2], 1);
                //스킬얻음페널 Text 바꾸기
                get_skill_text.text = "체력 300달성 스킬 흭득";
                get_skill_image.sprite = Resources.Load<Sprite>("Image/Skill/Skill icons Guardian/Icons/Filled/Quake_2") as Sprite;
                get_skill_name.text = "최상급 방패술";
                get_skill_explain.text = "체력+100 클릭골드 4배";//방어력 10배
                goToPanel.Instance.get_skill_panel.SetActive(true);
                DataController.Instance.health += 100;
                DataController.Instance.MultiplyGoldPerClick(4);
                
            }
            
            if(DataController.Instance.health >= 500 && PlayerPrefs.GetInt(skill_id[3]) == 0 && goToPanel.Instance.get_skill_panel.activeSelf == false)
            {
                PlayerPrefs.SetInt(skill_id[3], 1);
                //스킬얻음페널 Text 바꾸기
                get_skill_text.text = "체력 500달성 스킬 흭득";
                get_skill_image.sprite = Resources.Load<Sprite>("Image/Skill/Skill icons Guardian/Icons/Filled/CoverUp_2") as Sprite;
                get_skill_name.text = "수호자의 자질";
                get_skill_explain.text = "체력+200, 근력+100, 민첩+50, 마나+50 클릭골드 5배";//방어력 25배
                goToPanel.Instance.get_skill_panel.SetActive(true);
                DataController.Instance.health += 200;
                DataController.Instance.attack += 100;
                DataController.Instance.mana += 50;
                DataController.Instance.special += 50;
                DataController.Instance.MultiplyGoldPerClick(5);
                
            }
            
            if(DataController.Instance.attack >= 50 && PlayerPrefs.GetInt(skill_id[4]) == 0  && goToPanel.Instance.get_skill_panel.activeSelf == false)
            {
                PlayerPrefs.SetInt(skill_id[4], 1);
                //스킬얻음페널 Text 바꾸기
                get_skill_text.text = "근력 50달성 스킬 흭득";
                get_skill_image.sprite = Resources.Load<Sprite>("Image/Skill/Skill icons Guardian/Icons/Filled/CoverUp_2") as Sprite;
                get_skill_name.text = "전사의 분노";
                get_skill_explain.text = "체력+5, 근력+20, 민첩+10, 클릭골드 2배";
                goToPanel.Instance.get_skill_panel.SetActive(true);
                DataController.Instance.health += 5;
                DataController.Instance.attack += 20;
                DataController.Instance.mana += 10;
                DataController.Instance.MultiplyGoldPerClick(2);
                
            }
            
            if(DataController.Instance.attack >= 100 && PlayerPrefs.GetInt(skill_id[5]) == 0  && goToPanel.Instance.get_skill_panel.activeSelf == false)
            {
                PlayerPrefs.SetInt(skill_id[5], 1);
                //스킬얻음페널 Text 바꾸기
                get_skill_text.text = "근력 100달성 스킬 흭득";
                get_skill_image.sprite = Resources.Load<Sprite>("Image/Skill/Skill icons Guardian/Icons/Filled/CoverUp_2") as Sprite;
                get_skill_name.text = "사무라이의 일섬";
                get_skill_explain.text = "근력+50, 민첩+20, 클릭골드 3배";
                goToPanel.Instance.get_skill_panel.SetActive(true);
                DataController.Instance.attack += 50;
                DataController.Instance.mana += 20;
                DataController.Instance.MultiplyGoldPerClick(3);
                
            }
            
            if(DataController.Instance.attack >= 300 && PlayerPrefs.GetInt(skill_id[6]) == 0  && goToPanel.Instance.get_skill_panel.activeSelf == false)
            {
                PlayerPrefs.SetInt(skill_id[6], 1);
                //스킬얻음페널 Text 바꾸기
                get_skill_text.text = "근력 300달성 스킬 흭득";
                get_skill_image.sprite = Resources.Load<Sprite>("Image/Skill/Skill icons Guardian/Icons/Filled/CoverUp_2") as Sprite;
                get_skill_name.text = "일격필살";
                get_skill_explain.text = "근력+75 클릭골드 4배";//공격력 10배
                goToPanel.Instance.get_skill_panel.SetActive(true);
                DataController.Instance.attack += 75;
                DataController.Instance.MultiplyGoldPerClick(4);
                
            }
            
            if(DataController.Instance.attack >= 500 && PlayerPrefs.GetInt(skill_id[7]) == 0 && goToPanel.Instance.get_skill_panel.activeSelf == false)
            {
                PlayerPrefs.SetInt(skill_id[7], 1);
                //스킬얻음페널 Text 바꾸기
                get_skill_text.text = "근력 500달성 스킬 흭득";
                get_skill_image.sprite = Resources.Load<Sprite>("Image/Skill/Skill icons Guardian/Icons/Filled/CoverUp_2") as Sprite;
                get_skill_name.text = "검의달인";
                get_skill_explain.text = "체력+100 근력+250, 민첩+100, 마나+50, 클릭골드 5배";//공격력 20배
                goToPanel.Instance.get_skill_panel.SetActive(true);
                DataController.Instance.health += 100;
                DataController.Instance.attack += 250;
                DataController.Instance.mana += 100;
                DataController.Instance.special += 50;
                DataController.Instance.MultiplyGoldPerClick(5);
                
            }
            
            if(DataController.Instance.mana >= 50 && PlayerPrefs.GetInt(skill_id[8]) == 0 && goToPanel.Instance.get_skill_panel.activeSelf == false)
            {
                PlayerPrefs.SetInt(skill_id[8], 1);
                //스킬얻음페널 Text 바꾸기
                get_skill_text.text = "민첩 50달성 스킬 흭득";
                get_skill_image.sprite = Resources.Load<Sprite>("Image/Skill/Skill icons Guardian/Icons/Filled/CoverUp_2") as Sprite;
                get_skill_name.text = "암살의 길";
                get_skill_explain.text = "근력+5, 민첩+20, 마나+10, 클릭골드 2배";
                goToPanel.Instance.get_skill_panel.SetActive(true);
                DataController.Instance.attack += 5;
                DataController.Instance.mana += 20;
                DataController.Instance.special += 10;
                DataController.Instance.MultiplyGoldPerClick(2);
            }

            if(DataController.Instance.mana >= 100 && PlayerPrefs.GetInt(skill_id[9]) == 0 && goToPanel.Instance.get_skill_panel.activeSelf == false)
            {
                PlayerPrefs.SetInt(skill_id[9], 1);
                //스킬얻음페널 Text 바꾸기
                get_skill_text.text = "민첩 100달성 스킬 흭득";
                get_skill_image.sprite = Resources.Load<Sprite>("Image/Skill/Skill icons Guardian/Icons/Filled/CoverUp_2") as Sprite;
                get_skill_name.text = "숙련된 암살자";
                get_skill_explain.text = "민첩+50, 마나+30, 클릭골드 3배";
                goToPanel.Instance.get_skill_panel.SetActive(true);
                DataController.Instance.mana += 50;
                DataController.Instance.special += 30;
                DataController.Instance.MultiplyGoldPerClick(3);
            }
            if(DataController.Instance.mana >= 300 && PlayerPrefs.GetInt(skill_id[10]) == 0 && goToPanel.Instance.get_skill_panel.activeSelf == false)
            {
                PlayerPrefs.SetInt(skill_id[10], 1);
                //스킬얻음페널 Text 바꾸기
                get_skill_text.text = "민첩 300달성 스킬 흭득";
                get_skill_image.sprite = Resources.Load<Sprite>("Image/Skill/Skill icons Guardian/Icons/Filled/CoverUp_2") as Sprite;
                get_skill_name.text = "돈이 필요해";
                get_skill_explain.text = "민첩+75 클릭골드 25배";//크리티컬확률 6%
                goToPanel.Instance.get_skill_panel.SetActive(true);
                DataController.Instance.mana += 50;
                DataController.Instance.special += 30;
                DataController.Instance.MultiplyGoldPerClick(25);
            }
            if(DataController.Instance.mana >= 500 && PlayerPrefs.GetInt(skill_id[11]) == 0 && goToPanel.Instance.get_skill_panel.activeSelf == false)
            {
                PlayerPrefs.SetInt(skill_id[11], 1);
                //스킬얻음페널 Text 바꾸기
                get_skill_text.text = "민첩 500달성 스킬 흭득";
                get_skill_image.sprite = Resources.Load<Sprite>("Image/Skill/Skill icons Guardian/Icons/Filled/CoverUp_2") as Sprite;
                get_skill_name.text = "동화";
                get_skill_explain.text = "주변에 사물에 동화되어 눈에 띄지 않습니다.\n체력+50, 근력+100, 민첩+200, 마나+100, 클릭골드 5배";//크리티컬확률 9% 회피율 5%
                goToPanel.Instance.get_skill_panel.SetActive(true);
                DataController.Instance.health += 50;
                DataController.Instance.attack += 100;
                DataController.Instance.mana += 200;
                DataController.Instance.special += 100;
                DataController.Instance.MultiplyGoldPerClick(5);
            }
            if(DataController.Instance.special >= 50 && PlayerPrefs.GetInt(skill_id[12]) == 0 && goToPanel.Instance.get_skill_panel.activeSelf == false)
            {
                PlayerPrefs.SetInt(skill_id[12], 1);
                //스킬얻음페널 Text 바꾸기
                get_skill_text.text = "마나 50달성 스킬 흭득";
                get_skill_image.sprite = Resources.Load<Sprite>("Image/Skill/Skill icons Guardian/Icons/Filled/CoverUp_2") as Sprite;
                get_skill_name.text = "마나의 존재";
                get_skill_explain.text = "마나를 느낄수 있습니다.\n체력+5, 민첩+10, 마나+20, 클릭골드 2배";
                goToPanel.Instance.get_skill_panel.SetActive(true);
                DataController.Instance.health += 5;
                DataController.Instance.mana += 10;
                DataController.Instance.special += 20;
                DataController.Instance.MultiplyGoldPerClick(2);
            }
            if(DataController.Instance.special >= 100 && PlayerPrefs.GetInt(skill_id[13]) == 0 && goToPanel.Instance.get_skill_panel.activeSelf == false)
            {
                PlayerPrefs.SetInt(skill_id[13], 1);
                //스킬얻음페널 Text 바꾸기
                get_skill_text.text = "마나 100달성 스킬 흭득";
                get_skill_image.sprite = Resources.Load<Sprite>("Image/Skill/Skill icons Guardian/Icons/Filled/CoverUp_2") as Sprite;
                get_skill_name.text = "마나의 정수";
                get_skill_explain.text = "마나의 정수를 보았습니다. 좀 더 발전했군요.\n민첩+30, 마나+50, 클릭골드 3배";
                goToPanel.Instance.get_skill_panel.SetActive(true);
                DataController.Instance.mana += 30;
                DataController.Instance.special += 50;
                DataController.Instance.MultiplyGoldPerClick(3);
            }
            if(DataController.Instance.special >= 300 && PlayerPrefs.GetInt(skill_id[14]) == 0 && goToPanel.Instance.get_skill_panel.activeSelf == false)
            {
                PlayerPrefs.SetInt(skill_id[14], 1);
                //스킬얻음페널 Text 바꾸기
                get_skill_text.text = "마나 300달성 스킬 흭득";
                get_skill_image.sprite = Resources.Load<Sprite>("Image/Skill/Skill icons Guardian/Icons/Filled/CoverUp_2") as Sprite;
                get_skill_name.text = "마법진 그리기";
                get_skill_explain.text = "마법진을 그릴수 있습니다.\n마나+75, 클릭골드 4배";//연금술 성공확률 5%증가
                goToPanel.Instance.get_skill_panel.SetActive(true);
                DataController.Instance.special += 70;
                DataController.Instance.MultiplyGoldPerClick(4);
            }
            if(DataController.Instance.special >= 500 && PlayerPrefs.GetInt(skill_id[14]) == 0 && goToPanel.Instance.get_skill_panel.activeSelf == false)
            {
                PlayerPrefs.SetInt(skill_id[15], 1);
                //스킬얻음페널 Text 바꾸기
                get_skill_text.text = "마나 500달성 스킬 흭득";
                get_skill_image.sprite = Resources.Load<Sprite>("Image/Skill/Skill icons Guardian/Icons/Filled/CoverUp_2") as Sprite;
                get_skill_name.text = "영감";
                get_skill_explain.text = "깨달음을 얻었습니다.\n이는 당신을 더 높은 곳으로 이끌어줍니다.\n체력+100, 근력+50, 민첩+ 100, 마나+200, 클릭골드 5배";//연금술 성공확률 5%증가
                goToPanel.Instance.get_skill_panel.SetActive(true);
                DataController.Instance.health += 50;
                DataController.Instance.attack += 100;
                DataController.Instance.mana += 100;
                DataController.Instance.special += 200;
                DataController.Instance.MultiplyGoldPerClick(5);
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

    

    public void get_expp()
    {
        DataController.Instance.current_exp += 10;
    }


    public void select_skill0()
    {
        skill_explain.text = "0번유물\n권능\n치명타 확률 100%증가";
    }

    public void select_skill1()
    {
        //hide_select_tool();
       // SelectFrame1.SetActive(true);
        skill_explain.text = "무한의 대검\n권능\n치명타 확률 100%증가";
    }
    public void select_skill2()
    {
        skill_explain.text = "홍옥의 오브\n권능\n클릭 당 흭득 골드량 2배";
    }
    public void select_skill3()
    {
        skill_explain.text = "보라색 오브\n권능\n초당 골드 흭득량 10배";
    }
    public void select_skill4()
    {
        skill_explain.text = "황금 방사능병\n권능\n초당 골드 흭득량 10배";
    }
    public void select_skill5()
    {
        skill_explain.text = "마기가 담긴 병\n권능\n초당 골드 흭득량 10배";
    }

    public void select_skill6()
    {
        skill_explain.text = "화염의 병\n권능\n초당 골드 흭득량 10배";
    }

    public void select_skill7()
    {
        skill_explain.text = "얼음의 병\n권능\n초당 골드 흭득량 10배";
    }

    public void select_skill8()
    {
        skill_explain.text = "죽음의 병\n권능\n초당 골드 흭득량 10배";
    }

    public void select_skill9()
    {
        skill_explain.text = "슈퍼 망원경\n권능\n초당 골드 흭득량 10배";
    }

    public void select_skill10()
    {
        skill_explain.text = "구원\n권능\n초당 골드 흭득량 10배";
    }

    public void select_skill11()
    {
        skill_explain.text = "마나 스톤\n권능\n초당 골드 흭득량 10배";
    }
    public void select_skill12()
    {
        skill_explain.text = "풍요의 북\n권능\n초당 골드 흭득량 10배";
    }
    public void MyPosition (Transform transform)
    {
        var tra = transform;
        Debug.Log("Clicked button pos:" + tra.position);
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
        /*
        PlayerPrefs.DeleteKey("Health");
        PlayerPrefs.DeleteKey("attack");
        PlayerPrefs.DeleteKey("mana");
        PlayerPrefs.DeleteKey("special");
        PlayerPrefs.DeleteKey("Freestate");
        */
        DataController.Instance.health = 1;
        DataController.Instance.attack = 1;
        DataController.Instance.mana = 1;
        DataController.Instance.special = 1;
        DataController.Instance.freestate = 5;
        for(int i = 0; i <= skill_size; i ++)
        {
            PlayerPrefs.SetInt(skill_id[i], 0);
        }

    }

    

}
