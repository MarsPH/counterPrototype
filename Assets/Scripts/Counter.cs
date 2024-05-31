using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public static Counter Instance { get; private set; }
    public Text CounterText;

    private int Count = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // Optional
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Count = 0;
    }

    public void AddHitCount()
    {
        Count += 1;
        CounterText.text = "Ground Hit Count : " + Count;
    }
}
