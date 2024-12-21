using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemManager : MonoBehaviour
{
    public GameObject playerFisherPrefab;
    public GameObject playerGunPrefab;
    private PlayerItem playerFisher;
    private PlayerItem playerGun;
    private PlayerItem currentItem;

    void Start()
    {
        GameObject fisherInstance = Instantiate(playerFisherPrefab, transform);
        GameObject gunInstance = Instantiate(playerGunPrefab, transform);

        playerFisher = fisherInstance.GetComponent<PlayerItem>();
        playerGun = gunInstance.GetComponent<PlayerItem>();

        playerFisher.gameObject.SetActive(false);
        playerGun.gameObject.SetActive(false);

        currentItem = playerFisher;
        currentItem.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleCurrentItem();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SwitchItem();
        }
    }

    void ToggleCurrentItem()
    {
        currentItem.ToggleItem();
    }

    void SwitchItem()
    {
        currentItem.gameObject.SetActive(false); 

        if (currentItem == playerFisher)
        {
            currentItem = playerGun;
        }
        else
        {
            currentItem = playerFisher;
        }

        currentItem.gameObject.SetActive(true);
    }
}
