using UnityEngine;
using System.Collections;

public class Assassin : Character
{

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void DoAction()
    {
        base.DoAction();
        SoundManager.instance.PlayEffectSound(SoundManager.Effect.Assain_Hit);
        effectCtrler.ShowEffect(EffectType.AssassinSkill, 1f, transform.position);
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
