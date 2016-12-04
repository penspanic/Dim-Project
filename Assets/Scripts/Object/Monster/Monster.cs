using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
    // TODO : Monster 프리팹 Monster 태그 설정

    public int hp;
    public int DefaultShieldHp;
    public int AoeSkillDamage;
    public int BerserkModeDuration;

    public Vector2 defaultAttackCoolTimeRange;
    public Vector2 ShieldCoolTimeRange;
    public Vector2 AoeSkillCoolTimeRange;
    public Vector2 DeadlySkillCoolTimeRange;
    public Vector2 BerserkSkillCoolTimeRange;
    public bool isDead { get; private set; }
    public bool isStuned { get; private set; }
    public bool hasShield { get; private set; }
    public bool isBerserkMode { get; private set; }

    private int startHp = 0;
    private int shieldHp;

    private float berserkModeReduceRatio = 0.2f;

    public bool isNextAttackIsDeadlyAttack = false; // 다음 평타가 치명타인가?

    StageController stageCtrler;
    EffectController effectCtrler;
    HpBar hpBar;
    Animator animator;
    Collider2D defaultAttackCollider;

    void Awake()
    {
        stageCtrler = GameObject.FindObjectOfType<StageController>();
        effectCtrler = GameObject.FindObjectOfType<EffectController>();
        hpBar = transform.FindChild("Hp Bar").GetComponent<HpBar>();
        animator = GetComponent<Animator>();
        defaultAttackCollider = transform.FindChild("Default Attack").GetComponent<Collider2D>();
        defaultAttackCollider.enabled = false;
        startHp = hp;
        StartCoroutine(FillHp());

    }

    public void StartDefense()
    {
        StartCoroutine(DefaultAttackProcess());
        StartCoroutine(ShieldProcess());
        StartCoroutine(AoeSkillProcess());
        StartCoroutine(DeadlySkillProcess());
        StartCoroutine(BerserkSkillProces());
    }

    public void StageEnd(bool isCleared)
    {
        StopAllCoroutines();
    }

    public void OnDamaged(int damage)
    {
        if (isDead == true)
        {
            return;
        }

        // 쉴드가 있을 경우 쉴드의 데미지만 달게 한다. 예) 쉴드 체력 10, damage  30 -> 쉴드 파괴, hp 손실 X
        if (shieldHp > 0)
        {
            SoundManager.instance.PlayEffectSound(SoundManager.Effect.BossDefense);
            shieldHp -= damage;
            if (shieldHp < 0)
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
            SoundManager.instance.PlayBgmSound(SoundManager.BGM.Clear);
        }
        else if ((float)hp / startHp <= 0.3f&&SoundManager.instance.BgmAudioSource.clip.name!= "(BGM)DeongeonCatHeroes_InGame(Boss)")
        {

            SoundManager.instance.PlayBgmSound(SoundManager.BGM.InGame_Boss);
         
        }

    }

    private void OnDeath()
    {
        Debug.Log("Monster dead");
        isDead = true;
        animator.Play("Die", 0);
        if (stageCtrler.IsStageEnd == false)
        {
            stageCtrler.OnMonsterDeath(this);
        }
    }

    public void OnDieAnimationEnd()
    {
        Destroy(this.gameObject);
    }

    public void Stun(float duration)
    {
        effectCtrler.ShowEffect(EffectType.StunDizzy, 2f, transform.position + Vector3.up);
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
        SoundManager.instance.PlayEffectSound(SoundManager.Effect.BreakShield);
        hasShield = false;
        shieldHp = 0;
    }

    IEnumerator DefaultAttackProcess()
    {
        while (isDead == false && stageCtrler.IsStageEnd == false)
        {
            float attackCoolTime = Random.Range(defaultAttackCoolTimeRange.x, defaultAttackCoolTimeRange.y);

            if (isBerserkMode == true)
            {
                attackCoolTime = attackCoolTime * berserkModeReduceRatio;
            }

            yield return new WaitForSeconds(attackCoolTime);

            if (isDead == false) // 타이밍상 WaitForSeconds한 후 죽어있을 수도 있다.
            {
                defaultAttackCollider.enabled = true;
                SoundManager.instance.PlayEffectSound(SoundManager.Effect.BossHit);
                animator.Play("Attack", 0);
            }
        }
    }

    public void OnDefaultAttackMotionEnd()
    {
        defaultAttackCollider.enabled = false;
    }

    IEnumerator ShieldProcess()
    {
        while (isDead == false)
        {
            float coolTime = Random.Range(ShieldCoolTimeRange.x, ShieldCoolTimeRange.y);

            hasShield = false;

            yield return new WaitForSeconds(coolTime);

            if (isDead == true)
            {
                break;
            }

            hasShield = true;
            SoundManager.instance.PlayEffectSound(SoundManager.Effect.BossBarrierOn);
            shieldHp = DefaultShieldHp;
        }
    }

    // 광역기
    IEnumerator AoeSkillProcess()
    {
        while (isDead == false)
        {
            float coolTime = Random.Range(AoeSkillCoolTimeRange.x, AoeSkillCoolTimeRange.y);

            yield return new WaitForSeconds(coolTime);

            if (isDead == true)
            {
                break;
            }

            Vector2 aoeSkillRangeX = new Vector2(-4f, 4f);
            float targetPosX = Random.Range(aoeSkillRangeX.x, aoeSkillRangeX.y);

            effectCtrler.ShowEffect(EffectType.MonsterAoeSkillWarning, 2f, new Vector3(targetPosX, -2.5f, 0));
            yield return new WaitForSeconds(2f);
            effectCtrler.ShowEffect(EffectType.MonsterAoeSkill, 3f, new Vector3(targetPosX, -3f, 0f));
            Character[] characters = GameObject.FindObjectsOfType<Character>();

            foreach (Character eachCharacter in characters)
            {
                if (eachCharacter.isDead == true)
                {
                    continue;
                }

                if(Mathf.Abs(targetPosX - eachCharacter.transform.position.x) < 2f)
                {
                    SoundManager.instance.PlayEffectSound(SoundManager.Effect.BossAoe);
                    eachCharacter.OnDamaged(AoeSkillDamage);
                }
            }
        }
    }

    // 평타 강화, 쿨타임 지나고 다음 공격이 치명타
    IEnumerator DeadlySkillProcess()
    {
        while (isDead == false)
        {
            float coolTime = Random.Range(DeadlySkillCoolTimeRange.x, DeadlySkillCoolTimeRange.y);

            yield return new WaitForSeconds(coolTime);

            if (isDead == true)
            {
                break;
            }

            isNextAttackIsDeadlyAttack = true;
        }
    }

    IEnumerator BerserkSkillProces()
    {
        while (isDead == false)
        {
            float coolTime = Random.Range(BerserkSkillCoolTimeRange.x, BerserkSkillCoolTimeRange.y);

            yield return new WaitForSeconds(coolTime);

            if (isDead == true)
            {
                break;
            }

            isBerserkMode = true;
            SoundManager.instance.PlayEffectSound(SoundManager.Effect.BossBerserkerMode);
            effectCtrler.ShowEffect(EffectType.BerserkModeThunder, BerserkModeDuration, transform.position + Vector3.left * 3);

            yield return new WaitForSeconds(BerserkModeDuration);

            isBerserkMode = false;
        }
        yield return null;
    }
    IEnumerator FillHp()
    {
        float elapsedTime = 0f;
        float fillTime = 1f;
       while(elapsedTime<fillTime)
        {
            elapsedTime += Time.deltaTime;
            
            hp =(int) Mathf.Lerp(0f, startHp,elapsedTime / fillTime);
            if(hp==0)
            {
                hp = 1;
            }
            yield return null;

        }

       
    }

    void Update()
    {
        if (isDead == true)
        {
            return;
        }

        if(hpBar != null)
        {
            hpBar.SetValue((float)hp / (float)startHp);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }
}