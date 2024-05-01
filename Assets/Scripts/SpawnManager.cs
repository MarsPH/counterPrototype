using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] Enemies;

    static private float xLeftRange = 12f;
    static private  float xRightRange = 29f;

    private void Start()
    {
        InvokeRepeating("SpawnEnemies", 1f, 2f);
    }

    public void SpawnEnemies()
    {
        Instantiate(Enemies[Random.Range(0, Enemies.Length)], new Vector3(Random.Range(xLeftRange, xRightRange), 30, 20), transform.rotation);
    }
}
