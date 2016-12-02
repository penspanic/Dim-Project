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
}
