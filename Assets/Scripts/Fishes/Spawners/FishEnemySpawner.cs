using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public int maxConcurrentEnemies = 5;
    public float spawnDelay = 3f;
    public float despawnDistance = 20f;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < maxConcurrentEnemies; i++)
        {
            SpawnEnemy();
        }

        StartCoroutine(SpawnEnemyRoutine());
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = transform.position;

        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);
        newEnemy.transform.SetParent(transform); // Set the parent of the instantiated enemy to the spawner's transform
        spawnedEnemies.Add(newEnemy);
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            if (spawnedEnemies.Count < maxConcurrentEnemies)
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(spawnDelay); // Flat delay of 3 seconds
        }
    }

    void Update()
    {
        for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
        {
            if (spawnedEnemies[i] == null)
            {
                spawnedEnemies.RemoveAt(i);
                continue;
            }

            float distanceToPlayer = Vector2.Distance(spawnedEnemies[i].transform.position, player.position);
            if (distanceToPlayer > despawnDistance)
            {
                Destroy(spawnedEnemies[i]);
                spawnedEnemies.RemoveAt(i);
                StartCoroutine(RespawnEnemyWithCooldown());
            }
        }
    }

    IEnumerator RespawnEnemyWithCooldown()
    {
        yield return new WaitForSeconds(1f); // Respawn cooldown of 1 second
        if (spawnedEnemies.Count < maxConcurrentEnemies)
        {
            SpawnEnemy();
        }
    }
}
