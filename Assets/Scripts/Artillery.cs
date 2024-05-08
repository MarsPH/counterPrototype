using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Artillery : MonoBehaviour
{
    public float heatingCap;
    public float cooldown;
    public float range;
    private float shotAmount;

    public Transform artillery;
    public GameObject bulletPrefab;
    public Camera cam;
    private bool isCoolingDown;



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
        //bullet.GetComponent<Rigidbody>().AddForce(direction * bullet.GetComponent<ArtilleryBullet>().speed);
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
