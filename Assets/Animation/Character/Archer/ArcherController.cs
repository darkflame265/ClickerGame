using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcherController : MonoBehaviour
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

    public Image skillBar;
    int currentSkillPoint;
    int maxSkillPoint = 3;

    public GameObject arrow;
    public GameObject skill_arrow;

    bool deathbool = false;

    public GameObject shadow_Archer;

    public GameObject thunder_effect;
    public GameObject light_effect;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("attackSpeed", BlessingExchange.Instance.blessing_attackSpeed_ratio[PlayerPrefs.GetInt("bls_4")]);
        StartCoroutine("char_position");
        damage = this.GetComponent<Character>().striking_power;
        StartCoroutine("SkillUI");
        currentSkillPoint = 0;

        Transform pos;
        pos = this.transform;
        if(PowerController.Instance.return_power_list(17) == 1)
        {
            shadow_Archer = Instantiate(shadow_Archer, pos);    
            shadow_Archer.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0.5f);
            shadow_Archer.transform.SetParent(this.transform.parent);
            shadow_Archer.transform.position = this.transform.position;
            shadow_Archer.transform.position = new Vector3(pos.position.x-0.6f, pos.position.y+0.2f, pos.position.z);
            shadow_Archer.transform.localScale = this.transform.localScale;

            shadow_Archer.GetComponent<Character>().current_HP = this.GetComponent<Character>().current_HP;
            shadow_Archer.GetComponent<Character>().striking_power = this.GetComponent<Character>().striking_power;
            shadow_Archer.GetComponent<Character>().special_ratio = this.GetComponent<Character>().special_ratio;
        } 
        // if(PowerController.Instance.return_power_list(18) == 1)
        // {
        //     GameObject thunder = Instantiate(thunder_effect, this.transform);
        //     thunder.transform.SetParent(this.transform);

        //     GameObject light = Instantiate(light_effect, this.transform);
        //     light.transform.SetParent(this.transform);
        // }
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
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject close_e = null;
 
        if(Vector3.Distance(enemies[0].transform.position, this.transform.position) > Vector3.Distance(enemies[enemies.Length-1].transform.position, this.transform.position))
        {
            close_e = enemies[enemies.Length-1];
        } else { 
            close_e = enemies[0];
        }

        if(Vector3.Distance(close_e.transform.position, this.transform.position) < 3f)
        {
            Debug.Log("ene is close");
        }
        else {
            if(DataController.Instance.current_stage != -2)
            {
                yield return new WaitForSeconds(2f);
            }
        }
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
                if(deathbool == false)
                {
                    //아래로 이동
                    this.transform.position = new Vector3(transform.position.x, transform.position.y-0.1f, transform.position.z);
                    deathbool = true;
                }
                yield return new WaitForSeconds(1f);
                int eternityOn = Random.Range(1,100);
                GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
                if(PowerController.Instance.return_power_list(21) == 1 && eternityOn < 36 && gos.Length < 8)
                {
                    Vector3 loc = new Vector3(this.transform.position.x-1 ,this.transform.position.y, this.transform.position.z);
                    var clone = Instantiate(this.gameObject, loc, Quaternion.Euler(Vector2.zero));
                    clone.transform.localScale = new Vector3(2.8f,2.81f,2.8f);
                    clone.transform.SetParent(this.transform.parent);

                    var clone1 = Instantiate(this.gameObject, this.transform.position, Quaternion.Euler(Vector2.zero));
                    clone1.transform.localScale = new Vector3(2.8f,2.8f,2.8f);
                    clone1.transform.SetParent(this.transform.parent);
                }
                Destroy(this.gameObject);
            }
            Move();
        }
        
    }

    public void Move()
    {
        float xMov = 0.05f;
        if(DataController.Instance.current_stage != -2)
        {
            if(movebool == true)
            {
                allAnimatorStop();
                animator.SetBool("isWalk", true);
                this.transform.Translate(new Vector3(xMov, 0, 0));
            }
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
            Invoke("skill", 0.2f);
        }
          

        if(Vector3.Distance(close_enemy.transform.position, this.transform.position) < 10f)
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
        if(this.transform.GetComponent<Character>().current_HP <=0)
        {
            Destroy(this);
        }
    }

    public void skill()
    {
        if(Vector3.Distance(close_enemy.transform.position, this.transform.position) < 10f)
        {
            
            var skill_arrow_clone = Instantiate(skill_arrow, this.transform.position, Quaternion.Euler(Vector2.zero));
            skill_arrow_clone.transform.SetParent(this.transform);
        }
        currentSkillPoint = 0;  //스킬포인트 초기화
    }

    public void Attack()//애니메이션 이벤트 뒤에 배치
    {
        // if(currentSkillPoint >= maxSkillPoint)
        // {
        //     if(Vector3.Distance(close_enemy.transform.position, this.transform.position) < 10f)
        //     {
                
        //         var skill_arrow_clone = Instantiate(skill_arrow, this.transform.position, Quaternion.Euler(Vector2.zero));
        //         skill_arrow_clone.transform.SetParent(this.transform);
        //     }
        //     currentSkillPoint = 0;  //스킬포인트 초기화
        // }
        // if(Vector3.Distance(close_enemy.transform.position, this.transform.position) < 10f)
        // {
            var arrow_clone = Instantiate(arrow, this.transform.position, Quaternion.Euler(Vector2.zero));
            arrow_clone.transform.SetParent(this.transform);
            if(DataController.Instance.archer_level > 2)
            {
                currentSkillPoint++;
                //currentSkillPoint = currentSkillPoint + 3;
            }
        //}
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

