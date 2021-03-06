﻿using UnityEngine;
using System.Collections;

public class Shielder : Character
{

    // actionValue : 스턴 지속시간

    protected override void Awake()
    {
        base.Awake();
        type = CharacterType.Shielder;
    }

    protected override void DoAction()
    {
        base.DoAction();
        SoundManager.instance.PlayEffectSound(SoundManager.Effect.Shielder_Stun);
        effectCtrler.ShowEffect(EffectType.ShielderSkill, 1f, transform.position);
        GameObject.FindObjectOfType<Monster>().Stun(actionValue);
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
