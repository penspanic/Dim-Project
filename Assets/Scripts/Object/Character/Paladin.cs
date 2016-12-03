using UnityEngine;
using System.Collections;

public class Paladin : Character
{

    Character[] characters;
    protected override void Awake()
    {
        base.Awake();
        type = CharacterType.Paladin;

        characters = GameObject.FindObjectsOfType<Character>();
    }

    protected override void DoAction()
    {
        base.DoAction();

        foreach (Character eachCharacter in characters)
        {
            eachCharacter.Heal(actionValue);
        }
    }

    public override void SetBuff(float duration, float ratio)
    {
        base.SetBuff(duration, ratio);

        StartCoroutine(HealBuffProcess(duration));
    }

    private IEnumerator HealBuffProcess(float duration)
    {
        const int AdditionalHealValue = 20;

        actionValue += AdditionalHealValue;

        yield return new WaitForSeconds(duration);

        actionValue -= AdditionalHealValue;
    }

    protected override void Update()
    {
        base.Update();
    }
}
