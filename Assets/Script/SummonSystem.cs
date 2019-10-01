using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonSystem : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject[]hero = new GameObject[0];

    int result = 0;

    public GameObject prefab_floating_text;
    public Transform tranform_canvas;

    public Inventory inventory;

    void Start()
    {
        StartCoroutine("check_char_exist");
    }

    public IEnumerator check_char_exist()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            for(int i =0; i < hero.Length; i++)
            {//enum형식에 for 적용하려고 하는데 모르겠따.
                hero_list a = (hero_list)i;
                if(PowerController.check_list.Load(a.ToString()) == 1)
                {
                    //Debug.Log("answer is " + a.ToString());
                    hero[i].SetActive(true);
                }
                else
                {
                    hero[i].SetActive(false);
                    //hero[i].transform.SetParent(Hand);
                    //Debug.Log("Instpertor length:  " + power.Length);
                }
            }
        }
    }

    public enum hero_list
    {
        Knight,
        Archer
    }

    public void select_rank()
    {
        int number = Random.Range(1, 1001); //1~1000 사이의 숫자
         // 0=에러 1=C 2=B 3=A 4=S 5=??

        if(number > 0 && number <=600) //60%
        {
            result = 1; // 4가지 아이템
        }
        else if(number > 0 && number <=900) // 30%
        {
            result = 2; // 경험치 포션
        }
        else if(number > 0 && number <=990) // 9%
        {
            result = 3; // 영웅 (다차면 포션으로 대체)
        }
        else if(number > 0 && number <=1000) // 1%
        {
            result = 4; // 가호
        }
        else
        {
            Debug.Log("error");
        }
    }

    public void common_summon()
    {
        select_rank();
        
        if(result == 1)
        {
            SoundManager.Instance.click_get_item_sound();
            int i =Random.Range(0,4); // 0~3

            inventory.AddItem(i);
            string item = inventory.database.FetchItemByID(i).Title;
            Sprite image = inventory.database.FetchItemByID(i).Sprite;

            var clone = Instantiate(prefab_floating_text, new Vector3(-7, 0), Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingText>().text.text = "  " + item;
            clone.transform.SetParent(tranform_canvas);

            clone.transform.GetChild(0).gameObject.SetActive(true);
            clone.GetComponentInChildren<Image>().sprite = image;
        }

        if(result == 2)
        {
            SoundManager.Instance.click_get_item_sound();
            int i = 4;

            inventory.AddItem(i);
            string item = inventory.database.FetchItemByID(i).Title;
            Sprite image = inventory.database.FetchItemByID(i).Sprite;

            var clone = Instantiate(prefab_floating_text, new Vector3(-7, 0), Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingText>().text.text = "            " + item;
            clone.transform.SetParent(tranform_canvas);

            clone.transform.GetChild(0).gameObject.SetActive(true);
            clone.GetComponentInChildren<Image>().sprite = image;
        }

        if(result == 3)
        {
            int count = 0;
            while(true)
            {
                //C_power_list asdsd = (C_power_list)1;
                hero_list ThisResult_C = (hero_list)Random.Range(0,3); // 0~2
                bool get = false;
                
                for(int i = 0; i < 3; i++)
                {
                    if(ThisResult_C == (hero_list)i)
                    {
                        hero_list b = (hero_list)i;
                        if(PowerController.check_list.Load(b.ToString()) == 0) //새로운걸 얻을떄
                            {
                                if(i == 0) //knight
                                {
                                    PowerController.check_list.Save(b.ToString(), 1);
                                    get = true;
                                    Debug.Log("0");
                                    var clone = Instantiate(prefab_floating_text, new Vector3(-7, 0), Quaternion.Euler(Vector3.zero));
                                    clone.GetComponent<FloatingText>().text.text = "knight 흭득";
                                    clone.transform.SetParent(tranform_canvas);
                                    break;
                                }
                                if(i == 1) //archer 
                                {

                                    PowerController.check_list.Save(b.ToString(), 1);
                                    get = true;
                                    Debug.Log("1");
                                    var clone = Instantiate(prefab_floating_text, new Vector3(-7, 0), Quaternion.Euler(Vector3.zero));
                                    clone.GetComponent<FloatingText>().text.text = "Archer 흭득";
                                    clone.transform.SetParent(tranform_canvas);
                                    break;
                                }
                                if(i == 2) //eClickExp_300,
                                {
                                    get =true;
                                    var clone = Instantiate(prefab_floating_text, new Vector3(-7, 0), Quaternion.Euler(Vector3.zero));
                                    clone.GetComponent<FloatingText>().text.text = "꽝";
                                    clone.transform.SetParent(tranform_canvas);
                                    
                                    break;
                                }
                            }
                    } 
                }
                count++;
                if(get == true)
                {
                    break;
                }
                if(count == 50)
                {
                    result = result + 1; //4개 전부다 보유중이므로 다른거 
                    Debug.Log("네 개 다 보유중");
                    break;
                }
            }
        }
    }

    public void rare_summon()
    {
        int number = Random.Range(1, 1001); //1~1000 사이의 숫자
         // 0=에러 1=C 2=B 3=A 4=S 5=??

        if(number > 0 && number <=600) //60%
        {
            result = 1; // 경험치 포션
        }
        else if(number > 0 && number <=900) // 30%
        {
            result = 2; // 영웅 (다차면 포션으로 대체)
        }
        else if(number > 0 && number <=990) // 10%
        {
            result = 3; // 가호
        }
        
        if(result == 1)
        {
            SoundManager.Instance.click_get_item_sound();
            int i =Random.Range(0,4); // 0~3

            inventory.AddItem(i);
            string item = inventory.database.FetchItemByID(i).Title;
            Sprite image = inventory.database.FetchItemByID(i).Sprite;

            var clone = Instantiate(prefab_floating_text, new Vector3(-7, 0), Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingText>().text.text = "  " + item;
            clone.transform.SetParent(tranform_canvas);

            clone.transform.GetChild(0).gameObject.SetActive(true);
            clone.GetComponentInChildren<Image>().sprite = image;
        }

        if(result == 2)
        {
            SoundManager.Instance.click_get_item_sound();
            int i = 4;

            inventory.AddItem(i);
            string item = inventory.database.FetchItemByID(i).Title;
            Sprite image = inventory.database.FetchItemByID(i).Sprite;

            var clone = Instantiate(prefab_floating_text, new Vector3(-7, 0), Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingText>().text.text = "            " + item;
            clone.transform.SetParent(tranform_canvas);

            clone.transform.GetChild(0).gameObject.SetActive(true);
            clone.GetComponentInChildren<Image>().sprite = image;
        }

        if(result == 3)
        {
            int count = 0;
            while(true)
            {
                //C_power_list asdsd = (C_power_list)1;
                hero_list ThisResult_C = (hero_list)Random.Range(0,3); // 0~2
                bool get = false;
                
                for(int i = 0; i < 3; i++)
                {
                    if(ThisResult_C == (hero_list)i)
                    {
                        hero_list b = (hero_list)i;
                        if(PowerController.check_list.Load(b.ToString()) == 0) //새로운걸 얻을떄
                            {
                                if(i == 0) //knight
                                {
                                    PowerController.check_list.Save(b.ToString(), 1);
                                    get = true;
                                    Debug.Log("0");
                                    var clone = Instantiate(prefab_floating_text, new Vector3(-7, 0), Quaternion.Euler(Vector3.zero));
                                    clone.GetComponent<FloatingText>().text.text = "knight 흭득";
                                    clone.transform.SetParent(tranform_canvas);
                                    break;
                                }
                                if(i == 1) //archer 
                                {

                                    PowerController.check_list.Save(b.ToString(), 1);
                                    get = true;
                                    Debug.Log("1");
                                    var clone = Instantiate(prefab_floating_text, new Vector3(-7, 0), Quaternion.Euler(Vector3.zero));
                                    clone.GetComponent<FloatingText>().text.text = "Archer 흭득";
                                    clone.transform.SetParent(tranform_canvas);
                                    break;
                                }
                                if(i == 2) //eClickExp_300,
                                {
                                    get =true;
                                    var clone = Instantiate(prefab_floating_text, new Vector3(-7, 0), Quaternion.Euler(Vector3.zero));
                                    clone.GetComponent<FloatingText>().text.text = "꽝";
                                    clone.transform.SetParent(tranform_canvas);
                                    
                                    break;
                                }
                            }
                    } 
                }
                count++;
                if(get == true)
                {
                    break;
                }
                if(count == 50)
                {
                    result = result + 1; //4개 전부다 보유중이므로 다른거 
                    Debug.Log("네 개 다 보유중");
                    break;
                }
            }
        }
    }
    



    public void initiaze_hero()
    {
        for (int i = 0; i < 2; i++) //초기화 기능 넣기
        {
            hero_list a = (hero_list)i;
            if(PowerController.check_list.Load(a.ToString()) == 1)
            {
                if(i == 0)
                {
                    PowerController.check_list.Save(a.ToString(), 0);
                    Debug.Log("knight초기화!!");
                }
            }
            if(PowerController.check_list.Load(a.ToString()) == 1)
            {
                if(i == 1)
                {
                    PowerController.check_list.Save(a.ToString(), 0);
                    Debug.Log("archer초기화!!");
                }
            }
        }
    }

    public void debug()
    {
        for(int i = 0; i < 2; i++)
        {
            hero_list a = (hero_list)i;
            Debug.Log(PowerController.check_list.Load(a.ToString()));
        }
    }

    
}
