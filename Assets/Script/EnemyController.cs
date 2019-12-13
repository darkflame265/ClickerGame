using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{ 

   public long Max_HP;
   public long current_HP;
   public long damage;
   public float attackDistance;

   public float health_ratio;
    public float attack_ratio;
   public float mana_ratio;
   public float special_ratio;

   public bool timestop = false;

   [Header("Unity Stuff")]
   public Image healthBar;

   void Start()
   {
       current_HP = Max_HP;
       StartCoroutine("check_HP");
       StartCoroutine("check_stop");
   }

   IEnumerator check_HP()
   {
       while(true)
       {
            yield return new WaitForSeconds(0.1f);
            float hp_percent =  (float)current_HP / (float)Max_HP;
       
            healthBar.fillAmount = hp_percent;

            //Debug.Log("enemy_hp: " + current_HP);
            if(current_HP <= 0)
            {
                yield return new WaitForSeconds(1f);
                Destroy(this.gameObject);
            }
       }
   }

   IEnumerator check_stop()
   {
       while(true)
       {
           yield return new WaitForSeconds(2f);

           if(timestop == true) {
               timestop = false;
           }

       }
   }


   public void decreaseHP(long damage)
   {
       current_HP -= damage;
   }


   
}
