using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingRocket : MonoBehaviour
{
    public float initialrocketSpeed = 50;
    public float maxRocketSpeed = 100f;
    public float acceleration = 20f;
    public float curveMagnitude = 100f; 
    public float steeringSpeed = 1f;

    private Rigidbody rb;
    public Transform target;
    private float currentSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = initialrocketSpeed;
    }

    void FixedUpdate()
    {
        if (rb != null && target != null)
        {
            Vector3 targetDirection = (target.position - transform.position).normalized;
            Vector3 currentVelocity = rb.velocity.normalized;

            Vector3 newVelocity = Vector3.Lerp(currentVelocity, targetDirection, steeringSpeed * Time.deltaTime) * currentSpeed;
            rb.velocity = newVelocity;

            if (currentSpeed < maxRocketSpeed)
            {
                currentSpeed += acceleration * Time.deltaTime;
            }

            Vector3 curveForce = Vector3.up * curveMagnitude;
            rb.AddForce(curveForce, ForceMode.Acceleration); 

            Debug.DrawLine(transform.position, target.position, Color.red);

        }
    }


    public void Initialize(Vector3 direction, Transform assignedTarget)
    {
        transform.rotation = Quaternion.LookRotation(direction);
        target = assignedTarget;

    }

    
}
