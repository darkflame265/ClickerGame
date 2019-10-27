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

    public EnemyController enemyController;

    

    void Start()
    {   
        enemyController = GetComponent<EnemyController>();
        defalut_cycle = summon_cycle;
        StartCoroutine("summon");
    }

    IEnumerator summon()
    {
        while(true)
        {
            if(DataController.Instance.summon_monster_ID == "Bat")
            {
                monster = Bat;
                monster_scale = Bat.transform.localScale;
            }

            if(creature_count > 0)
            {
                //몬스터 소환;
                GameObject clone_enemy = Instantiate(monster, this.transform.position, Quaternion.Euler(Vector3.zero));
                clone_enemy.GetComponent<EnemyController>().Max_HP = DataController.Instance.summon_monster_hp;
                clone_enemy.GetComponent<EnemyController>().damage = DataController.Instance.summon_monster_damage;
                clone_enemy.transform.SetParent(this.transform);
                clone_enemy.transform.localScale = monster_scale;
                summon_cycle = defalut_cycle;
                creature_count -= 1;
                //Debug.Log("it's working");
                yield return new WaitForSeconds(5f);
            }

            if(enemyController.current_HP <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        
        
    }
}
