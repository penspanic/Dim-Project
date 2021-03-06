﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EffectType
{
    BardSkill,
    PriestSkill,
    Collision,
    LancerSkill,
    WizardSkill,
    PaladinSkill1,
    PaladinSkill2,
    AssassinSkill,
    WarriorSkill,
    ShielderSkill,
    CharacterHitted,
    MonsterAoeSkillWarning,
    BerserkModeThunder,
    ChracterReset,
    StunDizzy,
    MonsterAoeSkill

}

public class EffectController : MonoBehaviour
{

    Dictionary<EffectType, GameObject> effects = new Dictionary<EffectType, GameObject>();
    void Awake()
    {
        effects.Add(EffectType.BardSkill, Resources.Load<GameObject>("Prefabs/Effect/FX_bard_skill"));
        effects.Add(EffectType.PriestSkill, Resources.Load<GameObject>("Prefabs/Effect/FX_heal"));
        effects.Add(EffectType.Collision, Resources.Load<GameObject>("Prefabs/Effect/FX_hit_e1"));
        effects.Add(EffectType.LancerSkill, Resources.Load<GameObject>("Prefabs/Effect/FX_lnd_attack"));
        effects.Add(EffectType.WizardSkill, Resources.Load<GameObject>("Prefabs/Effect/FX_mage_skill"));
        effects.Add(EffectType.PaladinSkill1, Resources.Load<GameObject>("Prefabs/Effect/FX_pal_attack"));
        effects.Add(EffectType.PaladinSkill2, Resources.Load<GameObject>("Prefabs/Effect/FX_pri_attack"));
        effects.Add(EffectType.AssassinSkill, Resources.Load<GameObject>("Prefabs/Effect/FX_tri_attack"));
        effects.Add(EffectType.WarriorSkill, Resources.Load<GameObject>("Prefabs/Effect/FX_war_attack"));
        effects.Add(EffectType.ShielderSkill, Resources.Load<GameObject>("Prefabs/Effect/FX_Shd_skill"));
        effects.Add(EffectType.CharacterHitted, Resources.Load<GameObject>("Prefabs/Effect/Fx_hit_e2"));
        effects.Add(EffectType.MonsterAoeSkillWarning, Resources.Load<GameObject>("Prefabs/Effect/FX_Boss_Warning"));
        effects.Add(EffectType.BerserkModeThunder, Resources.Load<GameObject>("Prefabs/Effect/FX_boss_skill_steam"));
        effects.Add(EffectType.ChracterReset, Resources.Load<GameObject>("Prefabs/Effect/FX_death_d1"));
        effects.Add(EffectType.StunDizzy, Resources.Load<GameObject>("Prefabs/Effect/FX_stun_e1"));
        effects.Add(EffectType.MonsterAoeSkill, Resources.Load<GameObject>("Prefabs/Effect/FX_boss_Attack"));
    }

    public void ShowEffect(EffectType effectType, float duration, Vector3 position, Transform parent = null)
    {
        StartCoroutine(EffectShowProcess(effectType, duration, position, parent));
    }

    IEnumerator EffectShowProcess(EffectType effectType, float duration, Vector3 position, Transform parent = null)
    {
        GameObject effect = Instantiate<GameObject>(effects[effectType]);
        effect.transform.position = position;
        effect.transform.SetParent(parent);

        yield return new WaitForSeconds(duration);

        if(effect != null)
        {
            Destroy(effect);
        }
    }
}
