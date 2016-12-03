using UnityEngine;
using System.Collections;

public class DbManager : MonoBehaviour
{
    public static DbManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("DbManager").AddComponent<DbManager>();
            }
            return _instance;
        }
    }
    private static DbManager _instance;

    /*
    어떤 데이터들을 넣을지 확인해야 함
    스테이지 제한 시간
    */

    int[] stageClearLimitTime = new int[] { 60, 60, 60, 60, 60, 60, 60, 60, 60 };
    public int GetStageClearLimitTime(string stageId) // 스테이지 제한 시간
    {
        int stageNum = int.Parse(stageId.Substring(1, stageId.Length - 1));

        return stageClearLimitTime[stageNum - 1];
    }
}
