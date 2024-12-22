using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnShipManager : MonoBehaviour
{
    public Image uiImage; 
    private bool playerInArea = false;

    void Start()
    {
        if (uiImage != null)
        {
            uiImage.enabled = false; 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInArea = true;
            if (uiImage != null)
            {
                uiImage.enabled = true; 
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInArea = false;
            if (uiImage != null)
            {
                uiImage.enabled = false; 
            }
        }
    }

    void Update()
    {
        if (playerInArea && Input.GetKeyDown(KeyCode.H))
        {
            SceneManager.LoadScene("Recap"); // Load the "Recap" scene
        }
    }
}
