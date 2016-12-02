using UnityEngine;
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
                _instance = new GameObject("SceneController").AddComponent<SceneController>();
            }
            return _instance;
        }
    }
    private static SceneController _instance;

    public void FadeIn(float time)
    {
        StartCoroutine(FadeProcess(false, time));
    }
    
    public void FadeOut(float time)
    {
        StartCoroutine(FadeProcess(true, time));
    }

    IEnumerator FadeProcess(bool isFadeOut, float time)
    {
        float elapsedTime = 0f;

        while(elapsedTime <= time)
        {
            yield return null;
        }
    }

}
