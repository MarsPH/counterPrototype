using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingRocket : BaseRocket
{
    protected override void Awake()
    {
        base.Awake();
        initialSpeed = 50f;
        maxSpeed = 100f;
        acceleration = 20f;
        health = 100f;
        damagePower = 50f;
        ascentHeight = 100f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
