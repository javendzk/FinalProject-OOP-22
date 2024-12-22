using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FishCounter : MonoBehaviour
{
    public int fishCount;
    public TextMeshProUGUI totalFishText;
    public Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (totalFishText == null)
        {
            totalFishText = GetComponent<TextMeshProUGUI>();
            if (totalFishText == null)
            {
                return;
            }
        }

        if (player != null)
        {
            fishCount = player.GetFishCount();
            totalFishText.text = fishCount.ToString();
            player.OnFishCountChanged += UpdateFishCountText;
        }
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }

        if (player != null)
        {
            fishCount = player.GetFishCount();
            totalFishText.text = fishCount.ToString();
        }
    }

    void UpdateFishCountText(int newFishCount)
    {
        fishCount = newFishCount;
        totalFishText.text = fishCount.ToString();
    }

    public void AddFish(int fish)
    {
        if (player != null)
        {
            player.SetFishCount(player.GetFishCount() + fish);
        }
    }

    void OnDestroy()
    {
        if (player != null)
        {
            player.OnFishCountChanged -= UpdateFishCountText;
        }
    }
}
