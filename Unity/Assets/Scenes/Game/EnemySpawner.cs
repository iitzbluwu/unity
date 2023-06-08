using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;
    public float spawnY = 0f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        float spawnX = Random.Range(0, 2) == 0 ? -15f : 15f; // Zufällige Auswahl zwischen -15f und 15f für X-Position
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
