using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab1; // Erstes Enemy Prefab
    public GameObject enemyPrefab2; // Zweites Enemy Prefab
    public float spawnInterval = 5f;
    public float spawnY = 0f; // Y-Position des Spawn-Punkts

    private int consecutiveSpawns = 0; // Anzahl der aufeinanderfolgenden Spawns auf derselben Seite
    private bool spawnOnLeft = true; // Starte mit Spawn auf der linken Seite

    public int counter;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        float spawnX = spawnOnLeft ? -15f : 15f;
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        GameObject enemyPrefab;

        if (counter < 5)
        {
            enemyPrefab = enemyPrefab1;
        }
        else
        {
            enemyPrefab = enemyPrefab2;
        }

        // Instantiate the enemy prefab with the appropriate spawn position and rotation
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Flip the enemy's direction if it spawns on the left side
        if (spawnOnLeft)
        {
            // Get the sprite renderer component of the enemy
            SpriteRenderer enemyRenderer = enemy.GetComponent<SpriteRenderer>();

            // Flip the enemy's sprite horizontally
            enemyRenderer.flipX = true;
        }

        consecutiveSpawns++;
        counter++;

        // Check if the maximum number of consecutive spawns on the same side has been reached
        if (consecutiveSpawns >= 2)
        {
            spawnOnLeft = !spawnOnLeft; // Switch sides
            consecutiveSpawns = 0; // Reset the counter
        }
    }
}
