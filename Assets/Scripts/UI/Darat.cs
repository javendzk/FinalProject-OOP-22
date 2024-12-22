using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Darat : MonoBehaviour
{
    public void Dive()
    {
        // GameManager.Instance.LevelManager.LoadScene("Level1");
        SceneManager.LoadScene("Level1");
        Debug.Log("Hello");
    }

}
