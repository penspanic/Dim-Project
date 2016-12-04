using UnityEngine;
using System.Collections;

public class MonsterDefaultAttack : MonoBehaviour
{
    public int damage;
    
    private Monster monster;
    private EffectController effectCtrler;

    private readonly float DeadlySkillDamageRatio = 2f;
    void Awake()
    {
        monster = GameObject.FindObjectOfType<Monster>();
        effectCtrler = GameObject.FindObjectOfType<EffectController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(monster.isDead == true)
        {
            return;
        }

        if (other.CompareTag("Character") == true)
        {
            if(monster.isNextAttackIsDeadlyAttack == true)
            {
                monster.isNextAttackIsDeadlyAttack = false;

                effectCtrler.ShowEffect(EffectType.CharacterHitted, 1f, transform.position);
                other.GetComponent<Character>().OnDamaged((int)(damage * DeadlySkillDamageRatio), true);
                return;
            }
            effectCtrler.ShowEffect(EffectType.CharacterHitted, 1f, transform.position);
            other.GetComponent<Character>().OnDamaged(damage, true);
        }
    }
}