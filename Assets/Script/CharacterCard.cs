using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CharacterCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler//, IDropHandler
{
    public Transform location;
    public Transform parentToReturnTo = null;
    public bool exist = false;


    //이 캐릭터를 소유하여서 카드가 보이는가
    //캐릭터카드를 통해 데이터베이스에 있는 영웅정보를 가져온다.

    public enum Slot { dummy, knight, archer, wizard };
    public Slot typeOfHero;
    public Transform Front;
    public Transform Mid;
    public Transform Back;
    public Transform Hand;

    public string hero_name;

    public int cardNum = 0;


    
    private static CharacterCard instance;

    public static CharacterCard Instance
    {
        get{
            if(instance == null)
            {
                instance = FindObjectOfType<CharacterCard>();

                if(instance == null)
                {
                    GameObject container = new GameObject("CharacterCard");

                    instance = container.AddComponent<CharacterCard>();
                }
            }
            return instance;
        }
    }

    void Start()
    {                                   // 0,1,2, 는 카드 데코레이션 자리
        Hand = this.transform.parent.parent.GetChild(3);
        Front = this.transform.parent.parent.GetChild(4);
        Mid = this.transform.parent.parent.GetChild(5);
        Back = this.transform.parent.parent.GetChild(6);
        
        hero_name = this.transform.name;
        if(hero_name == "KnightCard")
        {
            cardNum = 1;
        }
        else if(hero_name == "ArcherCard")
        {
            cardNum = 2;
        }
        else if(hero_name == "WizardCard")
        {
            cardNum = 3;
        } 
        else {
            cardNum = 0;
        }
        SetLocation();

        //StartCoroutine("check_char_exist");
    }

    public void SetLocation()
    {
        if(DataController.Instance.hero_0_ID == cardNum)
        {
            this.transform.SetParent(Front);
        }
        else if(DataController.Instance.hero_1_ID == cardNum)
        {
            this.transform.SetParent(Mid);
        }
        else if(DataController.Instance.hero_2_ID == cardNum)
        {
            this.transform.SetParent(Back);
        }
        else 
        {
            this.transform.SetParent(Hand);
        }
    }


    


    public void OnBeginDrag(PointerEventData eventData)
    { 
        //Debug.Log("begin drag");
        parentToReturnTo = this.transform.parent;
        //PlayerPrefs
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
        //Debug.Log("enum count is "+System.Enum.GetValues(typeof(Slot)).Length);
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition); 

        Vector2 touchPos = new Vector2(wp.x, wp.y); 

        this.transform.position = touchPos;

            
        //this.transform.position = transform.position; // - offset

        
    
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("end drag");
        this.transform.SetParent(parentToReturnTo);
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 700f);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    /* 
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("drop");
    }
    */
}
