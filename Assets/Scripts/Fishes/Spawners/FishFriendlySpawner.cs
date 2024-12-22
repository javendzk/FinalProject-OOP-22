using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFriendlySpawner : MonoBehaviour
{
    public GameObject[] fishPrefabs;
    public int maxConcurrentFish = 15;
    public float spawnDelayMin = 1f;
    public float spawnDelayMax = 3f;
    public float fishLifespan = 60f;
    public float despawnDistance = 20f;
    private List<GameObject> spawnedFish = new List<GameObject>();
    private Transform player;
    private float minX, maxX, minY, maxY;
    private float camHalfHeight, camHalfWidth;

    void Start()
    {
        StartCoroutine(FindPlayer());
        RectTransform canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        Vector3[] canvasCorners = new Vector3[4];
        canvasRect.GetWorldCorners(canvasCorners);
        minX = canvasCorners[0].x;
        maxX = canvasCorners[2].x;
        minY = canvasCorners[0].y + 5f; 
        maxY = canvasCorners[2].y - 5f; 

        camHalfHeight = Camera.main.orthographicSize;
        camHalfWidth = camHalfHeight * Camera.main.aspect;

        StartCoroutine(SpawnFishRoutine());
    }

    IEnumerator FindPlayer()
    {
        while (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            yield return new WaitForSeconds(1f);
        }

        for (int i = 0; i < maxConcurrentFish; i++)
        {
            SpawnFish();
        }
    }

    void SpawnFish()
    {
        if (player == null) return;

        Vector3 spawnPosition = new Vector3(
            Random.Range(player.position.x - 10f, player.position.x + 10f),
            Random.Range(player.position.y - 10f, player.position.y + 10f),
            0f
        );

        spawnPosition.x = Mathf.Clamp(spawnPosition.x, minX + camHalfWidth, maxX - camHalfWidth);
        spawnPosition.y = Mathf.Clamp(spawnPosition.y, minY + 20, maxY - camHalfHeight);

        GameObject fishPrefab = fishPrefabs[Random.Range(0, fishPrefabs.Length)];
        GameObject newFish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity, transform);
        spawnedFish.Add(newFish);
        StartCoroutine(DestroyFishAfterLifespan(newFish));
    }

    IEnumerator DestroyFishAfterLifespan(GameObject fish)
    {
        yield return new WaitForSeconds(fishLifespan);
        if (fish != null)
        {
            spawnedFish.Remove(fish);
            Destroy(fish);
        }
    }

    IEnumerator SpawnFishRoutine()
    {
        while (true)
        {
            if (spawnedFish.Count < maxConcurrentFish)
            {
                SpawnFish();
            }

            yield return new WaitForSeconds(0.5f); // Flat delay of 0.5 seconds
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
                StartCoroutine(RespawnFishWithCooldown());
            }
        }
    }

    IEnumerator RespawnFishWithCooldown()
    {
        yield return new WaitForSeconds(1f); // Respawn cooldown of 1 second
        if (spawnedFish.Count < maxConcurrentFish)
        {
            SpawnFish();
        }
    }
}
