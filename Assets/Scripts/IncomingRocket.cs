using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingRocket : MonoBehaviour
{
    public float rocketSpeed = 500f;
    public float curveMagnitude = 100f; 
    public float steeringSpeed = 1f;

    private Rigidbody rb;
    public Transform target;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb != null && target != null)
        {
            Vector3 targetDirection = (target.position - transform.position).normalized;
            Vector3 currentVelocity = rb.velocity.normalized;

            Vector3 newVelocity = Vector3.Lerp(currentVelocity, targetDirection, steeringSpeed * Time.deltaTime) * rocketSpeed;
            rb.velocity = newVelocity;

            // Apply a continuous upward curving force
            Vector3 curveForce = new Vector3(0, curveMagnitude, 0);
            rb.AddForce(curveForce, ForceMode.Acceleration); // Use Force for a continuous application
            Debug.DrawLine(transform.position, target.position, Color.red);

        }
    }


    public void Initialize(Vector3 direction, Transform assignedTarget)
    {
        transform.rotation = Quaternion.LookRotation(direction);
        target = assignedTarget;

    }
}
