using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorFlicker : MonoBehaviour
{
    public GameObject bottom_text;
    private Color originColor;
    // Start is called before the first frame update
    void Start()
    {
        originColor = bottom_text.GetComponent<Text>().color;
    }

    // Update is called once per frame
    void Update()
    {
        float flicker = Mathf.Abs(Mathf.Sin(Time.time * 1));
        bottom_text.GetComponent<Text>().color = originColor * flicker;
    }
}
