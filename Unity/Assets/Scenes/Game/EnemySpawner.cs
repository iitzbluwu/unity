using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject ratPrefab; // Rat Prefab
    public GameObject legionaerPrefab; // Legionaer Prefab
    public float spawnInterval = 5f;
    public float spawnY = 0f; // Y-Position of the spawn point

    private float elapsedTime = 0f; // Total elapsed time
    private int ratCount = 0; // Number of spawned rat enemies
    private int legionaerCount = 0; // Number of spawned legionaer enemies

    // Define time intervals and their corresponding spawn probabilities
    public SpawnInterval[] spawnIntervals;

    [Serializable]
    public struct SpawnInterval
    {
        public float duration; // Duration of the interval
        [Range(0f, 1f)] public float ratSpawnProbability; // Probability of spawning a rat enemy
        [Range(0f, 1f)] public float legionaerSpawnProbability; // Probability of spawning a legionaer enemy
    }

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, spawnInterval);
        InvokeRepeating("DisplaySpawnProbabilities", 20f, 20f);
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    private void DisplaySpawnProbabilities()
    {
        SpawnInterval currentInterval = GetSpawnInterval();
        Debug.Log($"Current Interval: {currentInterval.duration}s");
        Debug.Log($"Rat Spawn Probability: {currentInterval.ratSpawnProbability * 100}%");
        Debug.Log($"Legionaer Spawn Probability: {currentInterval.legionaerSpawnProbability * 100}%");
    }

    private void SpawnEnemy()
    {
        SpawnInterval currentInterval = GetSpawnInterval();

        // Determine the enemy type based on the spawn probabilities
        GameObject selectedEnemyPrefab;
        float randomValue = Random.value;
        if (randomValue < currentInterval.ratSpawnProbability)
        {
            selectedEnemyPrefab = ratPrefab;
        }
        else if (randomValue < currentInterval.ratSpawnProbability + currentInterval.legionaerSpawnProbability)
        {
            selectedEnemyPrefab = legionaerPrefab;
        }
        else
        {
            // No enemy to spawn in this interval
            return;
        }

        // Determine the spawn position and side
        bool spawnOnLeft = Random.value < 0.5f;
        float spawnX = spawnOnLeft ? -15f : 15f;
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        // Instantiate the selected enemy prefab with the appropriate spawn position and rotation
        GameObject enemy = Instantiate(selectedEnemyPrefab, spawnPosition, Quaternion.identity);

        // Flip the enemy's direction if it spawns on the left side
        if (spawnOnLeft)
        {
            // Get the sprite renderer component of the enemy
            SpriteRenderer enemyRenderer = enemy.GetComponent<SpriteRenderer>();

            // Flip the enemy's sprite horizontally
            enemyRenderer.flipX = true;
        }

        if (selectedEnemyPrefab == ratPrefab)
        {
            ratCount++;
        }
        else if (selectedEnemyPrefab == legionaerPrefab)
        {
            legionaerCount++;
        }

        Enemy enemyComponent = enemy.GetComponent<Enemy>();
        enemyComponent.ratAnimator = enemy.GetComponent<Animator>();
        enemyComponent.enemyAI = enemy.GetComponent<EnemyAI>();
    }

    private SpawnInterval GetSpawnInterval()
    {
        foreach (SpawnInterval interval in spawnIntervals)
        {
            if (elapsedTime <= interval.duration)
            {
                return interval;
            }
        }

        // Return the last defined interval if the elapsed time exceeds all intervals
        return spawnIntervals[spawnIntervals.Length - 1];
    }
}
