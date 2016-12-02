using UnityEngine;
using System.Collections;

public class UiController : MonoBehaviour
{
    private StageController stageCtrler;
    void Awake()
    {
        stageCtrler = GameObject.FindObjectOfType<StageController>();

        StartCoroutine(StageStartProcess());
    }

    IEnumerator StageStartProcess()
    {
        yield return new WaitForSeconds(1f);

        stageCtrler.StageStart();
    }

    public void ShowStageEndUi(bool isCleared)
    {

    }
}
