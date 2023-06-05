using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ratPrefab;

    [SerializeField]
    private float ratInterval = 3.5f;

    int zahl1 = 15;
    int zahl2 = -15;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(ratInterval, ratPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {   
        int rndZahl = Random.Range(0, 2) == 0 ? zahl1: zahl2;

        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(rndZahl, 0, 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
