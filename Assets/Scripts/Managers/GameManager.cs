using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public LevelManager LevelManager { get; private set; }

    private GameObject mainCamera;
    private GameObject player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            LevelManager = GetComponentInChildren<LevelManager>();

            player = GameObject.FindWithTag("Player");

            if (player != null)
            {
                DontDestroyOnLoad(player);
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level1" || scene.name == "Level2" || scene.name == "Level3")
        {
            if (mainCamera != null)
            {
                mainCamera.SetActive(false);
            }
        }
        else
        {
            if (mainCamera != null)
            {
                mainCamera.SetActive(true);
            }
        }
    }

    // public void SetRespawnPosition(Vector3 position)
    // {
    //     respawnPosition = position;
    // }
}

