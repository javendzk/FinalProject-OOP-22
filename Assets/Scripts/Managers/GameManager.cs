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
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                DontDestroyOnLoad(player);
            }
        }

        GameObject sceneCamera = GameObject.FindWithTag("MainCamera");

        if (scene.name == "level1" || scene.name == "level2" || scene.name == "level3")
        {
            if (sceneCamera != null && sceneCamera != mainCamera)
            {
                sceneCamera.SetActive(false);
            }
            if (mainCamera != null)
            {
                mainCamera.SetActive(true);
                Debug.Log("adasad");
            }
        }
        else
        {
            if (sceneCamera != null && sceneCamera != mainCamera)
            {
                sceneCamera.SetActive(true);
            }
            if (mainCamera != null)
            {
                mainCamera.SetActive(false);
            }
        }
    }
}

