using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerController : MonoBehaviour
{
    public GameObject SelectFrame;

    public GameObject[]power = new GameObject[0];

    public Text PowerExplain;



    int result = 0;

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
                }
                else
                {
                    power[i].SetActive(false);
                    //Debug.Log("Instpertor length:  " + power.Length);
                }
            }
        }
        
    }

    

    public enum power_list
    {
        eBonusState_50,  //0
        eClickGold_100,
        eClickExp_300,
        eDamage_1000,

        eBonusState_20,
        eClickGold_40,
        eClickExp_100,
        eDamage_300,

        eBonusState_8,
        eClickGold_16,
        eClickExp_40,
        eDamage_100,

        eBonusState_3,
        eClickGold_6,
        eClickExp_16,
        eDamage_40,
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

    

    public void initiaze_power()
    {
        for (int i = 0; i < 16; i++) //초기화 기능 넣기
        {
            power_list a = (power_list)i;
            if(check_list.Load(a.ToString()) == 1)
            {
                if(i == 0)
                {
                    DataController.Instance.devideFreeStateBesu(50);
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("S등급 보너스스탯 50배 제거 완료!!");
                }
                if(i == 1)
                {
                    DataController.Instance.DevideGoldPerClick(100);
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("S등급 클릭골드 100배 제거 완료!!");
                }
                if(i == 2)
                {
                    DataController.Instance.DevideExpBesu(300);
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("S등급 클릭경험치 300배 제거 완료!!");
                }
                if(i == 3)
                {
                    DataController.Instance.devideDamageBesu(1000);
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("S등급 데미지 1000배 제거 완료!!");
                }
                if(i == 4)
                {
                    DataController.Instance.devideFreeStateBesu(20);
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("A등급 보너스스탯 20배 제거 완료!!");
                }
                if(i == 5)
                {
                    DataController.Instance.DevideGoldPerClick(40);
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("A등급 클릭골드 40배 제거 완료!!");
                }
                if(i == 6)
                {
                    DataController.Instance.DevideExpBesu(100);
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("A등급 클릭경험치 100배 제거 완료!!");
                }
                if(i == 7)
                {
                    DataController.Instance.devideDamageBesu(300);
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("A등급 데미지 300배 제거 완료!!");
                }
                if(i == 8)
                {
                    DataController.Instance.devideFreeStateBesu(8);
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("B등급 보너스스탯 8배 제거 완료!!");
                }
                if(i == 9)
                {
                    DataController.Instance.DevideGoldPerClick(16);
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("B등급 클릭골드 16배 제거 완료!!");
                }
                if(i == 10)
                {
                    DataController.Instance.DevideExpBesu(40);
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("B등급 클릭경험치 40배 제거 완료!!");
                }
                if(i == 11)
                {
                    DataController.Instance.devideDamageBesu(100);
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("B등급 데미지 100배 제거 완료!!");
                }
                if(i == 12)
                {
                    DataController.Instance.devideFreeStateBesu(3);
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("C등급 보너스스탯 3배 제거 완료!!");
                }
                if(i == 13)
                {
                    DataController.Instance.DevideGoldPerClick(6);
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("C등급 클릭골드 6배 제거 완료!!");
                }
                if(i == 14)
                {
                    DataController.Instance.DevideExpBesu(16);
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("C등급 클릭경험치 16배 제거 완료!!");
                }
                if(i == 15)
                {
                    DataController.Instance.devideDamageBesu(40);
                    check_list.Save(a.ToString(), 0);
                    Debug.Log("C등급 데미지 40배 제거 완료!!");
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
                    power_list ThisResult_C = (power_list)Random.Range(12,16); // 0~3
                    bool get = false;
                    
                    for(int i = 12; i < 16; i++)
                    {
                        
                        if(ThisResult_C == (power_list)i)
                        {
                            power_list b = (power_list)i;
                            if(check_list.Load(b.ToString()) == 0) //새로운걸 얻을떄
                                {
                                    if(i == 12) //eBonusState_50,
                                    {
                                        DataController.Instance.MultiplyFreeStateBesu(3);
                                        check_list.Save(b.ToString(), 1);
                                        Debug.Log("보너스스탯 3배수 적용 완료!!");
                                        get = true;
                                        break;
                                    }
                                    if(i == 13) //eClickGold_100,  
                                    {
                                        DataController.Instance.MultiplyGoldPerClick(6);
                                        check_list.Save(b.ToString(), 1);
                                        Debug.Log("클릭골드 6배 적용!!");
                                        get = true;
                                        break;
                                    }
                                    if(i == 14) //eClickExp_300,
                                    {
                                        DataController.Instance.MultiplyExpBesu(16);
                                        check_list.Save(b.ToString(), 1);
                                        Debug.Log("클릭경험치 16배 적용!!");
                                        get =true;
                                        break;
                                    }
                                    if(i == 15) //eDamage_1000,
                                    {
                                        DataController.Instance.MultiplyDamageBesu(40);
                                        check_list.Save(b.ToString(), 1);
                                        Debug.Log("데미지 40배 적용!!");
                                        get = true;
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
                                        DataController.Instance.MultiplyFreeStateBesu(8);
                                        check_list.Save(b.ToString(), 1);
                                        Debug.Log("보너스스탯 8배수 적용 완료!!");
                                        get = true;
                                        break;
                                    }
                                    if(i == 9) //eClickGold_100,  
                                    {
                                        DataController.Instance.MultiplyGoldPerClick(16);
                                        check_list.Save(b.ToString(), 1);
                                        Debug.Log("클릭골드 16배 적용!!");
                                        get = true;
                                        break;
                                    }
                                    if(i == 10) //eClickExp_300,
                                    {
                                        DataController.Instance.MultiplyExpBesu(40);
                                        check_list.Save(b.ToString(), 1);
                                        Debug.Log("클릭경험치 40배 적용!!");
                                        get =true;
                                        break;
                                    }
                                    if(i == 11) //eDamage_1000,
                                    {
                                        DataController.Instance.MultiplyDamageBesu(100);
                                        check_list.Save(b.ToString(), 1);
                                        Debug.Log("데미지 100배 적용!!");
                                        get = true;
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
                                        DataController.Instance.MultiplyFreeStateBesu(20);
                                        check_list.Save(b.ToString(), 1);
                                        Debug.Log("보너스스탯 20배수 적용 완료!!");
                                        get = true;
                                        break;
                                    }
                                    if(i == 5) //eClickGold_100,  
                                    {
                                        DataController.Instance.MultiplyGoldPerClick(40);
                                        check_list.Save(b.ToString(), 1);
                                        Debug.Log("클릭골드 40배 적용!!");
                                        get = true;
                                        break;
                                    }
                                    if(i == 6) //eClickExp_300,
                                    {
                                        DataController.Instance.MultiplyExpBesu(100);
                                        check_list.Save(b.ToString(), 1);
                                        Debug.Log("클릭경험치 100배 적용!!");
                                        get =true;
                                        break;
                                    }
                                    if(i == 7) //eDamage_1000,
                                    {
                                        DataController.Instance.MultiplyDamageBesu(300);
                                        check_list.Save(b.ToString(), 1);
                                        Debug.Log("데미지 300배 적용!!");
                                        get = true;
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
                    power_list ThisResult_C = (power_list)Random.Range(0,4); // 0~3
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
                                        DataController.Instance.MultiplyFreeStateBesu(50);
                                        check_list.Save(b.ToString(), 1);
                                        Debug.Log("보너스스탯 50배수 적용 완료!!");
                                        get = true;
                                        break;
                                    }
                                    if(i == 1) //eClickGold_100,  
                                    {
                                        DataController.Instance.MultiplyGoldPerClick(100);
                                        check_list.Save(b.ToString(), 1);
                                        Debug.Log("클릭골드 100배 적용!!");
                                        get = true;
                                        break;
                                    }
                                    if(i == 2) //eClickExp_300,
                                    {
                                        DataController.Instance.MultiplyExpBesu(300);
                                        check_list.Save(b.ToString(), 1);
                                        Debug.Log("클릭경험치 300배 적용!!");
                                        get =true;
                                        break;
                                    }
                                    if(i == 3) //eDamage_1000,
                                    {
                                        DataController.Instance.MultiplyDamageBesu(1000);
                                        check_list.Save(b.ToString(), 1);
                                        Debug.Log("데미지 1000배 적용!!");
                                        get = true;
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






    public void select_power_0()
    {
        PowerExplain.text = "레벨업 당 얻는 보너스 스탯을 50배 올려준다.\n";
    }

    public void select_power_1()
    {
        PowerExplain.text = "클릭 당 얻는 골드를 100배 올려준다.\n";
    }

    public void select_power_2()
    {
        PowerExplain.text = "클릭 당 얻는 경험치를 300배 올려준다.";
    }

    public void select_power_3()
    {
        PowerExplain.text = "캐릭터가 적에게 가하는 피해를 1000배 올려준다.\n by 아자토스";
    }

    public void select_power_4()
    {
        PowerExplain.text = "레벨업 당 얻는 보너스 스탯을 20배 올려준다.\n";
    }

    public void select_power_5()
    {
        PowerExplain.text = "클릭 당 얻는 골드를 40배 올려준다.\n";
    }

    public void select_power_6()
    {
        PowerExplain.text = "클릭 당 얻는 경험치를 100배 올려준다.";
    }

    public void select_power_7()
    {
        PowerExplain.text = "캐릭터가 적에게 가하는 피해를 300배 올려준다.\n by 아자토스";
    }

    public void select_power_8()
    {
        PowerExplain.text = "레벨업 당 얻는 보너스 스탯을 8배 올려준다.\n";
    }

    public void select_power_9()
    {
        PowerExplain.text = "클릭 당 얻는 골드를 16배 올려준다.\n";
    }

    public void select_power_10()
    {
        PowerExplain.text = "클릭 당 얻는 경험치를 40배 올려준다.";
    }

    public void select_power_11()
    {
        PowerExplain.text = "캐릭터가 적에게 가하는 피해를 100배 올려준다.\n by 아자토스";
    }

    public void select_power_12()
    {
        PowerExplain.text = "레벨업 당 얻는 보너스 스탯을 3배 올려준다.\n";
    }

    public void select_power_13()
    {
        PowerExplain.text = "클릭 당 얻는 골드를 6배 올려준다.\n";
    }

    public void select_power_14()
    {
        PowerExplain.text = "클릭 당 얻는 경험치를 16배 올려준다.";
    }

    public void select_power_15()
    {
        PowerExplain.text = "캐릭터가 적에게 가하는 피해를 40배 올려준다.\n by 아자토스";
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


