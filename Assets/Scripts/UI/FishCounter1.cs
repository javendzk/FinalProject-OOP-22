using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FishCounter1 : MonoBehaviour
{
    public int fishCount;
    public Text fishCountText;
    // Start is called before the first frame update
    void Start()
    {
        fishCount = 0;
        fishCountText.text = fishCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddFish()
    {
        fishCount ++;
        fishCountText.text = fishCount.ToString();
    }
}
