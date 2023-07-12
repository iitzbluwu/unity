using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject ratPrefab; // Rat Prefab
    public GameObject legionaerPrefab; // Legionaer Prefab
    public GameObject greifPrefab; // Greif Prefab
    public GameObject loewePrefab; // Loewe Prefab
    public float spawnInterval = 5f;

    private float elapsedTime = 0f; // Total elapsed time
    private int ratCount = 0; // Number of spawned rat enemies
    private int legionaerCount = 0; // Number of spawned legionaer enemies
    private int greifCount = 0; // Number of spawned greif enemies
    private int loeweCount = 0; // Number of spawned loewe enemies

    // Define time intervals and their corresponding spawn probabilities
    public SpawnInterval[] spawnIntervals;

    [Serializable]
    public struct SpawnInterval
    {
        public float duration; // Duration of the interval
        [Range(0f, 1f)] public float ratSpawnProbability; // Probability of spawning a rat enemy
        [Range(0f, 1f)] public float legionaerSpawnProbability; // Probability of spawning a legionaer enemy
        [Range(0f, 1f)] public float greifSpawnProbability; // Probability of spawning a greif enemy
        [Range(0f, 1f)] public float loeweSpawnProbability; // Probability of spawning a loewe enemy
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
        Debug.Log($"Greif Spawn Probability: {currentInterval.greifSpawnProbability * 100}%");
        Debug.Log($"Loewe Spawn Probability: {currentInterval.loeweSpawnProbability * 100}%");
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
        else if (randomValue < currentInterval.ratSpawnProbability + currentInterval.legionaerSpawnProbability + currentInterval.greifSpawnProbability)
        {
            selectedEnemyPrefab = greifPrefab;
        }
        else if (randomValue < currentInterval.ratSpawnProbability + currentInterval.legionaerSpawnProbability + currentInterval.greifSpawnProbability + currentInterval.loeweSpawnProbability)
        {
            selectedEnemyPrefab = loewePrefab;
        }
        else
        {
            // No enemy to spawn in this interval
            return;
        }

        // Determine the spawn position and side
        bool spawnOnLeft = Random.value < 0.5f;
        float spawnX;
        float spawnY;
        if (selectedEnemyPrefab == greifPrefab)
        {
            //if spawns -15f | move collision to -0.1
            spawnX = spawnOnLeft ? -15f : 15f;
            spawnY = 2.6f;
        }
        else
        {
            spawnX = spawnOnLeft ? -15f : 15f;
            spawnY = -1f;
        }
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
        else if (selectedEnemyPrefab == greifPrefab)
        {
            greifCount++;
        }
        else if (selectedEnemyPrefab == loewePrefab)
        {
            loeweCount++;
        }

        Enemy enemyComponent = enemy.GetComponent<Enemy>();
        enemyComponent.ratAnimator = enemy.GetComponent<Animator>();
        enemyComponent.enemyAI = enemy.GetComponent<EnemyAI>();

        //Greif greifComponent = enemy.GetComponent<Greif>();
        //greifComponent.greifAnimator = enemy.GetComponent<Animator>();
        //greifComponent.greifAI = enemy.GetComponent<GreifAI>();

        if (selectedEnemyPrefab == greifPrefab)
        {
            // Verschiebe die Kollisionsbox des Greif-Enemies um -0.2 auf der X-Achse nach rechts
            BoxCollider2D collider = enemy.GetComponent<BoxCollider2D>();
            collider.offset = new Vector2(collider.offset.x - 0.2f, collider.offset.y);
        }
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
