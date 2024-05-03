using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingRocket : MonoBehaviour
{
    public float rocketSpeed = 500f;
    public float curveMagnitude = 100f; 

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            Vector3 forwardVelocity = transform.forward * rocketSpeed;
            rb.velocity = forwardVelocity;

            // Apply a continuous upward curving force
            Vector3 curveForce = new Vector3(0, curveMagnitude, 0);
            rb.AddForce(curveForce, ForceMode.Force); // Use Force for a continuous application

        }
    }


    public void Initialize(Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction);

    }
}
