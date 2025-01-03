using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{

    public int index;
    [SerializeField] bool keyDown;
    [SerializeField] int maxIndex;

    // Start is called before the first frame update
    void Start()
    {
        keyDown = false;

        GameObject gameManagerObject = GameObject.FindWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            if (!keyDown)
            {
                if (Input.GetAxis("Vertical") < 0)
                {
                    if (index < maxIndex)
                    {
                        index++;
                    }
                    else
                    {
                        index = 0;
                    }
                }
                else if (Input.GetAxis("Vertical") > 0)
                {
                    if (index > 0)
                    {
                        index--;
                    }
                    else
                    {
                        index = maxIndex;
                    }
                }
                keyDown = true;
            }
        }
        else
        {
            keyDown = false;
        }
    }
    public void PlayGame()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance is not found.");
            return;
        }

        GameManager.Instance.LevelManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
