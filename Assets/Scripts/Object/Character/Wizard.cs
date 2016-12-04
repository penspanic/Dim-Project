using UnityEngine;
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
        SoundManager.instance.PlayEffectSound(SoundManager.Effect.Wizard_Hit);
        effectCtrler.ShowEffect(EffectType.WizardSkill, 1f, transform.position);
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
