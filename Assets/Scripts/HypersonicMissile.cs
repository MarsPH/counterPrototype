using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypersonicMissile : BaseRocket
{
    [SerializeField] private float heatThreshold = 100f; // Heat threshold for overheating mechanics
    private float heatLevel = 0f; // Heat level management

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        ManageHeat();
    }

    void ManageHeat()
    {
        heatLevel += currentSpeed * Time.deltaTime * 0.1f;

        if (heatLevel > heatThreshold)
        {
            currentSpeed -= acceleration * Time.deltaTime * 0.5f;
        }
    }
}
