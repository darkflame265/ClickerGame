using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightController : MonoBehaviour
{
    private static FightController instance;

    public static FightController Instance
    {
        get{
            if(instance == null)
            {
                instance = FindObjectOfType<FightController>();

                if(instance == null)
                {
                    GameObject container = new GameObject("FightController");

                    instance = container.AddComponent<FightController>();
                }
            }
            return instance;
        }
    }
    //stage
    public GameObject[] stageCh1 = new GameObject[0];

    //UI

    public GameObject[] ch_check_img_pack = new GameObject[0];

    public GameObject[] ch_btn = new GameObject[0];

    public static void SetBool(string name, bool booleanValue)
    {
        PlayerPrefs.SetInt(name, booleanValue ? 1 : 0);
    }

    public static bool GetBool(string name)
    {
        return PlayerPrefs.GetInt(name) == 1 ? true : false;
    }

    public static bool GetBool(string name, bool defaultValue)
    {
        if(PlayerPrefs.HasKey(name)) 
    {
            return GetBool(name);
        }
    
        return defaultValue;
    }

 
    // Use this for initialization
    void Start () {       //원래 스테이지 관련 기능은 여기다해야되는데 귀찮아서
                          //BattleManager에서 여기로 안 옮김

        //StartCoroutine("CheckStage");
        check_stage();
    }


    public void check_stage()
    {
        if(!PlayerPrefs.HasKey("userMaxStage")) {
            PlayerPrefs.SetInt("userMaxStage", 1);
        };
        
        for(int i = PlayerPrefs.GetInt("userMaxStage"); i < stageCh1.Length; i++)
            {
                if(GetBool("ch1" + i) == true)
                {
                    stageCh1[i].GetComponent<Button>().interactable = true;
                }
                else
                {
                    stageCh1[i].GetComponent<Button>().interactable = false;
                }
            }
        if(GetBool("ch1" + 19) == true)  //20~39
        {
            ch_check_img_pack[0].GetComponent<Image>().color = new Color(1,1,1,1);
        }
        else ch_check_img_pack[0].GetComponent<Image>().color = new Color(1,1,1,0);

        if(GetBool("ch1" + 39) == true)  //20~39
        {
            ch_check_img_pack[1].GetComponent<Image>().color = new Color(1,1,1,1);
        }
        else ch_check_img_pack[1].GetComponent<Image>().color = new Color(1,1,1,0);
        if(GetBool("ch1" + 59) == true)  //20~39
        {
            ch_check_img_pack[2].GetComponent<Image>().color = new Color(1,1,1,1);
        }
        else ch_check_img_pack[2].GetComponent<Image>().color = new Color(1,1,1,0);
        if(GetBool("ch1" + 79) == true)  //20~39
        {
            ch_check_img_pack[3].GetComponent<Image>().color = new Color(1,1,1,1);
        }
        else ch_check_img_pack[3].GetComponent<Image>().color = new Color(1,1,1,0);
        if(GetBool("ch1" + 99) == true)  //20~39
        {
            ch_check_img_pack[4].GetComponent<Image>().color = new Color(1,1,1,1);
            ch_btn[1].SetActive(true);
        }
        else {
            ch_check_img_pack[4].GetComponent<Image>().color = new Color(1,1,1,0);
            ch_btn[1].SetActive(false);
        }
            

        if(GetBool("ch1" + 119) == true)  //20~39
        {
            ch_check_img_pack[5].GetComponent<Image>().color = new Color(1,1,1,1);
        }
        else ch_check_img_pack[5].GetComponent<Image>().color = new Color(1,1,1,0);

        if(GetBool("ch1" + 139) == true)  //20~39
        {
         ch_check_img_pack[6].GetComponent<Image>().color = new Color(1,1,1,1);
        }
         else ch_check_img_pack[6].GetComponent<Image>().color = new Color(1,1,1,0);

        if(GetBool("ch1" + 159) == true)  //20~39
        {
         ch_check_img_pack[7].GetComponent<Image>().color = new Color(1,1,1,1);
        }
        else ch_check_img_pack[7].GetComponent<Image>().color = new Color(1,1,1,0);

        if(GetBool("ch1" + 179) == true)  //20~39
        {
         ch_check_img_pack[8].GetComponent<Image>().color = new Color(1,1,1,1);
        }
        else ch_check_img_pack[8].GetComponent<Image>().color = new Color(1,1,1,0);

        if(GetBool("ch1" + 199) == true)  //20~39
        {
            ch_check_img_pack[9].GetComponent<Image>().color = new Color(1,1,1,1);
            ch_btn[2].SetActive(true);
        }
        else {
             ch_check_img_pack[9].GetComponent<Image>().color = new Color(1,1,1,0);
            ch_btn[2].SetActive(false);
        }

        if(GetBool("ch1" + 219) == true)  //20~39
        {
             ch_check_img_pack[10].GetComponent<Image>().color = new Color(1,1,1,1);
        }
        else {
             ch_check_img_pack[10].GetComponent<Image>().color = new Color(1,1,1,0);
        }
        if(GetBool("ch1" + 239) == true)  //20~39
        {
             ch_check_img_pack[11].GetComponent<Image>().color = new Color(1,1,1,1);
        }
        else {
             ch_check_img_pack[11].GetComponent<Image>().color = new Color(1,1,1,0);
        }
        if(GetBool("ch1" + 259) == true)  //20~39
        {
             ch_check_img_pack[12].GetComponent<Image>().color = new Color(1,1,1,1);
        }
        else {
             ch_check_img_pack[12].GetComponent<Image>().color = new Color(1,1,1,0);
        }
        if(GetBool("ch1" + 279) == true)  //20~39
        {
             ch_check_img_pack[13].GetComponent<Image>().color = new Color(1,1,1,1);
        }
        else {
             ch_check_img_pack[13].GetComponent<Image>().color = new Color(1,1,1,0);
        }
        if(GetBool("ch1" + 299) == true)  //20~39
        {
             ch_check_img_pack[14].GetComponent<Image>().color = new Color(1,1,1,1);
        }
        else {
             ch_check_img_pack[14].GetComponent<Image>().color = new Color(1,1,1,0);
        }

            
    } 
    
    
}
