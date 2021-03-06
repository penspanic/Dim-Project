﻿using UnityEngine;
using System.Collections;

public class Warrior : Character
{
    protected override void Awake()
    {
        base.Awake();

        type = CharacterType.Warrior;
    }

    protected override void DoAction()
    {
        base.DoAction();
        SoundManager.instance.PlayEffectSound(SoundManager.Effect.Warrior_Hit);
        effectCtrler.ShowEffect(EffectType.WarriorSkill, 1f, transform.position);
        GameObject.FindObjectOfType<Monster>().OnDamaged(actionValue);
    }

    protected override void Update()
    {
        base.Update();
    }
}
