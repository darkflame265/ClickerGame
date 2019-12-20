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
    public GameObject ch_1_check_img;
    public GameObject ch_2_check_img;
    public GameObject ch_3_check_img;
    public GameObject ch_4_check_img;
    public GameObject ch_5_check_img;
    public GameObject ch_6_check_img;
    public GameObject ch_7_check_img;
    public GameObject ch_8_check_img;
    public GameObject ch_9_check_img;
    public GameObject ch_10_check_img;

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
        for(int i = 0; i < stageCh1.Length; i++)
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

            if(GetBool("ch1" + 19) == true)  //0~19
            {
                ch_1_check_img.GetComponent<Image>().color = new Color(1,1,1,1);
            }
            else ch_1_check_img.GetComponent<Image>().color = new Color(1,1,1,0);

            if(GetBool("ch1" + 39) == true)  //20~39
            {
                ch_2_check_img.GetComponent<Image>().color = new Color(1,1,1,1);
            }
            else ch_2_check_img.GetComponent<Image>().color = new Color(1,1,1,0);

            if(GetBool("ch1" + 59) == true)  //20~39
            {
                ch_3_check_img.GetComponent<Image>().color = new Color(1,1,1,1);
            }
            else ch_3_check_img.GetComponent<Image>().color = new Color(1,1,1,0);

            if(GetBool("ch1" + 79) == true)  //20~39
            {
                ch_4_check_img.GetComponent<Image>().color = new Color(1,1,1,1);
            }
            else ch_4_check_img.GetComponent<Image>().color = new Color(1,1,1,0);

            if(GetBool("ch1" + 99) == true)  //20~39
            {
                ch_5_check_img.GetComponent<Image>().color = new Color(1,1,1,1);
            }
            else ch_5_check_img.GetComponent<Image>().color = new Color(1,1,1,0);

            if(GetBool("ch1" + 119) == true)  //20~39
            {
                ch_6_check_img.GetComponent<Image>().color = new Color(1,1,1,1);
            }
            else ch_6_check_img.GetComponent<Image>().color = new Color(1,1,1,0);

            if(GetBool("ch1" + 139) == true)  //20~39
            {
                ch_7_check_img.GetComponent<Image>().color = new Color(1,1,1,1);
            }
            else ch_7_check_img.GetComponent<Image>().color = new Color(1,1,1,0);

            if(GetBool("ch1" + 159) == true)  //20~39
            {
                ch_8_check_img.GetComponent<Image>().color = new Color(1,1,1,1);
            }
            else ch_8_check_img.GetComponent<Image>().color = new Color(1,1,1,0);

            if(GetBool("ch1" + 179) == true)  //20~39
            {
                ch_9_check_img.GetComponent<Image>().color = new Color(1,1,1,1);
            }
            else ch_9_check_img.GetComponent<Image>().color = new Color(1,1,1,0);

            if(GetBool("ch1" + 199) == true)  //20~39
            {
                ch_10_check_img.GetComponent<Image>().color = new Color(1,1,1,1);
            }
            else ch_10_check_img.GetComponent<Image>().color = new Color(1,1,1,0);
    } 
    
    
}
