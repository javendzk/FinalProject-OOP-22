using UnityEngine;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField] string sceneName; // Tambahkan variabel untuk menyimpan nama scene

    void Start()
    {
        // Initialization code here
    }

    void Update()
    {
        // Update code here
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LevelManager.LoadScene(sceneName); // Panggil metode LoadScene dengan nama scene
        }
    }
}