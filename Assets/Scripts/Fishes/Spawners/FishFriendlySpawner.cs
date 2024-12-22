using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFriendlySpawner : MonoBehaviour
{
    public GameObject[] fishPrefabs;
    public int maxConcurrentFish = 15;
    public float spawnDelayMin = 1f;
    public float spawnDelayMax = 3f;
    public float despawnDistance = 20f;
    private List<GameObject> spawnedFish = new List<GameObject>();
    private Transform player;
    private float minX, maxX, minY, maxY;
    private float camHalfHeight, camHalfWidth;
    private bool isSpawning = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        RectTransform canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        Vector3[] canvasCorners = new Vector3[4];
        canvasRect.GetWorldCorners(canvasCorners);
        minX = canvasCorners[0].x;
        maxX = canvasCorners[2].x;
        minY = canvasCorners[0].y;
        maxY = canvasCorners[2].y;

        camHalfHeight = Camera.main.orthographicSize;
        camHalfWidth = camHalfHeight * Camera.main.aspect;

        for (int i = 0; i < maxConcurrentFish; i++)
        {
            SpawnFishImmediately();
        }

        StartCoroutine(SpawnFish());
    }

    void SpawnFishImmediately()
    {
        Vector3 spawnPosition;
        bool validPosition = false;

        do
        {
            float spawnX = Random.Range(minX + camHalfWidth * 2, maxX - camHalfWidth * 2);
            float spawnY = Random.Range(minY + camHalfHeight * 4, maxY - camHalfHeight * 4);
            spawnPosition = new Vector3(spawnX, spawnY, 0);

            Collider2D hitCollider = Physics2D.OverlapCircle(spawnPosition, 0.5f);
            if (hitCollider == null)
            {
                validPosition = true;
            }
        } while (!validPosition);

        GameObject fishPrefab = fishPrefabs[Random.Range(0, fishPrefabs.Length)];
        GameObject newFish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity, transform);
        spawnedFish.Add(newFish);
    }

    IEnumerator SpawnFish()
    {
        while (true)
        {
            if (!isSpawning && spawnedFish.Count < maxConcurrentFish)
            {
                isSpawning = true;
                SpawnFishImmediately();
                yield return new WaitForSeconds(Random.Range(spawnDelayMin, spawnDelayMax));
                isSpawning = false;
            }
            yield return null;
        }
    }

    void Update()
    {
        for (int i = spawnedFish.Count - 1; i >= 0; i--)
        {
            if (spawnedFish[i] == null)
            {
                spawnedFish.RemoveAt(i);
                continue;
            }

            float distanceToPlayer = Vector2.Distance(spawnedFish[i].transform.position, player.position);
            if (distanceToPlayer > despawnDistance)
            {
                Destroy(spawnedFish[i]);
                spawnedFish.RemoveAt(i);
            }
        }
    }
}
