using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoremStoneController : MonoBehaviour
{

    public long StoneDamage;
    public GameObject Stone;

    public GameObject prefab_floating_text;

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
                    var clone = Instantiate(prefab_floating_text, target.transform.position, Quaternion.Euler(Vector3.zero));
                    clone.transform.position += new Vector3(0, 2);
                    clone.GetComponent<FloatingText>().text.text = "-" + StoneDamage;
                    clone.GetComponent<FloatingText>().text.color = Color.red;

                    clone.transform.SetParent(this.transform);
                    target.transform.GetComponent<Character>().decreaseHP(StoneDamage);
                    Destroy(Stone);
                }

            }
    }
}
