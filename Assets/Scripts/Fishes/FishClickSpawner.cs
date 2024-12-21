using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FishClickSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] fishVariants;
    [SerializeField] private int selectedVariant = 0;

    void Start()
    {
        Assert.IsTrue(fishVariants.Length > 0, "Please add at least one Fish Prefab!");
    }

    void Update()
    {
        for (int i = 1; i <= fishVariants.Length; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                selectedVariant = i - 1;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            SpawnFish();
        }
    }

    private void SpawnFish()
    {
        if (selectedVariant < fishVariants.Length)
        {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0;
            Instantiate(fishVariants[selectedVariant], spawnPosition, Quaternion.identity);
        }
    }
}
