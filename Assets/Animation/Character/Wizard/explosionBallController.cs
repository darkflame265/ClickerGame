using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionBallController : MonoBehaviour
{
    Animator animator;
    public GameObject explosionball;
    public long ExplosionballDamage;
    public GameObject prefab_floating_text;

    void Start()
    {
        animator = GetComponent<Animator>();
        ExplosionballDamage = this.transform.parent.GetComponent<Character>().striking_power * 10;
    } 

    void Update()
    {
        this.transform.Translate(new Vector3(0.2f, 0, 0));

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject target in enemies) {
                float distance = Vector3.Distance(target.transform.position, this.transform.position);
                if(distance < 2f)
                {
                    if(target.GetComponent<EnemyController>().current_HP > 0 && animator.GetCurrentAnimatorStateInfo(0)
	                    .IsName("idle"))
                    {   
                         target.transform.GetComponent<EnemyController>().decreaseHP(ExplosionballDamage);
                        var clone = Instantiate(prefab_floating_text, target.transform.position, Quaternion.Euler(Vector3.zero));
                        clone.transform.position += new Vector3(0, 2);
                        clone.GetComponent<FloatingText>().text.color = Color.blue;
                        clone.GetComponent<FloatingText>().text.text = "-" + ExplosionballDamage;
                        clone.transform.SetParent(this.transform.parent);
                        animator.SetBool("isExplosion", true);
                    }
                }
            }
    }

    public void destroy_this()
    {
        Destroy(explosionball);
    }
}
