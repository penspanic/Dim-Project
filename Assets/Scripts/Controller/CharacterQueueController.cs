using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterQueueController : MonoBehaviour
{
    readonly Vector2 CharacterResetPosition = new Vector2(-8f, -3f);
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
        target.transform.position = CharacterResetPosition;
        target.transform.Translate(Vector3.up * 1.5f * queuedCharacters.Count);

        queuedCharacters.Enqueue(target);
    }

    void Update()
    {
        if (characters == null)
        {
            return;
        }

        List<Character> notQueuedCharacters = new List<Character>();

        foreach (var eachCharacter in characters)
        {
            if (queuedCharacters.Contains(eachCharacter) == false)
            {
                notQueuedCharacters.Add(eachCharacter);
            }
        }

        bool canDeque = true;

        foreach (var eachCharacter in notQueuedCharacters)
        {
            if (eachCharacter.transform.position.x < -6f)
            {
                canDeque = false;
            }
        }

        if (canDeque == true && queuedCharacters.Count > 0)
        {
            //Character dequedCharacter = queuedCharacters.Dequeue();
            //if (queuedCharacters.Count > 1)
            //{
            //    StartCoroutine(MoveDownProcess(dequedCharacter));
            //}
            //else
            //{
            //    dequedCharacter.Start();
            //}
        }

        foreach (var eachCharacter in queuedCharacters)
        {
            eachCharacter.transform.Translate(Vector3.down * Time.deltaTime);
        }
        if (queuedCharacters.Count > 0 && Mathf.Abs(queuedCharacters.Peek().transform.position.y - CharacterResetPosition.y) < 0.05f)
        {
            Character dequedCharacter = queuedCharacters.Dequeue();
            dequedCharacter.StartMove();
        }
    }

    IEnumerator MoveDownProcess(Character dequedCharacter)
    {
        Character[] tempCharacters = queuedCharacters.ToArray();

        List<Vector3> startPositionList = new List<Vector3>();
        foreach (var eachCharacter in tempCharacters)
        {
            startPositionList.Add(eachCharacter.transform.position);
        }

        float moveYValue = CharacterResetPosition.y - dequedCharacter.transform.position.y;
        float elaspedTime = 0f;
        const float moveTime = 1f;
        while (elaspedTime <= moveTime)
        {
            elaspedTime += Time.deltaTime;

            for (int i = 0; i < tempCharacters.Length; ++i)
            {
                Vector3 newPos = tempCharacters[i].transform.position;
                newPos.y = Mathf.Lerp(startPositionList[i].y, startPositionList[i].y + moveYValue, elaspedTime / moveTime);
                tempCharacters[i].transform.position = newPos;
            }

            yield return null;
        }

        dequedCharacter.StartMove();
    }
}