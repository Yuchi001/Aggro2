using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CharmCircleObject : MonoBehaviour
{
    private Transform canvas;
    public GameObject follow;
    public Image circleImage;
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("c").transform;
        gameObject.transform.SetParent(canvas);
    }
    void Update()
    {
        try
        {
            transform.position = follow.transform.position;
        }
        catch
        {
            Destroy(gameObject);
        }
    }
}
