using UnityEngine;
using System.Collections;

public class StageData : MonoBehaviour
{
    [SerializeField]
    private string stageLevel;
    [SerializeField]
    private bool stagelock;

    public bool Stagelock
    {
        get
        {
            return stagelock;
        }
    }


    void Start()
    {
      
        int[] temp = new int[2];
        temp[0] = int.Parse(stageLevel.Substring(1, stageLevel.Length - 1));
        temp[1] = int.Parse(PlayerData.instance.LastClearedStageId.Substring(1, stageLevel.Length - 1));
        if (temp[0] < temp[1])
        {
            stagelock = false;

        }
        else
        {
            stagelock = true;
        }
       // StageLock();
    }
 private void StageLock()
    {
        int[] temp = new int[2];
        temp[0] = int.Parse(stageLevel.Substring(1, stageLevel.Length - 1));
        temp[1] = int.Parse(PlayerData.instance.LastClearedStageId.Substring(1, stageLevel.Length - 1));
        if (temp[0] < temp[1])
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
        PlayerData.instance.SelectedStageId = stageLevel;
        Debug.Log(PlayerData.instance.SelectedStageId);
    }
}
