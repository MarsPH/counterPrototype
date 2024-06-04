using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI waveText;
    public TextMeshProUGUI scoreText;
    public GameObject easyRocketPrefab;
    public GameObject mediumRocketPrefab;
    public GameObject hardRocketPrefab;
    public Transform[] rocketSpawnPositions;
    public Transform[] targets;
    public int currentWave = 0;
    public int score = 0;
    public string targetTag = "Enemy's Target";
    private int rocketsToSpawn;
    private int rocketsRemaining;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeTargets();
        StartNextWave();
    }

    void InitializeTargets()
    {
        GameObject[] targetObjects = GameObject.FindGameObjectsWithTag(targetTag);
        targets = new Transform[targetObjects.Length];
        for (int i = 0; i < targetObjects.Length; i++)
        {
            targets[i] = targetObjects[i].transform;
        }
    }

    void StartNextWave()
    {
        currentWave++;
        rocketsToSpawn = currentWave * 5; // Example: Increase number of rockets each wave
        rocketsRemaining = rocketsToSpawn;
        waveText.text = "Wave: " + currentWave;

        StartCoroutine(LaunchRockets());
    }

    IEnumerator LaunchRockets()
    {
        for (int i = 0; i < rocketsToSpawn; i++)
        {
            LaunchRocketFromRandomPosition();
            yield return new WaitForSeconds(Random.Range(2, 5)); // Random delay between rocket spawns
        }
    }

    private void LaunchRocketFromRandomPosition()
    {
        if (targets.Length == 0)
        {
            Debug.LogError("No targets available!");
            return;
        }

        Transform chosenTarget = targets[Random.Range(0, targets.Length)];
        Transform spawnPoint = rocketSpawnPositions[Random.Range(0, rocketSpawnPositions.Length)];
        Vector3 initialDirection = (chosenTarget.position - spawnPoint.position).normalized;

        GameObject rocketPrefab = GetRocketPrefab();
        GameObject rocket = Instantiate(rocketPrefab, spawnPoint.position, Quaternion.LookRotation(initialDirection));
        BaseRocket baseRocket = rocket.GetComponent<BaseRocket>();
        if (baseRocket != null)
        {
            baseRocket.Initialize(initialDirection, chosenTarget);
        }
        else
        {
            Debug.LogError("BaseRocket component is missing on the rocket prefab!");
        }
    }

    GameObject GetRocketPrefab()
    {
        float difficultyRatio = Mathf.Clamp01((float)currentWave / 10); // Assuming max difficulty at wave 10
        float easyChance = Mathf.Lerp(0.7f, 0.1f, difficultyRatio);
        float mediumChance = Mathf.Lerp(0.2f, 0.4f, difficultyRatio);
        float hardChance = Mathf.Lerp(0.1f, 0.5f, difficultyRatio);

        float randomValue = Random.value;
        if (randomValue < easyChance)
        {
            return easyRocketPrefab;
        }
        else if (randomValue < easyChance + mediumChance)
        {
            return mediumRocketPrefab;
        }
        else
        {
            return hardRocketPrefab;
        }
    }

    public void RocketDestroyed()
    {
        rocketsRemaining--;
        //score += 100; 
        //scoreText.text = "Score: " + score;

        if (rocketsRemaining <= 0)
        {
            StartNextWave();
        }
    }
}
