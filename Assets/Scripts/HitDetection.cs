using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    private GameObject hitObject;
    private Counter Counter;
    void Start()
    {
        hitObject = GameObject.Find("Rocket Target");
        Counter = hitObject.GetComponentInParent<Counter>();

    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.CompareTag("Enemy"))
        {
            Counter.AddHitCount();
        }
    }

}
