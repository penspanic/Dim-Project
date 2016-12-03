﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UiController : MonoBehaviour
{
    private StageController stageCtrler;
    private Text timeText;
    private int stageClearLimitTime = 0;
    void Awake()
    {
        stageCtrler = GameObject.FindObjectOfType<StageController>();
        timeText = GameObject.Find("Time").GetComponent<Text>();
        stageClearLimitTime = DbManager.instance.GetStageClearLimitTime(PlayerData.SelectedStageId);

        timeText.text = stageClearLimitTime.ToString();

        StartCoroutine(StageStartProcess());
    }

    IEnumerator StageStartProcess()
    {
        yield return new WaitForSeconds(1f);

        stageCtrler.StageStart();

        StartCoroutine(StageClearTimeProcess());
    }

    IEnumerator StageClearTimeProcess()
    {
        while (stageClearLimitTime > 1)
        {
            yield return new WaitForSeconds(1f);
            stageClearLimitTime -= 1;

            timeText.text = stageClearLimitTime.ToString();
        }
    }

    public void ShowStageEndUi(bool isCleared)
    {
        stageClearLimitTime = 0;
        StopCoroutine(StageClearTimeProcess());
    }
}
