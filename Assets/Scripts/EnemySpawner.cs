using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    private List<IEnemy> spawnedEnemies = new List<IEnemy>();
    private int totalEnemiesToSpawn;
    private int enemiesSpawned;

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }

        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab is not assigned!");
            return;
        }

        totalEnemiesToSpawn = spawnPoints.Length;
        enemiesSpawned = 0;
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        Debug.Log("Spawning enemies...");

        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("Spawned enemy at: " + spawnPoint.position);

            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                spawnedEnemies.Add(enemyScript);
                enemyScript.SetCanShootOrBeHit(false);
            }
            else
            {
                Debug.LogError("Enemy script not found on prefab!");
            }

            enemiesSpawned++;

            if (enemiesSpawned == totalEnemiesToSpawn)
            {
                EnableEnemiesToShootOrBeHit();
            }
        }
    }

    void EnableEnemiesToShootOrBeHit()
    {
        Debug.Log("Enabling enemies to shoot and be hit.");

        foreach (IEnemy enemy in spawnedEnemies)
        {
            enemy.SetCanShootOrBeHit(true);
        }
    }
}
