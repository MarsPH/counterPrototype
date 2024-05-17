using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypersonicLauncer : MonoBehaviour
{
    public GameObject[] rocketPrefabs;
    public Transform[] rocketSpawnPosition;
    public Transform[] targets;

    static private float xLeftRange = 12f;
    static private float xRightRange = 29f;

    private void Start()
    {
        StartCoroutine(LaunchRockets());
    }

    IEnumerator LaunchRockets()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 5));
            LaunchRocketFromRandomPosition();
        }
    }

    private void LaunchRocketFromRandomPosition()
    {
        Transform chosenTarget = targets[Random.Range(0, targets.Length)];
        Transform spawnPoint = rocketSpawnPosition[Random.Range(0, rocketSpawnPosition.Length)];
        Vector3 initialDirection = (chosenTarget.position - spawnPoint.position).normalized;

        GameObject rocket = Instantiate(rocketPrefabs[Random.Range(0, rocketPrefabs.Length)], spawnPoint.position, Quaternion.LookRotation(initialDirection));
        HypersonicMissile hypersonicMissile = rocket.GetComponent<HypersonicMissile>();
        if (hypersonicMissile != null)
        {
            hypersonicMissile.Initialize(initialDirection, chosenTarget);
        }
        else
        {
            Debug.LogError("HypersonicMissile component is missing on the rocket prefab!");
        }
    }
}
