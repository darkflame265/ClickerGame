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

    public enum Slot { dummy, knight, archer, mage };
    public Slot typeOfHero;
    public Transform Front;
    public Transform Mid;
    public Transform Back;
    public Transform Hand;

    public string hero_name;

    
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
    {
        Hand = this.transform.parent.parent.GetChild(0);
        Front = this.transform.parent.parent.GetChild(1);
        Mid = this.transform.parent.parent.GetChild(2);
        Back = this.transform.parent.parent.GetChild(3);
        
        hero_name = this.transform.name;
        SetLocation();

        StartCoroutine("check_char_exist");
    }

    void SetLocation()
    {
        if(PlayerPrefs.GetInt(hero_name) == 0) //0 = front
        {
            this.transform.SetParent(Front);
            Debug.Log(this.transform.name + " is in "+ Front.name);
        }
        if(PlayerPrefs.GetInt(hero_name) == 1) //0 = front
        {
            this.transform.SetParent(Mid);
            Debug.Log(this.transform.name + " is in "+ Mid.name);
        }
        if(PlayerPrefs.GetInt(hero_name) == 2) //0 = front
        {
            this.transform.SetParent(Back);
            Debug.Log(this.transform.name + " is in "+ Back.name);
        }
        else
        {
            //this.transform.SetParent(Hand);
            Debug.Log(this.transform.name + " is in "+ Hand.name);
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

        transform.position = touchPos;
            
        this.transform.position = transform.position; // - offset
        
    
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("end drag");
        this.transform.SetParent(parentToReturnTo);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    /* 
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("drop");
    }
    */
}
