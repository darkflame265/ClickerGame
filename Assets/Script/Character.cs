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

    public GameObject thunder_effect;
    public GameObject light_effect;

    public GameObject prefab_floating_text;

    public long speed_count;


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
        if(PowerController.Instance.return_power_list(16) == 1)
        {
            float a;
            a = (DataController.Instance.health * health_ratio) * 10;
            Max_HP = (long)a;
            current_HP = Max_HP;
        }
        else
        {
            float a;
            a = DataController.Instance.health * health_ratio;
            Max_HP = (long)a;
            current_HP = Max_HP;
        }
        
        

        int shieldOn = Random.Range(0,4);

        if(PowerController.Instance.return_power_list(18) == 1 && shieldOn == 0) //쉴드 부여
        {
            Max_Shield = DataController.Instance.special * 5;
            current_Shield = Max_Shield;

            GameObject thunder = Instantiate(thunder_effect, this.transform);
            thunder.transform.SetParent(this.transform);
        } else {
            Max_Shield = 0;
            current_Shield = Max_Shield;
        }

        float b = DataController.Instance.attack * attack_ratio;
        striking_power = (long)b;
    }

    void Start()   //체력은 여기서 공격력은 DataController에서 관리
    {
        setState();
        check_speed_ability();
        StartCoroutine("check_HP");
        if(PowerController.Instance.return_power_list(16) == 1)
        {
            StartCoroutine("heal_HP");
        }
    }

    void check_speed_ability()
    {
        if(DataController.Instance.mana * this.mana_ratio >= 100)
        {
            speed_count = 10;
        }
        else if(DataController.Instance.mana * this.mana_ratio >= 300)
        {
            speed_count = 15;
        } else if(DataController.Instance.mana * this.mana_ratio >= 500) {
            speed_count = 20;
        } else if(DataController.Instance.mana * this.mana_ratio >= 700) {
            speed_count = 25;
        } else if(DataController.Instance.mana * this.mana_ratio >= 1000) {
            speed_count = 30;
        } else if(DataController.Instance.mana * this.mana_ratio >= 2000) {
            speed_count = 35;
        } else if(DataController.Instance.mana * this.mana_ratio >= 3000) {
            speed_count = 40;
        } else if(DataController.Instance.mana * this.mana_ratio >= 4000) {
            speed_count = 45;
        }else if(DataController.Instance.mana * this.mana_ratio >= 5000) {
            speed_count = 50;
        } else {
            speed_count = 1;
        }
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

    IEnumerator heal_HP()
    {
        while(true)
        {
            
            if(current_Shield < Max_Shield)
            {
                GameObject light = Instantiate(light_effect, this.transform);
                light.transform.SetParent(this.transform);
                float heal_Shield_amount = Max_Shield * 0.5f;
                current_Shield += (long)heal_Shield_amount;

                var clone = Instantiate(prefab_floating_text, this.transform.position, Quaternion.Euler(Vector3.zero));
                clone.transform.position += new Vector3(0, 2);
                clone.GetComponent<FloatingText>().text.text = "+" + heal_Shield_amount;
                clone.GetComponent<Text>().color = Color.green;
                clone.transform.SetParent(this.transform);

                if(current_Shield > Max_Shield)
                {
                    current_Shield = Max_Shield;
                }    
                yield return new WaitForSeconds(0.5f);
                Destroy(light);
            }
            else if(current_Shield <= 0 && current_HP < Max_HP && current_HP > 0)
            {
                GameObject light = Instantiate(light_effect, this.transform);
                light.transform.SetParent(this.transform);
                float heal_amount = Max_HP * 0.5f;
                current_HP += (long)heal_amount;

                var clone = Instantiate(prefab_floating_text, this.transform.position, Quaternion.Euler(Vector3.zero));
                clone.transform.position += new Vector3(0, 2);
                clone.GetComponent<FloatingText>().text.text = "+" + heal_amount;
                clone.GetComponent<Text>().color = Color.green;
                clone.transform.SetParent(this.transform);

                if(current_HP > Max_HP)
                {
                    current_HP = Max_HP;
                }
                yield return new WaitForSeconds(0.5f);
                Destroy(light);
            }
            

            yield return new WaitForSeconds(1f);
        }
    }

    public bool avoid_attack = false;

    public void decreaseHP(long damage)
    {
        avoid_attack = false;
        if(current_Shield > 0)
        {
            if(Random.Range(0, 100) < speed_count)
            {
                avoid_attack = true;
            } else {
                current_Shield -= damage;
            }

        } else {
            if(Random.Range(0, 100) < speed_count)
            {
                avoid_attack = true;
            } else {
                current_HP -= damage;
            }
            
        }
    }

}
