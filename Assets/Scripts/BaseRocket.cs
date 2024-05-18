using System.Collections;
using System.Collections.Generic;
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

    protected float currentHealth;
    protected bool isAscending = true;
    protected Rigidbody rb;
    public Transform target;
    protected float currentSpeed;

    protected virtual void Awake()
    {
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
        if (other.gameObject.CompareTag("Enemy"))
        {
            BaseRocket enemyRocket = other.GetComponent<BaseRocket>();
            if (enemyRocket != null)
            {
                enemyRocket.TakeDamage(damagePower);
                DestroyRocket();
            }
        
        }
        else if (other.gameObject.CompareTag("Enemy's Target"))
        {
            DestroyRocket();
        }
    }
}
