using UnityEngine;
using System.Collections;

public class StageData : MonoBehaviour
{[SerializeField]
    private string stageLevel;
    
 
    public void SetStagenumber()
    {
        PlayerData.SelectedStageId = stageLevel;
        Debug.Log(PlayerData.SelectedStageId);
    }
}
