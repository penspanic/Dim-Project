using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//변수선언
public partial class StageUIController : MonoBehaviour
{

    private static StageUIController _instance;

    //UI오브젝트
    [SerializeField]
    private GameObject chanllengeMeassge;
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

    
    #region//싱글톤 
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
    #region//메뉴

    public void ChallengeCheck()
    {
        
      
        
     
        //PlayerData.SelectedStageId = gameObject.GetComponent<StageData>().Stagenumber;
        chanllengeMeassge.SetActive(true);
        
    }
    public void CheckYesNo(bool value)
    {
        if (value)
        {
            StartCoroutine(SceneController.instance.FadeOut(1, "InGame"));
        }
        else
        {
            checkYesNo.SetActive(false);
        }

    }
    public void Cancle()
    {
        canChange = false;
        changeHero.SetActive(false);
        scout.SetActive(true);
    }
    public  void  Cancle(GameObject ui)
    {
        ui.SetActive(false);
    }
    public void Option()
    {

    }
    #endregion
    #region//파티관련
    public void scoutHero()
    {
        scout.SetActive(true);

    }
    public void SelcetHero()//
    {
        
        scout.SetActive(false);
        changeHero.SetActive(true);
        canChange = true;

    }
    public void ChangeHero()
    {
        if(canChange)
        {
            changeHero.SetActive(false) ;
            canChange = false;
        }
    }

    #endregion

   


}



