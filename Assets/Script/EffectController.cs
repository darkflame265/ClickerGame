using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{

    private static EffectController instance;

    public static EffectController Instance
    {
        get{
            if(instance == null)
            {
                instance = FindObjectOfType<EffectController>();

                if(instance == null)
                {
                    GameObject container = new GameObject("EffectController");

                    instance = container.AddComponent<EffectController>();
                }
            }
            return instance;
        }
    }

    public GameObject effect1;
    public GameObject content;
    public ParticleSystem explosion;

    

    void Start()
    {
        //explosion.Stop();
    }

    public void click() // 예제용 연결된 오브젝트 없음
    {
        if (Input.GetMouseButtonDown(0)) 
            { 
                // 스크린 좌표를 월드 좌표로 변환함 
                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition); 

                Vector2 touchPos = new Vector2(wp.x, wp.y); 

                // 오브젝트의 위치를 갱신함 
                transform.position = touchPos; 

                // 생성 
                GameObject instObj =  Instantiate(effect1, transform.position, Quaternion.identity) as GameObject; 
                instObj.transform.parent = content.transform;

                // 파괴 
                Destroy(instObj.gameObject, 0.1f); 
            }
    }

    public void button(){ // 클릭버튼과 연결됨
     
                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition); 

                Vector2 touchPos = new Vector2(wp.x, wp.y); 

                transform.position = touchPos;
             /* 
                GameObject child = Instantiate(effect1) as GameObject;
                child.SetActive(true);
                child.transform.parent = content.transform;
                child.transform.position = transform.position;

                Destroy(child.gameObject, 0.5f);

                Debug.Log(transform.position);
                */
            

    }

    public void WWExplosion_c()
    {
        //explosion.transform.parent = content.transform;
        //explosion.transform.position = new Vector3(-431, -113, -88);

        explosion.Play();

    }
}
