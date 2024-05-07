using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    private Transform target;
    public float speed = 30f;

    bool isTracking = false;
    // Update is called once per frame
    void Update()
    {
        if (target != null && isTracking)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void StartTracking(Transform newTarget)
    {
        target = newTarget;
        isTracking = true;
    }

    public void StopTracking()
    {
        isTracking = false;
        target = null;
        Destroy(gameObject);
    }
}
