using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptionMissileBehavior : MonoBehaviour
{
    public float speed = 1000.0f;
    public float curveMagnitude = 2f;
    public float damagePower;

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

            Vector3 curvingForce = new Vector3(0, curveMagnitude, 0);
            rb.AddForce(curvingForce, ForceMode.Acceleration);
        }
        else
        {
            Debug.Log("No target assigned");
        }

        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            BaseRocket baseRocket = other.GetComponent<BaseRocket>();
            if (baseRocket != null)
            {
                baseRocket.TakeDamage(damagePower);
                Destroy(gameObject);
            }
        }
    }
}
