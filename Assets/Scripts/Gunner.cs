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
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPoint = hit.point;
            Vector3 direction = (targetPoint - bulletSpawnPoint.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(direction));
            bullet.GetComponent<Rigidbody>().AddForce(direction * bullet.GetComponent<Bullet>().speed);
        }
        else
        {
            Vector3 direction = ray.direction.normalized;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(direction));
            bullet.GetComponent<Rigidbody>().AddForce (direction * bullet.GetComponent<Bullet>().speed * 50);
            Debug.Log("Missed");
        }
    }
}