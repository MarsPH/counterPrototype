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
    public float ascentHeight = 100f; 
    
    private bool isAscending = true; 


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
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (currentSpeed < maxRocketSpeed)
            {
                currentSpeed += acceleration * Time.deltaTime * (distanceToTarget / 1000);
            }

            if (isAscending && transform.position.y < ascentHeight)
            {
                // Continue upward force more strongly
                Vector3 ascentForce = Vector3.up * curveMagnitude;
                rb.AddForce(ascentForce, ForceMode.Acceleration);
            }
            else
            {
                // Switch to target-directed force after reaching desired height or if already descending
                isAscending = false;
                float adjustedCurveMagnitude = curveMagnitude * (distanceToTarget / 500);
                rb.AddForce(targetDirection * adjustedCurveMagnitude, ForceMode.Acceleration);
            }

            Vector3 newVelocity = Vector3.Lerp(rb.velocity.normalized, targetDirection, steeringSpeed * Time.deltaTime) * currentSpeed;
            rb.velocity = newVelocity;


            Debug.DrawLine(transform.position, target.position, Color.red);

        }
    }


    public void Initialize(Vector3 direction, Transform assignedTarget)
    {
        transform.rotation = Quaternion.LookRotation(direction);
        target = assignedTarget;
        isAscending = true; // Reset to ascent state on initialization
    }


}
