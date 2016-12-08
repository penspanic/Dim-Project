using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class InGameUiController : MonoBehaviour
{
    public GameObject stageWin;
    public GameObject stageLose;
    public GameObject optionPopup;

    private StageController stageCtrler;
    private Text timeText;
    private int stageClearLimitTime = 0;
    void Awake()
    {
        stageCtrler = GameObject.FindObjectOfType<StageController>();
        timeText = GameObject.Find("Time").GetComponent<Text>();
        stageClearLimitTime = DbManager.instance.GetStageClearLimitTime(PlayerData.instance.GetSelectedStageNum());

        timeText.text = stageClearLimitTime.ToString();

        StartCoroutine(StageStartProcess());

        Time.timeScale = 1f;
    }

    IEnumerator StageStartProcess()
    {
        yield return StartCoroutine(SceneController.instance.FadeIn(2f));

        yield return new WaitForSeconds(1f);

        stageCtrler.StageStart();

        StartCoroutine(StageClearTimeProcess());
    }

    IEnumerator StageClearTimeProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            stageClearLimitTime -= 1;

            if(stageClearLimitTime == 0)
            {
                stageCtrler.StageEnd(false);
                break;
            }

            timeText.text = stageClearLimitTime.ToString();
        }
    }

    public void ShowStageEndUi(bool isCleared)
    {
        stageClearLimitTime = 0;
        timeText.enabled = false;
        StopCoroutine(StageClearTimeProcess());

        if(isCleared == true)
        {
            stageWin.SetActive(true);
        }
        else
        {
            stageLose.SetActive(true);
        }
    }

    bool isSceneChanging = false;
    public void OnChangeScene()
    {
        if(isSceneChanging == true)
        {
            return;
        }

        isSceneChanging = true;

        SceneController.instance.haveToScout = true;
        StartCoroutine(SceneController.instance.FadeOut(1f, "Menu"));
    }


    #region Option button handlers
    public void OnOptionButtonDown()
    {
        Time.timeScale = 0f;
        optionPopup.SetActive(true);
    }

    public void OnRestartButtonDown()
    {
        optionPopup.SetActive(false);
        Time.timeScale = 0f;
        StartCoroutine(SceneController.instance.FadeOut(1f, "InGame"));
    }

    public void OnReturnToMenuButtonDown()
    {
        optionPopup.SetActive(false);
        Time.timeScale = 0f;
        StartCoroutine(SceneController.instance.FadeOut(1f, "Menu"));
    }

    public void OnCloseButtonDown()
    {
        optionPopup.SetActive(false);

        Time.timeScale = 1f;
    }

    #endregion

}
