using System.Collections;
using UnityEngine;

public class Artillery : MonoBehaviour
{
    public float heatingCap; // Maximum heat before needing to cooldown
    public float cooldown; // Time it takes to cooldown and reset shot amount
    public float range; // Maximum range for artillery firing
    public float fireRate = 0.1f; // Rapid fire rate interval
    public float bulletSpeed = 100.0f; // Speed of the bullet

    private float nextFireTime = 0.0f; // Time when the next shot can be fired
    private float shotAmount; // Current number of shots fired, related to heating

    public Transform artillery; // Transform component where bullets are spawned
    public GameObject bulletPrefab; // Bullet prefab to be instantiated when firing
    public Camera cam; // Camera to calculate firing direction
    private bool isCoolingDown; // Flag to manage cooldown state

    void Update()
    {
        if (Time.time >= nextFireTime && shotAmount < heatingCap)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Shoot();
                nextFireTime = Time.time + fireRate; // Set the next fire time
            }
        }
        else
        {
            if (!isCoolingDown)
            {
                StartCoroutine(CoolDown());
            }
        }
    }

    private void Shoot()
    {
        float maxRayDistance = range;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 targetPoint;
        Debug.DrawRay(ray.origin, ray.direction * maxRayDistance, Color.yellow, 2f);

        if (Physics.Raycast(ray, out hit, maxRayDistance))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(maxRayDistance);
        }

        Vector3 direction = (targetPoint - artillery.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, artillery.position, Quaternion.LookRotation(direction));
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // Directly calculate the velocity to reach the target point
        Vector3 velocity = direction * bulletSpeed;
        bulletRb.velocity = velocity;

        shotAmount += 1;
    }

    IEnumerator CoolDown()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(cooldown);
        shotAmount = 0;
        isCoolingDown = false;
    }
}
