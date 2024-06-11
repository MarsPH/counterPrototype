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
    public HealthBar healthBar;
    public int maxhealth = 10;
    public int currenthealth;
    public int points;


    private int Count = 0;

    private void Awake()
    {
        currenthealth = maxhealth;
        healthBar.SetHealth(maxhealth);

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

    public void AddHitCount(int point)
    {
        currenthealth--;
        healthBar.SetHealth((int)currenthealth);
        Count += 1;
        CounterText.text = "Ground Hit Count : " + Count;
        points += point;
    }
}
