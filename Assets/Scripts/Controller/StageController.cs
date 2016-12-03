using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageController : MonoBehaviour
{

    public bool IsStageEnd
    {
        get;private set;
    }

    UiController uiCtrler;
    CharacterQueueController queueCtrler;

    List<Character> characters = new List<Character>();
    Monster monster;
    int stageClearLimitTime = 0;
    private void Awake()
    {
        uiCtrler = GameObject.FindObjectOfType<UiController>();
        queueCtrler = GameObject.FindObjectOfType<CharacterQueueController>();
        stageClearLimitTime = DbManager.instance.GetStageClearLimitTime(PlayerData.instance.GetLastClearedStageNum());


        StartCoroutine(ObjectsCreateProcess());
    }

    // 소환되는거 나중에 이쁘게 처리하자.
    IEnumerator ObjectsCreateProcess()
    {
        int i = 0;
        foreach(var eachType in PlayerData.instance.SelectedCharacters)
        {
            GameObject character = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Character/" + eachType.ToString()));
            characters.Add(character.GetComponent<Character>());

            character.transform.Translate(Vector3.right * i);
            i++;
        }

        monster = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Monster/Monster_" + PlayerData.instance.GetSelectedStageId())).GetComponent<Monster>();
        monster.transform.position = new Vector3(4.75f, -2f, 0f);

        yield return null;
    }

    // UiController에서 호출하자.
    public void StageStart()
    {
        SoundManager.instance.PlayBgmSound(SoundManager.BGM.InGame_Default);

        foreach(var eachCharacter in characters)
        {
            eachCharacter.StartMove();
        }
        queueCtrler.OnStart(characters.ToArray());

        monster.StartDefense();
    }

    private void StageEnd(bool isCleared)
    {
        uiCtrler.ShowStageEndUi(isCleared);

        foreach(var eachCharacter in characters)
        {
            eachCharacter.StageEnd(isCleared);
        }

        IsStageEnd = true;
    }


    // Character의 Death처리가 끝난 호출되어야 한다.
    public void OnCharacterDeath(Character deadCharacter)
    {
        bool isAllDead = true;
        foreach(Character eachCharacter in characters)
        {
            if(eachCharacter.isDead == false)
            {
                isAllDead = false;
            }
        }

        if(isAllDead == true)
        {
            StageEnd(false);
        }
    }

    public void OnMonsterDeath(Monster deadMonster)
    {
        StageEnd(true);
    }
}
