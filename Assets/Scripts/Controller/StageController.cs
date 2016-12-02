using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageController : MonoBehaviour
{

    UiController uiCtrler;
    CharacterQueueController queueCtrler;

    List<Character> characters = new List<Character>();

    private void Awake()
    {
        uiCtrler = GameObject.FindObjectOfType<UiController>();
        queueCtrler = GameObject.FindObjectOfType<CharacterQueueController>();

        StartCoroutine(ObjectsCreateProcess());
    }

    // 소환되는거 나중에 이쁘게 처리하자.
    IEnumerator ObjectsCreateProcess()
    {
        CharacterType[] ownedTypes = { CharacterType.Assassin, CharacterType.Bard, CharacterType.Priest, CharacterType.Warrior, CharacterType.Wizard };
        CharacterType[] types = { CharacterType.Assassin, CharacterType.Bard, CharacterType.Priest, CharacterType.Warrior, CharacterType.Wizard };
        PlayerData.SetData(ownedTypes, types, "S1");

        int i = 0;
        foreach(var eachType in PlayerData.SelectedCharacters)
        {
            GameObject character = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Character/" + eachType.ToString()));
            characters.Add(character.GetComponent<Character>());

            character.transform.Translate(Vector3.right * i);
            i++;
        }

        GameObject monster = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Monster/Monster_" + PlayerData.SelectedStageId));
        monster.GetComponent<Monster>().Start();

        yield return null;
    }

    // UiController에서 호출하자.
    public void StageStart()
    {
        foreach(var eachCharacter in characters)
        {
            eachCharacter.Start();
        }
        queueCtrler.OnStart();
    }

    private void StageEnd(bool isCleared)
    {
        uiCtrler.ShowStageEndUi(isCleared);
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
