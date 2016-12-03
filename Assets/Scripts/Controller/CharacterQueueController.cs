using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterQueueController : MonoBehaviour
{
    readonly Vector2 CharacterResetPosition = new Vector2(-8f, -3.5f);
    Queue<Character> queuedCharacters = new Queue<Character>();
    Character[] characters;

    void Awake()
    {

    }

    public void OnStart(Character[] characters)
    {
        this.characters = characters;
    }

    public void Enqueue(Character target)
    {
        queuedCharacters.Enqueue(target);

        target.transform.position = CharacterResetPosition;
        target.transform.Translate(Vector3.up * 1.5f * queuedCharacters.Count);
    }

    void Update()
    {
        List<Character> notQueuedCharacters = new List<Character>();

        foreach(var eachCharacter in characters)
        {
            if(queuedCharacters.Contains(eachCharacter) == false)
            {
                notQueuedCharacters.Add(eachCharacter);
            }
        }

        bool canDeque = true;
        
        foreach(var eachCharacter in notQueuedCharacters)
        {
            if(eachCharacter.transform.position.x < -6f)
            {
                canDeque = false;
            }
        }

        if(canDeque== true)
        {
            StartCoroutine(MoveDownProcess());
            Character dequedCharacter = queuedCharacters.Dequeue();
        }
    }

    IEnumerator MoveDownProcess()
    {
        Character[] tempCharacters = queuedCharacters.ToArray();

        List<Vector3> startPositionList = new List<Vector3>();
        foreach (var eachCharacter in tempCharacters)
        {
            startPositionList.Add(eachCharacter.transform.position);
        }

        float moveYValue = -1f;
        float elaspedTime = 0f;
        const float moveTime = 1f;
        while (elaspedTime <= moveTime)
        {
            elaspedTime += Time.deltaTime;

            for(int i = 0; i<tempCharacters.Length;++i)
            {
                tempCharacters[i].transform.position = Mathf.Lerp(startPositionList[i].y, startPositionList[i].y + moveYValue, elaspedTime / moveTime);
            }

            yield return null;
        }
    }
}
