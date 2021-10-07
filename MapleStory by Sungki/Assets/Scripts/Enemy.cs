using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Enemy : MonoBehaviour
{
    public float movingSpeed;
    public int MaxHealth;
    public int CurHealth;
    public int damage;
    public float yOffset;

    float fMinX;
    float fMaxX;
    int Direction;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Animator anim;
    AudioSource audio;
    public GameObject canvas;
    bool isMove;
    bool onHit;
    public GameObject floatingPoints;
    public GameManager GameManager;
    public GameObject[] DropItems;
    private float ItemPosX = 0;
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        isMove = true;
        rigid = GetComponent<Rigidbody2D>();
        fMinX = transform.position.x -2.5f;
        fMaxX = transform.position.x + 2.5f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Direction = Random.Range(-1, 2);
    }

    void Update()
    {
        if(isMove && !onHit)
        EnemyMoevement();
    }

    void EnemyMoevement()
    {
        switch (Direction)
        {
            case -1:
                // Moving Left
                if (transform.position.x > fMinX)
                {
                    anim.SetBool("isWalk", true);
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-movingSpeed, GetComponent<Rigidbody2D>().velocity.y);
                    spriteRenderer.flipX = false;
                }
                else
                {
                    Direction = 1;
                }
                break;
            case 1:
                //Moving Right
                if (transform.position.x < fMaxX)
                {
                    anim.SetBool("isWalk", true);
                    GetComponent<Rigidbody2D>().velocity = new Vector2(movingSpeed, GetComponent<Rigidbody2D>().velocity.y);
                    spriteRenderer.flipX = true;
                }
                else
                {
                    Direction = 0;
                }
                break;
            case 0:
                anim.SetBool("isWalk", false);
                isMove = false;
                Invoke("changeDirection", 2f);
                break;
        }
    }

    void changeDirection()
    {
        Direction = Direction >= fMinX ? 1 : -1;
        isMove = true;
    }

    public void GetHitted(GameObject other)
    {
        canvas.SetActive(true);
        audio.Play();
        Skill skill = other.GetComponent<Skill>();
        GameObject point = Instantiate(floatingPoints, new Vector2(transform.position.x, transform.position.y + yOffset), Quaternion.identity) as GameObject;
        point.transform.GetChild(0).GetComponent<TextMeshPro>().text = skill.skillDmg.ToString();
        CurHealth -= skill.skillDmg;
        if(CurHealth <= 0)
        {
            this.gameObject.layer = 12;
            StartCoroutine(DropItem());
            GameManager.Respawn(transform.position);
            isMove = false;
            anim.SetTrigger("doDie");
            Destroy(gameObject, 1f);
        }
        Vector2 direction = transform.position - other.transform.position;
        StartCoroutine(OnDamage(direction));
    }

    IEnumerator DropItem()
    {
        foreach (GameObject ob in DropItems)
        {
            GameObject instantItem = Instantiate(ob, new Vector2(transform.position.x + ItemPosX, (transform.position.y - (spriteRenderer.bounds.size.y / 2) + 0.17f)), transform.rotation);
            ItemPosX += 0.5f;
            if(instantItem != null)
            {
                Destroy(instantItem, 7f);
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator OnDamage(Vector2 direction)
    {
        onHit = true;
        anim.SetBool("isWalk", false);
        anim.SetBool("isHit", true);
        direction = direction.normalized;
        int dir = spriteRenderer.flipX == true ? -1: 1;
        direction += Vector2.right * dir;
        rigid.AddForce(direction * 3, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isHit", false);
        onHit = false;
    }
}
