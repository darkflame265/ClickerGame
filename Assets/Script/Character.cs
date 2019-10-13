using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public enum Slot { Knight, Archer};
    public Slot typeOfHero;

    public long level;

    public long Max_HP;
    public long current_HP;
    

    public long striking_power;

    [Header("Unity Stuff")]
    public Image healthBar;

    public float health_ratio;
    public float attack_ratio;
    public float mana_ratio;
    public float special_ratio;

   


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

    void setState()
    {
        float a;
        a = DataController.Instance.health * health_ratio;
        Max_HP = (long)a;
        current_HP = Max_HP;

        a = DataController.Instance.attack * attack_ratio;
        striking_power = (long)a;
    }

    void Start()   //체력은 여기서 공격력은 DataController에서 관리
    {
        /* 
        if(typeOfHero == Slot.Knight)
        {
            //Debug.Log("it's knight");
            

            health_ratio = 1.5f;
            attack_ratio = 0.8f;
            mana_ratio = 1.1f;
            special_ratio = 0.9f;
            setState();
        }
        if(typeOfHero == Slot.Archer)
        {
            //Debug.Log("it's archer");

            health_ratio = 0.7f;
            attack_ratio = 1.5f;
            mana_ratio = 0.9f;
            special_ratio = 1.2f;

            setState();
        }
        */
        setState();
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
