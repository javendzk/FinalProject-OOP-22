using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animatorRight;
    [SerializeField] Animator animatorLeft;

    void Awake()
    {
        animatorRight.enabled = false;
        animatorLeft.enabled = false;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        animatorRight.Rebind();
        animatorLeft.Rebind();
        animatorRight.Update(0f);
        animatorLeft.Update(0f);
        animatorRight.enabled = true;
        animatorLeft.enabled = true;
        animatorRight.ResetTrigger("Start");
        animatorLeft.ResetTrigger("Start");
        yield return new WaitForSeconds(1f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (asyncLoad.isDone)
        {
            animatorRight.SetBool("Start", true);
            animatorLeft.SetBool("Start", true);
        }
    }
}