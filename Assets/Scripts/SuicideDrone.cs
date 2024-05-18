using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideDrone : BaseRocket
{
    public float proximityRange = 10f;
    public float selfDestructTime = 10f;
    private float selfDestructTimer;

    protected override void Awake()
    {
        base.Awake();
        initialSpeed = 200f;
        maxSpeed = 250f;
        acceleration = 50f;
        health = 50f;
        damagePower = 50f;
        ascentHeight = 100f;
        selfDestructTimer = selfDestructTime;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        CheckProximity();
        ManageSelfDestruct();
    }

    void CheckProximity()
    {
        if (Vector3.Distance(transform.position, target.position) <= proximityRange)
        {
            Explode();
        }
    }

    void ManageSelfDestruct()
    {
        selfDestructTimer -= Time.deltaTime;
        if (selfDestructTimer <= 0)
        {
            Explode();
        }
    }

    void Explode()
    {
        DestroyRocket();
    }
}
