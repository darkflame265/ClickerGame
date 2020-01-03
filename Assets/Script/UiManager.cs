using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{


    //Panel이 active상태인지 확인하기 위한 panel
    public GameObject character_state_panel;
    public GameObject fight_panel;


    //메인화면 디스플레이
   public Text char_level_displayer;
   public Text current_max_exp_displayer; 
   public Text goldDisplayer;
   public Text goldPerClickDisplayer;
   public Text goldPerSecDisplayer;
   public Text diamondDisplayer;
   public Text upgradeDisplayer;
   public Text GoldLevelDisplayer;

    //캐릭터 스테이터스
   public CharacterStateController characterStateController;
   public Text health;
   public Text attack;
   public Text mana;
   public Text special;

   public Text finalHealth;
   public Text finalAttack;
   public Text finalMana;
   public Text finalSpecial;

   public Text freestats;
   public Text charLevel;

   //상세 스테이터스
   public Text HP_state;
   public Text Damage_state;
   public Text mana_state;
   public Text special_state;
   public Text clickgold_state;
   public Text clickExp_state;
   public Text bonus_state;
   public Text guitar_etc;

   public Text artifact_ticket;
   public Text power_ticket;

   

   //전투
   public Text stage_gold_result;
   public Text stage_exp_result;
   public Text stage_crystal_result;
   public Text stage_resource_result;
   public Text tower_of_diamond;

   public Text show_current_stage;
   public Text Stage_explain;

   public bool checkFloatingNotice;
   public GameObject floatingNoticePanel;

   //설정창
   public GameObject clickButton;
   




   private static UiManager instance;

    public static UiManager Instance
    {
        get{
            if(instance == null)
            {
                instance = FindObjectOfType<UiManager>();

                if(instance == null)
                {
                    GameObject container = new GameObject("UiManager");

                    instance = container.AddComponent<UiManager>();
                }
            }
            return instance;
        }
    }

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
        if(!PlayerPrefs.HasKey("click_background"))
        {
            PlayerPrefs.SetInt("click_background", 0);
        }
        clickButton.GetComponent<Image>().sprite = background_pack[PlayerPrefs.GetInt("click_background")];
        background_name_text.text = background_name[PlayerPrefs.GetInt("click_background")];
        StartCoroutine("UpdateCanvasUi");
        Time.timeScale = 1;
    }


    static public string ToGoldStringKR(long bigInteger)
    {
        string output = "";

        if (BigInteger.Compare(bigInteger, new BigInteger(10000)) == -1)
        {
            output = bigInteger.ToString();
        }
        else if (BigInteger.Compare(bigInteger, new BigInteger(100000000)) == -1)
        {
            string strTmp = bigInteger.ToString();

            output = strTmp.Substring(0, strTmp.Length - 1 - 3) + "만" +
            strTmp.Substring(strTmp.Length -1 - 3);

        }
        else if (BigInteger.Compare(bigInteger, new BigInteger(1000000000000)) == -1)
        {
            string strTmp = bigInteger.ToString();

            output = strTmp.Substring(0, strTmp.Length - 8) + "억" + 
                        strTmp.Substring(strTmp.Length -8, 4) + "만";
                        /* + strTmp.Substring(strTmp.Length - 1 - 3);*/
        }
        else if (BigInteger.Compare(bigInteger, new BigInteger(10000000000000000)) == -1)
        {
            string strTmp = bigInteger.ToString();
 
            output = strTmp.Substring(0, strTmp.Length - 12) + "조" + 
                        strTmp.Substring(strTmp.Length -12, 4) + "억";
                       /*+ strTmp.Substring(strTmp.Length - 8, 4) + "만"
                        + strTmp.Substring(strTmp.Length - 4);*/
        }
        else if (BigInteger.Compare(bigInteger, new BigInteger(10000000000000000000)) == -1)
        {
            string strTmp = bigInteger.ToString();

            output = strTmp.Substring(0, strTmp.Length - 16) + "경" + 
                        strTmp.Substring(strTmp.Length -16, 4) + "조";
                        /*+ strTmp.Substring(strTmp.Length - 12, 4) + "억"
                        + strTmp.Substring(strTmp.Length - 8, 4) + "만"
                        + strTmp.Substring(strTmp.Length - 4);*/
        }
        return output;
    }

    static public string ToStringKR(long bigInteger)
    {
        string output = "";

        if (BigInteger.Compare(bigInteger, new BigInteger(10000)) == -1)
        {
            output = bigInteger.ToString();
        }
        else if (BigInteger.Compare(bigInteger, new BigInteger(100000000)) == -1)
        {
            string strTmp = bigInteger.ToString();

            output = strTmp.Substring(0, strTmp.Length - 1 - 3) + "만" +
            strTmp.Substring(strTmp.Length -1 - 3);

            if(strTmp.Substring(1, 1) == "0" || strTmp.Substring(2, 1) == "0" || strTmp.Substring(3, 1) == "0")
            {
                output = strTmp.Substring(0, strTmp.Length - 1 - 3) + "만";
            }

        }
        else if (BigInteger.Compare(bigInteger, new BigInteger(1000000000000)) == -1)
        {
            string strTmp = bigInteger.ToString();

            output = strTmp.Substring(0, strTmp.Length - 8) + "억" + 
                        strTmp.Substring(strTmp.Length -8, 4) + "만";
                        /* + strTmp.Substring(strTmp.Length - 1 - 3);*/
            if(strTmp.Substring(1, 1) == "0" || strTmp.Substring(2, 1) == "0" || strTmp.Substring(3, 1) == "0")
            {
                output = strTmp.Substring(0, strTmp.Length - 8) + "억";
            }
        }
        else if (BigInteger.Compare(bigInteger, new BigInteger(10000000000000000)) == -1)
        {
            string strTmp = bigInteger.ToString();
 
            output = strTmp.Substring(0, strTmp.Length - 12) + "조" + 
                        strTmp.Substring(strTmp.Length -12, 4) + "억";
                       /*+ strTmp.Substring(strTmp.Length - 8, 4) + "만"
                        + strTmp.Substring(strTmp.Length - 4);*/
            if(strTmp.Substring(1, 1) == "0" || strTmp.Substring(2, 1) == "0" || strTmp.Substring(3, 1) == "0")
            {
                output = strTmp.Substring(0, strTmp.Length - 12) + "조";
            }
        }
        else if (BigInteger.Compare(bigInteger, new BigInteger(10000000000000000000)) == -1)
        {
            string strTmp = bigInteger.ToString();

            output = strTmp.Substring(0, strTmp.Length - 16) + "경" + 
                        strTmp.Substring(strTmp.Length -16, 4) + "조";
                        /*+ strTmp.Substring(strTmp.Length - 12, 4) + "억"
                        + strTmp.Substring(strTmp.Length - 8, 4) + "만"
                        + strTmp.Substring(strTmp.Length - 4);*/
            if(strTmp.Substring(1, 1) == "0" || strTmp.Substring(2, 1) == "0" || strTmp.Substring(3, 1) == "0")
            {
                output = strTmp.Substring(0, strTmp.Length - 16) + "경";
            }
        }
        return output;
    }

    IEnumerator UpdateCanvasUi()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            //goldDisplayer.text = ToStringKR(DataController.Instance.gold) + "원";
            goldDisplayer.text = ToGoldStringKR(DataController.Instance.gold) + "원";
            DataController.Instance.goldPerClick = DataController.Instance.goldPerClick;
            goldPerClickDisplayer.text = "터치골드획득 : " + ToStringKR(DataController.Instance.goldPerClick);
            goldPerSecDisplayer.text = "초당퀘스트골드 : " + ToStringKR(DataController.Instance.GetGoldPerSec());
            diamondDisplayer.text = "" + DataController.Instance.diamond;
            artifact_ticket.text = "유물뽑기권: " + DataController.Instance.artifact_ticket;
            power_ticket.text = "권능해방권: " + DataController.Instance.power_ticket;
            upgradeDisplayer.text = "강화비용 " + ToGoldStringKR(DataController.Instance.currentCost);
            GoldLevelDisplayer.text = "레벨  " + DataController.Instance.level + "   X" + ToStringKR(DataController.Instance.besu); 
            char_level_displayer.text = "" + DataController.Instance.char_level;
            current_max_exp_displayer.text = "EXP: " + DataController.Instance.current_exp + "/" + DataController.Instance.max_exp;
            
            char_ui();
            
            if(fight_panel.activeSelf == true)
            {
                //전투
                // stage_gold_result.text = "" + DataController.Instance.get_stage_gold;
                // stage_exp_result.text = "" + DataController.Instance.get_stage_exp;
                // stage_crystal_result.text = "" + DataController.Instance.get_stage_crystal;
                // stage_resource_result.text = "" + DataController.Instance.get_stage_resource;
                if(DataController.Instance.current_stage == 0) //dps 측정
                {
                    show_current_stage.text = "전투력 측정";
                    Stage_explain.text = "몹 평균 체력\n" + 100000000000 + "\n몹 평균 공격력\n" + 0;
                }
                else if(DataController.Instance.current_stage == -1) //시련의 탑
                {
                    show_current_stage.text = "Special " + DataController.Instance.tower_stage.ToString();
                    Stage_explain.text = "몹 평균 체력\n" + DataController.Instance.avg_hp + "\n몹 평균 공격력\n" + DataController.Instance.avg_dg;
                }
                else if(DataController.Instance.current_stage == -2)   //무한모드
                {
                    show_current_stage.text = "infinity mode";
                    Stage_explain.text = "몹 평균 체력\n" + "0 -> infinity" + "\n몹 평균 공격력\n" + "0 -> infinity";
                }
                else {     //일반 스토리
                    show_current_stage.text = "Stage " + DataController.Instance.current_stage.ToString();
                    Stage_explain.text = "몹 평균 체력\n" + DataController.Instance.avg_hp + "\n몹 평균 공격력\n" + DataController.Instance.avg_dg;
                }
                

                
                tower_of_diamond.text = "시련의 탑" + " " + DataController.Instance.tower_stage + "층";
            }
            

            

            if(GetBool("floatnotice") == true)//check가 on이면 bool
            {
                SetBool("floatnotice", false);
                GameObject clone = Instantiate(floatingNoticePanel);
                clone.GetComponentInChildren<Text>().text = DataController.Instance.float_notice_text; 
            }
        }
    }

    public void char_ui()
    {
        if(character_state_panel.activeSelf == true)
            {
                //캐릭터
                health.text = "체력 : " + "(" + characterStateController.select_character_prefabs.GetComponent<Character>().health_ratio + "+"+ BlessingExchange.Instance.blessing_0_to_3_ratio[PlayerPrefs.GetInt("bls_0")] +")" + " * " + DataController.Instance.health + " = " + (characterStateController.select_character_prefabs.GetComponent<Character>().health_ratio + BlessingExchange.Instance.blessing_0_to_3_ratio[PlayerPrefs.GetInt("bls_0")]) * DataController.Instance.health; 
                attack.text = "공격력 : " + "(" + characterStateController.select_character_prefabs.GetComponent<Character>().attack_ratio + "+"+ BlessingExchange.Instance.blessing_0_to_3_ratio[PlayerPrefs.GetInt("bls_1")] +")" + " * " + DataController.Instance.attack + " = " + (characterStateController.select_character_prefabs.GetComponent<Character>().attack_ratio + BlessingExchange.Instance.blessing_0_to_3_ratio[PlayerPrefs.GetInt("bls_1")]) * DataController.Instance.attack; 
                mana.text = "민첩 : " + "(" + characterStateController.select_character_prefabs.GetComponent<Character>().mana_ratio + "+"+ BlessingExchange.Instance.blessing_0_to_3_ratio[PlayerPrefs.GetInt("bls_2")] +")" + " * " + DataController.Instance.mana + " = " + (characterStateController.select_character_prefabs.GetComponent<Character>().mana_ratio + BlessingExchange.Instance.blessing_0_to_3_ratio[PlayerPrefs.GetInt("bls_2")]) * DataController.Instance.mana; 
                special.text = "스킬공격력 : " + "(" + characterStateController.select_character_prefabs.GetComponent<Character>().special_ratio + "+"+ BlessingExchange.Instance.blessing_0_to_3_ratio[PlayerPrefs.GetInt("bls_3")] +")" + " * " + DataController.Instance.special + " = " + (characterStateController.select_character_prefabs.GetComponent<Character>().special_ratio + BlessingExchange.Instance.blessing_0_to_3_ratio[PlayerPrefs.GetInt("bls_3")]) * DataController.Instance.special; 
                freestats.text = "프리스탯 : " + DataController.Instance.freestate;
                charLevel.text = "lv." + characterStateController.select_character_prefabs.GetComponent<Character>().level;

                //상세스테이터스
                HP_state.text = "HP : " + DataController.Instance.health+ " X " + "10" + " = " + DataController.Instance.HP;
                //Damage_state.text = "데미지 : " + DataController.Instance.attack + " X " + DataController.Instance.damage_besu + " = " + DataController.Instance.char_damage;
                mana_state.text = "속도 : " + DataController.Instance.mana;
                special_state.text = "특성 : " + DataController.Instance.special;

                clickgold_state.text = "클릭골드 : " + DataController.Instance.level + " X " + DataController.Instance.besu + " = " + DataController.Instance.goldPerClick;
                clickExp_state.text = "클릭경험치 : ";
                bonus_state.text = "보너스스탯 : " + DataController.Instance.freestate_besu;
                guitar_etc.text = "기타등등 : ";
            }
    }

    public Sprite[] background_pack = new Sprite [0];
    public Text background_name_text;

    string[] background_name = {
        "왕궁",
        "잡화점",
        "마을",
        "대장간",
        "마법의 탑",
        "주점",
        "우주",
        "던전입구",
    };

    public void set_background_left_btn()
    {
        int i = PlayerPrefs.GetInt("click_background");
        i--;
        if(i == -1)
        {
            PlayerPrefs.SetInt("click_background", background_pack.Length-1);
        } else {
            PlayerPrefs.SetInt("click_background", i);
        }
        clickButton.GetComponent<Image>().sprite = background_pack[PlayerPrefs.GetInt("click_background")];
        background_name_text.text = background_name[PlayerPrefs.GetInt("click_background")];
    }

    public void set_background_right_btn()
    {
        int i = PlayerPrefs.GetInt("click_background");
        i++;
        if(i > background_pack.Length-1)
        {
            PlayerPrefs.SetInt("click_background", 0);
        } else {
            PlayerPrefs.SetInt("click_background", i);
        }
        clickButton.GetComponent<Image>().sprite = background_pack[PlayerPrefs.GetInt("click_background")];
        background_name_text.text = background_name[PlayerPrefs.GetInt("click_background")];
    }

}
