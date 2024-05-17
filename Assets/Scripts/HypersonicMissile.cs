using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypersonicMissile : MonoBehaviour
{
    public float initialrocketSpeed = 150f; // Higher initial speed for hypersonic missile
    public float maxRocketSpeed = 300f; // Much higher maximum speed
    public float acceleration = 100f; // Higher acceleration rate
    public float curveMagnitude = 100f;
    public float steeringSpeed = 3f; // Higher steering speed for better maneuverability
    public float ascentHeight = 100f;
    public float rocketHealth;
    public float heatThreshold = 100f; // Heat threshold for overheating mechanics

    private float currentHealth;
    private bool isAscending = true;
    private float heatLevel = 0f; // Heat level management
    private Rigidbody rb;
    public Transform target;
    private float currentSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = initialrocketSpeed;
        currentHealth = rocketHealth;
    }

    void FixedUpdate()
    {
        if (rb != null && target != null)
        {
            Vector3 targetDirection = (target.position - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (currentSpeed < maxRocketSpeed)
            {
                currentSpeed += acceleration * Time.deltaTime;
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

            // Manage heat
            ManageHeat();

            Debug.DrawLine(transform.position, target.position, Color.red);
        }
    }

    void ManageHeat()
    {
        // Increase heat level based on current speed and time
        heatLevel += currentSpeed * Time.deltaTime * 0.1f;

        // Check if the heat level has exceeded the heat threshold
        if (heatLevel > heatThreshold)
        {
            // Reduce the current speed if overheating
            currentSpeed -= acceleration * Time.deltaTime * 0.5f;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (currentHealth > 0)
            currentHealth -= damageAmount;

        if (currentHealth <= 0)
            DestroyRocket();
    }

    public void DestroyRocket()
    {
        Destroy(gameObject);
    }

    public void Initialize(Vector3 direction, Transform assignedTarget)
    {
        transform.rotation = Quaternion.LookRotation(direction);
        target = assignedTarget;
        isAscending = true; // Reset to ascent state on initialization
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy's Target"))
        {
            DestroyRocket();
        }
    }
}
