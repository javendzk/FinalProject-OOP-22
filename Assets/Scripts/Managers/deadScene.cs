using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadScene : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            GameManager.Instance.LevelManager.LoadScene("land");
        }
    }
}