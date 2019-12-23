using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KnightController : MonoBehaviour
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

    public Image skillBar;
    int currentSkillPoint;
    int maxSkillPoint = 3;
    bool skilling = false;

    int power_timestop = 0;

    public int defenseCount = 0;

    
    
    void Start(){
        animator = GetComponent<Animator>();
        animator.SetFloat("attackSpeed", BlessingExchange.Instance.blessing_attackSpeed_ratio[PlayerPrefs.GetInt("bls_4")]);
        StartCoroutine("char_position");
        StartCoroutine("SkillUI");
        currentSkillPoint = 0;
        power_timestop = PowerController.Instance.return_power_list(20);

        if(PowerController.Instance.return_power_list(16) == 1)
        {
            defenseCount = 10;
        }
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
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //Debug.Log("eneimies:  " + enemies.Length);
        
        
    }

    public void allAnimatorStop()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalk", false);
        animator.SetBool("isAttack", false);
        animator.SetBool("isSkill", false);
        animator.SetBool("isSkill1", false);
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

            //Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousel);
            //Debug.Log(Input.mousePosition);
            if(this.transform.GetComponent<Character>().current_HP <=0) //사망할 시
            {
                movebool = false;
                allAnimatorStop();
                animator.SetBool("isDeath", true);
                yield return new WaitForSeconds(1f); //1초 기다리고 캐릭터 비활성화
                int eternityOn = Random.Range(1,100);
                GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
                if(PowerController.Instance.return_power_list(21) == 1 && eternityOn < 36 && gos.Length < 8)
                {
                    Vector3 loc = new Vector3(this.transform.position.x-1 ,this.transform.position.y, this.transform.position.z);
                    var clone = Instantiate(this.gameObject, loc, Quaternion.Euler(Vector2.zero));
                    clone.transform.localScale = new Vector3(1f,1f,1f);
                    clone.transform.SetParent(this.transform.parent);

                    var clone1 = Instantiate(this.gameObject, this.transform.position, Quaternion.Euler(Vector2.zero));
                    clone1.transform.localScale = new Vector3(1f,1f,1f);
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
            //Debug.Log("Distance:  " + distance);
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
            Invoke("skill", 0.5f);
        }
          
         
        if(Vector3.Distance(close_enemy.transform.position, this.transform.position) < 1f && skilling == true)
        {
            skilling = true;
            movebool = false;
            allAnimatorStop();
            animator.SetBool("isSkill1", true);    //스킬공격
            
            currentSkillPoint = 0;  //스킬포인트 초기화
        }   

        else if(Vector3.Distance(close_enemy.transform.position, this.transform.position) < 1f && skilling==false)
        {                       //(Vector3.Distance(close_enemy.transform.position, this.transform.position) < 1f && skilling==false)
            movebool = false;
            allAnimatorStop();
            animator.SetBool("isAttack", true);    //공격
            //WaitFotIt();
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
        skilling = true;
        movebool = false;
        allAnimatorStop();
        animator.SetBool("isSkill1", true);    //공격
        currentSkillPoint = 0;  //스킬포인트 초기화
    }

    
 
    public void Attack()//애니메이션 이벤트 뒤에 배치
    { 
        if(power_timestop == 1)
        {
            int a = Random.Range(0, 10);
            if(a == 1)
            {

                close_enemy.GetComponent<EnemyController>().timestop = true;
            }
        }

        

        close_enemy.transform.GetComponent<EnemyController>().decreaseHP(this.GetComponent<Character>().striking_power);
        var clone = Instantiate(prefab_floating_text, close_enemy.transform.position, Quaternion.Euler(Vector3.zero));
        clone.transform.position += new Vector3(0, 2);
        clone.GetComponent<FloatingText>().text.text = "-" + this.GetComponent<Character>().striking_power;
        clone.transform.SetParent(this.transform);
        if(DataController.Instance.knight_level > 2)
        {
            currentSkillPoint++;
        }
        
    }

    public void SkillAtack()
    {
        close_enemy.transform.GetComponent<EnemyController>().decreaseHP(this.GetComponent<Character>().striking_power*2);
        var clone = Instantiate(prefab_floating_text, close_enemy.transform.position, Quaternion.Euler(Vector3.zero));
        clone.transform.position += new Vector3(0, 2);
        clone.GetComponent<FloatingText>().text.color = Color.blue;
        clone.GetComponent<FloatingText>().text.text = "-" + this.GetComponent<Character>().striking_power*2 + "!!";
        clone.transform.SetParent(this.transform);
    }

    public void SkillFinish()
    {
        skilling = false;
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
