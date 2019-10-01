using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class checkPressedBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public bool isBtnDown = false;

    public bool isHealthUp = false;
    public bool isAttackUp = false;
    public bool ismanaUp = false;
    public bool isSpecialUp = false;


    private float pointerDownTimer;

    public void Update()
    {
        if(isBtnDown) {
            Debug.Log("pointerDownTimer is " + pointerDownTimer);
            pointerDownTimer += Time.deltaTime;
            complate_point_up();
            
        }
    }

    private void Reset()
    {
        isBtnDown = false;
        isHealthUp = false;
        isAttackUp = false;
        ismanaUp = false;
        isSpecialUp = false;
        pointerDownTimer = 0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isBtnDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //isBtnDown = false;
        Reset();
    }

    public void its_health()
    {
        isHealthUp = true;
    }

    public void its_attack()
    {
        isAttackUp = true;
    }

    public void its_mana()
    {
        ismanaUp = true;
    }

    public void its_special()
    {
        isSpecialUp = true;
    }

    public void complate_point_up()
    {
        if(DataController.Instance.freestate >= 1 && isHealthUp == true && pointerDownTimer > 1.5f)
        {
            DataController.Instance.health += 1;
            DataController.Instance.freestate -= 1;
        }

        else if(DataController.Instance.freestate >= 1 && isAttackUp == true && pointerDownTimer > 1.5f)
        {
            DataController.Instance.attack += 1;
            DataController.Instance.freestate -= 1;
        }

        else if(DataController.Instance.freestate >= 1 && ismanaUp == true && pointerDownTimer > 1.5f)
        {
            DataController.Instance.mana += 1;
            DataController.Instance.freestate -= 1;
        }

        else if(DataController.Instance.freestate >= 1 && isSpecialUp == true && pointerDownTimer > 1.5f)
        {
            DataController.Instance.special += 1;
            DataController.Instance.freestate -= 1;
        }
    }

    public void up_health_point()
    {
        if(DataController.Instance.freestate >= 1)
        {
            DataController.Instance.health += 1;
            DataController.Instance.freestate -= 1;
        }

        
    }

    public void up_attack_point()
    {
        if(DataController.Instance.freestate >= 1)
        {
            DataController.Instance.attack += 1;
            DataController.Instance.freestate -= 1;
        }
    }

    public void up_mana_point()
    {
        if(DataController.Instance.freestate >= 1)
        {
            DataController.Instance.mana += 1;
            DataController.Instance.freestate -= 1;
        }
    }

    public void up_special_point()
    {
        if(DataController.Instance.freestate >= 1)
        {
            DataController.Instance.special += 1;
            DataController.Instance.freestate -= 1;
        }
    }

    public void up_freestate_point()
    {
        DataController.Instance.freestate += 1;
    }
}
