using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishCounter : MonoBehaviour
{
    public int fishCount;
    public Text totalFishText;
    // Start is called before the first frame update
    void Start()
    {
        fishCount = 0; //start = 0 fish
        totalFishText.text = fishCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddFish(int fish)
    {
        fishCount += fish;
        totalFishText.text = fishCount.ToString();
    }
}
