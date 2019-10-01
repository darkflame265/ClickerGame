using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public enum Slot { Knight, Archer};
    public Slot typeOfHero;

    public long Max_HP;
    public long current_HP;

    [Header("Unity Stuff")]
    public Image healthBar;


    private static Character instance;

    public static Character Instance
    {
        get{
            if(instance == null)
            {
                instance = FindObjectOfType<Character>();

                if(instance == null)
                {
                    GameObject container = new GameObject("Character");

                    instance = container.AddComponent<Character>();
                }
            }
            return instance;
        }
    }

    void Start()   //체력은 여기서 공격력은 DataController에서 관리
    {
        if(typeOfHero == Slot.Knight)
        {
            //Debug.Log("it's knight");
            Max_HP = DataController.Instance.health * 10;
            current_HP = Max_HP;
        }
        if(typeOfHero == Slot.Archer)
        {
            //Debug.Log("it's archer");
            Max_HP = DataController.Instance.archer_HP;
            current_HP = Max_HP;
        }
        StartCoroutine("check_HP");
    }

    IEnumerator check_HP()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            float hp_percent =  (float)current_HP / (float)Max_HP;
            healthBar.fillAmount = hp_percent;    
        }
    }


    public void decreaseHP(long damage)
    {
        current_HP -= damage;
    }
}
