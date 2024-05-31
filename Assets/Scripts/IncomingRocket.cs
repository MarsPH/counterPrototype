using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class IncomingRocket : BaseRocket
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
        if (currentHealth <= 0) 
        {
            int score = GameManager.instance.score += 1; // Additional behavior: Add another poin
            GameManager.instance.scoreText.text = "Score: " + score;

        }
    }
}
