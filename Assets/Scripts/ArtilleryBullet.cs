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

    // Unity Methods:
    // - Transform.Translate or Rigidbody.MovePosition for movement.
    // - Physics.OverlapSphere: To check for nearby targets when exploding.
    // - Destroy: To remove the bullet and create an explosion effect.
    // - Instantiate: To create explosion effects.
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
        currentPosition = transform.position;
        float travelDistance = Vector3.Distance(currentPosition, firePosition);

        if (travelDistance < maxDestination)
        {
            rb.AddForce(Vector3.forward * speed, ForceMode.Acceleration);
        }
        else
        {
            Destroy(gameObject);
        }

        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, explosionRadius);
        if (hitColliders.Length > 0)
        {
            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject.CompareTag("Enemy"))
                {
                    IncomingRocket incomingRocket = collider.gameObject.GetComponent<IncomingRocket>();
                    incomingRocket.TakeDamage();
                }
            }
        }
    
        // - If an enemy is close or time is up, explode.
        // - Apply gravity effect to simulate curve due to gravity.


    }
    // OnTriggerEnter or Physics.OverlapSphere for detecting proximity.
    // Use Rigidbody for physics-based movement and applying forces.
}
