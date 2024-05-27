using UnityEngine;

public class InterceptionLaser : MonoBehaviour
{
    public float heatUpTime = 2f;
    public float laserDuration = 0.5f;
    public Color coldColor = Color.blue;
    public Color hotColor = Color.red;
    public LineRenderer lineRenderer;
    public Transform laserOrigin;

    private float heatUpTimer = 0f;
    private bool isHeatingUp = false;
    private bool isFiring = false;
    private Transform target;

    void Update()
    {
        if (isHeatingUp)
        {
            heatUpTimer += Time.deltaTime;
            float progress = heatUpTimer / heatUpTime;
            lineRenderer.startColor = Color.Lerp(coldColor, hotColor, progress);
            lineRenderer.endColor = lineRenderer.startColor;

            // Update the positions while heating up to follow the target
            if (target != null)
            {
                lineRenderer.SetPosition(0, laserOrigin.position);
                lineRenderer.SetPosition(1, target.position);
            }

            if (heatUpTimer >= heatUpTime)
            {
                isHeatingUp = false;
                isFiring = true;
                heatUpTimer = 0f;
                FireLaser();
            }
        }

        if (isFiring)
        {
            // Update the positions while firing to ensure accuracy
            lineRenderer.SetPosition(0, laserOrigin.position);
            if (target != null)
            {
                lineRenderer.SetPosition(1, target.position);
            }
            else
            {
                // If the target is null, disable the line renderer
                lineRenderer.enabled = false;
                isFiring = false;
            }

            laserDuration -= Time.deltaTime;
            if (laserDuration <= 0)
            {
                isFiring = false;
                lineRenderer.enabled = false;
            }
        }
    }

    public void StartHeating(Transform target)
    {
        this.target = target;
        isHeatingUp = true;
        heatUpTimer = 0f;
        lineRenderer.enabled = true;
        lineRenderer.startColor = coldColor;
        lineRenderer.endColor = coldColor;
        lineRenderer.positionCount = 2; // Ensure the line has 2 positions
        lineRenderer.SetPosition(0, laserOrigin.position);
        lineRenderer.SetPosition(1, target.position);
    }

    private void FireLaser()
    {
        if (target != null)
        {
            lineRenderer.SetPosition(0, laserOrigin.position);
            lineRenderer.SetPosition(1, target.position);

            BaseRocket rocket = target.GetComponent<BaseRocket>();
            if (rocket != null)
            {
                rocket.TakeDamage(rocket.GetHealth()); // Destroy the rocket completely
            }
        }
    }
}
