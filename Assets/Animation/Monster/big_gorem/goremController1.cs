﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goremController1 : MonoBehaviour
{
    Animator animator;
    GameObject player;

    public GameObject Stone;

    Vector3 cp;  //캐릭터 위치

    public float speed = 3f;
    bool movebool = true;
    public Transform other;

    public float xMov;
    void Start(){
        if(DataController.Instance.current_stage <= 100)
        {
            xMov = Random.Range(-0.05f, -0.10f);
        } else if (DataController.Instance.current_stage <= 200) {
            xMov = Random.Range(-0.10f, -0.15f);
        } else if (DataController.Instance.current_stage <= 300) {
            xMov = Random.Range(-0.20f, -0.30f);
        } else xMov = -0.05f;
        animator = GetComponent<Animator>();
        StartCoroutine("char_position");
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
                animator.SetBool("isAttack", true);
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
        if(DataController.Instance.current_stage != -2)
        {
            yield return new WaitForSeconds(2f);
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
            if(this.transform.GetComponent<EnemyController>().current_HP <=0)
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
        if(movebool == true)
        {
            
            this.transform.Translate(new Vector3(xMov, 0, 0));
            allAnimatorStop();
            animator.SetBool("isWalk", true);
            
        }
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject target in enemies) {
            float distance = Vector3.Distance(target.transform.position, this.transform.position);
            
            
            if(distance < 10f)
            {
                movebool = false;
                allAnimatorStop();
                animator.SetBool("isAttack", true);
            }
            else        //공격 시 대기시간 넣기
            {
                movebool = true;
            }
        }
        
        if(enemies.Length == 0)
        {
            movebool = false;
        }

        
    }
    //공격은 "돌맹이"가 합니다.
    
    public void Throw_Stone()
    {
        GameObject stone = Instantiate(Stone, this.transform);
    }

    
}
