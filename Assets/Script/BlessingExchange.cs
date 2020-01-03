using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlessingExchange : MonoBehaviour
{
    int current_select_blessing;
    public Text blessing_explanation;
    public Text booty_name;
    public Text require_booty_text;
    public Text current_have_booty;
    public GameObject right_panel;

    public float[] blessing_0_to_3_ratio = {0f, 0.4f, 0.8f, 1.2f, 1.6f, 2f, 2.4f, 2.8f, 3.2f, 3.6f, 4f, 4f};
    public float[] blessing_attackSpeed_ratio = {1.0f, 1.2f, 1.4f, 1.6f, 1.8f, 2.0f, 2.2f, 2.4f, 2.6f, 2.8f, 3.0f, 3.0f};       //11개 + 1
    public float[] blessing_gold_cost_ratio = {1.0f, 0.95f, 0.90f, 0.85f, 0.80f, 0.75f, 0.70f, 0.65f, 0.60f, 0.55f, 0.50f, 0.50f};
    public float[] blessing_consumEffect_ratio = {1.0f, 1.2f, 1.4f, 1.6f, 1.8f, 2.0f, 2.2f, 2.2f, 2.4f, 2.6f, 2.8f, 3.0f, 3.0f};
    public int[] blessing_get_booty_ratio = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 11};

    float before_ratio;
    float after_ratio;

    int item_num;

    int current_booty;
    int current_bls_Lv;

    private static BlessingExchange instance;

    public static BlessingExchange Instance
    {
        get{
            if(instance == null)
            {
                instance = FindObjectOfType<BlessingExchange>();

                if(instance == null)
                {
                    GameObject container = new GameObject("BlessingExchange");

                    instance = container.AddComponent<BlessingExchange>();
                }
            }
            return instance;
        }
    }

    

    public void purchase_blessing()
    {
        if(PlayerPrefs.GetInt("bls_" + current_select_blessing) == 0 && PlayerPrefs.GetInt("bls_" + current_select_blessing) != 10) //가호의 레벨이 0일 떄
        {
            if(PlayerPrefs.GetInt("booty_" + 1) >= 5)  //부산물 확인
            {                                                      //부산물 처리, 가호 레벨 상승, 가호 효과 적용(?) => char탭에서
                current_booty = PlayerPrefs.GetInt("booty_" + 1);
                current_bls_Lv = PlayerPrefs.GetInt("bls_" + current_select_blessing);
                PlayerPrefs.SetInt("bls_" + current_select_blessing, current_bls_Lv+1);
                PlayerPrefs.SetInt("booty_" + 1, current_booty - 5); 
                SoundManager.Instance.upgrade_button_sound();
            } else {
                goToPanel.Instance.show_noticePanel();
                goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "자원이 부족합니다.";
            }
        }
        else if(PlayerPrefs.GetInt("bls_" + current_select_blessing) == 1 && PlayerPrefs.GetInt("bls_" + current_select_blessing) != 10) //가호의 레벨이 1일 떄
        {
            if(PlayerPrefs.GetInt("booty_" + 1) >= 10)  //부산물 확인
            {
                current_booty = PlayerPrefs.GetInt("booty_" + 1);
                current_bls_Lv = PlayerPrefs.GetInt("bls_" + current_select_blessing);
                PlayerPrefs.SetInt("bls_" + current_select_blessing, current_bls_Lv+1);
                PlayerPrefs.SetInt("booty_" + 1, current_booty - 10); 
                SoundManager.Instance.upgrade_button_sound();
            } else {
                goToPanel.Instance.show_noticePanel();
                goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "자원이 부족합니다.";
            }
        }
        else if(PlayerPrefs.GetInt("bls_" + current_select_blessing) == 2 && PlayerPrefs.GetInt("bls_" + current_select_blessing) != 10) //가호의 레벨이 1일 떄
        {
            if(PlayerPrefs.GetInt("booty_" + 2) >= 15)  //부산물 확인
            {
                current_booty = PlayerPrefs.GetInt("booty_" + 2);
                current_bls_Lv = PlayerPrefs.GetInt("bls_" + current_select_blessing);
                PlayerPrefs.SetInt("bls_" + current_select_blessing, current_bls_Lv+1);
                PlayerPrefs.SetInt("booty_" + 2, current_booty - 15); 
                SoundManager.Instance.upgrade_button_sound();
            } else {
                goToPanel.Instance.show_noticePanel();
                goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "자원이 부족합니다.";
            }
        }
        else if(PlayerPrefs.GetInt("bls_" + current_select_blessing) == 3 && PlayerPrefs.GetInt("bls_" + current_select_blessing) != 10) //가호의 레벨이 1일 떄
        {
            if(PlayerPrefs.GetInt("booty_" + 2) >= 20)  //부산물 확인
            {
                current_booty = PlayerPrefs.GetInt("booty_" + 2);
                current_bls_Lv = PlayerPrefs.GetInt("bls_" + current_select_blessing);
                PlayerPrefs.SetInt("bls_" + current_select_blessing, current_bls_Lv+1);
                PlayerPrefs.SetInt("booty_" + 2, current_booty - 20); 
                SoundManager.Instance.upgrade_button_sound();
            } else {
                goToPanel.Instance.show_noticePanel();
                goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "자원이 부족합니다.";
            }
        }
        else if(PlayerPrefs.GetInt("bls_" + current_select_blessing) == 4 && PlayerPrefs.GetInt("bls_" + current_select_blessing) != 10) //가호의 레벨이 1일 떄
        {
            if(PlayerPrefs.GetInt("booty_" + 3) >= 25)  //부산물 확인
            {
                current_booty = PlayerPrefs.GetInt("booty_" + 3);
                current_bls_Lv = PlayerPrefs.GetInt("bls_" + current_select_blessing);
                PlayerPrefs.SetInt("bls_" + current_select_blessing, current_bls_Lv+1);
                PlayerPrefs.SetInt("booty_" + 3, current_booty - 25); 
                SoundManager.Instance.upgrade_button_sound();
            } else {
                goToPanel.Instance.show_noticePanel();
                goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "자원이 부족합니다.";
            }
        }
        else if(PlayerPrefs.GetInt("bls_" + current_select_blessing) == 5 && PlayerPrefs.GetInt("bls_" + current_select_blessing) != 10) //가호의 레벨이 1일 떄
        {
            if(PlayerPrefs.GetInt("booty_" +3) >= 30)  //부산물 확인
            {
                current_booty = PlayerPrefs.GetInt("booty_" + 3);
                current_bls_Lv = PlayerPrefs.GetInt("bls_" + current_select_blessing);
                PlayerPrefs.SetInt("bls_" + current_select_blessing, current_bls_Lv+1);
                PlayerPrefs.SetInt("booty_" + 3, current_booty - 30); 
                SoundManager.Instance.upgrade_button_sound();
            } else {
                goToPanel.Instance.show_noticePanel();
                goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "자원이 부족합니다.";
            }
        }
        else if(PlayerPrefs.GetInt("bls_" + current_select_blessing) == 6 && PlayerPrefs.GetInt("bls_" + current_select_blessing) != 10) //가호의 레벨이 6일 떄
        {
            if(PlayerPrefs.GetInt("booty_" + 4) >= 35)  //부산물 확인
            {
                current_booty = PlayerPrefs.GetInt("booty_" + 4);
                current_bls_Lv = PlayerPrefs.GetInt("bls_" + current_select_blessing);
                PlayerPrefs.SetInt("bls_" + current_select_blessing, current_bls_Lv+1);
                PlayerPrefs.SetInt("booty_" + 4, current_booty - 35); 
                SoundManager.Instance.upgrade_button_sound();
            } else {
                goToPanel.Instance.show_noticePanel();
                goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "자원이 부족합니다.";
            }
        }
        else if(PlayerPrefs.GetInt("bls_" + current_select_blessing) == 7 && PlayerPrefs.GetInt("bls_" + current_select_blessing) != 10) //가호의 레벨이 7일 떄
        {
            if(PlayerPrefs.GetInt("booty_" +4) >= 40)  //부산물 확인
            {
                current_booty = PlayerPrefs.GetInt("booty_" + 4);
                current_bls_Lv = PlayerPrefs.GetInt("bls_" + current_select_blessing);
                PlayerPrefs.SetInt("bls_" + current_select_blessing, current_bls_Lv+1);
                PlayerPrefs.SetInt("booty_" + 4, current_booty - 40); 
                SoundManager.Instance.upgrade_button_sound();
            } else {
                goToPanel.Instance.show_noticePanel();
                goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "자원이 부족합니다.";
            }
        }
        else if(PlayerPrefs.GetInt("bls_" + current_select_blessing) == 8 && PlayerPrefs.GetInt("bls_" + current_select_blessing) != 10) //가호의 레벨이 7일 떄
        {
            if(PlayerPrefs.GetInt("booty_" +5) >= 45)  //부산물 확인
            {
                current_booty = PlayerPrefs.GetInt("booty_" + 5);
                current_bls_Lv = PlayerPrefs.GetInt("bls_" + current_select_blessing);
                PlayerPrefs.SetInt("bls_" + current_select_blessing, current_bls_Lv+1);
                PlayerPrefs.SetInt("booty_" + 5, current_booty - 45); 
                SoundManager.Instance.upgrade_button_sound();
            } else {
                goToPanel.Instance.show_noticePanel();
                goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "자원이 부족합니다.";
            }
        }
        else if(PlayerPrefs.GetInt("bls_" + current_select_blessing) == 9 && PlayerPrefs.GetInt("bls_" + current_select_blessing) != 10) //가호의 레벨이 7일 떄
        {
            if(PlayerPrefs.GetInt("booty_" +5) >= 50)  //부산물 확인
            {
                current_booty = PlayerPrefs.GetInt("booty_" + 5);
                current_bls_Lv = PlayerPrefs.GetInt("bls_" + current_select_blessing);
                PlayerPrefs.SetInt("bls_" + current_select_blessing, current_bls_Lv+1);
                PlayerPrefs.SetInt("booty_" + 5, current_booty - 50); 
                SoundManager.Instance.upgrade_button_sound();
            } else {
                goToPanel.Instance.show_noticePanel();
                goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "자원이 부족합니다.";
            }
        }
        else 
        {
            goToPanel.Instance.show_noticePanel();
            goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "최고레벨 입니다.";
        }
        UpgradeButton.Instance.UpdateUpgrade();   //ui에 바로 비용을 표시하기 위해 바로 db에 저장 set함
        show_require_booty(PlayerPrefs.GetInt("bls_" + current_select_blessing));
        if(current_select_blessing == 0)
        { select_blessing_0(); }
        else if(current_select_blessing == 1)
        { select_blessing_1(); }
        else if(current_select_blessing == 2)
        { select_blessing_2(); }
        else if(current_select_blessing == 3)
        { select_blessing_3(); }
        else if(current_select_blessing == 4)
        { select_blessing_4(); }
        else if(current_select_blessing == 5)
        { select_blessing_5(); }
        else if(current_select_blessing == 6)
        { select_blessing_6(); }
        else if(current_select_blessing == 7)
        { select_blessing_7(); }
    }

    public void show_require_booty(int lv)
    {
        current_have_booty.text = PlayerPrefs.GetInt("booty_" + 1) +"\n" +
                                    PlayerPrefs.GetInt("booty_" + 2) +"\n" +
                                    PlayerPrefs.GetInt("booty_" + 3) +"\n" +
                                    PlayerPrefs.GetInt("booty_" + 4) +"\n" +
                                    PlayerPrefs.GetInt("booty_" + 5) +"\n";
        if(lv == 0)
        {
            booty_name.text = "납 주괴";
            if(PlayerPrefs.GetInt("booty_" + 1) >= 5)
            {
                require_booty_text.text ="<color=#008000>" + PlayerPrefs.GetInt("booty_" + 1) + " / 5" + "</Color>";
            } else {
                require_booty_text.text ="<color=#ff0000>" + PlayerPrefs.GetInt("booty_" + 1) + " / 5" + "</Color>";
            }  
        }
        else if(lv == 1)
        {   booty_name.text = "납 주괴";
            if(PlayerPrefs.GetInt("booty_" + 1) >= 10)
            {
                require_booty_text.text ="<color=#008000>" + PlayerPrefs.GetInt("booty_" + 1) + " / 10" + "</Color>";
            } else {
                require_booty_text.text ="<color=#ff0000>" + PlayerPrefs.GetInt("booty_" + 1) + " / 10" + "</Color>";
            }  
        }
        else if(lv == 2)
        {   booty_name.text = "철 주괴";
            if(PlayerPrefs.GetInt("booty_" + 2) >= 15)
            {
                require_booty_text.text ="<color=#008000>" + PlayerPrefs.GetInt("booty_" + 2) + " / 15" + "</Color>";
            } else {
                require_booty_text.text ="<color=#ff0000>" + PlayerPrefs.GetInt("booty_" + 2) + " / 15" + "</Color>";
            }  
        }
        else if(lv == 3)
        {   booty_name.text = "철 주괴";
            if(PlayerPrefs.GetInt("booty_" + 2) >= 20)
            {
                require_booty_text.text ="<color=#008000>" + PlayerPrefs.GetInt("booty_" + 2) + " / 20" + "</Color>";
            } else {
                require_booty_text.text ="<color=#ff0000>" + PlayerPrefs.GetInt("booty_" + 2) + " / 20" + "</Color>";
            }  
        }
        else if(lv == 4)
        {   booty_name.text = "금 주괴";
            if(PlayerPrefs.GetInt("booty_" + 3) >= 25)
            {
                require_booty_text.text ="<color=#008000>" + PlayerPrefs.GetInt("booty_" + 3) + " / 25" + "</Color>";
            } else {
                require_booty_text.text ="<color=#ff0000>" + PlayerPrefs.GetInt("booty_" + 3) + " / 25" + "</Color>";
            }  
        }
        else if(lv == 5)
        {   booty_name.text = "금 주괴";
            if(PlayerPrefs.GetInt("booty_" + 3) >= 30)
            {
                require_booty_text.text ="<color=#008000>" + PlayerPrefs.GetInt("booty_" + 3) + " / 30" + "</Color>";
            } else {
                require_booty_text.text ="<color=#ff0000>" + PlayerPrefs.GetInt("booty_" + 3) + " / 30" + "</Color>";
            }  
        }
        else if(lv == 6)
        {   booty_name.text = "다이아몬드";
            if(PlayerPrefs.GetInt("booty_" + 4) >= 35)
            {
                require_booty_text.text ="<color=#008000>" + PlayerPrefs.GetInt("booty_" + 4) + " / 35" + "</Color>";
            } else {
                require_booty_text.text ="<color=#ff0000>" + PlayerPrefs.GetInt("booty_" + 4) + " / 35" + "</Color>";
            }  
        }
        else if(lv == 7)
        {   booty_name.text = "다이아몬드";
            if(PlayerPrefs.GetInt("booty_" + 4) >= 40)
            {
                require_booty_text.text ="<color=#008000>" + PlayerPrefs.GetInt("booty_" + 4) + " / 40" + "</Color>";
            } else {
                require_booty_text.text ="<color=#ff0000>" + PlayerPrefs.GetInt("booty_" + 4) + " / 40" + "</Color>";
            }  
        }
        else if(lv == 8)
        {   booty_name.text = "오리하르콘";
            if(PlayerPrefs.GetInt("booty_" + 5) >= 45)
            {
                require_booty_text.text ="<color=#008000>" + PlayerPrefs.GetInt("booty_" + 5) + " / 45" + "</Color>";
            } else {
                require_booty_text.text ="<color=#ff0000>" + PlayerPrefs.GetInt("booty_" + 5) + " / 45" + "</Color>";
            }  
        }
        else if(lv == 9)
        {   booty_name.text = "오리하르콘";
            if(PlayerPrefs.GetInt("booty_" + 5) >= 50)
            {
                require_booty_text.text ="<color=#008000>" + PlayerPrefs.GetInt("booty_" + 5) + " / 50" + "</Color>";
            } else {
                require_booty_text.text ="<color=#ff0000>" + PlayerPrefs.GetInt("booty_" + 5) + " / 50" + "</Color>";
            }  
        }
        else {
            booty_name.text = "<color=#ff0000>" + "강화 불가능" + "</Color>";
            require_booty_text.text ="";

        }
    }

    public void get_booty_super()
    {
        for(int i = 1; i < 6; i++)
        {
            PlayerPrefs.SetInt("booty_" + i, 100);
        }
    }

    public void init_booty_blessing()
    {
        for(int i = 1; i < 6; i++)
        {
            PlayerPrefs.SetInt("booty_" + i, 0);
        }
        
        for(int i = 0; i < 8; i++)
        {
            PlayerPrefs.SetInt("bls_" + i, 0);
        }
        UpgradeButton.Instance.UpdateUpgrade();
    }

    public void select_blessing_0()  //체력
    {
        right_panel.SetActive(true);
        for(int i = 0; i <= PlayerPrefs.GetInt("bls_0"); i++)
        {
            before_ratio = blessing_0_to_3_ratio[i];
            after_ratio = blessing_0_to_3_ratio[i+1];
        }
        blessing_explanation.text = "자연의 섭리 LV." + PlayerPrefs.GetInt("bls_0") +"\n체력 스탯 배율 강화\n" + "(" + before_ratio + " -> " + after_ratio + ")";
        if(PlayerPrefs.GetInt("bls_0") == 10) { blessing_explanation.text = "자연의 섭리 LV." + PlayerPrefs.GetInt("bls_0") +"\n체력 스탯 배율 강화\n" + "(" + after_ratio + ")"; }
        current_select_blessing = 0;
        show_require_booty(PlayerPrefs.GetInt("bls_0"));
    }

    public void select_blessing_1()  //체력
    {
        right_panel.SetActive(true);
        for(int i = 0; i <= PlayerPrefs.GetInt("bls_1"); i++)
        {
            before_ratio = blessing_0_to_3_ratio[i];
            after_ratio = blessing_0_to_3_ratio[i+1];
        }
        blessing_explanation.text = "근력 강화 LV." + PlayerPrefs.GetInt("bls_1") +"\n공격력 스탯 배율 강화\n" + "(" + before_ratio + " -> " + after_ratio + ")";
        if(PlayerPrefs.GetInt("bls_1") == 10) { blessing_explanation.text = "근력 강화 LV." + PlayerPrefs.GetInt("bls_1") +"\n공격력 스탯 배율 강화\n" + "("+ after_ratio + ")"; }
        current_select_blessing = 1;
        show_require_booty(PlayerPrefs.GetInt("bls_1"));
    }

    public void select_blessing_2()  //체력
    {
        right_panel.SetActive(true);
        for(int i = 0; i <= PlayerPrefs.GetInt("bls_2"); i++)
        {
            before_ratio = blessing_0_to_3_ratio[i];
            after_ratio = blessing_0_to_3_ratio[i+1];
        }
        blessing_explanation.text = "바람의 가호 LV." + PlayerPrefs.GetInt("bls_2") +"\n민첩 스탯 배율 강화\n" + "(" + before_ratio + " -> " + after_ratio + ")";
        if(PlayerPrefs.GetInt("bls_2") == 10) { blessing_explanation.text = "바람의 가호 LV." + PlayerPrefs.GetInt("bls_2") +"\n민첩 스탯 배율 강화\n" + "(" + after_ratio + ")"; }
        current_select_blessing = 2;
        show_require_booty(PlayerPrefs.GetInt("bls_2"));
    }

    public void select_blessing_3()  //체력
    {
        right_panel.SetActive(true);
        for(int i = 0; i <= PlayerPrefs.GetInt("bls_3"); i++)
        {
            before_ratio = blessing_0_to_3_ratio[i];
            after_ratio = blessing_0_to_3_ratio[i+1];
        }
        blessing_explanation.text = "우주적 지혜 LV." + PlayerPrefs.GetInt("bls_3") +"\n스킬 공격력 스탯 배율 강화\n" + "(" + before_ratio + " -> " + after_ratio + ")";
        if(PlayerPrefs.GetInt("bls_3") == 10) { blessing_explanation.text = "우주적 지혜 LV." + PlayerPrefs.GetInt("bls_3") +"\n스킬 공격력 스탯 배율 강화\n" + "(" + after_ratio + ")"; }
        current_select_blessing = 3;
        show_require_booty(PlayerPrefs.GetInt("bls_3"));
    }

    public void select_blessing_4()  //체력
    {
        right_panel.SetActive(true);
        for(int i = 0; i <= PlayerPrefs.GetInt("bls_4"); i++)
        {
            before_ratio = blessing_attackSpeed_ratio[i];
            after_ratio = blessing_attackSpeed_ratio[i+1];
        }
        blessing_explanation.text = "공격속도 강화 LV." + PlayerPrefs.GetInt("bls_4") +"\n공격속도 배율 강화\n" + "(" + before_ratio + " -> " + after_ratio + ")";
        if(PlayerPrefs.GetInt("bls_4") == 10) { blessing_explanation.text = "공격속도 강화 LV." + PlayerPrefs.GetInt("bls_4") +"\n공격속도 배율 강화\n" + "(" + after_ratio + ")"; }
        current_select_blessing = 4;
        show_require_booty(PlayerPrefs.GetInt("bls_4"));
    }

    public void select_blessing_5()  //체력
    {
        right_panel.SetActive(true);
        for(int i = 0; i <= PlayerPrefs.GetInt("bls_5"); i++)
        {
            before_ratio = blessing_gold_cost_ratio[i];
            after_ratio = blessing_gold_cost_ratio[i+1];
        }
        blessing_explanation.text = "금전적 가치 LV." + PlayerPrefs.GetInt("bls_5") +"\n골드강화비용 감소\n" + "(" + before_ratio + "%" + " -> " + after_ratio + "%" + ")";
        if(PlayerPrefs.GetInt("bls_5") == 10) { blessing_explanation.text = "금전적 가치 LV." + PlayerPrefs.GetInt("bls_5") +"\n골드강화비용 감소\n" + "(" + after_ratio + "%" + ")"; }
        current_select_blessing = 5;
        show_require_booty(PlayerPrefs.GetInt("bls_5"));
    }

    public void select_blessing_6()  //체력
    {
        right_panel.SetActive(true);
        for(int i = 0; i <= PlayerPrefs.GetInt("bls_6"); i++)
        {
            before_ratio = blessing_consumEffect_ratio[i];
            after_ratio = blessing_consumEffect_ratio[i+1];
        }
        blessing_explanation.text = "최적의 알고리즘 LV." + PlayerPrefs.GetInt("bls_6") +"\n소모품 효과 증가\n" + "(" + before_ratio + "배" + " -> " + after_ratio + "배" + ")";
        if(PlayerPrefs.GetInt("bls_6") == 10) { blessing_explanation.text = "최적의 알고리즘 LV." + PlayerPrefs.GetInt("bls_6") +"\n소모품 효과 증가\n" + "(" + after_ratio + "배" + ")";}
        current_select_blessing = 6;
        show_require_booty(PlayerPrefs.GetInt("bls_6"));
    }

    public void select_blessing_7()  //체력
    {
        right_panel.SetActive(true);
        for(int i = 0; i <= PlayerPrefs.GetInt("bls_7"); i++)
        {
            before_ratio = blessing_get_booty_ratio[i];
            after_ratio = blessing_get_booty_ratio[i+1];
        }
        blessing_explanation.text = "행운의 빛 LV." + PlayerPrefs.GetInt("bls_7") +"\n흭득가능 광석개수 최대치 증가\n" + "(" + before_ratio + "개" + " -> " + after_ratio +  "개" + ")";
        if(PlayerPrefs.GetInt("bls_7") == 10) { blessing_explanation.text = "행운의 빛 LV." + PlayerPrefs.GetInt("bls_7") +"\n흭득가능 광석개수 최대치 증가\n" + "(" + after_ratio + "개" +")"; }
        current_select_blessing = 7;
        show_require_booty(PlayerPrefs.GetInt("bls_7"));
    }

}
