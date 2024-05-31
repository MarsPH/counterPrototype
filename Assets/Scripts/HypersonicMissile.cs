using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HypersonicMissile : BaseRocket
{
    [SerializeField] private float heatThreshold = 100f; 
    private float heatLevel = 0f; 

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
    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
        if (currentHealth <= 0)
        {
            int score = GameManager.instance.score += 2;
            GameManager.instance.scoreText.text = "Score: " + score;

        }
    }
}
