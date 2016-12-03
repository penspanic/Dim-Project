using UnityEngine;
using System.Collections;

public class DbManager : MonoBehaviour
{
    public static DbManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameObject("DbManager").AddComponent<DbManager>();
            }
            return _instance;
        }
    }
    private static DbManager _instance;

    /*
    어떤 데이터들을 넣을지 확인해야 함
    아마 필요 없을 듯
    */
}
