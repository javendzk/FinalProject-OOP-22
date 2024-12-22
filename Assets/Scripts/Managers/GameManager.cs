using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public LevelManager LevelManager { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out Canvas canvas) || child.TryGetComponent(out Image image))
                {
                    child.gameObject.SetActive(false);
                }
            }

            LevelManager = GetComponentInChildren<LevelManager>();

            var mainCamera = GameObject.FindWithTag("MainCamera");
            var player = GameObject.FindWithTag("Player");
            if (mainCamera != null)
            {
                DontDestroyOnLoad(mainCamera);
            }

            if (player != null)
            {
                DontDestroyOnLoad(player);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

