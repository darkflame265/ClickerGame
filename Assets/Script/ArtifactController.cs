using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
                    if(artifact[0].activeSelf == true)  //무한의대검
                    {
                        add_artifact();
                    }
                    else
                    {
                        
                        SetBool("ar0", true);
                        DataController.Instance.MultiplyGoldPerClick(2);
                        //get_artifact_text.text ="유물 흭득";
                        get_artifact_name.text ="무한의 대검";
                        get_artifact_explain.text = "공격시 일정 확률로 적에게 전기를 내리친다.";
                        get_artifact_image.sprite = Resources.Load<Sprite>("Image/Artifact/RPG icons/512X512/ar0") as Sprite;
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
                        DataController.Instance.MultiplyGoldPerClick(2);
                        get_artifact_image.sprite = Resources.Load<Sprite>("Image/Artifact/RPG icons/512X512/ar1") as Sprite;
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
                        DataController.Instance.MultiplyGoldPerClick(2);
                        get_artifact_image.sprite = Resources.Load<Sprite>("Image/Artifact/RPG icons/512X512/ar2") as Sprite;
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
                        DataController.Instance.MultiplyGoldPerClick(2);
                        get_artifact_image.sprite = Resources.Load<Sprite>("Image/Artifact/RPG icons/512X512/ar3") as Sprite;
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
                        DataController.Instance.MultiplyGoldPerClick(2);
                        get_artifact_image.sprite = Resources.Load<Sprite>("Image/Artifact/RPG icons/512X512/ar4") as Sprite;
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
                        DataController.Instance.MultiplyGoldPerClick(2);
                        get_artifact_image.sprite = Resources.Load<Sprite>("Image/Artifact/RPG icons/512X512/ar5") as Sprite;
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
                        DataController.Instance.MultiplyGoldPerClick(2);
                        get_artifact_image.sprite = Resources.Load<Sprite>("Image/Artifact/RPG icons/512X512/ar6") as Sprite;
                        calculation_cost();
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
                        DataController.Instance.MultiplyGoldPerClick(2);
                        get_artifact_image.sprite = Resources.Load<Sprite>("Image/Artifact/RPG icons/512X512/ar7") as Sprite;
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
                        DataController.Instance.MultiplyGoldPerClick(2);
                        get_artifact_image.sprite = Resources.Load<Sprite>("Image/Artifact/RPG icons/512X512/ar8") as Sprite;
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
                        DataController.Instance.MultiplyGoldPerClick(2);
                        get_artifact_image.sprite = Resources.Load<Sprite>("Image/Artifact/RPG icons/512X512/ar9") as Sprite;
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
                        DataController.Instance.MultiplyGoldPerClick(2);
                        get_artifact_image.sprite = Resources.Load<Sprite>("Image/Artifact/RPG icons/512X512/ar10") as Sprite;
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
                        DataController.Instance.MultiplyGoldPerClick(2);
                        get_artifact_image.sprite = Resources.Load<Sprite>("Image/Artifact/RPG icons/512X512/ar11") as Sprite;
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
                        DataController.Instance.MultiplyGoldPerClick(2);
                        get_artifact_image.sprite = Resources.Load<Sprite>("Image/Artifact/RPG icons/512X512/ar12") as Sprite;
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

    public void select_artifact1()
    {
        //hide_select_tool();
       // SelectFrame1.SetActive(true);
        artifact_explanation_text.text = "무한의 대검\n권능\n치명타 확률 100%증가";
    }
    public void select_artifact2()
    {
        artifact_explanation_text.text = "홍옥의 오브\n권능\n클릭 당 흭득 골드량 2배";
    }
    public void select_artifact3()
    {
        artifact_explanation_text.text = "보라색 오브\n권능\n초당 골드 흭득량 10배";
    }
    public void select_artifact4()
    {
        artifact_explanation_text.text = "황금 방사능병\n권능\n초당 골드 흭득량 10배";
    }
    public void select_artifact5()
    {
        artifact_explanation_text.text = "마기가 담긴 병\n권능\n초당 골드 흭득량 10배";
    }

    public void select_artifact6()
    {
        artifact_explanation_text.text = "화염의 병\n권능\n초당 골드 흭득량 10배";
    }

    public void select_artifact7()
    {
        artifact_explanation_text.text = "얼음의 병\n권능\n초당 골드 흭득량 10배";
    }

    public void select_artifact8()
    {
        artifact_explanation_text.text = "죽음의 병\n권능\n초당 골드 흭득량 10배";
    }

    public void select_artifact9()
    {
        artifact_explanation_text.text = "슈퍼 망원경\n권능\n초당 골드 흭득량 10배";
    }

    public void select_artifact10()
    {
        artifact_explanation_text.text = "구원\n권능\n초당 골드 흭득량 10배";
    }

    public void select_artifact11()
    {
        artifact_explanation_text.text = "마나 스톤\n권능\n초당 골드 흭득량 10배";
    }
    public void select_artifact12()
    {
        artifact_explanation_text.text = "풍요의 북\n권능\n초당 골드 흭득량 10배";
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
