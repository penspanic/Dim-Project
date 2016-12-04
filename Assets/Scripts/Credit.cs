using UnityEngine;
using System.Collections;

public class Credit : MonoBehaviour
{

    public void Exit()
    {
        StartCoroutine(SceneController.instance.FadeOut(1, "Menu"));
    }
}
