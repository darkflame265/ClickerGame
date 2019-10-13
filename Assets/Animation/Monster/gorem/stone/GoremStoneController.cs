using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoremStoneController : MonoBehaviour
{

    public long StoneDamage;
    public GameObject Stone;

    void Start()
    {
        StoneDamage = this.transform.parent.GetComponent<EnemyController>().damage;
    } 

    void Update()
    {
        this.transform.Translate(new Vector3(-0.2f, 0, 0));

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject target in enemies) {
                float distance = Vector3.Distance(target.transform.position, this.transform.position);
                if(distance < 1f)
                {
                    target.transform.GetComponent<Character>().decreaseHP(StoneDamage);
                }

                if(distance < 0.5f)
                {
                    Destroy(Stone);
                }
            }
    }
}
