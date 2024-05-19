using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideDrone : BaseRocket
{
    public float proximityRange = 10f;

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
        DestroyRocket();
    }
}
