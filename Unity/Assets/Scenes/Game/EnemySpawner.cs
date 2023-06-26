using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject ratPrefab; // Rat Prefab
    public GameObject legionaerPrefab; // Legionaer Prefab
    public float spawnInterval = 5f;
    public float spawnY = 0f; // Y-Position of the spawn point

    private int consecutiveSpawns = 0; // Number of consecutive spawns on the same side
    private bool spawnOnLeft = true; // Start with spawn on the left side

    public int ratCount;

    //public static event Action OnLegionaerDeath; // Added event for legionaer death

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        float spawnX;
        bool spawnOnLeft;

        if (ratCount < 5)
        {
            spawnOnLeft = UnityEngine.Random.value < 0.5f; // Randomly determine the side for rat
            spawnX = spawnOnLeft ? -15f : 15f;
        }
        else
        {
            spawnOnLeft = UnityEngine.Random.value < 0.5f; // Randomly determine the side for legionaer
            spawnX = spawnOnLeft ? -15f : 15f;
        }

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        GameObject selectedEnemyPrefab;

        if (ratCount < 5)
        {
            selectedEnemyPrefab = ratPrefab;
        }
        else
        {
            selectedEnemyPrefab = legionaerPrefab;
        }

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

        ratCount++;

        // Check if the maximum number of regular enemies has been reached
        if (ratCount >= 5)
        {
            spawnOnLeft = !spawnOnLeft; // Switch sides for legionaer
        }
        else
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            enemyComponent.ratAnimator = enemy.GetComponent<Animator>();
            enemyComponent.enemyAI = enemy.GetComponent<EnemyAI>();
        }
    }
}
