using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishCounter : MonoBehaviour
{
    public int fishCount;
    public Text totalFishText;
    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player != null)
        {
            fishCount = player.GetFishCount();
            totalFishText.text = fishCount.ToString();
        }
    }

    void Update()
    {
        if (player != null)
        {
            fishCount = player.GetFishCount();
            totalFishText.text = fishCount.ToString();
        }
    }

    public void AddFish(int fish)
    {
        if (player != null)
        {
            player.SetFishCount(player.GetFishCount() + fish);
            fishCount = player.GetFishCount();
            totalFishText.text = fishCount.ToString();
        }
    }
}
