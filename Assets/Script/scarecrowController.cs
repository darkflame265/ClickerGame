﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class scarecrowController : MonoBehaviour
{
    GameObject player;

    Vector3 cp;  //캐릭터 위치

    public float speed = 3f;
    bool movebool = true;
    public Transform other;

    bool wait = false;

    List<GameObject> A = new List<GameObject>();
    List<float> B = new List<float>();

    float close_distance = 5f;
    GameObject close_enemy;


    
    public long damage;
    public GameObject prefab_floating_text;

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
        damage = GetComponent<EnemyController>().damage;
        StartCoroutine("char_position");
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
            //this.transform.Translate(new Vector3(xMove,yMove,0));  //이동

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
                yield return new WaitForSeconds(1f);
                Destroy(this.gameObject);
                
            }

            Move();

            
        }
    }


    public void Move()
    { 
        if(this.transform.position.z != 0.0f)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -2.5f);

        }
        if(movebool == true)
        {
            this.transform.Translate(new Vector3(xMov, 0f, 0f));  
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");
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
            if(target.GetComponent<Character>().current_HP < 0)
            {
                A.Clear();
            }

            if(Vector3.Distance(close_enemy.transform.position, this.transform.position) < 2f)
            {
                movebool = false;
                //Invoke("Attack", 0.5f);
                Attack();
            }
            else
            {
                movebool = true;
            }
        }
        if(enemies.Length == 0)
        {
            movebool = false;
        }
    }

    public void Attack()//애니메이션 이벤트 뒤에 배치
    {
        close_enemy.transform.GetComponent<Character>().decreaseHP(this.damage);
        var clone = Instantiate(prefab_floating_text, close_enemy.transform.position, Quaternion.Euler(Vector3.zero));
        clone.transform.position += new Vector3(0, 2);
        if(close_enemy.transform.GetComponent<Character>().avoid_attack == true)
        {
            clone.GetComponent<FloatingText>().text.text = "회피";
        } else {
            clone.GetComponent<FloatingText>().text.text = "-" + this.damage;
        }
        
        clone.GetComponent<FloatingText>().text.color = Color.red;
        clone.transform.SetParent(this.transform);
    }

}