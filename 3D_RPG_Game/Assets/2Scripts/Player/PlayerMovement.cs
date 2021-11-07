using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    Animator anim;

    public Camera followCamera;
    public float speed = 6.0f;
    public float jumpSpeed = 1.5f;
    public float gravity = 9.81f;
    public float turnSmoothing = 15f;
    public float weapon_rate = 1;
    public GameObject[] weapons;
    public PlayerAttack playerAttack;
    public GameObject skill_1;
    public GameObject skill_wing;
    public GameObject inventory;
    private float h;
    private float v;
    private float Yvelocity;
    private float fireDelay;
    private bool isFireReady = true;
    private bool isJump;
    private bool isSkill;
    private bool aDown;
    private bool sDown;
    private Weapon weapon_L;
    private Weapon weapon_R;
    private AudioSource audioSource;
    public PlayerMovement PlayerInstance;

    int combo = 1;

    void Start()
    {
        if (PlayerInstance == null)
        {
            PlayerInstance = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if (PlayerInstance != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.

        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        weapon_L = weapons[0].GetComponent<Weapon>();
        weapon_R = weapons[1].GetComponent<Weapon>();
        // controller.detectCollisions = false;
    }

    void Update()
    {
        GetInput();
        if (isFireReady && !isSkill)
        {
            Movement();
            Jump();
        }
        if (!IsMouseOverUI())
        {
            Attack();
        }
        Skill();

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.GetComponent<Inventory>().OpenClose();
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    void GetInput()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        aDown = Input.GetButtonDown("Fire1");
        sDown = Input.GetButtonDown("Skill");

    }

    void Movement()
    {
        if (h == 0 || v == 0)
        {
            anim.SetFloat("MoveSpeed", 0f);
        }

        Vector3 direction = new Vector3(h, 0, v).normalized;
        direction = followCamera.transform.TransformDirection(direction);
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSmoothing * Time.deltaTime);
            if (h != 0 || v != 0)
            {
                if (v != -1)
                {
                    transform.rotation = newRotation;
                    speed = 13f;
                    anim.SetFloat("MoveSpeed", 1f);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0);
                    speed = 6f;
                    anim.SetFloat("MoveSpeed", 0.5f);
                }
            }
        }


        Yvelocity -= gravity * Time.deltaTime;
        direction.y = Yvelocity;
        controller.Move(direction * speed * Time.deltaTime);
    }
    void Jump()
    {
        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Yvelocity = jumpSpeed;
                anim.SetBool("isJump", true);
                isJump = true;
            }
            else
            {
                anim.SetBool("isJump", false);
                isJump = false;
            }
        }

    }

    void Attack()
    {
        if (!isJump)
        {
            fireDelay += Time.deltaTime;
            isFireReady = weapon_rate < fireDelay;
            if (aDown && isFireReady)
            {

                Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                if (Physics.Raycast(ray, out rayHit, 100))
                {
                    transform.rotation = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0);
                }

                if (combo == 1)
                {
                    StartCoroutine(LeftEffectOn());
                    LeftAttack();

                }
                else if (combo == 2)
                {
                    StartCoroutine(RightEffectOn());
                    RightAttack();
                }
            }
        }
    }

    void LeftAttack()
    {
        anim.SetTrigger("doAttackL");
        Invoke("audioPlay", 0.6f);
        fireDelay = 0;
        combo = 2;
    }

    void RightAttack()
    {
        anim.SetTrigger("doAttackR");
        Invoke("audioPlay", 0.6f);
        fireDelay = 0;
        combo = 1;
    }

    IEnumerator LeftEffectOn()
    {
        weapon_L.effect.SetActive(true);
        yield return new WaitForSeconds(1f);
        weapon_L.effect.SetActive(false);
    }

    IEnumerator RightEffectOn()
    {
        yield return new WaitForSeconds(0.2f);
        weapon_R.effect.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        weapon_R.effect.SetActive(false);
    }

    void Skill()
    {
        if (sDown && !isSkill && !isJump)
        {
            anim.SetTrigger("doSkill");
            isSkill = true;
            skill_wing.SetActive(true);
            StartCoroutine(SkillOut());
        }
    }

    IEnumerator SkillOut()
    {
        GameObject skill_effect = Instantiate(skill_1, transform.position + new Vector3(0f, 1.2f, 0f), Quaternion.Euler(new Vector3(-90, 0, 0)));
        Destroy(skill_effect, 2.3f);
        yield return new WaitForSeconds(2f);
        isSkill = false;
        skill_wing.SetActive(false);
    }


    void OnDamageNormal()
    {
        playerAttack.NormalAttack();
    }

    void audioPlay()
    {
        audioSource.Play();
    }
}
