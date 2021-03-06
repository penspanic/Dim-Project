﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageController : MonoBehaviour
{
    public bool IsStageEnd
    {
        get; private set;
    }

    InGameUiController uiCtrler;

    List<Character> characters = new List<Character>();
    Monster monster;
    int stageClearLimitTime = 0;
    private void Awake()
    {
        uiCtrler = GameObject.FindObjectOfType<InGameUiController>();
        stageClearLimitTime = DbManager.instance.GetStageClearLimitTime(PlayerData.instance.GetSelectedStageNum());


        StartCoroutine(ObjectsCreateProcess());
    }

    // 소환되는거 나중에 이쁘게 처리하자.
    IEnumerator ObjectsCreateProcess()
    {
        int i = 0;
        foreach (var eachType in PlayerData.instance.SelectedCharacters)
        {
            GameObject character = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Character/" + eachType.ToString()));
            characters.Add(character.GetComponent<Character>());

            character.transform.Translate(Vector3.right * i);
            i++;
        }

        monster = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Monster/Monster_S" + PlayerData.instance.GetSelectedStageNum())).GetComponent<Monster>();
        monster.transform.position = new Vector3(4.75f, -2f, 0f);

        yield return null;
    }

    // UiController에서 호출하자.
    public void StageStart()
    {
        SoundManager.instance.PlayBgmSound(SoundManager.BGM.InGame_Default);

        foreach (var eachCharacter in characters)
        {
            eachCharacter.StartMove();
        }

        monster.StartDefense();
    }

    public void StageEnd(bool isCleared)
    {
        if(isCleared == true)
        {
            if (PlayerData.instance.GetSelectedStageNum() > PlayerData.instance.GetLastClearedStageNum() )
            {
                PlayerData.instance.SetLastClearedStage(PlayerData.instance.GetSelectedStageNum());
            }
        }

        
        uiCtrler.ShowStageEndUi(isCleared);

        foreach (var eachCharacter in characters)
        {
            eachCharacter.StageEnd(isCleared);
        }

        IsStageEnd = true;
        SoundManager.instance.PlayBgmSound(SoundManager.BGM.Clear);
    }


    // Character의 Death처리가 끝난 호출되어야 한다.
    public void OnCharacterDeath(Character deadCharacter)
    {
        bool isAllDead = true;
        foreach (Character eachCharacter in characters)
        {
            if (eachCharacter.isDead == false)
            {
                isAllDead = false;
            }
        }
     
        if (isAllDead == true)
        {
            SoundManager.instance.PlayBgmSound(SoundManager.BGM.Fail);
            StageEnd(false);


        }
    }

    public void OnMonsterDeath(Monster deadMonster)
    {
        StageEnd(true);
    }
    private void CheckCharacterCount()
    {
        int deadCount = 0;
        foreach (var eachCharacter in characters)
        {

            if (eachCharacter.isDead)
            {
                deadCount++;
                if (deadCount >= 2 && SoundManager.instance.BgmAudioSource.clip.name !="(BGM)DeongeonCatHeroes_InGame(Player)")
                {
                    SoundManager.instance.PlayBgmSound(SoundManager.BGM.InGame_Player);
                }
            }
        }
    }

    void Update()
    {
        CheckCharacterCount();
    }
}
