using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Priest : Character
{

    Character[] characters;
    protected override void Awake()
    {
        base.Awake();
        type = CharacterType.Priest;

        characters = GameObject.FindObjectsOfType<Character>();
    }

    // 사제 : 힐
    protected override void DoAction()
    {
        base.DoAction();
        SoundManager.instance.PlayEffectSound(SoundManager.Effect.PriestHeal);
        foreach(Character eachCharacter in characters)
        {
            if(eachCharacter.isDead == true)
            {
                continue;
            }
            effectCtrler.ShowEffect(EffectType.PriestSkill, 1f, eachCharacter.transform.position, eachCharacter.transform);
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
