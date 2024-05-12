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
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        // - Move bullet forward.
        // - After a certain time or distance, check for nearby enemies.
        // - If an enemy is close or time is up, explode.
        // - Apply gravity effect to simulate curve due to gravity.


    }
    // OnTriggerEnter or Physics.OverlapSphere for detecting proximity.
    // Use Rigidbody for physics-based movement and applying forces.
}
