using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Artillery : MonoBehaviour
{
    public float heatingCap; // Maximum heat before needing to cooldown
    public float cooldown; // Time it takes to cooldown and reset shot amount
    public float range; // Maximum range for artillery firing
    private float shotAmount; // Current number of shots fired, related to heating

    public Transform artillery; // Transform component where bullets are spawned
    public GameObject bulletPrefab; // Bullet prefab to be instantiated when firing
    public Camera cam; // Camera to calculate firing direction
    private bool isCoolingDown; // Flag to manage cooldown state



    void Update()
    {

        if (shotAmount < heatingCap)
        {
            if (Input.GetKey(KeyCode.Space) && !isCoolingDown)
            {
                Shoot();
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
        Debug.DrawRay(ray.origin, ray.direction * maxRayDistance, Color.yellow, 2f);

        Vector3 direction = ray.direction;

        GameObject bullet = Instantiate(bulletPrefab, artillery.position, Quaternion.LookRotation(direction));
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
