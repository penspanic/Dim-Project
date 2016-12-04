using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageData : MonoBehaviour
{
    [SerializeField]
    private string stageLevel;
    [SerializeField]
    private bool stagelock;

    public bool StageLocked
    {
        get
        {
            return stagelock;
        }
    }

    void Awake()
    {
        LockStage();
        if(stagelock == false)
        {
            transform.FindChild("Background").GetComponent<Image>().sprite = Resources.Load<Sprite>("Ui/Dungeon_open");
        }
    }

    private void LockStage()
    {
        int targetStageNum = int.Parse(stageLevel.Substring(1, stageLevel.Length - 1));

        if(targetStageNum == 1)
        {
            stagelock = false;
            return;
        }

        if(PlayerData.instance.GetLastClearedStageNum() == 0)
        {
            stagelock = true;
            return;
        }

        int lastClearedStageNum = PlayerData.instance.GetLastClearedStageNum();
        if (lastClearedStageNum + 1 >= targetStageNum)
        {
            stagelock = false;
        }
        else
        {
            stagelock = true;
        }
    }

    public void SetStagenumber()
    {
        PlayerData.instance.SetSelectedStage(int.Parse(stageLevel.Substring(1, stageLevel.Length - 1)));
    }
}
