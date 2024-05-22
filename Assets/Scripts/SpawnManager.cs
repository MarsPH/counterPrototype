using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;


public class SpawnManager : MonoBehaviour
{
    public GameObject[] rocketPrefabs;

    public Transform[] rocketSpawnPosition;
    public Transform[] targets;


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

        GameObject rocket = Instantiate(rocketPrefabs[Random.Range(0,rocketPrefabs.Length)], spawnPoint.position, Quaternion.LookRotation(initialDirection));
        IncomingRocket incomingRocket = rocket.GetComponent<IncomingRocket>();
        if (incomingRocket != null)
        {
            incomingRocket.Initialize(initialDirection, chosenTarget);
        }
        else
        {
            Debug.LogError("IncomingRocket component is missing on the rocket prefab!");
        }
    }

}
