using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
    // TODO : Monster 프리팹 Monster 태그 설정

    public int hp;
    public Vector2 SkillCoolTimeRange;
    private bool canUseSKill = false;

    public bool isDead
    { get; protected set; }

    StageController stageCtrler;

    void Awake()
    {
        stageCtrler = GameObject.FindObjectOfType<StageController>();
    }

    public void Start()
    {
        StartCoroutine(SkillCoolTimeProcess()); // 처음에 스킬 쿨타임이 돌아야 한다.
    }

    public void OnDamaged(int damage)
    {
        hp -= damage;

        if(hp <=0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        isDead = true;

        stageCtrler.OnMonsterDeath(this);
    }

    private void OnSkillUse()
    {
        if(canUseSKill == false)
        {
            return;
        }

        StartCoroutine(SkillCoolTimeProcess());
    }

    IEnumerator SkillCoolTimeProcess()
    {
        canUseSKill = false;

        float coolTime = Random.Range(SkillCoolTimeRange.x, SkillCoolTimeRange.y);

        yield return new WaitForSeconds(coolTime);

        canUseSKill = true;
    }
}
