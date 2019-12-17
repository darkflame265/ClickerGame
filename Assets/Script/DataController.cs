using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using System;
using System.Text;

public class DataController : MonoBehaviour
{

    private static DataController instance;

    public static DataController Instance
    {
        get{
            if(instance == null)
            {
                instance = FindObjectOfType<DataController>();

                if(instance == null)
                {
                    GameObject container = new GameObject("DataController");

                    instance = container.AddComponent<DataController>();
                }
            }
            return instance;
        }
    }

    

    private ItemButton[] itemButtons;

    public int afterSec;
    
    DateTime GetLastPlayDate()
    {
        if(!PlayerPrefs.HasKey("Time"))
        {
            return DateTime.Now;
        }

        string timeBinaryInString = PlayerPrefs.GetString("Time");  //string 가져옴
        long timeBinaryInLong = Convert.ToInt64(timeBinaryInString); //string을 long으로

        return DateTime.FromBinary(timeBinaryInLong); //long타입을 DateTime으로 바꿈
    }

    void UpdateLastPlayDate()
    {
        PlayerPrefs.SetString("Time", DateTime.Now.ToBinary().ToString()); //string으로 젖아
    }

    void OnApplicationQuit()
    {
        UpdateLastPlayDate();
    }

    public int timeAfterLastPlay
    {
        get
        {
            DateTime currentTime = DateTime.Now;
            DateTime lastPlayTime = GetLastPlayDate();

            return (int)currentTime.Subtract(lastPlayTime).TotalSeconds;
        }
    }

    void Awake() 
    {
        itemButtons = FindObjectsOfType<ItemButton>();
    }

    void Start()
    {
        afterSec = timeAfterLastPlay;
        
        Invoke("getStackedGoldPerSec", 0.5f);
        //gold += GetGoldPerSec() * timeAfterLastPlay;
        InvokeRepeating("UpdateLastPlayDate", 0f, 5f);
    }

    public long current_heart   
    {
        get
        {
            if(!PlayerPrefs.HasKey("current_heart")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpcurrent_heart = PlayerPrefs.GetString("current_heart");
            return long.Parse(tmpcurrent_heart);
        }
        set
        {
            PlayerPrefs.SetString("current_heart", value.ToString()); 
        }
    }

    public long max_heart
    {
        get
        {
            if(!PlayerPrefs.HasKey("max_heart")) // 골드가 없을떄
            {
                return 3;
            }
            string tmpmax_heart = PlayerPrefs.GetString("max_heart");
            return long.Parse(tmpmax_heart);
        }
        set
        {
            PlayerPrefs.SetString("max_heart", value.ToString()); 
        }
    }

    public float limit_time
    {
        get
        {
            if(!PlayerPrefs.HasKey("limit_time")) // 골드가 없을떄
            {
                return 1200;
            }
            string tmplimit_time = PlayerPrefs.GetString("limit_time");
            return long.Parse(tmplimit_time);
        }
        set
        {
            PlayerPrefs.SetString("limit_time", value.ToString()); 
        }
    }

    public long clickCount
    {
        get
        {
            if(!PlayerPrefs.HasKey("clickCount")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpclickCount = PlayerPrefs.GetString("clickCount");
            return long.Parse(tmpclickCount);
        }
        set
        {
            PlayerPrefs.SetString("clickCount", value.ToString()); 
        }
    }

    public long stateTotal
    {
        get
        {
            if(!PlayerPrefs.HasKey("stateTotal")) // 골드가 없을떄
            {
                return health + attack + mana + special;
            }
            string tmpstateTotal = PlayerPrefs.GetString("stateTotal");
            return long.Parse(tmpstateTotal);
        }
        set
        {
            value = health + attack + mana + special;
            PlayerPrefs.SetString("stateTotal", value.ToString()); 
        }
    }

    public long maxDps
    {
        get
        {
            if(!PlayerPrefs.HasKey("maxDps")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpmaxDps = PlayerPrefs.GetString("maxDps");
            return long.Parse(tmpmaxDps);
        }
        set
        {
            PlayerPrefs.SetString("maxDps", value.ToString()); 
        }
    }

    public long artifact_ticket
    {
        get
        {
            if(!PlayerPrefs.HasKey("artifact_ticket")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpartifact_ticket = PlayerPrefs.GetString("artifact_ticket");
            return long.Parse(tmpartifact_ticket);
        }
        set
        {
            PlayerPrefs.SetString("artifact_ticket", value.ToString()); 
        }
    }

    public long power_ticket
    {
        get
        {
            if(!PlayerPrefs.HasKey("power_ticket")) // 골드가 없을떄
            {
                return 0;
            }
            string tmppower_ticket = PlayerPrefs.GetString("power_ticket");
            return long.Parse(tmppower_ticket);
        }
        set
        {
            PlayerPrefs.SetString("power_ticket", value.ToString()); 
        }
    }


    public long gold
    {
        get
        {
            if(!PlayerPrefs.HasKey("Gold")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpGold = PlayerPrefs.GetString("Gold");
            return long.Parse(tmpGold);
        }
        set
        {
            PlayerPrefs.SetString("Gold", value.ToString()); 
        }
    }

    public long countgold
    {
        get
        {
            if(!PlayerPrefs.HasKey("countGold")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpcountGold = PlayerPrefs.GetString("countGold");
            return long.Parse(tmpcountGold);
        }
        set
        {
            PlayerPrefs.SetString("countGold", value.ToString()); 
        }
    }

    public long goldPerClick
    {
        get
        {
            if(!PlayerPrefs.HasKey("GoldPerClick")) // 골드가 없을떄
            {
                return 1;
            }
            string tmpGoldPerClick = PlayerPrefs.GetString("GoldPerClick");
            return long.Parse(tmpGoldPerClick);
        }
        set
        {
            value = level * besu;
            PlayerPrefs.SetString("GoldPerClick", value.ToString());
        }
    }

    public long ExpPerClick
    {
        get
        {
            if(!PlayerPrefs.HasKey("ExpPerClick")) // 골드가 없을떄
            {
                return 1;
            }
            string tmpExpPerClick = PlayerPrefs.GetString("ExpPerClick");
            return long.Parse(tmpExpPerClick);

        }
        set
        {
            value = exp_besu;
            PlayerPrefs.SetString("GoldExpPerClick", value.ToString());
        }
    }

    public long diamond
    {
        get
        {
            if(!PlayerPrefs.HasKey("Diamond")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpDiamond = PlayerPrefs.GetString("Diamond");
            return long.Parse(tmpDiamond);
        }
        set
        {
            PlayerPrefs.SetString("Diamond", value.ToString()); 
        }
    }

    public int itemNum
    {
        get
        {
            if(!PlayerPrefs.HasKey("itemNum")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpitemNum = PlayerPrefs.GetString("itemNum");
            return int.Parse(tmpitemNum);
        }
        set
        {
            PlayerPrefs.SetString("itemNum", value.ToString()); 
        }
    }

    public long level
    {
        get
        {
            if(!PlayerPrefs.HasKey("Level")) // 골드가 없을떄
            {
                return 1;
            }
            string tmpLevel = PlayerPrefs.GetString("Level");
            return long.Parse(tmpLevel);
        }
        set
        {
            PlayerPrefs.SetString("Level", value.ToString()); 
        }
    }
    
    public long besu 
    {
        get
        {
            if(!PlayerPrefs.HasKey("Besu")) // 골드가 없을떄
            {
                return 1;
            }
            string tmpBesu = PlayerPrefs.GetString("Besu");
            return long.Parse(tmpBesu);
        }
        set
        {
            PlayerPrefs.SetString("Besu", value.ToString()); 
        }
    }

    public long exp_besu 
    {
        get
        {
            if(!PlayerPrefs.HasKey("exp_Besu")) // 골드가 없을떄
            {
                return 1;
            }
            string tmpexp_Besu = PlayerPrefs.GetString("exp_Besu");
            return long.Parse(tmpexp_Besu);
        }
        set
        {
            PlayerPrefs.SetString("exp_Besu", value.ToString()); 
        }
    }

    
    public long currentCost
    {
        get
        {
            if(!PlayerPrefs.HasKey("CurrentCost")) // 골드가 없을떄
            {
                return 1;
            }
            string tmpCurrentCost = PlayerPrefs.GetString("CurrentCost");
            return long.Parse(tmpCurrentCost);
        }
        set
        {
            value = 1 * (long) Mathf.Pow(1.14f, level);
            PlayerPrefs.SetString("CurrentCost", value.ToString()); 
        }
    }

    public long artifactCost
    {
        get
        {
            if(!PlayerPrefs.HasKey("artifactCost")) // 골드가 없을떄
            {
                return 50;
            }
            string tmpartifactCost = PlayerPrefs.GetString("artifactCost");
            return long.Parse(tmpartifactCost);
        }
        set
        {
            PlayerPrefs.SetString("artifactCost", value.ToString()); 
        }
    }

    public long char_level
    {
        get
        {
            if(!PlayerPrefs.HasKey("Char_Level")) // 골드가 없을떄
            {
                return 1;
            }
            string tmpChar_Level = PlayerPrefs.GetString("Char_Level");
            return long.Parse(tmpChar_Level);
        }
        set
        {
            PlayerPrefs.SetString("Char_Level", value.ToString()); 
        }
    }

    public long current_exp
    {
        get
        {
            if(!PlayerPrefs.HasKey("Current_Exp")) // 골드가 없을떄
            {
                return 1;
            }
            string tmpCurrent_Exp = PlayerPrefs.GetString("Current_Exp");
            return long.Parse(tmpCurrent_Exp);
        }
        set
        {
            PlayerPrefs.SetString("Current_Exp", value.ToString()); 
        }
    }

    public long max_exp
    {
        get
        {
            if(!PlayerPrefs.HasKey("Max_Exp")) // 골드가 없을떄
            {
                return 2;
            }
            string tmpMax_Exp = PlayerPrefs.GetString("Max_Exp");
            return long.Parse(tmpMax_Exp);
        }
        set
        {
            
            PlayerPrefs.SetString("Max_Exp", value.ToString()); 
        }
    }

    public long health
    {
        get
        {
            if(!PlayerPrefs.HasKey("Health1")) // 골드가 없을떄
            {
                return 5;
            }
            string tmpHealth = PlayerPrefs.GetString("Health1");
            return long.Parse(tmpHealth);
        }
        set
        {
            PlayerPrefs.SetString("Health1", value.ToString()); 
        }
    }

    public long attack
    {
        get
        {
            if(!PlayerPrefs.HasKey("attack")) // 골드가 없을떄
            {
                return 5;
            }
            string tmpattack = PlayerPrefs.GetString("attack");
            return long.Parse(tmpattack);
        }
        set
        {
            
            PlayerPrefs.SetString("attack", value.ToString()); 
        }
    }

   public long mana
    {
        get
        {
            if(!PlayerPrefs.HasKey("mana")) // 골드가 없을떄
            {
                return 5;
            }
            string tmpmana = PlayerPrefs.GetString("mana");
            return long.Parse(tmpmana);
        }
        set
        {
            
            PlayerPrefs.SetString("mana", value.ToString()); 
        }
    }

    public long special
    {
        get
        {
            if(!PlayerPrefs.HasKey("special")) // 골드가 없을떄
            {
                return 5;
            }
            string tmpspecial = PlayerPrefs.GetString("special");
            return long.Parse(tmpspecial);
        }
        set
        {
            
            PlayerPrefs.SetString("special", value.ToString()); 
        }
    }

    public long freestate
    {
        get
        {
            if(!PlayerPrefs.HasKey("Freestate")) // 골드가 없을떄
            {
                return 1;
            }
            string tmpFreestate = PlayerPrefs.GetString("Freestate");
            return long.Parse(tmpFreestate);
        }
        set
        {
            
            PlayerPrefs.SetString("Freestate", value.ToString()); 
        }
    }

    public long freestate_besu
    {
        get
        {
            if(!PlayerPrefs.HasKey("freestate_besu")) // 골드가 없을떄
            {
                return 1;
            }
            string tmpfreestate_besu = PlayerPrefs.GetString("freestate_besu");
            return long.Parse(tmpfreestate_besu);
        }
        set
        {
            
            PlayerPrefs.SetString("freestate_besu", value.ToString()); 
        }
    }

    public void richbutton()
    {
        gold = 10000000000;
        diamond = 1000000000;
    }


    public void superbutton()
    {

        level =   50;
        besu = 500000000000;
        health = 500;
        attack = 500;
        mana = 500;
        special = 500;
        freestate = 10000;
    }

    public void MultiplyGoldPerClick(int be)
    {
        besu *= be;
    }

    public void DevideGoldPerClick(int be)
    {
        besu /= be;
    }

    public void MultiplyFreeStateBesu(int be)
    { //현재고민 보너스 스탯적용을 곱셈으로 할까 덧셈으로 할까
        if(freestate_besu == 1)
        {
            freestate_besu += be - 1;
        }
        else
        {
            freestate_besu += be;
        }
    }

    public void devideFreeStateBesu(int be)
    { //현재고민 보너스 스탯적용을 곱셈으로 할까 덧셈으로 할까
        long value = freestate_besu - be;
        if(value < 1)
        {
            freestate_besu -= (be - 1);
        }
        else
        {
            freestate_besu -= be;
        }
    }


    public void MultiplyExpBesu(int be)
    {
        exp_besu *= be;
    }

    public void DevideExpBesu(int be)
    {
        exp_besu /= be;
    }

    public void MultiplyDamageBesu(int be)
    { 
        if(damage_besu == 1)
        {
            damage_besu += be - 1;
        }
        else
        {
            damage_besu += be;
        }
    }
    public void devideDamageBesu(int be)
    { 
        long value = damage_besu - be;
        if(value < 1)
        {
            damage_besu -= (be - 1);
        }
        else
        {
            damage_besu -= be;
        }
    }

    public void LoadUpgradeButton(UpgradeButton upgradeButton)
    {
        string key = upgradeButton.upgradeName;

        
       
        upgradeButton.goldByUpgrade = PlayerPrefs.GetInt(key + "_goldByUpgrade", 
        upgradeButton.startGoldByUpgrade);
        
    }

    public void SaveUpgradeButton(UpgradeButton upgradeButton)
    {
        string key = upgradeButton.upgradeName;

        PlayerPrefs.SetInt(key + "_goldByUpgrade", upgradeButton.goldByUpgrade);
    }

    public void LoadItemButton(ItemButton itemButton)
    {
         string key = itemButton.itemName;  //set: string으로 저장 get: string을 long으로 바꿈

        itemButton.level = PlayerPrefs.GetInt(key + "_level");
        itemButton.currentCost = long.Parse(PlayerPrefs.GetString(key + "_cost"));
        itemButton.goldPerSec = long.Parse(PlayerPrefs.GetString(key + "_goldPerSec"));
        itemButton.currentQuestGold = long.Parse(PlayerPrefs.GetString(key + "_currentQuestGold"));
        itemButton.MaxQuestGold = long.Parse(PlayerPrefs.GetString(key + "_MaxQuestGold"));

        itemButton.currentCost = long.Parse(PlayerPrefs.GetString(key + "_currentCost"));
        if(PlayerPrefs.GetInt(key + "_isPurchased") == 1)//true, false
        {
            itemButton.isPurchased = true;
        }
        else{
            itemButton.isPurchased = false;
        }
    }

    public void SaveItemButton(ItemButton itemButton)
    {
        string key = itemButton.itemName;

        PlayerPrefs.SetInt(key + "_level", itemButton.level);
        PlayerPrefs.SetString(key + "_cost", itemButton.currentCost.ToString());
        PlayerPrefs.SetString(key + "_goldPerSec", itemButton.goldPerSec.ToString());
        PlayerPrefs.SetString(key + "_currentQuestGold", itemButton.currentQuestGold.ToString());
        PlayerPrefs.SetString(key + "_MaxQuestGold", itemButton.MaxQuestGold.ToString());
        PlayerPrefs.SetString(key + "_currentCost", itemButton.currentCost.ToString());
        if(itemButton.isPurchased == true)//true, false
        {
            PlayerPrefs.SetInt(key + "_isPurchased", 1);
        }
        else{
            PlayerPrefs.SetInt(key + "_isPurchased", 0);
        }
    }

    public void InitGold()
    {
         gold = 1;
         goldPerClick = 1;
         diamond = 500;
    }

    public void InitItem(ItemButton itemButton)
    {
        string key = itemButton.itemName;

        PlayerPrefs.SetInt(key + "_level", 0);
        PlayerPrefs.SetString(key + "_cost", itemButton.startCurrentCost.ToString());
        PlayerPrefs.SetString(key + "_goldPerSec", itemButton.startGoldPerSec.ToString());
        PlayerPrefs.SetString(key + "_currentQuestGold", itemButton.startcurrentQuestGold.ToString());
        PlayerPrefs.SetString(key + "_MaxQuestGold", itemButton.startMaxQuestGold.ToString());

        itemButton.isPurchased = false;
    }

    public void InitAllItem()
    {
        for(int i = 0; i < itemButtons.Length; i ++)
        {
            string key = itemButtons[i].itemName;

            PlayerPrefs.SetInt(key + "_level", 0);
            PlayerPrefs.SetString(key + "_cost", itemButtons[i].startCurrentCost.ToString());
            PlayerPrefs.SetString(key + "_goldPerSec", itemButtons[i].startGoldPerSec.ToString());
            PlayerPrefs.SetString(key + "_currentQuestGold", itemButtons[i].startcurrentQuestGold.ToString());
            PlayerPrefs.SetString(key + "_MaxQuestGold", itemButtons[i].startMaxQuestGold.ToString());
            PlayerPrefs.SetInt(key + "_isPurchased", 0);

            PlayerPrefs.SetString(key + "_currentCost", itemButtons[i].startCurrentCost.ToString());

            itemButtons[i].isPurchased = false;
            LoadItemButton(itemButtons[i]);
            itemButtons[i].UpdateUI();
        }

        
    }

    public void getAllQuestGold()
    {
        countgold = 0;
        for(int i = 0; i < itemButtons.Length; i ++)
        {
            string key = itemButtons[i].itemName;
            
            countgold += itemButtons[i].currentQuestGold;
            gold += itemButtons[i].currentQuestGold;
            PlayerPrefs.SetString(key + "_currentQuestGold", "0");

            LoadItemButton(itemButtons[i]);
            itemButtons[i].UpdateUI();
        }
        itemButtons[0].getAllQuestGold();
    }
    
    public long GetGoldPerSec()
    {
        long goldPerSec = 0;
        for(int i = 0; i < itemButtons.Length; i ++)
        {
            if(itemButtons[i].isPurchased == true)
            {
                goldPerSec += itemButtons[i].goldPerSec;
            }
        }
        return goldPerSec;
    }

    public void getStackedGoldPerSec()
    {
        for(int i = 0; i < itemButtons.Length; i ++)
        {
            if(itemButtons[i].isPurchased == true)
            {
                itemButtons[i].currentQuestGold += itemButtons[i].goldPerSec * afterSec;
            }
        }
    }

    public long startBattle
    {
        get
        {
            if(!PlayerPrefs.HasKey("StartBattle")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpStartBattle = PlayerPrefs.GetString("StartBattle");
            return long.Parse(tmpStartBattle);
        }
        set
        {
            PlayerPrefs.SetString("StartBattle", value.ToString()); 
        }
    }

    public long HP
    {
        get
        {
            long tmpHP = health *10;
            return (tmpHP);
        }
    }

    // public long char_damage
    // {
    //     get
    //     {
    //         long tmpdamage = attack * damage_besu;
    //         return (tmpdamage);
    //     }
    // }

    public long damage_besu
    {
        get
        {
            if(!PlayerPrefs.HasKey("damage_besu")) // 골드가 없을떄
            {
                return 1;
            }
            string tmpdamage_besu = PlayerPrefs.GetString("damage_besu");
            return long.Parse(tmpdamage_besu);
        }
        set
        {
            
            PlayerPrefs.SetString("damage_besu", value.ToString()); 
        }
    }

    public long get_stage_gold
    {
        get
        {
            if(!PlayerPrefs.HasKey("get_stage_gold")) // 골드가 없을떄
            {
                return 1;
            }
            string tmpget_stage_gold = PlayerPrefs.GetString("get_stage_gold");
            return long.Parse(tmpget_stage_gold);

        }
        set
        {
            PlayerPrefs.SetString("get_stage_gold", value.ToString());
        }
    }
    public long get_stage_exp
    {
        get
        {
            if(!PlayerPrefs.HasKey("get_stage_exp")) // 골드가 없을떄
            {
                return 1;
            }
            string tmpget_stage_exp = PlayerPrefs.GetString("get_stage_exp");
            return long.Parse(tmpget_stage_exp);

        }
        set
        {
            PlayerPrefs.SetString("get_stage_exp", value.ToString());
        }
    } 

    public long get_stage_crystal
    {
        get
        {
            if(!PlayerPrefs.HasKey("get_stage_crystal")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpget_stage_crystal = PlayerPrefs.GetString("get_stage_crystal");
            return long.Parse(tmpget_stage_crystal);

        }
        set
        {
            PlayerPrefs.SetString("get_stage_crystal", value.ToString());
        }
    }


    //환생
    public long reincarnation_count
    {
        get
        {
            if(!PlayerPrefs.HasKey("reincarnation_count")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpreincarnation_count = PlayerPrefs.GetString("reincarnation_count");
            return long.Parse(tmpreincarnation_count);

        }
        set
        {
            PlayerPrefs.SetString("reincarnation_count", value.ToString());
        }
    }

    //히어로 정보
    public long hero_0_ID
    {
        get
        {
            if(!PlayerPrefs.HasKey("hero_0_ID")) // 골드가 없을떄
            {
                return 0; //-1 = null
            }
            string tmphero_0_ID = PlayerPrefs.GetString("hero_0_ID");
            return long.Parse(tmphero_0_ID);

        }
        set
        {
            PlayerPrefs.SetString("hero_0_ID", value.ToString());
        }
    }

    public long hero_1_ID
    {
        get
        {
            if(!PlayerPrefs.HasKey("hero_1_ID")) // 골드가 없을떄
            {
                return 0;
            }
            string tmphero_1_ID = PlayerPrefs.GetString("hero_1_ID");
            return long.Parse(tmphero_1_ID);

        }
        set
        {
            PlayerPrefs.SetString("hero_1_ID", value.ToString());
        }
    }

    public long hero_2_ID
    {
        get
        {
            if(!PlayerPrefs.HasKey("hero_2_ID")) // 골드가 없을떄
            {
                return 0;
            }
            string tmphero_2_ID = PlayerPrefs.GetString("hero_2_ID");
            return long.Parse(tmphero_2_ID);

        }
        set
        {
            PlayerPrefs.SetString("hero_2_ID", value.ToString());
        }
    }
    //히어로 위치
    public long hero_spawn_0 
    {
        get
        {
            if(!PlayerPrefs.HasKey("hero_spawn_0")) // 골드가 없을떄
            {
                return 0;
            }
            string tmphero_spawn_0 = PlayerPrefs.GetString("hero_spawn_0");
            return long.Parse(tmphero_spawn_0);

        }
        set
        {
            PlayerPrefs.SetString("hero_spawn_0", value.ToString());
        }
    }

    public long tower_stage
    {
        get
        {
            if(!PlayerPrefs.HasKey("tower_stage")) // 골드가 없을떄
            {
                return 1;
            }
            string tmptower_stage = PlayerPrefs.GetString("tower_stage");
            return long.Parse(tmptower_stage);

        }
        set
        {
            PlayerPrefs.SetString("tower_stage", value.ToString());
        }
    }

    public long tower_repeatCount
    {
        get
        {
            if(!PlayerPrefs.HasKey("tower_repeatCount")) // 골드가 없을떄
            {
                return 1;
            }
            string tmptower_repeatCount = PlayerPrefs.GetString("tower_repeatCount");
            return long.Parse(tmptower_repeatCount);

        }
        set
        {
            PlayerPrefs.SetString("tower_repeatCount", value.ToString());
        }
    }

    public long monster_hp
    {
        get
        {
            if(!PlayerPrefs.HasKey("monster_hp")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpmonster_hp = PlayerPrefs.GetString("monster_hp");
            return long.Parse(tmpmonster_hp);

        }
        set
        {
            PlayerPrefs.SetString("monster_hp", value.ToString());
        }
    }

    public long monster_damage
    {
        get
        {
            if(!PlayerPrefs.HasKey("monster_damage")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpmonster_damage = PlayerPrefs.GetString("monster_damage");
            return long.Parse(tmpmonster_damage);

        }
        set
        {
            PlayerPrefs.SetString("monster_damage", value.ToString());
        }
    }


    public string monster_0_ID 
    {
        get
        {
            if(!PlayerPrefs.HasKey("monster_0_ID")) // 골드가 없을떄
            {
                return null;
            }
            string tmpmonster_0_ID = PlayerPrefs.GetString("monster_0_ID");
            return tmpmonster_0_ID;
        }
        set
        {
            PlayerPrefs.SetString("monster_0_ID", value.ToString());
        }
    }

    public long monster_0_hp
    {
        get
        {
            if(!PlayerPrefs.HasKey("monster_0_hp")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpmonster_0_hp = PlayerPrefs.GetString("monster_0_hp");
            return long.Parse(tmpmonster_0_hp);

        }
        set
        {
            PlayerPrefs.SetString("monster_0_hp", value.ToString());
        }
    }

    public long monster_0_damage
    {
        get
        {
            if(!PlayerPrefs.HasKey("monster_0_damage")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpmonster_0_damage = PlayerPrefs.GetString("monster_0_damage");
            return long.Parse(tmpmonster_0_damage);

        }
        set
        {
            PlayerPrefs.SetString("monster_0_damage", value.ToString());
        }
    }

 

    

    public string monster_1_ID 
    {
        get
        {
            if(!PlayerPrefs.HasKey("monster_1_ID")) // 골드가 없을떄
            {
                return null;
            }
            string tmpmonster_1_ID = PlayerPrefs.GetString("monster_1_ID");
            return tmpmonster_1_ID;
        }
        set
        {
            PlayerPrefs.SetString("monster_1_ID", value.ToString());
        }
    }

    public long monster_1_hp
    {
        get
        {
            if(!PlayerPrefs.HasKey("monster_1_hp")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpmonster_1_hp = PlayerPrefs.GetString("monster_1_hp");
            return long.Parse(tmpmonster_1_hp);

        }
        set
        {
            PlayerPrefs.SetString("monster_1_hp", value.ToString());
        }
    }

    public long monster_1_damage
    {
        get
        {
            if(!PlayerPrefs.HasKey("monster_1_damage")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpmonster_1_damage = PlayerPrefs.GetString("monster_1_damage");
            return long.Parse(tmpmonster_1_damage);

        }
        set
        {
            PlayerPrefs.SetString("monster_1_damage", value.ToString());
        }
    }

    public string monster_2_ID 
    {
        get
        {
            if(!PlayerPrefs.HasKey("monster_2_ID")) // 골드가 없을떄
            {
                return null;
            }
            string tmpmonster_2_ID = PlayerPrefs.GetString("monster_2_ID");
            return tmpmonster_2_ID;
        }
        set
        {
            PlayerPrefs.SetString("monster_2_ID", value.ToString());
        }
    }

    public long monster_2_hp
    {
        get
        {
            if(!PlayerPrefs.HasKey("monster_2_hp")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpmonster_2_hp = PlayerPrefs.GetString("monster_2_hp");
            return long.Parse(tmpmonster_2_hp);

        }
        set
        {
            PlayerPrefs.SetString("monster_2_hp", value.ToString());
        }
    }


    public long monster_2_damage
    {
        get
        {
            if(!PlayerPrefs.HasKey("monster_2_damage")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpmonster_2_damage = PlayerPrefs.GetString("monster_2_damage");
            return long.Parse(tmpmonster_2_damage);

        }
        set
        {
            PlayerPrefs.SetString("monster_2_damage", value.ToString());
        }
    }

    public string summon_monster_ID 
    {
        get
        {
            if(!PlayerPrefs.HasKey("summon_monster_ID")) // 골드가 없을떄
            {
                return null;
            }
            string tmpsummon_monster_ID = PlayerPrefs.GetString("summon_monster_ID");
            return tmpsummon_monster_ID;
        }
        set
        {
            PlayerPrefs.SetString("summon_monster_ID", value.ToString());
        }
    }
 
    public long summon_monster_hp
    {
        get
        {
            if(!PlayerPrefs.HasKey("summon_monster_hp")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpsummon_monster_hp = PlayerPrefs.GetString("summon_monster_hp");
            return long.Parse(tmpsummon_monster_hp);

        }
        set
        {
            PlayerPrefs.SetString("summon_monster_hp", value.ToString());
        }
    }


    public long summon_monster_damage
    {
        get
        {
            if(!PlayerPrefs.HasKey("summon_monster_damage")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpsummon_monster_damage = PlayerPrefs.GetString("summon_monster_damage");
            return long.Parse(tmpsummon_monster_damage);

        }
        set
        {
            PlayerPrefs.SetString("summon_monster_damage", value.ToString());
        }
    }

    public long knight_level
    {
        get
        {
            if(!PlayerPrefs.HasKey("knight_level")) // 골드가 없을떄
            {
                return 1;
            }
            string tmpknight_level = PlayerPrefs.GetString("knight_level");
            return long.Parse(tmpknight_level);

        }
        set
        {
            PlayerPrefs.SetString("knight_level", value.ToString());
        }
    } 

    public long archer_level
    {
        get
        {
            if(!PlayerPrefs.HasKey("archer_level")) // 골드가 없을떄
            {
                return 1;
            }
            string tmparcher_level = PlayerPrefs.GetString("archer_level");
            return long.Parse(tmparcher_level);

        }
        set
        {
            PlayerPrefs.SetString("archer_level", value.ToString());
        }
    } 

    public long wizard_level
    {
        get
        {
            if(!PlayerPrefs.HasKey("wizard_level")) // 골드가 없을떄
            {
                return 1;
            }
            string tmpwizard_level = PlayerPrefs.GetString("wizard_level");
            return long.Parse(tmpwizard_level);

        }
        set
        {
            PlayerPrefs.SetString("wizard_level", value.ToString());
        }
    } 

    public long archer_current_cost
    {
        get
        {
            if(!PlayerPrefs.HasKey("archer_current_cost")) // 골드가 없을떄
            {
                return 1;
            }
            string tmparcher_current_cost = PlayerPrefs.GetString("archer_current_cost");
            return long.Parse(tmparcher_current_cost);

        }
        set
        {
            PlayerPrefs.SetString("archer_current_cost", value.ToString());
        }
    } 

    public long archer_HP
    {
        get
        {
            if(!PlayerPrefs.HasKey("archer_HP")) // 골드가 없을떄
            {
                return 1;
            }
            string tmparcher_HP = PlayerPrefs.GetString("archer_HP");
            return long.Parse(tmparcher_HP);

        }
        set
        {
            PlayerPrefs.SetString("archer_HP", value.ToString());
        }
    }

    public long archer_damage
    {
        get
        {
            if(!PlayerPrefs.HasKey("archer_damage")) // 골드가 없을떄
            {
                return 1;
            }
            string tmparcher_damage = PlayerPrefs.GetString("archer_damage");
            return long.Parse(tmparcher_damage);

        }
        set
        {
            PlayerPrefs.SetString("archer_damage", value.ToString());
        }
    }

    public long current_stage
    {
        get
        {
            if(!PlayerPrefs.HasKey("current_stage")) // 골드가 없을떄
            {
                return 1;
            }
            string tmpcurrent_stage = PlayerPrefs.GetString("current_stage");
            return long.Parse(tmpcurrent_stage);

        }
        set
        {
            PlayerPrefs.SetString("current_stage", value.ToString());
        }
    }

    public long item_amount 
    {
        get
        {
            if(!PlayerPrefs.HasKey("current_stage")) // 골드가 없을떄
            {
                return 0;
            }
            string tmpcurrent_stage = PlayerPrefs.GetString("current_stage");
            return long.Parse(tmpcurrent_stage);

        }
        set
        {
            PlayerPrefs.SetString("current_stage", value.ToString());
        }
    }

    public string float_notice_text
    {
        get
        {
            if(!PlayerPrefs.HasKey("float_notice_text")) // 골드가 없을떄
            {
                return "";
            }
            string tmpfloat_notice_text = PlayerPrefs.GetString("float_notice_text");
            return tmpfloat_notice_text;

        }
        set
        {
            PlayerPrefs.SetString("float_notice_text", value.ToString());
        }
    }



    

}
