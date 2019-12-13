using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public enum Slot { Knight, Archer, Wizard};
    public Slot typeOfHero;

    public long level;

    public long Max_HP;
    public long current_HP;

    public long Max_Shield;
    public long current_Shield;
    

    public long striking_power;

    [Header("Unity Stuff")]
    public Image healthBar;
    public Image shieldBar;

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
        if(PowerController.Instance.return_power_list(18) == 1) 
        {
            Max_Shield = DataController.Instance.special * 5;
            current_Shield = Max_Shield;
        } else {
            Max_Shield = 0;
            current_Shield = Max_Shield;
        }

        a = DataController.Instance.attack * attack_ratio;
        striking_power = (long)a;
    }

    void Start()   //체력은 여기서 공격력은 DataController에서 관리
    {
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

            float shield_percent = (float)current_Shield / (float)Max_Shield;
            shieldBar.fillAmount = shield_percent;
        }
    }


    public void decreaseHP(long damage)
    {
        if(current_Shield > 0)
        {
            current_Shield -= damage;
        } else {
            current_HP -= damage;
        }
        
    }
}
