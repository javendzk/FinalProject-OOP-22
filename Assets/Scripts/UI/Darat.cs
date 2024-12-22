using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Darat : MonoBehaviour
{
    public Button diveButton;

    void Start()
    {
        diveButton = GameObject.Find("Canvas/Dive!").GetComponent<Button>();

        diveButton.gameObject.SetActive(false);
        StartCoroutine(EnableButtonAfterDelay(3f));
    }

    IEnumerator EnableButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        diveButton.gameObject.SetActive(true);
    }

    public void Dive()
    {
        GameManager.Instance.LevelManager.LoadScene("Level1");
        // SceneManager.LoadScene("Level1");
        Debug.Log("Hello");
    }
}
