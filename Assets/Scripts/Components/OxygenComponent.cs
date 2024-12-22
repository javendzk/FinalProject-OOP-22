using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenComponent : MonoBehaviour
{
    public OxygenBar oxygenBar;
    public float maxOxygen = 100f;
    public float currentOxygen;
    public float oxygenDepletionRate = 1.0f;

    void Start()
    {
        if (oxygenBar == null)
        {
            GameObject oxygenBarObject = GameObject.Find("OxygenBar");
            if (oxygenBarObject != null)
            {
                oxygenBar = oxygenBarObject.GetComponent<OxygenBar>();
            }
        }

        currentOxygen = maxOxygen;
        oxygenBar.SetMaxOxygen(maxOxygen);
    }

    void Update()
    {
        UpdateOxygen();
    }

    public void UpdateOxygen()
    {
        currentOxygen -= oxygenDepletionRate * Time.deltaTime;
        if (currentOxygen < 0)
        {
            currentOxygen = 0;
        }
        oxygenBar.SetOxygen(currentOxygen);
    }
}
