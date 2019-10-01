using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    private Item item;
    private string data;
    public GameObject tooltip;

    void Start()
    {
        //tooltip = GameObject.Find("ToolTip");  //이미 public에서 정함
        tooltip.SetActive(false);
    }

    void Update()
    {
        if(tooltip.activeSelf == true)
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition); 

            Vector2 touchPos = new Vector2(wp.x, wp.y); 

            transform.position = touchPos;
            tooltip.transform.position = transform.position;
        }
    }

    public void Activate(Item item)
    {
        this.item = item;
        ConstructDataString();
        tooltip.SetActive(true);
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void ConstructDataString()
    {
        data = "<color=#0473f0><b>" + item.Title + "</b></color>\n\n" + item.Description + "";
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }
}
