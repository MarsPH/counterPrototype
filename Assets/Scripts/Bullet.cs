using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1000.0f;
    private Rigidbody rb;
    public Transform target;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) 
        {
            Debug.LogError("Rigidbody component is missing from this GameObject: " + gameObject.name);
        }
    }
    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
        else
        {
            Debug.Log("No collider touched");
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy(gameObject);
        if (other.gameObject.CompareTag("Target"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
