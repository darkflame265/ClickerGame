using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectHelp : MonoBehaviour
{
    
    public GameObject Fight_stage_1;
    public GameObject selectInvFrame;
    

    public void select_stage()
    {
        
        if(this.transform.name == "stage_1")
        {
            Debug.Log("it's 1");
        }
        if(this.transform.name == "stage_2")
        {
            Debug.Log("it's 2");
        }
        if(this.transform.name == "stage_3")
        {
            Debug.Log("it's 3");
        }
    }

    public void MyPosition (Transform transform)
    {
        var tra = transform;
        selectInvFrame.SetActive(true);
        selectInvFrame.transform.position = tra.position;
        selectInvFrame.transform.parent = tra.transform;
    }

    
    
}
