using UnityEngine;
using System.Collections;

public class Lancer : Character
{

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void DoAction()
    {
        base.DoAction();
        GameObject.FindObjectOfType<Monster>().BreakShield();
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