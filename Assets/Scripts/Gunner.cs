using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gunner : MonoBehaviour
{
    public Camera cam;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    private void Shoot()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float maxRayDistance = 200f;

        Debug.DrawRay(ray.origin, ray.direction * maxRayDistance, Color.green, 2f);

        if (Physics.Raycast(ray, out hit, maxRayDistance))
        {
            // A target was hit
            Vector3 direction = (hit.point - bulletSpawnPoint.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(direction));
            bullet.GetComponent<Rigidbody>().AddForce(direction * bullet.GetComponent<Bullet>().speed);

            // Set the target for the bullet if it hits a collider
            if (hit.collider != null)
            {
                bullet.GetComponent<Bullet>().SetTarget(hit.collider.transform);
            }
        }
        else
        {
            // No target hit, instantiate and destroy if missed
            Vector3 direction = (ray.GetPoint(maxRayDistance) - bulletSpawnPoint.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(direction));
            bullet.GetComponent<Rigidbody>().AddForce(direction * bullet.GetComponent<Bullet>().speed);

            StartCoroutine(DestroyMissedMissiles(bullet));  // Call coroutine to destroy the missed bullet
        }

    }
    IEnumerator DestroyMissedMissiles(GameObject bullet)
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        if (bullet != null)  // Check if the bullet still exists
        {
            Destroy(bullet);
        }
        Destroy(bullet);
    }

    
}
