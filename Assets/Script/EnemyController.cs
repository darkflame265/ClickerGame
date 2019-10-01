using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{ 

   public long Max_HP;
   public long current_HP;
   public long damage;

   [Header("Unity Stuff")]
   public Image healthBar;

   void Start()
   {
       current_HP = Max_HP;
       StartCoroutine("check_HP");
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


   public void decreaseHP(long damage)
   {
       current_HP -= damage;
   }


   
}
