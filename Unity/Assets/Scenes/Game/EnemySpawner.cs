using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;
    public float spawnY = 0f; // Y-Position des Spawn-Punkts

    private int consecutiveSpawns = 0; // Anzahl der aufeinanderfolgenden Spawns auf derselben Seite
    private bool spawnOnLeft = true; // Starte mit Spawn auf der linken Seite

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        float spawnX = spawnOnLeft ? -15f : 15f;
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        consecutiveSpawns++;
        
        // Überprüfe, ob die maximale Anzahl auf derselben Seite erreicht wurde
        if (consecutiveSpawns >= 2)
        {
            spawnOnLeft = !spawnOnLeft; // Wechsle die Seite
            consecutiveSpawns = 0; // Setze den Counter zurück
        }
    }
}
