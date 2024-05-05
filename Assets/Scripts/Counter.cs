using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text CounterText;

    private int Count = 0;

    private void Start()
    {
        Count = 0;
    }

    public void AddHitCount()
    {
        Count += 1;
        CounterText.text = "Ground Hit Count : " + Count;
        //Destroy(other.gameObject);
    }
}
