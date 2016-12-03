using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// 싱글톤.
public class SceneController : MonoBehaviour
{
    public static SceneController instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Fade Object")).AddComponent<SceneController>();
            }
            return _instance;
        }
    }
    private static SceneController _instance;
    private SpriteRenderer sprRenderer;

    void Awake()
    {
        sprRenderer = GetComponent<SpriteRenderer>();

        DontDestroyOnLoad(this.gameObject);
    }

    public IEnumerator FadeOut(float duration, string nextScene = null)
    {
        float elapsedTime = 0f;

        sprRenderer.enabled = true;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float alpha = Mathf.MoveTowards(0, 1, elapsedTime / duration);
            sprRenderer.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        sprRenderer.color = new Color(0, 0, 0, 1);
        if (nextScene != null)
            SceneManager.LoadScene(nextScene);
    }

    public IEnumerator FadeIn(float duration, string nextScene = null)
    {
        float elapsedTime = 0f;

        sprRenderer.enabled = true;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float alpha = 1f - Mathf.MoveTowards(0, 1, elapsedTime / duration);
            sprRenderer.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        sprRenderer.color = new Color(0, 0, 0, 0);
        sprRenderer.enabled = false;
        if (nextScene != null)
            SceneManager.LoadScene(nextScene);
    }
}
