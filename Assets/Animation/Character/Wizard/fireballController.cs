using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballController : MonoBehaviour
{
    public long FireballDamage;
    public GameObject fireball;
    public GameObject prefab_floating_text;

    int power_timestop = 0;

    void Start()
    {
        FireballDamage = this.transform.parent.GetComponent<Character>().striking_power;
        power_timestop = PowerController.Instance.return_power_list(20);
    } 

    void Update()
    {
        this.transform.Translate(new Vector3(0, 0.2f, 0));

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject target in enemies) {
                float distance = Vector3.Distance(target.transform.position, this.transform.position);
                if(distance < 2f)
                {
                    if(target.GetComponent<EnemyController>().current_HP > 0)
                    {   
                        if(power_timestop == 1)
                        {
                            int a = Random.Range(0, 10);
                            if(a == 1)
                            {

                                target.GetComponent<EnemyController>().timestop = true;
                            }
                        }

                         target.transform.GetComponent<EnemyController>().decreaseHP(FireballDamage);
                        var clone = Instantiate(prefab_floating_text, target.transform.position, Quaternion.Euler(Vector3.zero));
                        clone.transform.position += new Vector3(0, 2);
                        clone.GetComponent<FloatingText>().text.text = "-" + this.transform.parent.GetComponent<Character>().striking_power;
                        clone.transform.SetParent(this.transform.parent);
                        Destroy(fireball);
                    }
                   
                }
            }
    }
}
