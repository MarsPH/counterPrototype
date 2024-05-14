using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryBullet : MonoBehaviour
{
    /// <summary>
    /// Its going to be something like the bullet.
    ///  Variables for speed, gravity effect, explosion radius, lifetime.
    // Update method to move the bullet and apply gravity.
    // OnTriggerEnter or use Physics.OverlapSphere for proximity detection.
    // Method to handle explosion.
    /// </summary>
    public float speed = 1.0f;
    public float explosionRadius;
    public float maxDestination = 100f;
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
            if (collider.gameObject.CompareTag("Enemy"))
            {
                IncomingRocket incomingRocket = collider.gameObject.GetComponent<IncomingRocket>();
                if (incomingRocket != null)
                {
                    incomingRocket.TakeDamage();
                }
            }
        }
        Destroy(gameObject);
    }
    // OnTriggerEnter or Physics.OverlapSphere for detecting proximity.
    // Use Rigidbody for physics-based movement and applying forces.
}
