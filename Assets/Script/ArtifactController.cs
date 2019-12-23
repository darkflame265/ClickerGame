using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ArtifactController : MonoBehaviour
{
    public GameObject SelectFrame1;
    public static int artifact_size = 13;

    public GameObject[] artifact = new GameObject[artifact_size];

    public static void SetBool(string key, bool value)
    {
        if(value)
            PlayerPrefs.SetInt(key, 1);
        else
            PlayerPrefs.SetInt(key, 0);
    }

    public string []artifact_id = new string[artifact_size];

    public Text get_artifact_text;
    public Image get_artifact_image;
    public Text get_artifact_name;
    public Text get_artifact_explain;


    public Text artifact_explanation_text;

    public int startcost = 100;

    public int artifact_plus = 0;
    public int artifact_multiply = 1; 

    bool full = false;

    List<string> artifact_name = new List<string>();
    List<string> artifact_effect = new List<string>();
    List<string> artifact_image = new List<string>();

    private static ArtifactController instance;

    public static ArtifactController GetInstance()
    {
        if(instance == null)
        {
            instance = FindObjectOfType<ArtifactController>();

            if(instance == null)
            {
                GameObject container = new GameObject("ArtifactController");

                instance = container.AddComponent<ArtifactController>();
            }
        }
        return instance;
    }
    
    void Start()
    {  ///연속으로 뽑으면 가끔 똑같은 유물이 나오고 돈만 빼가는 버그 발생?!
        StartCoroutine("CheckArtifact");
        insert_artifact_name_toList();
        insert_artifact_effect_toList();
        insert_artifact_Image_toList();
    }

    

    IEnumerator CheckArtifact() //artifact_bool값을 프리팹으로 저장하기 
    {
        while(true)
        {
            yield return new WaitForSeconds(0.2f);
            for(int i = 0; i < artifact_size; i++)
            {
                if(PlayerPrefs.GetInt(artifact_id[i]) == 1)
                {
                    artifact[i].SetActive(true);
                }
                else
                {
                    artifact[i].SetActive(false);
                }
            }
            
        }
    }

    public bool check_artifact_full()
    {
        int count = 0;
        for(int i = 0; i < artifact_size; i++)
            {
                if(PlayerPrefs.GetInt(artifact_id[i]) == 1)
                {
                    count = count + 1;
                }
            }

        if(count == artifact_size)
        {
            return true;
        }
        else{
            return false;
        }
    }

    public void calculation_cost()
    {

        if(DataController.Instance.artifact_ticket>= 1)
        {
            DataController.Instance.artifact_ticket--;
            goToPanel.Instance.show_get_artifact_panel();
        }
    }

    public void init_artifact()
    {
        for(int i = 0; i < artifact_size; i++)
        {
            PlayerPrefs.SetInt(artifact_id[i], 0);
        }
        DataController.Instance.artifactCost = 100;
        full = false;
        Debug.Log("Artifact_Size = " + artifact_size);
    }


    public void set_get_artifact_information(int i)
    {
        get_artifact_name.text = artifact_name[i];
        get_artifact_explain.text = artifact_effect[i]; 

        //get_artifact_image.sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;
        //"Image/Artifact/RPG icons/512X512/surihan"
        //string image_address = artifact_image[i];
        get_artifact_image.sprite = Resources.Load<Sprite>(artifact_image[i]) as Sprite;
    }

   
    public void add_artifact()
    {
        
        int itemNum = Random.Range(0, 13); //0~12 13제외
        if(DataController.Instance.artifact_ticket == 0)
        {
            goToPanel.Instance.show_noticePanel();
            goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "유물 뽑기권이 없습니다.";
        }

        if(DataController.Instance.artifact_ticket>= 1)
        {

            if(check_artifact_full())  
            {
                goToPanel.Instance.show_noticePanel();
                goToPanel.Instance.NoticePanel.GetComponentInChildren<Text>().text = "더 이상 뽑을수 있는 유물이 없습니다.";
                full = true;       
            }
            

            else if(full == false)
            {
                Debug.Log("random_artifact: " + itemNum);
                if(itemNum == 0)
                {
                    if(artifact[0].activeSelf == true)  //서리한
                    {
                        add_artifact();
                    }
                    else
                    {
                        
                        SetBool("ar0", true);
                        DataController.Instance.attack += 500;
                        set_get_artifact_information(0);
                        calculation_cost();

                    }
                }
                else if(itemNum == 1)      //심홍의 심장
                {
                    if(artifact[1].activeSelf == true)  
                    {
                        add_artifact();
                    }
                    else
                    {
                        
                        SetBool("ar1", true);
                        DataController.Instance.special += 200;
                        set_get_artifact_information(1);
                        calculation_cost();
                    }
                }
                else if(itemNum == 2)         //보라색 심장
                {
                    if(artifact[2].activeSelf == true)
                    {
                        add_artifact();
                    }
                    else
                    {
                        
                        SetBool("ar2", true);
                        DataController.Instance.special += 200;
                        DataController.Instance.MultiplyGoldPerClick(2);
                        set_get_artifact_information(2);
                        calculation_cost();
                    }
                }
                else if(itemNum == 3)            //재앙을 불러오는 병
                {
                    if(artifact[3].activeSelf == true)
                    {
                        add_artifact();
                    }
                    else
                    {
                        
                        SetBool("ar3", true);
                        DataController.Instance.health += 200;
                        set_get_artifact_information(3);
                        calculation_cost();
                    }
                }
                else if(itemNum == 4)        //마기가 담긴 병
                {
                    if(artifact[4].activeSelf == true)
                    {
                        add_artifact();
                    }
                    else
                    {
                        
                        SetBool("ar4", true);
                        DataController.Instance.health += 200;
                        DataController.Instance.MultiplyGoldPerClick(2);
                        set_get_artifact_information(4);
                        calculation_cost();
                    }
                }
                else if(itemNum == 5)           //불꽃의 병
                {
                    if(artifact[5].activeSelf == true)
                    {
                        add_artifact();
                    }
                    else
                    {
                        
                        SetBool("ar5", true);
                        DataController.Instance.attack += 200;
                        set_get_artifact_information(5);
                        calculation_cost();
                    }
                }
                else if(itemNum == 6)         //얼음의 병
                {
                    if(artifact[6].activeSelf == true)
                    {
                        add_artifact();
                    }
                    else
                    {
                        
                        SetBool("ar6", true);
                        DataController.Instance.attack += 200;
                        DataController.Instance.MultiplyGoldPerClick(2);
                        set_get_artifact_information(6);
                    }
                }
                else if(itemNum == 7)           //죽음의 병
                {
                    if(artifact[7].activeSelf == true)
                    {
                        add_artifact();
                    }
                    else
                    {
                        
                        SetBool("ar7", true);
                        DataController.Instance.attack += 100;
                        set_get_artifact_information(7);
                        calculation_cost();
                    }
                }
                else if(itemNum == 8)         //모험의 망원경
                {
                    if(artifact[8].activeSelf == true)
                    {
                        add_artifact();
                    }
                    else
                    {
                        
                        SetBool("ar8", true);
                        DataController.Instance.attack += 100;
                        set_get_artifact_information(8);
                        calculation_cost();
                    }
                }
                else if(itemNum == 9)        //구원
                {
                    if(artifact[9].activeSelf == true)
                    {
                        add_artifact();
                    }
                    else
                    {
                        
                        SetBool("ar9", true);
                        DataController.Instance.health += 100;
                        set_get_artifact_information(9);
                        calculation_cost();
                    }
                }
                else if(itemNum == 10)          //마나 스톤
                {
                    if(artifact[10].activeSelf == true)
                    {
                        add_artifact();
                    }
                    else
                    {
                        
                        SetBool("ar10", true);
                        DataController.Instance.health += 100;
                        set_get_artifact_information(10);
                        calculation_cost();
                    }
                }
                else if(itemNum == 11)           //풍요의 북
                {
                    if(artifact[11].activeSelf == true)
                    {
                        add_artifact();
                    }
                    else
                    {
                        
                        SetBool("ar11", true);
                        DataController.Instance.special += 100;
                        set_get_artifact_information(11);
                        calculation_cost();
                    }
                }
                else if(itemNum == 12)           //풍요의 북
                {
                    if(artifact[12].activeSelf == true)
                    {
                        add_artifact();
                    }
                    else
                    {
                        
                        SetBool("ar12", true);
                        DataController.Instance.special += 100;
                        set_get_artifact_information(12);
                        calculation_cost();
                    }
                }
                else{
                    add_artifact();
                    Debug.Log("에러남");
                }
            }
        }
    }

    public void hide_select_tool()  //아직은 안씀
    {
        SelectFrame1.SetActive(false);
    }

    public void select_artifact0()
    {
        artifact_explanation_text.text = "0번유물\n권능\n치명타 확률 100%증가";
    }

    void insert_artifact_name_toList()
    {
        artifact_name.Add("서리한");

        artifact_name.Add("루미네트");
        artifact_name.Add("마나 어비스");
        artifact_name.Add("돔");
        artifact_name.Add("드래곤 아머");
        artifact_name.Add("블러드 핼버드");
        artifact_name.Add("극살 도끼");

        artifact_name.Add("천둥 도끼");
        artifact_name.Add("황금 석궁");
        artifact_name.Add("로미에 투구");
        artifact_name.Add("청룡 투구");
        artifact_name.Add("마나 링IV");
        artifact_name.Add("피어드 링");
    }

    void insert_artifact_effect_toList()
    {
        //상급
        artifact_effect.Add("공격력 +500, 골드 흭득량 2배");

        //중급
        artifact_effect.Add("스킬공격력 +200");                     //스킬 2번 발동
        artifact_effect.Add("스킬공격력 +200, 골드 흭득량 2배");
        artifact_effect.Add("체력 +200");                       //쉴드 추가
        artifact_effect.Add("체력 +200, 골드 흭득량 2배");
        artifact_effect.Add("공격력 +200");                     //일정확률로 공격 2번
        artifact_effect.Add("공격력 +200, 골드 흭득량 2배");

        //하급
        artifact_effect.Add("공격력 +100"); 
        artifact_effect.Add("공격력 +100");
        artifact_effect.Add("체력 +100");
        artifact_effect.Add("체력 +100");
        artifact_effect.Add("스킬공격력 +100");
        artifact_effect.Add("스킬공격력 +100");
    }

    void insert_artifact_Image_toList()
    {
        artifact_image.Add("Image/Artifact/RPG icons/512X512/surihan");
        artifact_image.Add("Image/Artifact/RPG icons/512X512/rubynate");
        artifact_image.Add("Image/Artifact/RPG icons/512X512/mana_abyss");
        artifact_image.Add("Image/Artifact/RPG icons/512X512/doom");
        artifact_image.Add("Image/Artifact/RPG icons/512X512/dragon_armor");
        artifact_image.Add("Image/Artifact/RPG icons/512X512/blood_helverd");
        artifact_image.Add("Image/Artifact/RPG icons/512X512/murder_axe");
        artifact_image.Add("Image/Artifact/RPG icons/512X512/thunder_axe");
        artifact_image.Add("Image/Artifact/RPG icons/512X512/gold_crossbow");
        artifact_image.Add("Image/Artifact/RPG icons/512X512/romie_helmet");
        artifact_image.Add("Image/Artifact/RPG icons/512X512/seaDragon_helmet");
        artifact_image.Add("Image/Artifact/RPG icons/512X512/mana_ringIV");
        artifact_image.Add("Image/Artifact/RPG icons/512X512/feered_ring");
    }


    public void show_artifact_inform()
    {
        for(int i = 0; i <= artifact.Length-1; i++)
        {
            if(EventSystem.current.currentSelectedGameObject == artifact[i])
            {
                artifact_explanation_text.text = artifact_name[i] + "\n효과: " + artifact_effect[i];
            }
        }
    }

 
    public void MyPosition (Transform transform)
    {
        var tra = transform;
        Debug.Log("Clicked button pos:" + tra.position);
        SelectFrame1.SetActive(true);
        SelectFrame1.transform.position = tra.position;
        SelectFrame1.transform.parent = tra.transform;
    }


   
}
