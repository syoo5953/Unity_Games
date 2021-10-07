using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public GameObject skill;
    new AudioSource audio;
    public AudioClip attackSound;
    public AudioClip jumpSound;
    public int maxHealth;
    public int curHealth;
    public GameObject floatingPoints;
    public float yOffset;
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    public GameObject inventory;

    bool isAttack;
    bool isSkill;
    bool isHit;
    bool canAction;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.playOnAwake = false;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!isAttack && !isSkill && !canAction)
        {
            Movement();
            Attack();
            Skill();
        }
        Jump();

        if(Input.GetKeyDown(KeyCode.I))
        {
            inventory.GetComponent<Inventory>().OpenClose();
        }
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.RightArrow)) //If you press the key to move right
        {
            spriteRenderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            spriteRenderer.flipX = true;
        }

        //Stopping the velocity of the player
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }


        //속도 리밋주기
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJump"))
        {
            audio.clip = jumpSound;
            audio.Play();
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(rigid.velocity.x) == 0)
        {
            anim.SetBool("isWalk", false);
        }
        else
        {
            anim.SetBool("isWalk", true);
        }


        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    anim.SetBool("isJump", false);
                }
            }
        }
    }

    void Attack()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            audio.clip = attackSound;
            audio.Play();
            int val = Random.Range(1, 4);

            switch(val)
            {
                case 1:
                    anim.SetTrigger("doAttack");
                    break;
                case 2:
                    anim.SetTrigger("doAttack1");
                    break;
                case 3:
                    anim.SetTrigger("doAttack2");
                    break;
            }
            isAttack = true;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, LayerMask.GetMask("Enemy"));

            foreach(Collider2D enemy in hitEnemies)
            {
                Debug.Log("touched");
            }

            Invoke("AttackOut", 0.5f);
        }
    }
    void AttackOut()
    {
        isAttack = false;
    }

    void Skill()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            int val = Random.Range(1, 4);

            switch (val)
            {
                case 1:
                    anim.SetTrigger("doAttack");
                    break;
                case 2:
                    anim.SetTrigger("doAttack1");
                    break;
                case 3:
                    anim.SetTrigger("doAttack2");
                    break;
            }
            isSkill = true;
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 4f, LayerMask.GetMask("Enemy"));

            if(hitEnemies == null)
            {
                Debug.Log(null);
            } else
            {
                foreach (Collider2D enemy in hitEnemies)
                {
                    GameObject instantSkill = Instantiate(skill, new Vector2(enemy.transform.position.x, enemy.transform.position.y + 0.5f), enemy.transform.rotation);
                    enemy.GetComponent<Enemy>().GetHitted(instantSkill);
                    Destroy(instantSkill, 0.6f);
                }
                Invoke("SkillOut", 0.5f);
            }
        }
    }

    void SkillOut()
    {
        isSkill = false;
    }

    public void OnDamage(Collider2D collision)
    {
        if (!isHit)
        {
            int dmg = collision.GetComponent<Enemy>().damage;
            curHealth -= dmg;
            GameObject point = Instantiate(floatingPoints, new Vector2(transform.position.x, transform.position.y + yOffset), Quaternion.identity) as GameObject;
            point.transform.GetChild(0).GetComponent<TextMeshPro>().text = dmg.ToString();
            Vector2 direction = transform.position - collision.transform.position;
            StartCoroutine(OnDamgeProcess(direction));
        }
    }

    IEnumerator OnDamgeProcess(Vector2 direction)
    {
        isHit = true;
        canAction = true;
        anim.SetBool("getHit", true);
        int dir = spriteRenderer.flipX == true ? 1 : -1;
        rigid.velocity = new Vector2(6 * dir, rigid.velocity.y);
        yield return new WaitForSeconds(0.3f);
        canAction = false;
        anim.SetBool("getHit", false);

        yield return new WaitForSeconds(1.4f);
        isHit = false;
    }
}
