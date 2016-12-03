using UnityEngine;
using System.Collections;

public class MonsterDefaultAttack : MonoBehaviour
{
    public int damage;
    
    private Monster monster;
    private readonly float DeadlySkillDamageRatio = 2f;
    void Awake()
    {
        monster = GameObject.FindObjectOfType<Monster>();
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
                other.GetComponent<Character>().OnDamaged((int)(damage * DeadlySkillDamageRatio));
                return;
            }
            other.GetComponent<Character>().OnDamaged(damage);
        }
    }
}