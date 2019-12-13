using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerController : MonoBehaviour
{
    public GameObject SelectFrame;

    public GameObject[]power = new GameObject[0];
    public GameObject[]before_power = new GameObject[0];


    public GameObject powerExplainPanel;  // floatingText 위치 전용
    public Text PowerExplain;

    public GameObject prefab_floating_text;

    public string power_name;
    public string power_grade;

    int result = 0;

    private static PowerController instance;

    public static PowerController Instance
    {
        get{
            if(instance == null)
            {
                instance = FindObjectOfType<PowerController>();

                if(instance == null)
                {
                    GameObject container = new GameObject("PowerController");

                    instance = container.AddComponent<PowerController>();
                }
            }
            return instance;
        }
    }
    

    void Start()
    {
        StartCoroutine("check_power");
    }

    IEnumerator check_power()
    {
        
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            for(int i =0; i < power.Length; i++)
            {//enum형식에 for 적용하려고 하는데 모르겠따.
                power_list a = (power_list)i;
                if(check_list.Load(a.ToString()) == 1)
                {
                    //Debug.Log("answer is " + a.ToString());
                    power[i].SetActive(true);
                    before_power[i].SetActive(false);
                }
                else
                {
                    power[i].SetActive(false);
                    before_power[i].SetActive(true);
                    //Debug.Log("Instpertor length:  " + power.Length);
                }
            }
        }
        
    }

    

    public enum power_list
    {
        eHealth_100,  //0
        eAttack_100,
        eSpeed_100,
        eSkill_100,

        eHealth_300,  //0
        eAttack_300,
        eSpeed_300,
        eSkill_300,

        eHealth_500,  //0
        eAttack_500,
        eSpeed_500,
        eSkill_500,

        eHealth_1000,  //0
        eAttack_1000,
        eSpeed_1000,
        eSkill_1000,

        eWarrior,
        eArcher,
        eWizard,
        eGoldHand,
        eTimeStop,
        
    }
    
//PlayerPrefs.GetString("reincarnation_count");
    public class check_list{
        public static void Save (string name, int value)
        {
            PlayerPrefs.SetInt(name, value);
        }

        public static int Load (string name)
        {
            if(!PlayerPrefs.HasKey(name)) // 골드가 없을떄
            {
                return 0;
            }

            return PlayerPrefs.GetInt(name);
        }
    }

    public int return_power_list(int i)
    {
        power_list a = (power_list)i;
        return check_list.Load(a.ToString());
    }

    

    public void initiaze_power()
    {
        for (int i = 0; i < 21; i++) //초기화 기능 넣기
        {
            power_list a = (power_list)i;
            if(check_list.Load(a.ToString()) == 1)
            {
                if(i == 0)
                {
                    DataController.Instance.health -= 100;
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("S등급 보너스스탯 50배 제거 완료!!");
                }
                if(i == 1)
                {
                    DataController.Instance.attack -= 100;
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("S등급 클릭골드 100배 제거 완료!!");
                }
                if(i == 2)
                {
                    DataController.Instance.mana -= 100;
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("S등급 클릭경험치 300배 제거 완료!!");
                }
                if(i == 3)
                {
                    DataController.Instance.special -= 100;
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("S등급 데미지 1000배 제거 완료!!");
                }
                if(i == 4)
                {
                    DataController.Instance.health -= 300;
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("A등급 보너스스탯 20배 제거 완료!!");
                }
                if(i == 5)
                {
                    DataController.Instance.attack -= 300;
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("A등급 클릭골드 40배 제거 완료!!");
                }
                if(i == 6)
                {
                    DataController.Instance.mana -= 300;
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("A등급 클릭경험치 100배 제거 완료!!");
                }
                if(i == 7)
                {
                    DataController.Instance.special -= 300;
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("A등급 데미지 300배 제거 완료!!");
                }
                if(i == 8)
                {
                    DataController.Instance.health -= 500;
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("B등급 보너스스탯 8배 제거 완료!!");
                }
                if(i == 9)
                {
                    DataController.Instance.attack -= 500;
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("B등급 클릭골드 16배 제거 완료!!");
                }
                if(i == 10)
                {
                    DataController.Instance.mana -= 500;
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("B등급 클릭경험치 40배 제거 완료!!");
                }
                if(i == 11)
                {
                    DataController.Instance.special -= 500;
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("B등급 데미지 100배 제거 완료!!");
                }
                if(i == 12)
                {
                    DataController.Instance.health -= 1000;
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("C등급 보너스스탯 3배 제거 완료!!");
                }
                if(i == 13)
                {
                    DataController.Instance.attack -= 1000;;
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("C등급 클릭골드 6배 제거 완료!!");
                }
                if(i == 14)
                {
                    DataController.Instance.mana -= 1000;
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("C등급 클릭경험치 16배 제거 완료!!");
                }
                if(i == 15)
                {
                    DataController.Instance.special -= 1000;
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("C등급 데미지 40배 제거 완료!!");
                }
                if(i == 16)
                {
                    check_list.Save(a.ToString(), 0);
                }
                if(i == 17)
                {
                    check_list.Save(a.ToString(), 0);
                }
                if(i == 18)
                {
                    check_list.Save(a.ToString(), 0);
                }
                if(i == 19)
                {
                    check_list.Save(a.ToString(), 0);
                }
                if(i == 20)
                {
                    check_list.Save(a.ToString(), 0);
                }
            }
        }
    }


    public void select_rank()
    {
        int number = Random.Range(1, 1001); //1~1000 사이의 숫자
         // 0=에러 1=C 2=B 3=A 4=S 5=??

        if(number > 0 && number <=400) //40%
        {
            result = 1; // C등급
        }
        else if(number > 0 && number <=700) // 30%
        {
            result = 2; // B등급
        }
        else if(number > 0 && number <=900) // 20%
        {
            result = 3; // A등급
        }
        else if(number > 0 && number <=1000) // 10%
        {
            result = 4; // S등급
        }
    }

    public void choose_power()
    {
        select_rank();
        if(DataController.Instance.power_ticket >= 1)
        {
            if(result == 1)
            {
                int count = 0;
                while(true)
                {
                    //C_power_list asdsd = (C_power_list)1;
                    power_list ThisResult_C = (power_list)Random.Range(0, 4); // 0~3
                    bool get = false;
                    
                    for(int i = 0; i < 4; i++)
                    {
                        
                        if(ThisResult_C == (power_list)i)
                        {
                            power_list b = (power_list)i;
                            if(check_list.Load(b.ToString()) == 0) //새로운걸 얻을떄
                                {
                                    if(i == 0) //eBonusState_50,
                                    {
                                        DataController.Instance.health += 100;
                                        check_list.Save(b.ToString(), 1);
                                        get = true;
                                        power_name = "체력 스탯 +100";
                                        power_grade = "C";
                                        show_floating_text();
                                        break;
                                    }
                                    if(i == 1) //eClickGold_100,  
                                    {
                                        DataController.Instance.attack += 100;
                                        check_list.Save(b.ToString(), 1);
                                        get = true;
                                        power_name = "공격력 스탯 +100";
                                        power_grade = "C";
                                        show_floating_text();
                                        break;
                                    }
                                    if(i == 2) //eClickExp_300,
                                    {
                                        DataController.Instance.mana += 100;
                                        check_list.Save(b.ToString(), 1);
                                        get =true;
                                        power_name = "민첩 스탯 +100";
                                        power_grade = "C";
                                        show_floating_text();
                                        break;
                                    }
                                    if(i == 3) //eDamage_1000,
                                    {
                                        DataController.Instance.special += 100;
                                        check_list.Save(b.ToString(), 1);
                                        get = true;
                                        power_name = "스킬공격력 스탯 +100";
                                        power_grade = "C";
                                        show_floating_text();
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

            if(result == 2)
            {
                int count = 0;
                while(true)
                {
                    //C_power_list asdsd = (C_power_list)1;
                    power_list ThisResult_C = (power_list)Random.Range(4,8); // 0~3
                    bool get = false;
                    
                    for(int i = 4; i < 8; i++)
                    {
                        
                        if(ThisResult_C == (power_list)i)
                        {
                            power_list b = (power_list)i;
                            if(check_list.Load(b.ToString()) == 0) //새로운걸 얻을떄
                                {
                                    if(i == 4) //eBonusState_50,
                                    {
                                        DataController.Instance.health += 300;
                                        check_list.Save(b.ToString(), 1);
                                        get = true;
                                        power_name = "체력 스탯 +300";
                                        power_grade = "B";
                                        show_floating_text();
                                        break;
                                    }
                                    if(i == 5) //eClickGold_100,  
                                    {
                                        DataController.Instance.attack += 300;
                                        check_list.Save(b.ToString(), 1);                   
                                        get = true;
                                        power_name = "공격력 스탯 +300";
                                        power_grade = "B";
                                        show_floating_text();
                                        break;
                                    }
                                    if(i == 6) //eClickExp_300,
                                    {
                                        DataController.Instance.mana += 300;
                                        check_list.Save(b.ToString(), 1);
                                        get =true;
                                        power_name = "민첩 스탯 +300";
                                        power_grade = "B";
                                        show_floating_text();
                                        break;
                                    }
                                    if(i == 7) //eDamage_1000,
                                    {
                                        DataController.Instance.special += 300;
                                        check_list.Save(b.ToString(), 1);
                                        get = true;
                                        power_name = "스킬 공격력 스탯 +300";
                                        power_grade = "B";
                                        show_floating_text();
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

            if(result == 3)
            {
                int count = 0;
                while(true)
                {
                    //C_power_list asdsd = (C_power_list)1;
                    power_list ThisResult_C = (power_list)Random.Range(8,12); // 0~3
                    bool get = false;
                    
                    for(int i = 8; i < 12; i++)
                    {
                        
                        if(ThisResult_C == (power_list)i)
                        {
                            power_list b = (power_list)i;
                            if(check_list.Load(b.ToString()) == 0) //새로운걸 얻을떄
                                {
                                    if(i == 8) //eBonusState_50,
                                    {
                                        DataController.Instance.health += 500;
                                        check_list.Save(b.ToString(), 1);
                                        get = true;
                                        power_name = "체력 스탯 +500";
                                        power_grade = "A";
                                        show_floating_text();
                                        break;
                                    }
                                    if(i == 9) //eClickGold_100,  
                                    {
                                        DataController.Instance.attack += 500;
                                        check_list.Save(b.ToString(), 1);
                                        get = true;
                                        power_name = "공격력 스탯 +500";
                                        power_grade = "A";
                                        show_floating_text();
                                        break;
                                    }
                                    if(i == 10) //eClickExp_300,
                                    {
                                        DataController.Instance.mana += 500;
                                        check_list.Save(b.ToString(), 1);
                                        get =true;
                                        power_name = "민첩 스탯 +500";
                                        power_grade = "A";
                                        show_floating_text();
                                        break;
                                    }
                                    if(i == 11) //eDamage_1000,
                                    {
                                        DataController.Instance.special += 500;
                                        check_list.Save(b.ToString(), 1);
                                        get = true;
                                        power_name = "스킬 공격력 스탯 +500";
                                        power_grade = "A";
                                        show_floating_text();
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
            
            if(result == 4)
            {
                int count = 0;
                while(true)
                {
                    //C_power_list asdsd = (C_power_list)1;
                    power_list ThisResult_C = (power_list)Random.Range(12,21); // 0~3
                    bool get = false;
                    
                    for(int i = 12; i < 21; i++)
                    {
                        
                        if(ThisResult_C == (power_list)i)
                        {
                            power_list b = (power_list)i;
                            if(check_list.Load(b.ToString()) == 0) //새로운걸 얻을떄
                                {
                                    if(i == 12) //eBonusState_50,
                                    {
                                        DataController.Instance.health += 1000;
                                        check_list.Save(b.ToString(), 1);
                                        get = true;
                                        power_name = "체력 스탯 +1000";
                                        power_grade = "S";
                                        show_floating_text();
                                        break;
                                    }
                                    if(i == 13) //eClickGold_100,  
                                    {
                                        DataController.Instance.attack += 1000;
                                        check_list.Save(b.ToString(), 1);
                                        get = true;
                                        power_name = "공격력 스탯 +1000";
                                        power_grade = "S";
                                        show_floating_text();
                                        break;
                                    }
                                    if(i == 14) //eClickExp_300,
                                    {
                                        DataController.Instance.mana += 1000;
                                        check_list.Save(b.ToString(), 1);
                                        get =true;
                                        power_name = "민첩 스탯 +1000";
                                        power_grade = "S";
                                        show_floating_text();
                                        break;
                                    }
                                    if(i == 15) //eDamage_1000,
                                    {
                                        DataController.Instance.special += 1000;
                                        check_list.Save(b.ToString(), 1);
                                        get = true;
                                        power_name = "스킬 공격력 스탯 +1000";
                                        power_grade = "S";
                                        show_floating_text();
                                        break;
                                    }
                                    if(i == 16) //warrior
                                    {
                                        check_list.Save(b.ToString(), 1);
                                        get = true;
                                        power_name = "전사의 권능";
                                        power_grade = "S";
                                        show_floating_text();
                                        break;
                                    }
                                    if(i == 17) //archer
                                    {
                                        check_list.Save(b.ToString(), 1);
                                        get = true;
                                        power_name = "궁수의 권능";
                                        power_grade = "S";
                                        show_floating_text();
                                        break;
                                    }
                                    if(i == 18) //wizard
                                    {
                                        check_list.Save(b.ToString(), 1);
                                        get = true;
                                        power_name = "마법사의 권능";
                                        power_grade = "S";
                                        show_floating_text();
                                        break;
                                    }
                                    if(i == 19) //goldhand
                                    {
                                        DataController.Instance.MultiplyGoldPerClick(50);
                                        check_list.Save(b.ToString(), 1);
                                        get = true;
                                        power_name = "황금의 손";
                                        power_grade = "S";
                                        show_floating_text();
                                        break;
                                    }
                                    if(i == 20) //timestop
                                    {
                                        check_list.Save(b.ToString(), 1);
                                        get = true;
                                        power_name = "시간의 권능";
                                        power_grade = "S";
                                        show_floating_text();
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
            DataController.Instance.power_ticket--;
        }
        

        Debug.Log("result =  " + result);
        //DataController.Instance.goldPerClick = DataController.Instance.level * DataController.Instance.besu;
    }



    void show_floating_text()
    {
        var clone = Instantiate(prefab_floating_text, new Vector3(0, 2), Quaternion.Euler(Vector3.zero));
        if(power_grade == "C")
        {
            clone.GetComponent<Text>().color = Color.gray;
            clone.GetComponent<Outline>().effectColor = Color.black;
        }
        else if(power_grade == "B")
        {
            clone.GetComponent<Text>().color = Color.green;
            clone.GetComponent<Outline>().effectColor = Color.green;
        }
        else if(power_grade == "A")
        {
            clone.GetComponent<Text>().color = Color.red;
            clone.GetComponent<Outline>().effectColor = Color.red;
        }
        else if(power_grade == "S")
        {
            clone.GetComponent<Text>().color = Color.yellow;
            clone.GetComponent<Outline>().effectColor = Color.black;
        }
        else {
            clone.GetComponent<Text>().color = Color.gray;
            clone.GetComponent<Outline>().effectColor = Color.black;
        }
        clone.GetComponent<FloatingText>().destroyTime = 3f;
        clone.GetComponent<Text>().text = power_name;
        clone.transform.SetParent(powerExplainPanel.transform);

    }

    public void only_debug()
    {
        //power_list ThisResult_C = (power_list)Random.Range(0,4); // 0~3
        //Debug.Log("ThisResult_C:  " + ThisResult_C);

        for(int i = 0; i < power.Length; i ++)
        {
            power_list b = (power_list)i;
            Debug.Log("power " + i + "    " + check_list.Load(b.ToString()));
        }
    }

    public void ticket_charge()
    {
        DataController.Instance.artifact_ticket = 100;
        DataController.Instance.power_ticket = 100;
    }






    public void select_power_C_Health()
    {
        PowerExplain.text = "체력 스탯을 100만큼 올려줍니다.\n";
    }

    public void select_power_C_Attack()
    {
        PowerExplain.text = "공격력 스탯을 100만큼 올려줍니다.\n";
    }

    public void select_power_C_Speed()
    {
        PowerExplain.text = "민첩 스탯을 100만큼 올려줍니다.\n";
    }

    public void select_power_C_Skill()
    {
        PowerExplain.text = "스킬 공격력 스탯을 100만큼 올려줍니다.\n";
    }

    public void select_power_B_Health()
    {
        PowerExplain.text = "체력 스탯을 300만큼 올려줍니다.\n";
    }

    public void select_power_B_Attack()
    {
        PowerExplain.text = "공격력 스탯을 300만큼 올려줍니다.\n";
    }

    public void select_power_B_Speed()
    {
        PowerExplain.text = "민첩 스탯을 300만큼 올려줍니다.\n";
    }

    public void select_power_B_Skill()
    {
        PowerExplain.text = "스킬 공격력 스탯을 300만큼 올려줍니다.\n";
    }

    public void select_power_A_Health()
    {
        PowerExplain.text = "체력 스탯을 500만큼 올려줍니다.\n";
    }

    public void select_power_A_Attack()
    {
        PowerExplain.text = "공격력 스탯을 500만큼 올려줍니다.\n";
    }

    public void select_power_A_Speed()
    {
        PowerExplain.text = "민첩 스탯을 500만큼 올려줍니다.\n";
    }

    public void select_power_A_Skill()
    {
        PowerExplain.text = "스킬 공격력 스탯을 500만큼 올려줍니다.\n";
    }

    public void select_power_S_Health()
    {
        PowerExplain.text = "체력 스탯을 1000만큼 올려줍니다.\n";
    }

    public void select_power_S_Attack()
    {
        PowerExplain.text = "공격력 스탯을 1000만큼 올려줍니다.\n";
    }

    public void select_power_S_Speed()
    {
        PowerExplain.text = "민첩 스탯을 1000만큼 올려줍니다.\n";
    }

    public void select_power_S_Skill()
    {
        PowerExplain.text = "스킬 공격력 스탯을 1000만큼 올려줍니다.\n";
    }

    public void select_power_S_Warrior()
    {
        PowerExplain.text = "전사 영웅은 전투 시 적의 공격을 5회 막을 수 있습니다.\n";
    }

    public void select_power_S_Archer()
    {
        PowerExplain.text = "궁수 영웅뒤에 그림자가 생성되며 궁수 영웅과 함께 공격합니다.\n";
    }

    public void select_power_S_Wizard()
    {
        PowerExplain.text = "전투 시 스킬공격력의 x5 만큼의 보호막을 영웅 중 1명에게 부여합니다.\n";
    }

    public void select_power_S_GoldHand()
    {
        PowerExplain.text = "클릭 당 골드양을 50배 늘려줍니다.\n";
    }

    public void select_power_S_TimeStop()
    {
        PowerExplain.text = "일반공격 적중 시 10%확률로 적을 0~2초간 멈춥니다.\n";
    }



    public void select_lock_power()
    {
        if(DataController.Instance.reincarnation_count == 0)
        {
            PowerExplain.text = "환생을 1번 이상 하셔야 권능을 얻으실수 있습니다.";
        }

        else if(DataController.Instance.reincarnation_count < 3)
        {
            PowerExplain.text = "환생을 3번 이상 하셔야 권능을 얻으실수 있습니다.";
        }

        else if(DataController.Instance.reincarnation_count < 5)
        {
            PowerExplain.text = "환생을 5번 이상 하셔야 권능을 얻으실수 있습니다.";
        }

        else if(DataController.Instance.reincarnation_count < 10)
        {
            PowerExplain.text = "환생을 10번 이상 하셔야 권능을 얻으실수 있습니다.";
        }

        else if(DataController.Instance.reincarnation_count < 25)
        {
            PowerExplain.text = "환생을 25번 이상 하셔야 권능을 얻으실수 있습니다.";
        }

        else if(DataController.Instance.reincarnation_count < 50)
        {
            PowerExplain.text = "환생을 25번 이상 하셔야 권능을 얻으실수 있습니다.";
        }
        else if(DataController.Instance.reincarnation_count < 100)
        {
            PowerExplain.text = "환생을 100번 이상 하셔야 권능을 얻으실수 있습니다.";
        }
    }

    public void select_probability()
    {
        PowerExplain.text = "   권능 확률   \nS급 10%\nA급 20%\nB급 30%\nC급 40%";
    }

    public void MyPosition (Transform transform)
    {
        var tra = transform;
        Debug.Log("Clicked button pos:" + tra.position);
        SelectFrame.SetActive(true);
        SelectFrame.transform.position = tra.position;
        SelectFrame.transform.parent = tra.transform;
    }
}


