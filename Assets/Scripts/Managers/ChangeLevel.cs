using UnityEngine;
public class ChangeLevel : MonoBehaviour
{
    [SerializeField] string sceneName; 
    // [SerializeField] Vector3 respawnPosition;

    void Start()
    {
        // Initialization code here
    }
    void Update()
    {
        // Update code here
    }
    private void OnTriggerEnter2D(Collider2D other) // Tambahkan parameter respawnPosition
    {
        if (other.CompareTag("Player"))
        {
            // GameManager.Instance.SetRespawnPosition(respawnPosition);
            GameManager.Instance.LevelManager.LoadScene(sceneName); // Panggil metode LoadScene dengan nama scene
        }
    }
}