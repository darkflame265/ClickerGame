using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class summonController : MonoBehaviour
{
    public float creature_count;
    public float summon_cycle;

    private float defalut_cycle;

    private GameObject monster;
    public Vector3 monster_scale;

    public GameObject Bat;
    public GameObject Bat_level2;
    public GameObject Spider;
    public GameObject Gorem;
    public GameObject Big_Gorem;
    public GameObject Troll;
    public GameObject goblin_general;
    public GameObject plant_monster;
    public GameObject ice_gorem;
    public GameObject warm_monster;


    string[] summon_list = { "Bat", "Bat_2", "Spider", "Gorem", "BigGorem", "Troll", "goblin_general", "plant_monster", "ice_gorem", "warm_monster"};
    string summon_id;

    

    

    void Start()
    {   
        defalut_cycle = summon_cycle;
        StartCoroutine("summon");
        StartCoroutine("check_hp");
    }

    IEnumerator check_hp()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            if(this.GetComponent<EnemyController>().current_HP <= 0)
            {
                this.tag = "Untagged";
                yield return new WaitForSeconds(0.5f);
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator summon()
    {
        while(true)
        {
            summon_id = summon_list[Random.Range(0, 10)];
            switch(summon_id)
            {
                case "Bat":
                    monster= Bat;
                    monster_scale = Bat.transform.localScale;
                    break;
                case "Bat_2":
                    monster= Bat_level2;
                    monster_scale = Bat_level2.transform.localScale;
                    break;
                case "Spider":
                    monster= Spider;
                    monster_scale = Spider.transform.localScale;
                    break;
                case "Gorem":
                    monster= Gorem;
                    monster_scale = Gorem.transform.localScale;
                    break;
                case "BigGorem":
                    monster= Big_Gorem;
                    monster_scale = Big_Gorem.transform.localScale;
                    break;
                case "Troll":
                    monster= Troll;
                    monster_scale = Troll.transform.localScale;
                    break;
                case "goblin_general":
                    monster= goblin_general;
                    monster_scale = goblin_general.transform.localScale;
                    break;
                case "plant_monster":
                    monster= plant_monster;
                    monster_scale = plant_monster.transform.localScale;
                    break;
                case "ice_gorem":
                    monster= ice_gorem;
                    monster_scale = ice_gorem.transform.localScale;
                    break;
                case "warm_monster":
                    monster= warm_monster;
                    monster_scale = warm_monster.transform.localScale;
                    break;
            }
            
            // if(DataController.Instance.summon_monster_ID == "Bat")
            // {
            //     monster = Bat;
            //     monster_scale = Bat.transform.localScale;
            // }

            if(creature_count > 0)
            {
                //몬스터 소환;
                GameObject clone_enemy = Instantiate(monster, this.transform.position, Quaternion.Euler(Vector3.zero));
                clone_enemy.GetComponent<EnemyController>().Max_HP = DataController.Instance.monster_hp;
                clone_enemy.GetComponent<EnemyController>().damage = DataController.Instance.monster_damage;
                clone_enemy.transform.SetParent(this.transform);
                clone_enemy.transform.localScale = monster_scale;
                summon_cycle = defalut_cycle;
                creature_count -= 1;
                //Debug.Log("it's working");
                float time = Random.Range(1, 5);
                yield return new WaitForSeconds(time);
            }
        }
    }
}
