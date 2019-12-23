using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillArrowController : MonoBehaviour
{
    public long ArrowDamage;
    public GameObject arrow;
    public GameObject prefab_floating_text;

    void Start()
    {
        //ArrowDamage = this.transform.parent.GetComponent<Character>().striking_power;
        float a;
        a = (this.transform.parent.GetComponent<Character>().special_ratio + BlessingExchange.Instance.blessing_0_to_3_ratio[PlayerPrefs.GetInt("bls_3")]) * DataController.Instance.special;
        ArrowDamage = (long)a;
    } 

    void Update()
    {
        this.transform.Translate(new Vector3(0.2f, 0, 0));

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject target in enemies) {
                float distance = Vector3.Distance(target.transform.position, this.transform.position);
                if(distance < 2f)
                {
                    if(target.GetComponent<EnemyController>().current_HP > 0)
                    {  
                        target.transform.GetComponent<EnemyController>().decreaseHP(ArrowDamage);
                        var clone = Instantiate(prefab_floating_text, new Vector2(target.transform.position.x+1, target.transform.position.y), Quaternion.Euler(Vector3.zero)); //  target.transform.position
                        clone.transform.position += new Vector3(0, 2);
                        clone.GetComponent<FloatingText>().text.color = Color.blue;
                        clone.GetComponent<FloatingText>().text.text = "-" + ArrowDamage;
                        clone.transform.SetParent(this.transform.parent); //this.transform.parent
                        Destroy(arrow);
                    }
                }
            }
        if(Vector3.Distance(this.transform.parent.position, this.transform.position) > 20f)
        {
            Destroy(arrow);
        }
    }
}
