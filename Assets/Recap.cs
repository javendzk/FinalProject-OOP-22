using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Recap : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
    private bool counterDone;
    private int displayedFishCount;
    private int targetFishCount;
    [SerializeField] private float counterSpeed = 1f;
    private TextMeshProUGUI fishCountText;

    void Start()
    {
        fishCountText = GameObject.Find("Canvas/Counter").GetComponent<TextMeshProUGUI>();
        targetFishCount = player.GetFishCount();
        displayedFishCount = 0;
        counterDone = false;
        UpdateFishCountText();
    }

    // Update is called once per frame
    void Update()
    {
        if (!counterDone)
        {
            if (displayedFishCount < targetFishCount)
            {
                displayedFishCount += Mathf.CeilToInt(counterSpeed);
                if (displayedFishCount >= targetFishCount)
                {
                    displayedFishCount = targetFishCount;
                    counterDone = true;
                }
                UpdateFishCountText();
            }
        }

        if (Input.anyKeyDown)
        {
            if (counterDone)
            {
                GameManager.Instance.LevelManager.LoadScene("Land");
            }
            else
            {
                displayedFishCount = targetFishCount;
                counterDone = true;
                UpdateFishCountText();
            }
        }
    }

    void UpdateFishCountText()
    {
        fishCountText.text = displayedFishCount.ToString();
    }
}
