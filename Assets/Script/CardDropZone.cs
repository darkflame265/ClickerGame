using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDropZone : MonoBehaviour, IDropHandler
{
    public enum Slot { FRONT, MID, BACK, Hand};
    public Slot typeOfBase;
    

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerDrag.name + "OnDrop to " + gameObject.name);

        CharacterCard d = eventData.pointerDrag.GetComponent<CharacterCard>();
        if(d != null) {
            d.parentToReturnTo = this.transform;// 카드의 parent를 이 zone으로 변경
        }
        StartCoroutine("wait_by_exist_child");
    }

    IEnumerator wait_by_exist_child()
    { //렉 떄문에 0.2초만에 자식위치를 못바꿀경우 반복해야함 count도 새면서
      //20번이상 반복해도 자식이 존재하지 않을 경우 탈출
        yield return new WaitForSeconds(0.2f);

        if(this.transform.GetChild(0) !=null) //자식이 존재할때까지 반복
        {
            if(this.transform.GetChild(0) !=null)
            {
                checkBase(); //이건 카드를 드롭할때만 발동됨
            }
        }
    }

    public void checkBase()
    {  //영웅들의 위치정보를 어디로 옮기지 
        Transform HeroCard = this.transform.GetChild(0);
        long num = (long)HeroCard.GetComponent<CharacterCard>().typeOfHero;
        if(typeOfBase == Slot.FRONT)
        {
            Debug.Log("num is " + num);
            DataController.Instance.hero_0_ID = num;
            PlayerPrefs.SetInt(HeroCard.name, 0);
            Debug.Log("PlayerPrefs.SetInt(HeroCard.name, 0) is " + PlayerPrefs.GetInt(HeroCard.name));
            //DataController.Instance.hero_spawn_0 = 1; // 0 = Front
            //현재 위치(front)와 영웅정보(hero1)을 보내기
            //애초에 위치 정보는 여기서 정할 필요 없지 애초에 배틀매니저에서 정함
        }
        else if(typeOfBase == Slot.MID)
        {
            DataController.Instance.hero_1_ID = num;
            PlayerPrefs.SetInt(HeroCard.name, 1);
            //DataController.Instance.hero_spawn_1 = 1; // 0 = Front
            //현재 위치(front)와 영웅정보(hero1)을 보내기

        }
        else if (typeOfBase == Slot.BACK)
        {
            DataController.Instance.hero_2_ID = num;
            PlayerPrefs.SetInt(HeroCard.name, 2);
        }
        else
        {
            PlayerPrefs.SetInt(HeroCard.name, 3);
        }

        if(this.transform.parent.GetChild(4).childCount == 0)  //0,1,2는 데코 3은 hand
        {
            Debug.Log("this.transform.parent.GetChild(1).name is " + this.transform.parent.GetChild(1).name);
            DataController.Instance.hero_0_ID = 0;  //dummy;
            Debug.Log("this.transform.parent.GetChild(1).childCount == 0 is " + this.transform.parent.GetChild(1).childCount);
        }
        if(this.transform.parent.GetChild(5).childCount == 0)
        {
            DataController.Instance.hero_1_ID = 0;  //dummy;
        }
        if(this.transform.parent.GetChild(6).childCount == 0)
        {
            DataController.Instance.hero_2_ID = 0;  //dummy;
        }
    
        

    }

    public void OnlyDebug()
    {
        Debug.Log("hero_0 : " + DataController.Instance.hero_0_ID);
        Debug.Log("hero_1 : " + DataController.Instance.hero_1_ID);
        Debug.Log("hero_2 : " + DataController.Instance.hero_2_ID);
    }

    public void reset()
    {
        DataController.Instance.hero_0_ID = 0;
        DataController.Instance.hero_1_ID = 0;
        DataController.Instance.hero_2_ID = 0;
    }

    

    

}
