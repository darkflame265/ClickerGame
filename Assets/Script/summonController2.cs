using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class summonController2 : MonoBehaviour
{
    public float creature_count;
    public float summon_cycle;

    private float defalut_cycle;

    private GameObject monster;
    public Vector3 monster_scale;

    public GameObject bttakgari;

    public GameObject summon_effect;

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
            monster= bttakgari;
            monster_scale = bttakgari.transform.localScale;


            if(creature_count > 0)
            {
                //몬스터 소환;
                float random_value = Random.Range(-3.0f, 3.0f);
                Vector3 summon_position = new Vector3(this.transform.position.x+random_value, this.transform.position.y, this.transform.position.z);
                GameObject clone_enemy = Instantiate(monster, summon_position, Quaternion.Euler(Vector3.zero));

                Vector3 summon_eff_position = new Vector3(this.transform.position.x+random_value, this.transform.position.y);
                GameObject sum_effect = Instantiate(summon_effect, summon_eff_position, Quaternion.Euler(Vector3.zero));
                sum_effect.transform.localScale = Vector3.one;
                sum_effect.transform.SetParent(this.transform);

                clone_enemy.GetComponent<EnemyController>().Max_HP = DataController.Instance.monster_hp;
                clone_enemy.GetComponent<EnemyController>().damage = DataController.Instance.monster_damage;
                clone_enemy.transform.SetParent(this.transform);
                clone_enemy.transform.localScale = monster_scale;
                summon_cycle = defalut_cycle;
                creature_count -= 1;
                //Debug.Log("it's working");
                //float time = Random.Range(1, 5);
                yield return new WaitForSeconds(summon_cycle);
            }
        }
    }
}
