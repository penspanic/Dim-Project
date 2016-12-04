using UnityEngine;
using System.Collections;

public class Credit : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine(SceneController.instance.FadeIn(1f));
    }

    public void Exit()
    {
        StartCoroutine(SceneController.instance.FadeOut(1f, "Menu"));
    }
}
