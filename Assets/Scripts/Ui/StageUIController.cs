using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//변수선언
public partial class StageUIController : MonoBehaviour
{

    private static StageUIController _instance;

    //UI오브젝트
    [SerializeField]
    private GameObject stageContent;
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
        StageScrollAdd(stageContent);
        Time.timeScale = 1f; // 인게임에서 0으로 설정한 뒤 Menu씬에 도달할 수도 있음

        StartCoroutine(SceneController.instance.FadeIn(1));
        SoundManager.instance.PlayBgmSound(SoundManager.BGM.Lobby);

        if(SceneController.instance.haveToScout == true)
        {
            StartCoroutine(HaveToScoutProcess());
        }

        SetPortraits();
    }
   private void StageScrollAdd(GameObject StageUi)
    {
        int Stages = StageUi.transform.childCount;
        RectTransform stageUi = (RectTransform)StageUi.transform;
        stageUi.offsetMin = new Vector2(stageUi.offsetMin.x, -150 * Stages);

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

    IEnumerator HaveToScoutProcess()
    {
        SceneController.instance.haveToScout = false;

        yield return new WaitForSeconds(1f);

        ScoutHero();
    }

    public void Cancel() // 캐릭터 고르고 취소
    {
        canChange = false;
        changeHero.SetActive(false);
        scout.SetActive(true);

        foreach(var character in scoutableCharacters)
        {
            character.SetActive(true);
        }

        Destroy(selectedScoutHero);
    }

    public void Cancel(GameObject ui) // 캐릭터 고르기 전에 취소
    {
        SoundManager.instance.PlayEffectSound(SoundManager.Effect.ButtonClose);
        ui.SetActive(false);

        foreach (var character in scoutableCharacters)
        {
            character.SetActive(false);
        }
    }

    public void Option()
    {
        SoundManager.instance.PlayEffectSound(SoundManager.Effect.ButtonClick);
        option.SetActive(true);
    }

    #endregion

    #region 파티관련

    CharacterType[] characterTypes = new CharacterType[] {CharacterType.Assassin, CharacterType.Bard, CharacterType.Lancer,
    CharacterType.Paladin, CharacterType.Priest, CharacterType.Shielder, CharacterType.Warrior, CharacterType.Wizard};

    private void SetPortraits()
    {
        for(int i = 0; i < PlayerData.instance.SelectedCharacters.Count; ++i)
        {
            heroButtons[i].image.sprite = Resources.Load<Sprite>("Ui/Portrait/" + PlayerData.instance.SelectedCharacters[i].ToString());
        }
    }

    private CharacterType[] GetScoutCharacters()
    {
        List<CharacterType> scoutCharacters = new List<CharacterType>();

        for (int i = 0; i < 3; ++i)
        {
            while (true)
            {
                CharacterType newType = characterTypes[Random.Range(0, characterTypes.Length)];

                if (scoutCharacters.Contains(newType) == false)
                {
                    scoutCharacters.Add(newType);
                    break;
                }
            }
        }

        return scoutCharacters.ToArray();
    }

    List<CharacterType> scoutableTypes = new List<CharacterType>();
    List<GameObject> scoutableCharacters = new List<GameObject>();
    public void ScoutHero()
    {
        scout.SetActive(true);

        CharacterType[] types = GetScoutCharacters();
        for (int i = 0; i < 3; ++i)
        {
            scoutableTypes.Add(types[i]);

            scout.transform.FindChild("Hero_" + (i + 1).ToString());

            GameObject hero = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Ui/Scout Character/" + types[i].ToString()));
           
            scoutableCharacters.Add(hero);
        }
        
        scoutableCharacters[0].transform.Translate(new Vector3(-3.5f, 0f, 0f));
        scoutableCharacters[2].transform.Translate(new Vector3(3.5f, 0f, 0f));
    }

    int selectedHeroIndex = 0;

    GameObject selectedScoutHero;
    public void SelectHero(int index)
    {
        foreach(var character in scoutableCharacters)
        {
            character.SetActive(false);
        }

        if(PlayerData.instance.SelectedCharacters.Count < 5)
        {
            PlayerData.instance.SelectedCharacters.Add(scoutableTypes[index]);
            SetPortraits();

            scout.SetActive(false);
            return;
        }

        selectedScoutHero = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Ui/Scout Character/" + scoutableTypes[index]));
        selectedScoutHero.transform.position = Vector3.zero;

        selectedHeroIndex = index;
        scout.SetActive(false);
        changeHero.SetActive(true);
        canChange = true;
    }

    public void ChangeHero(int index) // 왼쪽의 5개 초상화 버튼 눌렀을 때, 
    {
        if (canChange)
        {
            changeHero.SetActive(false);
            canChange = false;

            if (PlayerData.instance.SelectedCharacters.Count <= index)
            {
                return;
            }

            if(PlayerData.instance.SelectedCharacters.Count < 5)
            {
                PlayerData.instance.SelectedCharacters.Add(scoutableTypes[selectedHeroIndex]);
            }
            else
            {
                PlayerData.instance.SelectedCharacters[index] = scoutableTypes[selectedHeroIndex];
            }

            Destroy(selectedScoutHero);

            SetPortraits();
        }
    }

    #endregion
    #region 옵션
    public void Reset()
    {
        SoundManager.instance.PlayEffectSound(SoundManager.Effect.ButtonClick);
        PlayerData.instance.ResetData();
        StartCoroutine(SceneController.instance.FadeOut(1f, "Menu"));
    }

    public void Credit()
    {
        SoundManager.instance.PlayEffectSound(SoundManager.Effect.ButtonClick);
        StartCoroutine(SceneController.instance.FadeOut(1f, "Credit"));
    }
   
    #endregion
}



