using UnityEngine;
using System.Collections;

//변수선언
public partial class StageUIController : MonoBehaviour
{

    private static StageUIController _instance;

    //UI오브젝트
    [SerializeField]
    private GameObject checkParty;
    [SerializeField]
    private GameObject scout;
    [SerializeField]
    private GameObject changeHero;
    [SerializeField]
    private GameObject checkYesNo;

    //선택된 캐릭터
    [SerializeField]
    private bool canChange;

}
//함수구현
public partial class StageUIController : MonoBehaviour
{

    // 싱글톤
    #region 
    public static StageUIController instance
    {
        get
        {
            if (_instance == null)
            {
                var newobj = new GameObject("UIcontroller");
                _instance = newobj.AddComponent<StageUIController>();
            }
            return _instance;
        }

    }
    #endregion

    public void PartyCheck()
    {
        checkParty.SetActive(true);
    }
    public void SelcetHero()//
    {
        canChange = true;
        scout.SetActive(false);
        changeHero.SetActive(true); 




    }
    public void ChangeHero()
    {

        if(canChange)
        {
            changeHero.SetActive(false) ;
            canChange = false;
        }
    }
    public void Option()
    {

    }
    public void Cancle()
    {
        canChange = false;
        scout.SetActive(false);
    }
    public void CheckYesNo(bool value)
    {
        if (value)
        {
            //게임시작
        }
        else
        {
            checkYesNo.SetActive(false);
        }

    }


}



