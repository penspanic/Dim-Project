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
    protected readonly Vector2 CharacterResetPosition = new Vector2(-7f, -3.5f);

    public float DefaultMoveSpeed; // 초당 움직이는 미터수
    public int DefaultActionValue; // 기본 액션 수치
    public int DefaultHp;

    public bool isDead
    {
        get;
        protected set;
    }
    public CharacterType type
    {
        get;
        private set;
    }
    private bool isMoving = false; // 현재 캐릭터가 움직이고 있는가?
    private float moveSpeed = 0; // 현재 이동속도, 이속 버프 적용되었을 수도 있는 수치
    private int hp = 0;
    private int actionValue = 0;
    private float buffRatio = 0f;

    StageController stageCtrler;
    void Awake()
    {
        moveSpeed = DefaultMoveSpeed;
        hp = DefaultHp;

        stageCtrler = GameObject.FindObjectOfType<StageController>();
    }

    public void Start()
    {
        isDead = false;
        isMoving = true;
    }

    // 일정 지점에서 몬스터에게 달려갈 때 호출( OnTriggerEnter2D )
    private void RunToMonster()
    {
        moveSpeed = moveSpeed *= RunToMonsterSpeedIncreaseRatio;   
    }

    // 몬스터와 닿았을 때 하는 행동( OnTriggerEnter2D )
    protected virtual void DoAction()
    {

    }

    // 기본적인 처리( 이동 등등 )
    protected virtual void Update()
    {
        if(isDead == true)
        {
            return;
        }

        if( isMoving == true )
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }
    }

    // 몬스터에게 공격을 하고 다시 되돌아올 때
    public void ResetPosition()
    {
        // 일단 포지션 설정으로 하고, 나중에 코루틴으로 처리 하자

        transform.position = CharacterResetPosition;
    }

    public void SetBuff(float duration, float ratio)
    {
        StartCoroutine(BuffProcess(duration, ratio));
    }

    private IEnumerator BuffProcess(float duration, float ratio)
    {
        buffRatio += ratio;
        // 1-> 1.5
        actionValue += (int)(DefaultActionValue* ratio);

        yield return new WaitForSeconds(duration);

        actionValue -= (int)(DefaultActionValue * ratio);

        buffRatio -= ratio;

        if( buffRatio < 0.05f) // 수치 오차를 위해
        {
            buffRatio = 0f;
        }
    }

    public void OnDamaged(int damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        isDead = true;

        stageCtrler.OnCharacterDeath(this);
    }

    public void OnTouch()
    {
        Debug.Log("OnTouch : " + name);
        if(isDead == false)
        {
            ResetPosition();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Monster") == true)
        {
            DoAction();
        }
        ResetPosition();
    }
}