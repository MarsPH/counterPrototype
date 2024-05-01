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
        float maxRayDistance = 100.0f;

        Debug.DrawRay(ray.origin, ray.direction * maxRayDistance, Color.red, 2f);

        Vector3 targetPoint = ray.origin + ray.direction * maxRayDistance;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        
        Vector3 direction = (targetPoint - bulletSpawnPoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(direction));
        bullet.GetComponent<Rigidbody>().AddForce(direction * bullet.GetComponent<Bullet>().speed);


        if (hit.collider != null)  // Check if the hit object can be a target
        {
            bullet.GetComponent<Bullet>().SetTarget(hit.collider.transform);
        }
    }
}
