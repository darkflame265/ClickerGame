using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightController : MonoBehaviour
{
    //stage
    public GameObject[] stageCh1 = new GameObject[0];
 
    // Use this for initialization
    void Start () {       //원래 스테이지 관련 기능은 여기다해야되는데 귀찮아서
                          //BattleManager에서 여기로 안 옮김

        StartCoroutine("CheckStage");
        
    }

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

    IEnumerator CheckStage()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
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
        }
        
        
    }
}
