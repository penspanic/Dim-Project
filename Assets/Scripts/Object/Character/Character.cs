using UnityEngine;
using System.Collections;

public enum CharacterType
{
    Unknown,
    Warrior, // 전사
    Bard, // 음유시인
    Assassin, // 도적
    Priest, // 사제
    Shielder, // 방패전사
    Lancer, // 창기사
    Paladin, // 성기사
    Wizard // 마법사
}

// TODO : Character 클래스를 추상 클래스로 바꾼다.
public class Character : MonoBehaviour, ITouchable
{
    protected readonly float RunToMonsterSpeedIncreaseRatio = 2f; // 몬스터한테 달려갈 때 빨라지는 비율

    public float DefaultMoveSpeed; // 초당 움직이는 미터수
    public int DefaultActionValue; // 기본 액션 수치
    public int hp;
    public int DefaultDamage;

    public bool isDead
    {
        get;
        protected set;
    }
    public bool isInvincible
    {
        get;
        protected set;
    }
    public CharacterType type
    {
        get;
        protected set;
    }
    public int actionValue
    {
        get; protected set;
    }

    private bool isMoving = false; // 현재 캐릭터가 움직이고 있는가?
    private float moveSpeed = 0; // 현재 이동속도, 이속 버프 적용되었을 수도 있는 수치
    private int damage = 0;
    private int startHp = 0;

    protected StageController stageCtrler;
    protected CharacterQueueController queueCtrler;
    protected EffectController effectCtrler;
    HpBar hpBar;
    Animator animator;

    protected virtual void Awake()
    {
        moveSpeed = DefaultMoveSpeed;
        actionValue = DefaultActionValue;
        damage = DefaultDamage;
        startHp = hp;

        stageCtrler = GameObject.FindObjectOfType<StageController>();
        queueCtrler = GameObject.FindObjectOfType<CharacterQueueController>();
        effectCtrler = GameObject.FindObjectOfType<EffectController>();
        hpBar = transform.FindChild("Hp Bar").GetComponent<HpBar>();
        animator = GetComponent<Animator>();
    }

    public void StartMove()
    {
        isDead = false;
        isMoving = true;
    }

    public void StageEnd(bool isCleared)
    {
        isMoving = false;
    }

    // 일정 지점에서 몬스터에게 달려갈 때 호출( OnTriggerEnter2D )
    public void RunToMonster()
    {
        moveSpeed = moveSpeed *= RunToMonsterSpeedIncreaseRatio;   
    }

    // 몬스터와 닿았을 때 하는 행동( OnTriggerEnter2D )
    protected virtual void DoAction()
    {
        isInvincible = true;
        isMoving = false;
        animator.Play("skill", 0);
    }

    public void OnSkillMotionEnd()
    {
        isInvincible = false;
        ResetPosition();
    }

    // 기본적인 처리( 이동 등등 )
    protected virtual void Update()
    {
        if(isDead == true || stageCtrler.IsStageEnd == true)
        {
            return;
        }

        if(hpBar != null)
        {
            hpBar.SetValue((float)hp / (float)startHp);
        }

        if( isMoving == true )
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }
    }

    // 몬스터에게 공격을 하고 다시 되돌아올 때
    public void ResetPosition()
    {
        queueCtrler.Enqueue(this);
        isMoving = false;
        moveSpeed = DefaultMoveSpeed;
    }

    public virtual void SetBuff(float duration, float ratio)
    {
        StartCoroutine(DamageBuffProcess(duration, ratio));
    }

    private IEnumerator DamageBuffProcess(float duration, float buffRatio)
    {
        int increasedDamage = (int)(DefaultDamage * buffRatio);
        damage += increasedDamage;

        yield return new WaitForSeconds(duration);

        damage -= increasedDamage;
    }

    public void Heal(int healValue)
    {
        hp += healValue;
        if(hp > startHp)
        {
            hp = startHp;
        }
        // 힐 버프 애니메이션 같은 처리 여기서 해주면 된다.
    }

    public void OnDamaged(int damage, bool isResetPos = false)
    {
        if(isDead == true || isInvincible == true)
        {
            return;
        }

        hp -= damage;

        if(hp <= 0)
        {
            OnDeath();
            return;
        }
        
        if(isResetPos == true)
        {
            ResetPosition();
        }
    }

    private void OnDeath()
    {
        isDead = true;

        Destroy(hpBar.gameObject);

        if(stageCtrler.IsStageEnd == false)
        {
            stageCtrler.OnCharacterDeath(this);
        }

        effectCtrler.ShowEffect(EffectType.ChracterReset, 1f, transform.position);
        StartCoroutine(DeadFallProcess());
    }

    IEnumerator DeadFallProcess()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;

        yield return new WaitForSeconds(2f);

        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    public void OnTouch()
    {
        if(isDead == false && isMoving == true)
        {
            ResetPosition();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Monster") == true)
        {
            other.GetComponent<Monster>().OnDamaged(damage);
            effectCtrler.ShowEffect(EffectType.Collision, 1f, transform.position + new Vector3(0f,0.3f,0f));
            DoAction();
        }
    }
}