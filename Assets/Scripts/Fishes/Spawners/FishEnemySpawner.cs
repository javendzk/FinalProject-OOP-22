using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public int maxConcurrentEnemies = 5;
    public float spawnDelayMin = 1f;
    public float spawnDelayMax = 3f;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private bool isSpawning = false;

    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            SpawnEnemyImmediately();
        }

        StartCoroutine(SpawnEnemy());
    }

    void SpawnEnemyImmediately()
    {
        Vector3 spawnPosition = transform.position;
        bool validPosition = false;

        do
        {
            Collider2D hitCollider = Physics2D.OverlapCircle(spawnPosition, 0.5f);
            if (hitCollider == null)
            {
                validPosition = true;
            }
        } while (!validPosition);

        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);
        spawnedEnemies.Add(newEnemy);
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (!isSpawning && spawnedEnemies.Count < maxConcurrentEnemies)
            {
                isSpawning = true;
                SpawnEnemyImmediately();
                yield return new WaitForSeconds(Random.Range(spawnDelayMin, spawnDelayMax));
                isSpawning = false;
            }
            yield return null;
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
        }
    }
}
