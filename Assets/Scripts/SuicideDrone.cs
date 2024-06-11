using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SuicideDrone : BaseRocket
{
    public float proximityRange = 10f;
    public int dronePoints;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        CheckProximity();
    }

    void CheckProximity()
    {
        if (Vector3.Distance(transform.position, target.position) <= proximityRange)
        {
            Explode();
        }
    }
    void Explode()
    {
        Counter.Instance.AddHitCount(1);
        DestroyRocket();
    }
}
