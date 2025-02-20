using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryBullet : MonoBehaviour
{
    public float speed = 1.0f;
    public float explosionRadius;
    public float maxDestination = 100f;
    public float damagePower;
    private Rigidbody rb;
    private Vector3 firePosition;
    private Vector3 currentPosition;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        firePosition = transform.position;
        currentPosition = transform.position;

    }

    void FixedUpdate()
    {
        Vector3 currentPosition = transform.position;
        float travelDistance = Vector3.Distance(currentPosition, firePosition);

        if (travelDistance < maxDestination)
        {
            // The bullet will accelrate normally
        }
        else
        {
            Explode();
        }

        rb.AddForce(Physics.gravity);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Explode();
                break;
            }
        }
    }

    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in hitColliders)
        {
            BaseRocket baseRocket = collider.GetComponent<BaseRocket>();
            if (baseRocket != null)
            {
                baseRocket.TakeDamage(damagePower);
                Destroy(gameObject);
            }
        }
        Destroy(gameObject);
    }

}
