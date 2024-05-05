using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    private GameObject hitObject;
    private Counter Counter;
    // Start is called before the first frame update
    void Start()
    {
        hitObject = GameObject.Find("Rocket Target");
        Counter = hitObject.GetComponentInParent<Counter>();

    }

    // Update is called once per frame
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
