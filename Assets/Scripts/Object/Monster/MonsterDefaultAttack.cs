using UnityEngine;
using System.Collections;

public class MonsterDefaultAttack : MonoBehaviour
{
    public int damage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Character")== true)
        {
            other.GetComponent<Character>().OnDamaged(damage);
        }
    }
}
