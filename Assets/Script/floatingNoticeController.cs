﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class floatingNoticeController : MonoBehaviour
{
    public float moveSpeed;
    public float destroyTime;

    public Text text;

    private Vector2 vector;
    
    void Update() {
        vector.Set(text.transform.position.x, text.transform.position.y + (moveSpeed * Time.deltaTime));
        this.transform.position = vector;
        destroyTime -= Time.deltaTime;
        if(destroyTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
