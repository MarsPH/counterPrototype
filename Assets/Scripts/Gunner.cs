using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Gunner : MonoBehaviour
{
    public Camera cam;
    public GameObject autoTrackingMissilePrefab;
    public GameObject manualTrackingMissilePrefab;
    public GameObject multiTargetMissilePrefab;
    public Transform bulletSpawnPoint;
    public float airSpaceEntryBorderX;
    private HashSet<GameObject> targetedRockets = new HashSet<GameObject>();
    private GameObject currentMissile;
    public InterceptionLaser interceptionLaser;
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
            Shoot();
        if (Input.GetMouseButtonDown(1))
            multitargetShoot();
        else if (Input.GetMouseButtonDown(0)) 
        {
            ManualTargetTrackMissileShoot();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (currentMissile != null)
            {
                currentMissile.GetComponent<MissileController>().StopTracking();
                currentMissile = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ShootLaser();
        }
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
            GameObject bullet = Instantiate(autoTrackingMissilePrefab, bulletSpawnPoint.position, Quaternion.LookRotation(direction));
            bullet.GetComponent<Rigidbody>().AddForce(direction * bullet.GetComponent<InterceptionMissileBehavior>().speed);

            // Set the target for the bullet if it hits a collider
            if (hit.collider != null)
            {
                bullet.GetComponent<InterceptionMissileBehavior>().SetTarget(hit.collider.transform);
            }
        }
        else
        {
            // No target hit, instantiate and destroy if missed
            Vector3 direction = (ray.GetPoint(maxRayDistance) - bulletSpawnPoint.position).normalized;
            GameObject bullet = Instantiate(autoTrackingMissilePrefab, bulletSpawnPoint.position, Quaternion.LookRotation(direction));
            bullet.GetComponent<Rigidbody>().AddForce(direction * bullet.GetComponent<InterceptionMissileBehavior>().speed);

            StartCoroutine(DestroyMissedMissiles(bullet));
        }

    }
    private void multitargetShoot()
    {

        GameObject[] rockets = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject rocket in rockets)
        {
            if (!targetedRockets.Contains(rocket) && rocket.transform.position.x > airSpaceEntryBorderX)
            {
                Vector3 direction = (rocket.transform.position - bulletSpawnPoint.position).normalized;
                GameObject bullet = Instantiate(multiTargetMissilePrefab, bulletSpawnPoint.position, Quaternion.LookRotation(direction));
                bullet.GetComponent<Rigidbody>().AddForce(direction * bullet.GetComponent<InterceptionMissileBehavior>().speed);
                bullet.GetComponent<InterceptionMissileBehavior>().SetTarget(rocket.transform);

                targetedRockets.Add(rocket); 
            }

        }
    }
    private void ManualTargetTrackMissileShoot()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 350f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                if (currentMissile != null)
                {
                    Destroy(currentMissile);
                }
                currentMissile = Instantiate(manualTrackingMissilePrefab, bulletSpawnPoint.position, Quaternion.identity);
                currentMissile.GetComponent<MissileController>().StartTracking(hit.transform);
            }
        }
    }
    private void ShootLaser()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 350f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                interceptionLaser.StartHeating(hit.transform);
            }
        }
    }
    IEnumerator DestroyMissedMissiles(GameObject bullet)
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        if (bullet != null)  
        {
            Destroy(bullet);
        }
        Destroy(bullet);
    }

    
}
