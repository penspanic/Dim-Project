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
    [SerializeField]
    private GameObject option;
    [SerializeField]
    private Button[] heroButtons;
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

    #region 메뉴
    void Start()
    {
        StartCoroutine(SceneController.instance.FadeIn(1));
        SoundManager.instance.PlayBgmSound(SoundManager.BGM.Lobby);
    }
    public void ChallengeCheck(StageData stagedata)
    {
        if (!stagedata.StageLocked)
        {
            SoundManager.instance.PlayEffectSound(SoundManager.Effect.ButtonClick);
            chanllengeMeassge.SetActive(true);
        }
    }

    public void CheckYesNo(bool value)
    {
        if (value)
        {
            SoundManager.instance.PlayEffectSound(SoundManager.Effect.ButtonSuccess);
            StartCoroutine(SceneController.instance.FadeOut(1, "InGame"));
        }
        else
        {
            SoundManager.instance.PlayEffectSound(SoundManager.Effect.ButtonClose);
            checkYesNo.SetActive(false);
        }

    }

    public void Cancel() // 캐릭터 3개 뜨고 취소
    {
        canChange = false;
        changeHero.SetActive(false);
        scout.SetActive(true);
    }

    public void Cancel(GameObject ui) // 캐릭터 고르기 전에 취소
    {
        ui.SetActive(false);
    }

    public void Option()
    {
        option.SetActive(true);
    }

    #endregion

    #region 파티관련
    public void scoutHero()
    {
        scout.SetActive(true);

    }
    public void SelectHero()
    {
        scout.SetActive(false);
        changeHero.SetActive(true);
        canChange = true;

    }
    public void ChangeHero() // 왼쪽의 5개 초상화 버튼 눌렀을 때, 
    {
        if (canChange)
        {
            changeHero.SetActive(false);
            canChange = false;
        }
    }

    #endregion
    #region 옵션
    public void Reset()
    {

    }
    public void Credit()
    {
    
    }
   
    #endregion
}



