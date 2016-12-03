using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {
        SoundManager.instance.LoadSound();
    }
    void Start()
    {
        SoundManager.instance.PlayBgmSound(SoundManager.BGM.Logo);
    }
    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SceneController.instance.FadeOut(1, "Menu"));
        }



    }
}
