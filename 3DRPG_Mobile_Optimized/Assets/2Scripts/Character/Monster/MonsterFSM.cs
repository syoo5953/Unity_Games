using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterFSM : MonoBehaviour {
    public NavMeshAgent nav;
    PlayerBase target;
    public PlayerBase playerBase;
    public MonsterBase monsterBase;
    Vector3 startPosition;
    Animator anim;
    private float startTotargetDist;
    private bool isTargeted;
    public bool isAttack;

    public HitBox hitBox;

    private void Start()
    {
        Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), playerBase.GetComponent<CharacterController>());
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        startPosition = transform.position;
        nav = GetComponent<NavMeshAgent>();
        target = GameManager.Instance.player;
        playerBase = target;
    }

    private void OnEnable()
    {
        gameObject.layer = 10;
        nav.enabled = true;
        isTargeted = true;
        isAttack = false;
    }

    void FixedUpdate()
    {
        if(playerBase.gameObject.layer == 9)
        {
            DetectPlayer();
            Targeting();
        } else if(playerBase.gameObject.layer == 17)
        {
            isTargeted = false;
            nav.enabled = false;
        }

        if (nav.enabled && isTargeted == true)
        {
            nav.SetDestination(target.transform.position);
            anim.SetBool("isMove", true);
        }
    }

    private void DetectPlayer()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        float startTotarget = (transform.position - startPosition).magnitude;

        if(!playerBase.IsDie)
        {
            if (dist < 15 && !isAttack)
            {
                isTargeted = true;
                nav.enabled = true;
            }
            if (dist > 15)
            {
                isTargeted = false;
                nav.enabled = false;
                startTotargetDist = 2f;

                if ((startTotarget > startTotargetDist) && (Vector3.Distance(transform.position, startPosition) > 5) || playerBase.IsDie)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(startPosition - transform.position), 15f * Time.deltaTime);
                    transform.position += transform.forward * 8f * Time.deltaTime;
                }
                else
                {
                    anim.SetBool("isMove", false);
                }
            }
        }
    }

    private void Targeting()
    {
        /*
        float targetRadius = 0.5f;
        float targetRange = 0.5f;
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));
        
        if (rayHits.Length > 0.01f && !isAttack)
        {
            StartCoroutine(Attack());
        }
        */

        if (gameObject.GetComponentInChildren<HitBox>().targetList.Contains(GameManager.Instance.player.GetComponent<CharacterController>()) && !isAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        transform.LookAt(target.transform.position);
        isTargeted = false;
        nav.enabled = false;
        isAttack = true;
        anim.SetBool("isMove", false);
        anim.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isAttack", false);
        nav.enabled = true;
        isTargeted = true;
        isAttack = false;
    }

    void OnDamageNormal()
    {
        monsterBase.AttackToTarget(hitBox);
    }
}