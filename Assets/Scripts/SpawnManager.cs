using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;


public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] Enemies;
    public GameObject rocketPrefab;

    public Transform[] rocketSpawnPosition;
    public Transform[] targets;

    static private float xLeftRange = 12f;
    static private  float xRightRange = 29f;

    private void Start()
    {
        StartCoroutine(LaunchRockets());
    }

    public void SpawnEnemies()
    {
        Instantiate(Enemies[Random.Range(0, Enemies.Length)], new Vector3(Random.Range(xLeftRange, xRightRange), 30, 20), transform.rotation);
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

        GameObject rocket = Instantiate(rocketPrefab, spawnPoint.position, Quaternion.LookRotation(initialDirection));
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
