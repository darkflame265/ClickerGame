using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goToPanel : MonoBehaviour
{
    private static goToPanel instance;

    public static goToPanel Instance
    {
        get{
            if(instance == null)
            {
                instance = FindObjectOfType<goToPanel>();

                if(instance == null)
                {
                    GameObject container = new GameObject("goToPanel");

                    instance = container.AddComponent<goToPanel>();
                }
            }
            return instance;
        }
    }

    public static void SetBool(string name, bool booleanValue)
    {
        PlayerPrefs.SetInt(name, booleanValue ? 1 : 0);
    }

    public Transform Canvas;

    //메인메뉴창
    public GameObject charPanel;
    public GameObject artifactPanel;
    public GameObject powerPanel;
    public GameObject townPanel;
    public GameObject pvpPanel;
    public GameObject duengeonPanel;

    public GameObject charPanelButton;
    public GameObject artifactPanelButton;
    public GameObject powerPanelButton;
    public GameObject townPanelButton;
    public GameObject pvpPanelButton;
    public GameObject duengeonPanelButton;

    Color transparentColor = new Color(1,1,1,0.5f);
    Color cleanColor = new Color(1,1,1,1);


    public GameObject black_smith_panel;
    public GameObject general_store;
    public GameObject magic_tower;
    public GameObject bar_panel;
    
    //??
    public GameObject char_state_panel;
    public GameObject get_skill_panel;
    public GameObject get_artifact_panel;
    public GameObject reincarnation_panel;

    public GameObject NoticePanel;
    public GameObject exp_percent_panel;
    public GameObject gold_amount_panel;
    public GameObject upgradeButton;

    //사이드 메뉴
    public GameObject option_panel;
    public GameObject store_panel;
    public GameObject quest_panel;
    public GameObject buyCash_panel;
    public GameObject gamble_panel;
    public GameObject challenge_panel;
    public GameObject inventory_panel;

    //상세 스테이터스
    public GameObject battle_text_panel;
    public GameObject buff_text_panel;

    //배틀 관련
    public BattleManager battleManager;
    public GameObject stage_explain_panel;
    public GameObject stage_reward_explain_panel;
    public Text show_current_stage;
    public Text Stage_explain;
    public GameObject BigNoticePanel;
    
    public GameObject camp_dispose_panel;

    public Scrollbar battle_scrollbarr;
    public Image chapter1_img;
    public GameObject[] chapter1_panel;
    public Image chapter2_img;
    public GameObject[] chapter2_panel;
    public Image chapter3_img;
    public GameObject[] chapter3_panel;

    public GameObject stage_1_panel;
    public GameObject stage_2_panel;
    public GameObject stage_3_panel;
    public GameObject stage_4_panel;
    public GameObject stage_5_panel;
    public GameObject stage_6_panel;
    public GameObject stage_7_panel;
    public GameObject stage_8_panel;
    public GameObject stage_9_panel;
    public GameObject stage_10_panel;
    public GameObject stage_11_panel;
    public GameObject stage_12_panel;
    public GameObject stage_13_panel;
    public GameObject stage_14_panel;
    public GameObject stage_15_panel;

    public GameObject battleScene_panel;
    public GameObject mapClone;
    public GameObject battle_pause_panel;
    public GameObject result_panel;

    public Sprite triangle_left_sprite;
    public Sprite triangle_right_sprite;


    bool IsPause;

    void Start()
    {
        IsPause = false;
    }
    
    public void hide_without_panel()
    {
        charPanel.SetActive(false);
        artifactPanel.SetActive(false);
        powerPanel.SetActive(false);
        townPanel.SetActive(false);
        pvpPanel.SetActive(false);
        duengeonPanel.SetActive(false);
        char_state_panel.SetActive(false);
        get_artifact_panel.SetActive(false);

        

        charPanelButton.GetComponent<Image>().color = transparentColor;
        artifactPanelButton.GetComponent<Image>().color = transparentColor;
        powerPanelButton.GetComponent<Image>().color = transparentColor;
        townPanelButton.GetComponent<Image>().color = transparentColor;
        pvpPanelButton.GetComponent<Image>().color = transparentColor;
        duengeonPanelButton.GetComponent<Image>().color = transparentColor;
    }

    public void show_all_button()
    {
        

        charPanelButton.GetComponent<Image>().color = cleanColor;
        artifactPanelButton.GetComponent<Image>().color = cleanColor;
        powerPanelButton.GetComponent<Image>().color = cleanColor;
        townPanelButton.GetComponent<Image>().color = cleanColor;
        pvpPanelButton.GetComponent<Image>().color = cleanColor;
        duengeonPanelButton.GetComponent<Image>().color = cleanColor;
    }
   
    public void visible_character_panel()
    {
         // 오브젝트의 차일드까지 변경함 굿굿^^
         SoundManager.Instance.click_button_sound();
          
        if(charPanel.activeSelf == false)
        {
            hide_without_panel();
            charPanel.SetActive(true);
            charPanelButton.GetComponent<Image>().color = cleanColor;
        }
        else if(charPanel.activeSelf == true)
        {
            show_all_button();
            charPanel.SetActive(false);
        }
        CharacterStateController.Instance.clear_panel();
        CharacterStateController.Instance.check_current_char();
        //charPanel.SetActive(!charPanel.active);
    }

    public void visible_artifact_panel()
    {
        SoundManager.Instance.click_button_sound();
        if(artifactPanel.activeSelf == false)
        {
            hide_without_panel();
            artifactPanel.SetActive(true);
            artifactPanelButton.GetComponent<Image>().color = cleanColor;
        }
        else if(artifactPanel.activeSelf == true)
        {
            show_all_button();
            artifactPanel.SetActive(false);
        }
        //artifactPanel.SetActive(!artifactPanel.active);
        
    }


    public void visible_power_panel()
    {
        SoundManager.Instance.click_button_sound();
        if(powerPanel.activeSelf == false)
        {
            hide_without_panel();
            powerPanel.SetActive(true);
            powerPanelButton.GetComponent<Image>().color = cleanColor;
        }
        else if(powerPanel.activeSelf == true)
        {
            show_all_button();
            powerPanel.SetActive(false);
        }
       //powerPanel.SetActive(!powerPanel.active); // 오브젝트의 차일드까지 변경함 굿굿^^
    }


    public void visible_town_panel()
    {
        SoundManager.Instance.click_button_sound();
        if(townPanel.activeSelf == false)
        {
            hide_without_panel();
            townPanel.SetActive(true);
            townPanelButton.GetComponent<Image>().color = cleanColor;
        }
        else if(townPanel.activeSelf == true)
        {
            show_all_button();
            townPanel.SetActive(false);
        }
        //townPanel.SetActive(!townPanel.active); // 오브젝트의 차일드까지 변경함 굿굿^^
    }


    public void visible_pvp_panel()
    {
        SoundManager.Instance.click_button_sound();
        if(pvpPanel.activeSelf == false)
        {
            hide_without_panel();
            pvpPanel.SetActive(true);
            pvpPanelButton.GetComponent<Image>().color = cleanColor;
            BlessingExchange.Instance.right_panel.SetActive(false);
        }
        else if(pvpPanel.activeSelf == true)
        {
            show_all_button();
            pvpPanel.SetActive(false);
        }
    }


    public void visible_duengeon_panel()
    {
        SoundManager.Instance.click_button_sound();
        if(duengeonPanel.activeSelf == false)
        {
            hide_without_panel();
            duengeonPanel.SetActive(true);
            duengeonPanelButton.GetComponent<Image>().color = cleanColor;
            DataController.Instance.current_stage = -77;
        }
        else if(duengeonPanel.activeSelf == true)
        {
            show_all_button();
            duengeonPanel.SetActive(false);
            DataController.Instance.current_stage = -77;
        }
    
    }

    public void show_char_state_panel()
    {
        char_state_panel.SetActive(true);
    }

    public void hide_char_state_panel()
    {
        char_state_panel.SetActive(false);
    }

    public void hide_get_skill_panel()
    {
        get_skill_panel.SetActive(false);
    }





    public void show_get_artifact_panel()
    {
        get_artifact_panel.SetActive(true);
        //EffectController.Instance.WWExplosion_c();
    }

    public void hide_get_artifact_panel()
    {
        get_artifact_panel.SetActive(false);
    }



    public void show_reincarnation_panel()
    {
        reincarnation_panel.SetActive(true);
        //EffectController.Instance.WWExplosion_c();
    }

    public void hide_reincarnation_panel()
    {
        reincarnation_panel.SetActive(false);
    }

    public void show_option_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        option_panel.SetActive(true);
    }

    public void hide_option_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        option_panel.SetActive(false);
    }

    public void show_store_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        store_panel.SetActive(true);
        QuestController.Instance.set_start_position();
    }

    public void hide_store_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        store_panel.SetActive(false);
    }

    public void show_quest_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        quest_panel.SetActive(true);
    }

    public void hide_quest_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        quest_panel.SetActive(false);
    }

    public void show_buyCash_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        buyCash_panel.SetActive(true);
    }

    public void hide_buyCash_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        buyCash_panel.SetActive(false);
    }

    public void show_gamble_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        gamble_panel.SetActive(true);
    }

    public void hide_gamble_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        gamble_panel.SetActive(false);
    }

    public void show_challenge_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        challenge_panel.SetActive(true);
        //ChallengeMissionSystem.Instance.set_chellenge_text();
        ChallengeMissionSystem.Instance.first_Text();
    }

    public void hide_challenge_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        challenge_panel.SetActive(false);
    }

    public void show_inventory_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        inventory_panel.SetActive(true);
    }

    public void hide_inventory_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        inventory_panel.SetActive(false);
    }

    public void show_camp_dispose_panel()
    {
        //SoundManager.Instance.play_fight_panel_btn_sound();
        camp_dispose_panel.SetActive(true);
    }

    public void hide_camp_dispose_panel()
    {
        //SoundManager.Instance.play_fight_panel_btn_sound();
        camp_dispose_panel.SetActive(false);
    }

    void return_position_dispose_panel()
    {
        camp_dispose_panel.transform.position = new Vector2(0, 0);
    }

    public void turn_on_and_off_dispose_panel()
    {
        camp_dispose_panel.transform.position = new Vector2(4000, 4000);
        camp_dispose_panel.SetActive(true);
        Invoke("hide_camp_dispose_panel", 1f);
        Invoke("return_position_dispose_panel", 1.2f);
    }

    public void show_exp_percent_panel()
    {
        exp_percent_panel.SetActive(!exp_percent_panel.active);
        gold_amount_panel.SetActive(!gold_amount_panel.active);
        upgradeButton.SetActive(!upgradeButton.active);
    }

    public void show_chapter1_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        for(int i = 0; i < chapter1_panel.Length; i++)
        {
            chapter1_panel[i].SetActive(!chapter1_panel[i].activeSelf);
        }
        if(chapter1_panel[0].activeSelf == false)
        {
            chapter1_img.sprite = triangle_right_sprite;
        } else {
            chapter1_img.sprite = triangle_left_sprite;
        }
        //battle_scrollbarr.GetComponent<Scrollbar>().value = 1f;
    }

    public void show_chapter2_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        for(int i = 0; i < chapter2_panel.Length; i++)
        {
            chapter2_panel[i].SetActive(!chapter2_panel[i].activeSelf);
        }
        if(chapter2_panel[0].activeSelf == false)
        {
            chapter2_img.sprite = triangle_right_sprite;
        } else {
            chapter2_img.sprite = triangle_left_sprite;
        }
        //battle_scrollbarr.GetComponent<Scrollbar>().value = 1f;
    }

    public void show_chapter3_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        for(int i = 0; i < chapter3_panel.Length; i++)
        {
            chapter3_panel[i].SetActive(!chapter3_panel[i].activeSelf);
        }
        if(chapter3_panel[0].activeSelf == false)
        {
            chapter3_img.sprite = triangle_right_sprite;
        } else {
            chapter3_img.sprite = triangle_left_sprite;
        }
        //battle_scrollbarr.GetComponent<Scrollbar>().value = 1f;
    }

    

    public void show_stage_1_panel()
    {
        stage_1_panel.SetActive(true);
        SoundManager.Instance.play_fight_panel_btn_sound();
        //stage_explain_panel.SetActive(true);
    }

    public void hide_stage_1_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_1_panel.SetActive(false);
        stage_explain_panel.SetActive(false);
        stage_reward_explain_panel.SetActive(false);
        show_current_stage.text = "";
        Stage_explain.text = "";
    }

    public void show_stage_2_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_2_panel.SetActive(true);
        //stage_explain_panel.SetActive(true);
    }

    public void hide_stage_2_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_2_panel.SetActive(false);
        stage_explain_panel.SetActive(false);
        stage_reward_explain_panel.SetActive(false);
        show_current_stage.text = "";
        Stage_explain.text = "";
    }

    public void show_stage_3_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_3_panel.SetActive(true);
        //stage_explain_panel.SetActive(true);
    }

    public void hide_stage_3_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_3_panel.SetActive(false);
        stage_explain_panel.SetActive(false);
        stage_reward_explain_panel.SetActive(false);
        show_current_stage.text = "";
        Stage_explain.text = "";
    }

    public void show_stage_4_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_4_panel.SetActive(true);
        //stage_explain_panel.SetActive(true);
    }

    public void hide_stage_4_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_4_panel.SetActive(false);
        stage_explain_panel.SetActive(false);
        stage_reward_explain_panel.SetActive(false);
        show_current_stage.text = "";
        Stage_explain.text = "";
    }

    public void show_stage_5_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_5_panel.SetActive(true);
        //stage_explain_panel.SetActive(true);
    }

    public void hide_stage_5_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_5_panel.SetActive(false);
        stage_explain_panel.SetActive(false);
        stage_reward_explain_panel.SetActive(false);
        show_current_stage.text = "";
        Stage_explain.text = "";
    }

    public void show_stage_6_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_6_panel.SetActive(true);
        //stage_explain_panel.SetActive(true);
    }
    public void hide_stage_6_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_6_panel.SetActive(false);
        stage_explain_panel.SetActive(false);
        stage_reward_explain_panel.SetActive(false);
        show_current_stage.text = "";
        Stage_explain.text = "";
    }

    public void show_stage_7_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_7_panel.SetActive(true);
        //stage_explain_panel.SetActive(true);
    }
    public void hide_stage_7_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_7_panel.SetActive(false);
        stage_explain_panel.SetActive(false);
        stage_reward_explain_panel.SetActive(false);
        show_current_stage.text = "";
        Stage_explain.text = "";
    }

    public void show_stage_8_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_8_panel.SetActive(true);
        //stage_explain_panel.SetActive(true);
    }
    public void hide_stage_8_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_8_panel.SetActive(false);
        stage_explain_panel.SetActive(false);
        stage_reward_explain_panel.SetActive(false);
        show_current_stage.text = "";
        Stage_explain.text = "";
    }

    public void show_stage_9_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_9_panel.SetActive(true);
        //stage_explain_panel.SetActive(true);
    }
    public void hide_stage_9_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_9_panel.SetActive(false);
        stage_explain_panel.SetActive(false);
        stage_reward_explain_panel.SetActive(false);
        show_current_stage.text = "";
        Stage_explain.text = "";
    }

    public void show_stage_10_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_10_panel.SetActive(true);
    }

    public void show_stage_11_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_11_panel.SetActive(true);
    }

    public void show_stage_12_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_12_panel.SetActive(true);
    }

    public void show_stage_13_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_13_panel.SetActive(true);
    }

    public void show_stage_14_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_14_panel.SetActive(true);
    }

    public void show_stage_15_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_15_panel.SetActive(true);
    }

    public void hide_all_stage_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_1_panel.SetActive(false);
        stage_2_panel.SetActive(false);
        stage_3_panel.SetActive(false);
        stage_4_panel.SetActive(false);
        stage_5_panel.SetActive(false);
        stage_6_panel.SetActive(false);
        stage_7_panel.SetActive(false);
        stage_8_panel.SetActive(false);
        stage_9_panel.SetActive(false);
        stage_10_panel.SetActive(false);
        stage_11_panel.SetActive(false);
        stage_12_panel.SetActive(false);
        stage_13_panel.SetActive(false);
        stage_14_panel.SetActive(false);
        stage_15_panel.SetActive(false);
        stage_explain_panel.SetActive(false);
        stage_reward_explain_panel.SetActive(false);
        show_current_stage.text = "";
        Stage_explain.text = "";
    }
    public void hide_stage_10_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();

        stage_10_panel.SetActive(false);
        stage_explain_panel.SetActive(false);
        stage_reward_explain_panel.SetActive(false);
        show_current_stage.text = "";
        Stage_explain.text = "";
    }





    public void show_battle_scene_panel()
    {
        mapClone = Instantiate(battleScene_panel, Canvas);
        mapClone.SetActive(true);
    }

    public void hide_battle_scene_panel()
    {
        
        Destroy(mapClone);
        Time.timeScale = 1;
    }

    public void show_result_panel()
    {
        result_panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void hide_result_panel()
    {
        SetBool("getResult", true);
        Destroy(mapClone);
        result_panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void show_pause_panel()
    {
        battle_pause_panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void hide_pause_panel()
    {
        battle_pause_panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void change_battle_state_text()
    {
        buff_text_panel.SetActive(false);
        battle_text_panel.SetActive(true);
    }

    public void chage_buff_state_text()
    {
        battle_text_panel.SetActive(false);
        buff_text_panel.SetActive(true);
    }

    public void show_black_smith_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        black_smith_panel.SetActive(true);
    }

    public void hide_black_smith_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        black_smith_panel.SetActive(false);
    }

    public void show_general_store_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        general_store.SetActive(true);
    }

    public void hide_general_store_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        general_store.SetActive(false);
    }

    public void show_magic_tower_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        magic_tower.SetActive(true);
        MagicTowerController.Instance.select_knight();
    }

    public void hide_magic_tower_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        magic_tower.SetActive(false);
    }
    
    public void show_bar_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        bar_panel.SetActive(true);
    }

    public void hide_bar_panel()
    {
        SoundManager.Instance.play_fight_panel_btn_sound();
        bar_panel.SetActive(false);
    }

    public void show_noticePanel()
    {
        NoticePanel.SetActive(true);
    }


}
