using UnityEngine;
using System.Collections;

public class Bard : Character
{
    public float AttackBuffRatio;

    Character[] characters;

    protected override void Awake()
    {
        base.Awake();
        type = CharacterType.Bard;

        characters = GameObject.FindObjectsOfType<Character>();
    }

    protected override void DoAction()
    {
        base.DoAction();

        foreach(var eachChracter in characters)
        {
            eachChracter.SetBuff(actionValue, AttackBuffRatio);
        }
    }

    public override void SetBuff(float duration, float ratio)
    {
        base.SetBuff(duration, ratio);
    }

    protected override void Update()
    {
        base.Update();
    }
}
