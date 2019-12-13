using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wizardController : MonoBehaviour
{
    Animator animator;
    EnemyController enemycontrol;
    //리스트
    List<GameObject> A = new List<GameObject>();
    List<float> B = new List<float>();

    Vector3 cp;  //캐릭터 위치
    bool movebool = true;

    public float speed = 5f;

    float close_distance = 5f;
    GameObject close_enemy;
    bool enemy_is_die = false;

    public GameObject prefab_floating_text;

    public long damage;

    public GameObject fireball;

     public Image skillBar;
    int currentSkillPoint;
    int maxSkillPoint = 3;


    public GameObject skill_explosion;

    void Start(){
        animator = GetComponent<Animator>();
        StartCoroutine("char_position");
        damage = this.GetComponent<Character>().striking_power;
        StartCoroutine("SkillUI");
        currentSkillPoint = 0;
    }
    void Update() {
        
        if(Input.anyKey == true)
        {
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) == true)
            {
                animator.SetBool("isWalk", true);
                animator.SetBool("isIdle", false);
            }
            if(Input.GetKey(KeyCode.Space) == true)
            {
                animator.SetBool("isAttack", true);;
                animator.SetBool("isIdle", false);
            }
        }
        
    }

    public void allAnimatorStop()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalk", false);
        animator.SetBool("isAttack", false);
    }

    IEnumerator char_position()
    {   
        yield return new WaitForSeconds(2f);
        while(true)
        {
            yield return new WaitForSeconds(0.01f);
            float xMove=Input.GetAxis ("Horizontal")*speed*Time.deltaTime ; //x축으로 이동할 양 
            float yMove=Input.GetAxis ("Vertical")*speed*Time.deltaTime; //y축으로 이동할양 
            this.transform.Translate(new Vector3(xMove,yMove,0));  //이동

            cp = this.transform.position;
            //Debug.Log(cp);

            if(cp.x > 8.5f)
            {
                this.transform.position = new Vector3(8.5f, cp.y, 0);
            }
            if(cp.x < -8.5f)
            {
                this.transform.position = new Vector3(-8.5f, cp.y, 0);
            }

            if(this.transform.GetComponent<Character>().current_HP <=0)
            {
                movebool = false;
                allAnimatorStop();
                animator.SetBool("isDeath", true);
                yield return new WaitForSeconds(1f);
                Destroy(this.gameObject);
            }
            Move();
        }
        
    }

    public void Move()
    {
        float xMov = 0.05f;
        if(movebool == true)
        {
            this.transform.Translate(new Vector3(xMov, 0, 0));
            allAnimatorStop();
            animator.SetBool("isWalk", true);
        }
        
        
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject target in enemies) {
            float distance = Vector3.Distance(target.transform.position, this.transform.position);
            A.Add(target);
            for(int i = 0; i < A.Count-1; i++)
            {
                if(A.Count == 1)
                {
                    break;
                }
                if(Vector3.Distance(A[i].transform.position, this.transform.position) > Vector3.Distance(A[i+1].transform.position, this.transform.position))
                {
                    GameObject tmp = A[i];
                    A[i] = A[i+1];
                    A[i+1] = tmp;
                }
            }
            
            close_enemy = A[0];
            //Debug.Log("close_enemy is " + close_enemy.name);
            if(target.GetComponent<EnemyController>().current_HP < 0)
            {
                A.Clear();
            }
        }

        if(currentSkillPoint >= maxSkillPoint)  //스킬포인트가 차면
        {
            //skill();
        }

        if(Vector3.Distance(close_enemy.transform.position, this.transform.position) < 8f)
        {
            movebool = false;
            allAnimatorStop();
            animator.SetBool("isAttack", true);
            WaitFotIt();
        }
        else
        {
            movebool = true;
        }
        

        if(enemies.Length == 0)
        {
            movebool = false;
            allAnimatorStop();
            animator.SetBool("isWin", true);  
        }
    }

    public void skill()
    {

            if(Vector3.Distance(close_enemy.transform.position, this.transform.position) < 10f)
            {
                
                var skill_explosion_clone = Instantiate(skill_explosion, this.transform.position, Quaternion.Euler(Vector2.zero));
                skill_explosion_clone.transform.SetParent(this.transform);
            }
            currentSkillPoint = 0;  //스킬포인트 초기화
        
        
    }

    public void Attack()//애니메이션 이벤트 뒤에 배치
    {
        if(currentSkillPoint >= maxSkillPoint)
            {
                if(Vector3.Distance(close_enemy.transform.position, this.transform.position) < 10f)
                {
                    
                    var skill_explosion_clone = Instantiate(skill_explosion, this.transform.position, Quaternion.Euler(Vector2.zero));
                    skill_explosion_clone.transform.SetParent(this.transform);
                }
                currentSkillPoint = 0;  //스킬포인트 초기화
            }
        else if(Vector3.Distance(close_enemy.transform.position, this.transform.position) < 8f)
        {
            //close_enemy.transform.GetComponent<EnemyController>().decreaseHP(this.GetComponent<Character>().striking_power);
            // var clone = Instantiate(prefab_floating_text, close_enemy.transform.position, Quaternion.Euler(Vector3.zero));
            // clone.transform.position += new Vector3(0, 2);
            // clone.GetComponent<FloatingText>().text.text = "-" + this.GetComponent<Character>().striking_power;
            // clone.transform.SetParent(this.transform);

            var fire = Instantiate(fireball, this.transform.position, Quaternion.Euler(0, 0, -90));
            fire.transform.SetParent(this.transform);
            if(DataController.Instance.wizard_level > 2)
            {
                currentSkillPoint++;
            }
            
        }
        
    }

    IEnumerator SkillUI()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            float skillPercent =  (float)currentSkillPoint / (float)maxSkillPoint;
            skillBar.fillAmount = skillPercent;    
        }
    }

    IEnumerator WaitFotIt()
    {
        yield return new WaitForSeconds(2.0f);
    }
}
