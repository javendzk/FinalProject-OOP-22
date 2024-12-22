using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour
{
    public int moneyCount;
    public Text totalMoneyText;
    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player != null)
        {
            moneyCount = player.GetMoneyCount();
            totalMoneyText.text = moneyCount.ToString();
        }
    }

    void Update()
    {
        if (player != null)
        {
            moneyCount = player.GetMoneyCount();
            totalMoneyText.text = moneyCount.ToString();
        }
    }
}
