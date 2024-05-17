using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideDrone : MonoBehaviour
{
    public float initialrocketSpeed = 200f; // Higher initial speed for suicide drones
    public float maxRocketSpeed = 250f; // Maximum speed
    public float acceleration = 50f; // Lower acceleration rate
    public float curveMagnitude = 100f; // Sharp turns
    public float steeringSpeed = 3f; // Higher steering speed for better maneuverability
    public float ascentHeight = 100f; // Ascent height (if applicable)
    public float rocketHealth = 50f; // Lower health
    public float proximityRange = 10f; // Proximity range for explosion
    public float selfDestructTime = 10f; // Self-destruct timer

    private float currentHealth;
    private Rigidbody rb;
    public Transform target; // Target to follow
    private float currentSpeed;
    private float selfDestructTimer;
    private bool isAscending = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = initialrocketSpeed;
        currentHealth = rocketHealth;
        selfDestructTimer = selfDestructTime;
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
                Vector3 ascentForce = Vector3.up * curveMagnitude;
                rb.AddForce(ascentForce, ForceMode.Acceleration);
            }
            else
            {
                isAscending = false;
                float adjustedCurveMagnitude = curveMagnitude * (distanceToTarget / 500);
                rb.AddForce(targetDirection * adjustedCurveMagnitude, ForceMode.Acceleration);
            }

            Vector3 newVelocity = Vector3.Lerp(rb.velocity.normalized, targetDirection, steeringSpeed * Time.deltaTime) * currentSpeed;
            rb.velocity = newVelocity;

            // Check if within proximity range to explode
            CheckProximity();

            // Manage self-destruct timer
            ManageSelfDestruct();

            Debug.DrawLine(transform.position, target.position, Color.yellow);
        }
    }

    void CheckProximity()
    {
        if (Vector3.Distance(transform.position, target.position) <= proximityRange)
        {
            Explode();
        }
    }

    void ManageSelfDestruct()
    {
        selfDestructTimer -= Time.deltaTime;
        if (selfDestructTimer <= 0)
        {
            Explode();
        }
    }

    void Explode()
    {
        DestroyRocket();
    }

    public void TakeDamage(float damageAmount)
    {
        if (currentHealth > 0)
            currentHealth -= damageAmount;

        if (currentHealth <= 0)
            Explode();
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
            Explode();
        }
    }
}
