using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseRocket : MonoBehaviour
{
    [SerializeField] protected float initialSpeed;
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected float acceleration;
    [SerializeField] protected float curveMagnitude;
    [SerializeField] protected float steeringSpeed;
    [SerializeField] protected float ascentHeight;
    [SerializeField] protected float health;
    [SerializeField] protected float damagePower;
    
    protected static TextMeshProUGUI destroyedCountText;
    protected static float destroyedCount = 0;
    protected float currentHealth;
    protected bool isAscending = true;
    protected Rigidbody rb;
    public Transform target;
    protected float currentSpeed;

    protected virtual void Awake()
    {
        if (destroyedCountText == null)
        {
            destroyedCountText = GameObject.Find("DestroyedRocketCountText").GetComponent<TextMeshProUGUI>();
        }
        rb = GetComponent<Rigidbody>();
        currentSpeed = initialSpeed;
        currentHealth = health;
    }

    protected virtual void FixedUpdate()
    {
        if (rb != null && target != null)
        {
            Vector3 targetDirection = (target.position - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (currentSpeed < maxSpeed)
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

            Debug.DrawLine(transform.position, target.position, Color.red);
        }
    }

    public virtual void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            destroyedCount += 1;
            destroyedCountText.text = $"Intercepted Rockets: {destroyedCount}";
            DestroyRocket();
        }
    }

    protected virtual void DestroyRocket()
    {
        Destroy(gameObject);
    }

    public virtual void Initialize(Vector3 direction, Transform assignedTarget)
    {
        transform.rotation = Quaternion.LookRotation(direction);
        target = assignedTarget;
        isAscending = true;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy's Target"))
        {
            DestroyRocket();
        }
    }
}
