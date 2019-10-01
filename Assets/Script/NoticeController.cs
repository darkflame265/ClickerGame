using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeController : MonoBehaviour
{
    private static NoticeController instance;

    public static NoticeController Instance
    {
        get{
            if(instance == null)
            {
                instance = FindObjectOfType<NoticeController>();

                if(instance == null)
                {
                    GameObject container = new GameObject("NoticeController");

                    instance = container.AddComponent<NoticeController>();
                }
            }
            return instance;
        }
    }
    //goToPanel로 noticePanel을 활성화함;

    void Update()
    {
        if(this.gameObject.activeSelf == true)  //패널이 활성화될시 작동
        {
            StartCoroutine("active");
        }
    }

    IEnumerator active()
    {
        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);   // 1.5초후 패널 비황성화
    }
}
