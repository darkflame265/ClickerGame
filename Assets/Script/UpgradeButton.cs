using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;

public class UpgradeButton : MonoBehaviour
{
    public string upgradeName;

    [HideInInspector]
    public int goldByUpgrade; 
    public int startGoldByUpgrade = 1; //맨처음 업그레이드 비용

    [HideInInspector]
    public int startCurrentCost = 1;

    [HideInInspector]
    public float upgradePow = 1.07f; //업그레이드 비용 증가 배수

    public float costPow = 1.14f;

    private static UpgradeButton instance;

    public static UpgradeButton Instance
    {
        get{
            if(instance == null)
            {
                instance = FindObjectOfType<UpgradeButton>();

                if(instance == null)
                {
                    GameObject container = new GameObject("UpgradeButton");

                    instance = container.AddComponent<UpgradeButton>();
                }
            }
            return instance;
        }
    }



    void Start() //Awake보다 한박자 느림
    {
        //나 자신을 가져와ㅑ 데이터 load
        DataController.Instance.LoadUpgradeButton(this);
       
    }

    public void PurchaseUpgrade() 
    {
        if(DataController.Instance.gold >= DataController.Instance.currentCost)
        {
            SoundManager.Instance.upgrade_button_sound();
            DataController.Instance.gold -= DataController.Instance.currentCost;
            DataController.Instance.level++;
            DataController.Instance.goldPerClick += goldByUpgrade;
       
            UpdateUpgrade();
            //DataController.Instance.SaveUpgradeButton(this);
        }
    }

    public void UpdateUpgrade() //업그레이드 비용 증가
    {
        goldByUpgrade = startGoldByUpgrade * (int)Mathf.Pow(upgradePow, DataController.Instance.level);
        DataController.Instance.currentCost = startCurrentCost * (long) Mathf.Pow(costPow, DataController.Instance.level);
    }

    

     
    public void Init()
    {
        DataController.Instance.currentCost = 1;
        DataController.Instance.level = 1;
        DataController.Instance.besu = 1;
        //DataController.Instance.LoadUpgradeButton(this);
    }
    
}
