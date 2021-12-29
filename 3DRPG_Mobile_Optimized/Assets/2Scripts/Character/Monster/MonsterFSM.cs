using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterFSM : MonoBehaviour {
    public NavMeshAgent nav;
    PlayerBase target;
    public PlayerBase playerBase;
    public MonsterBase monsterBase;
    Animator anim;
    public bool isAttack;
    private Vector3 origianlPos;

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
        float dist = Vector3.Distance(transform.position, target.transform.position);

        if(dist < 15 && !playerBase.IsDie)
        {
            FollowPlayer();
            Targeting();
        } else if(dist > 15 && !playerBase.IsDie)
        {
            ReturnToOrigin();
        }
    }

    private void FollowPlayer()
    {
        if(!playerBase.IsDie)
        {
            nav.SetDestination(target.transform.position);
            anim.SetBool("isMove", true);
        }
    }

    private void ReturnToOrigin()
    {
        float dist = Vector3.Distance(transform.position, origianlPos);
        nav.SetDestination(origianlPos);
        Debug.Log(Vector3.Distance(transform.position, origianlPos));

        if(dist < 3)
        {
            anim.SetBool("isMove", false);
        }
    }
    private void Targeting()
    {
        if (gameObject.GetComponentInChildren<HitBox>().targetList.Contains(GameManager.Instance.player.GetComponent<CharacterController>()) && !isAttack)
        {
            StartCoroutine(Attack());
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
        if(transform.gameObject.tag.Equals("Skeleton"))
            yield return new WaitForSeconds(2f);
        else if(transform.gameObject.tag.Equals("Turtleshell"))
            yield return new WaitForSeconds(0.5f);
        anim.SetBool("isAttack", false);
        nav.isStopped = false;
        isAttack = false;
    }

    void OnDamageNormal()
    {
        monsterBase.AttackToTarget(hitBox);
    }
}