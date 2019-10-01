using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BigNoticeController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private static BigNoticeController instance;

    public static BigNoticeController Instance
    {
        get{
            if(instance == null)
            {
                instance = FindObjectOfType<BigNoticeController>();

                if(instance == null)
                {
                    GameObject container = new GameObject("BigNoticeController");

                    instance = container.AddComponent<BigNoticeController>();
                }
            }
            return instance;
        }
    }
    //goToPanel로 noticePanel을 활성화함;

    public bool isBtnDown = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isBtnDown = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isBtnDown = true;
    }

    void Update()
    {
        if(isBtnDown == true)  //패널이 활성화될시 작동
        {
            this.gameObject.SetActive(false);
        }
    }

}
