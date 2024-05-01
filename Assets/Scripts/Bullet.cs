using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1000.0f;
    private Rigidbody rb;
    public Transform target;
    // Start is called before the first frame update
    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * speed;
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
        }
    }
}
