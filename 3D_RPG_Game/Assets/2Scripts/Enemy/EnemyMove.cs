using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    NavMeshAgent nav;
    Transform target;
    EnemyHealth enemyHealth;
    Vector3 startPosition;
    Animator anim;
    public EnemyAttack enemyAttack;

    bool isDetected = false;
    private bool isTargeted;
    private float startTotargetDist;
    private bool isAttack;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        startPosition = transform.position;
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        isTargeted = false;
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
        Targeting();

        if (nav.enabled && isTargeted == true)
        {
            nav.SetDestination(target.transform.position);
            anim.SetBool("isMove", true);
        }
    }

    void DetectPlayer()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        float startTotarget = (transform.position - startPosition).magnitude;

        if (!enemyHealth.isDead)
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

            if ((startTotarget > startTotargetDist) && (Vector3.Distance(transform.position, startPosition) > 5))
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

    void Targeting()
    {
        float targetRadius = 0.5f;
        float targetRange = 0.5f;
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));

        if (rayHits.Length > 0 && !isAttack)
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
        yield return new WaitForSeconds(1.9f);
        anim.SetBool("isAttack", false);
        nav.enabled = true;
        isTargeted = true;
        isAttack = false;
    }

    void OnDamageNormal()
    {
        enemyAttack.NormalAttack();
    }
}
