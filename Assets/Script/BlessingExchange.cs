using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlessingExchange : MonoBehaviour
{
    int current_select_blessing;
    public Text blessing_explanation;


    public void select_blessing_0()  //체력
    {
        blessing_explanation.text = "자연의 섭리\n체력 스탯 배율 증가\n" + "(1.3 - > 1.4)";
    }
}
