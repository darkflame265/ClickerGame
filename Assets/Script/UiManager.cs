﻿using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
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
   public Text tower_of_diamond;

   public bool checkFloatingNotice;
   public GameObject floatingNoticePanel;
   




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



   void Update()
   {
    
       goldDisplayer.text = ToStringKR(DataController.Instance.gold) + "원";
       goldPerClickDisplayer.text = "터치골드획득 : " + ToStringKR(DataController.Instance.goldPerClick);
       goldPerSecDisplayer.text = "초당퀘스트골드 : " + ToStringKR(DataController.Instance.GetGoldPerSec());
       diamondDisplayer.text = "" + DataController.Instance.diamond;
       artifact_ticket.text = "유물뽑기권: " + DataController.Instance.artifact_ticket;
       power_ticket.text = "권능해방권: " + DataController.Instance.power_ticket;
       upgradeDisplayer.text = "강화비용 " + ToStringKR(DataController.Instance.currentCost);
       GoldLevelDisplayer.text = "레벨  " + DataController.Instance.level + "   X" + ToStringKR(DataController.Instance.besu); 
       char_level_displayer.text = "" + DataController.Instance.char_level;
       current_max_exp_displayer.text = "EXP: " + DataController.Instance.current_exp + "/" + DataController.Instance.max_exp;
       
       //캐릭터
       health.text = "체력 : " + "(" + characterStateController.select_character_prefabs.GetComponent<Character>().health_ratio + ")" + " * " + DataController.Instance.health + " = " + characterStateController.select_character_prefabs.GetComponent<Character>().health_ratio * DataController.Instance.health; 
       attack.text = "공격력 : " + "(" + characterStateController.select_character_prefabs.GetComponent<Character>().attack_ratio + ")" + " * " + DataController.Instance.attack + " = " + characterStateController.select_character_prefabs.GetComponent<Character>().attack_ratio * DataController.Instance.attack; 
       mana.text = "마나 : " + "(" + characterStateController.select_character_prefabs.GetComponent<Character>().mana_ratio + ")" + " * " + DataController.Instance.mana + " = " +characterStateController.select_character_prefabs.GetComponent<Character>().mana_ratio * DataController.Instance.mana; 
       special.text = "스킬효과 : " + "(" + characterStateController.select_character_prefabs.GetComponent<Character>().special_ratio + ")" + " * " + DataController.Instance.special + " = " + characterStateController.select_character_prefabs.GetComponent<Character>().special_ratio * DataController.Instance.special; ;
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
       

       //전투
       stage_gold_result.text = "" + DataController.Instance.get_stage_gold;
       stage_exp_result.text = "" + DataController.Instance.get_stage_exp;
       stage_crystal_result.text = "" + DataController.Instance.get_stage_crystal;
       tower_of_diamond.text = "시련의 탑+" + DataController.Instance.tower_repeatCount + "\n" + DataController.Instance.tower_stage + "층";

       if(GetBool("floatnotice") == true)//check가 on이면 bool
       {
           SetBool("floatnotice", false);
           GameObject clone = Instantiate(floatingNoticePanel);
           clone.GetComponentInChildren<Text>().text = DataController.Instance.float_notice_text; 
       }
   }

}
