using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
    // TODO : Monster 프리팹 Monster 태그 설정

    public int DefaultHp;
    public int DefaultShieldHp;
    public int damage;
    public Vector2 SkillCoolTimeRange;
    public Vector2 ShieldCoolTimeRange;

    private bool canUseSKill = false;
    private int hp;
    private int shieldHp;

    public bool isDead { get; protected set; }
    public bool isStuned { get; protected set; }
    public bool hasShield { get; protected set; }

    StageController stageCtrler;
    HpBar hpBar;
    Animator animator;

    void Awake()
    {
        stageCtrler = GameObject.FindObjectOfType<StageController>();
        hpBar = transform.FindChild("Hp Bar").GetComponent<HpBar>();
        animator = GetComponent<Animator>();

        hp = DefaultHp;
    }

    public void StartDefense()
    {
        StartCoroutine(SkillCoolTimeProcess()); // 처음에 스킬 쿨타임이 돌아야 한다.
        StartCoroutine(ShieldProcess());
    }

    public void StageEnd(bool isCleared)
    {
        StopAllCoroutines();
    }

    public void OnDamaged(int damage)
    {
        // 쉴드가 있을 경우 쉴드의 데미지만 달게 한다. 예) 쉴드 체력 10, damage  30 -> 쉴드 파괴, hp 손실 X
        if( shieldHp > 0 )
        {
            shieldHp -= damage;
            if(shieldHp < 0)
            {
                hasShield = false;
                shieldHp = 0;
            }
            return;
        }

        hp -= damage;

        if (hp <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        Debug.Log("Monster dead");
        isDead = true;
        animator.Play("Die", 0);
        if(stageCtrler.IsStageEnd == false)
        {
            stageCtrler.OnMonsterDeath(this);
        }
    }

    public void OnDieAnimationEnd()
    {
        Destroy(this.gameObject);
    }

    private void OnSkillUse()
    {
        if (canUseSKill == false)
        {
            return;
        }

        StartCoroutine(SkillCoolTimeProcess());
    }

    public void Stun(float duration)
    {
        StartCoroutine(StunProcess(duration));
    }

    IEnumerator StunProcess(float duration)
    {
        isStuned = true;
        yield return new WaitForSeconds(duration);
        isStuned = false;
    }

    public void BreakShield()
    {
        hasShield = false;
        shieldHp = 0;
    }

    // 스킬 이름을 명시적으로 적어줘야 할듯?? 몬스터는 스킬이 여러개니깐
    IEnumerator SkillCoolTimeProcess()
    {
        canUseSKill = false;

        float coolTime = Random.Range(SkillCoolTimeRange.x, SkillCoolTimeRange.y);

        yield return new WaitForSeconds(coolTime);

        canUseSKill = true;
    }

    IEnumerator ShieldProcess()
    {
        float coolTime = Random.Range(ShieldCoolTimeRange.x, ShieldCoolTimeRange.y);

        while (isDead == false)
        {
            hasShield = false;

            yield return new WaitForSeconds(coolTime);

            hasShield = true;
            shieldHp = DefaultShieldHp;
        }
    }

    void Update()
    {
        hpBar.SetValue((float)hp / (float)DefaultHp);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Character") == true)
        {
            other.GetComponent<Character>().OnDamaged(damage);
        }
    }
}