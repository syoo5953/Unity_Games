using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class BossFSM : MonoBehaviour
{
    public NavMeshAgent nav;
    PlayerBase target;
    public PlayerBase playerBase;
    public MonsterBase monsterBase;
    Animator anim;
    public bool isAttack;
    private Vector3 origianlPos;
    private float SkillDelay;
    public float skill_rate;
    private bool isSkillReady;
    private bool isSkill;
    float dist;

    public HitBox hitBox;

    private void Start()
    {
        Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), playerBase.GetComponent<CharacterController>());
    }

    private void Awake()
    {
        origianlPos = transform.position;
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        target = GameManager.Instance.player;
        playerBase = target;
    }

    private void OnEnable()
    {
        gameObject.layer = 10;
        nav.enabled = true;
        isAttack = false;
    }

    void FixedUpdate()
    {
        SkillDelay += Time.deltaTime;
        BossSkill();

        dist = Vector3.Distance(transform.position, target.transform.position);

        if (dist < 15 && !playerBase.IsDie)
        {
            FollowPlayer();
            Targeting();
        }
        else if (dist > 15 && !playerBase.IsDie)
        {
            ReturnToOrigin();
        }
    }

    private void FollowPlayer()
    {
        if (!playerBase.IsDie)
        {
            nav.SetDestination(target.transform.position);
            anim.SetBool("isMove", true);
        }
    }

    private void ReturnToOrigin()
    {
        float distance = Vector3.Distance(transform.position, origianlPos);
        nav.SetDestination(origianlPos);

        if (distance < 3)
        {
            anim.SetBool("isMove", false);
        }
    }
    private void Targeting()
    {
        if (gameObject.GetComponentInChildren<HitBox>().targetList.Contains(GameManager.Instance.player.GetComponent<CharacterController>()) && !isAttack && !isSkill)
        {
            StartCoroutine(Attack());
        }
    }

    private void BossSkill()
    {
        isSkillReady = skill_rate < SkillDelay;
        if(isSkillReady)
        {
            if(dist > 15 && dist < 50)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerBase.transform.position - transform.position), 2f * Time.deltaTime);
                isSkill = true;
                nav.isStopped = true;
                anim.SetTrigger("RangeAttack");
                SkillDelay = 0;
            }
        }
    }

    IEnumerator Attack()
    {
        nav.velocity = Vector3.zero;
        nav.isStopped = true;
        transform.LookAt(target.transform.position);
        isAttack = true;
        anim.SetBool("isMove", false);
        anim.SetBool("isAttack", true);
        yield return null;
    }

    public void StopAttack()
    {
        anim.SetBool("isAttack", false);
        isAttack = false;
        nav.isStopped = false;
    }

    public void ReleaseSkill()
    {
        isAttack = false;
        isSkill = false;
        nav.isStopped = false;
    }

    void OnDamageNormal()
    {
        monsterBase.AttackToTarget(hitBox);
    }
}