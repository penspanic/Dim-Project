﻿using UnityEngine;
using System.Collections;

public class Wizard : Character
{
    protected override void Awake()
    {
        base.Awake();
        type = CharacterType.Wizard;
    }

    protected override void DoAction()
    {
        base.DoAction();
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
