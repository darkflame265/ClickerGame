using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorFlicker : MonoBehaviour
{

    public GameObject or;
    private Color orr;
    // Start is called before the first frame update
    void Start()
    {

        orr = or.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        float flicker = Mathf.Abs(Mathf.Sin(Time.time * 1));
        or.GetComponent<Image>().color = orr * flicker;

    }
}
