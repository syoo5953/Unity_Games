using UnityEngine;
using UnityStandardAssets.ImageEffects;
public class PlayerBase : CharacterBase{
    private static PlayerBase playerInstance;
    public Quest GetWeaponQuest;
    public int Level = 1;

    private ExpBar expBar;
    public Vector3 MoveDir { get; set; }

    public Vector3 StartPos { get { 
            if(GameManager.Instance.passTheScene == 3)
            {
                mainCam.GetComponent<CameraController>().transform.rotation = Quaternion.Euler(33, -5, 0);
                mainCam.GetComponent<CameraController>().x = -5;
                mainCam.GetComponent<CameraController>().y = 33;
                return new Vector3(62f, 22f, -59f);
            } else if(GameManager.Instance.passTheScene == 2)
            {
                return new Vector3(53f, -1.31f, -115.5f);
            } else
            {
                return new Vector3(44.95f, -1.92f, 38.9f);
            }
        } 
    }

    private int resurrectCountDown = 5;

    public Animator Anim_levelup;

    private Joystick joystick;
    public HitBox hitBox;

    //mine
    public float turnSmoothing = 15f;
    public float jumpSpeed;
    private float gravity = 9.8f;
    private float h, v;
    private float speed = 6.0f;
    private float Yvelocity;
    private Animator anim;
    private CharacterController controller;
    private Camera mainCam;
    
    //Skill
    public GameObject skill_1;
    public GameObject skill_wing;
    public GameObject skill_2;
    public GameObject skill_2_effect;
    public GameObject skill_3_dash;
    private bool isSkill;
    private bool isDash = false;
    private bool isSkillMove = false;

    //Attack
    int combo = 1;
    public float weapon_rate;
    private float fireDelay;
    private bool isFireReady = true;
    public GameObject R_Weapon;
    private bool isAttack = false;
    public bool AttackBtnPressed { get; set; }
    private readonly bool attackEvent;

    protected override void Awake() {
        base.Awake();
        controller = GetComponent<CharacterController>();
        mainCam = GameManager.Instance.Cam;
        expBar = GetComponent<ExpBar>();
        joystick = UIManager.Instance.joystick;
        UIManager.Instance.playerLevelText.text = "Lv. " + Level.ToString();
        anim = GetComponent<Animator>();

        if (playerInstance == null)
        {
            playerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (playerInstance != this)
            Destroy(gameObject);
    }

    protected override void OnEnable() {
        base.OnEnable();
        if(!NPCUIScript.Instance.NPCCameraOn)
        {
            Init();
        }
    }

    private void Init()
    {
        controller.enabled = false;
        transform.position = StartPos;
        controller.enabled = true;
        transform.rotation = Quaternion.identity;
        gameObject.layer = 9;
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;

    }
    private void FixedUpdate()
    {
        fireDelay += Time.deltaTime;
        isAttack = weapon_rate < fireDelay;
        GetInput();
        if (controller.isGrounded)
        {
            anim.SetBool("isJump", false);
        }
        if (!isSkill && isAttack)
        {
            Move();
        }

        if(isSkillMove)
        {
            SkillMove();
        }
    }

    void GetInput()
    {
#if (!UNITY_ANDROID || !UNITY_IPHONE) && UNITY_EDITOR
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
#else
        h = joystick.Horizontal;
        v = joystick.Vertical;
#endif
    }

    void Move()
    {
        if ((h == 0 || v == 0) && !isDash)
        {
            anim.SetFloat("Movement", 0f);
        }

        Vector3 direction = new Vector3(h, 0, v).normalized;
        direction = mainCam.transform.TransformDirection(direction);
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSmoothing * Time.deltaTime);
            if (h != 0 || v != 0)
            {
                transform.rotation = newRotation;
                anim.SetFloat("Movement", 1f);
                speed = 13f;
            }
        }

        Yvelocity -= gravity * Time.deltaTime;
        direction.y = Yvelocity;

        if(isDash)
        {
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            speed = 30;
        } else
        {
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        }
        controller.Move(direction * speed * Time.deltaTime);
    }


    public void Heal(float healthGain) {
        CurrentHealth += healthGain;
        if(CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;

        healthBar.OnHealthChanged(CurrentHealth, MaxHealth);
    }

    public void IncreaseExp(float ExpGained) {
        float resultExp = CurrentExp + ExpGained;
        if(resultExp >= MaxExp) {
            while(resultExp >= MaxExp) {
                resultExp -= MaxExp;
                LevelUp();
            }
            CurrentExp += resultExp;
        }
        else {
            CurrentExp = resultExp;
        }
        expBar.OnExpChanged(CurrentExp, MaxExp);

        NotificationManager.Instance.Generate_GetExp(ExpGained);
    }

    private void LevelUp() {
        Level++;

        MaxHealth += 50;
        CurrentHealth = MaxHealth;

        MaxExp += 20;
        CurrentExp = 0;

        MinimalDamage += 1;

        healthBar.OnHealthChanged(CurrentHealth, MaxHealth);
        expBar.OnExpChanged(CurrentExp, MaxExp);
        UIManager.Instance.playerLevelText.text = "Lv. " + Level.ToString();

        Anim_levelup.SetTrigger("levelup");
        ParticleController.Instance.PlayParticles("PlayerLevelUpParticle", transform);
    }

    protected override void DieAnimEvent()
    {
        SoundManager.Instance.playAudio("PlayerDie");
        base.DieAnimEvent();
        this.GetComponentInChildren<HitBox>().targetList.Clear();
        if (GameManager.Instance.passTheScene == 3)
        {
            if (BossQuest.Instance.OnFighting)
            {
                BossQuest.Instance.ExitCastle();
            }
        }
        UIManager.Instance.ResurrectUI.SetActive(true);
        ResurrectTimer();
    }

    private void ResurrectTimer()
    {
        UIManager.Instance.countDownText.text = resurrectCountDown.ToString();
        if (resurrectCountDown-- != 0)
            Invoke("ResurrectTimer", 1f);
        else
        {
            resurrectCountDown = 5;
            UIManager.Instance.ResurrectUI.SetActive(false);
            gameObject.SetActive(true);
            ParticleController.Instance.PlayParticles("PlayerResurrectParticle", transform);
        }
    }


    public void PlayerJumpBtn()
    {
        if(!isSkill)
        {
            if (controller.isGrounded)
            {
                SoundManager.Instance.playAudio("Jump");
                Yvelocity = jumpSpeed;
                anim.SetBool("isJump", true);
            }
        }
    }

    public void PlayerAttackBtn() {
        if (controller.isGrounded && !isSkill)
        {
            isAttack = true;
            isFireReady = weapon_rate < fireDelay;
            if (isFireReady)
            {
                if (combo == 1)
                {
                    LeftAttack();

                }
                else if (combo == 2)
                {
                    RightAttack();
                }
            }
        }
    }
    void LeftAttack()
    {
        anim.SetTrigger("doAttackL");
        SoundManager.Instance.playAudio("PlayerAttack");
        fireDelay = 0;
        combo = 2;
    }

    void RightAttack()
    {
        anim.SetTrigger("doAttackR");
        SoundManager.Instance.playAudio("PlayerAttack");
        fireDelay = 0;
        combo = 1;
    }

    public void PlayerSkillBtn()
    {
        if(GetWeaponQuest.state == QuestState.Complete)
        {
            if (!isSkill && controller.isGrounded && isAttack)
            {
                isSkill = true;
                anim.SetTrigger("doSkill");
                skill_wing.SetActive(true);
                StartCoroutine(SkillOut(1, skill_1, skill_wing));
            }
        }
        else
        {
            NotificationManager.Instance.Generate_ConnotUseTheSkills();
        }
    }

    public void PlayerSkillBtn_1()
    {
        if(GetWeaponQuest.state == QuestState.Complete)
        {
            if (!isSkill && controller.isGrounded && isAttack)
            {
                isSkill = true;
                anim.SetTrigger("doSkill1");
                StartCoroutine(SkillOut(2, skill_2, skill_2_effect));
                isSkillMove = true;
            }
        }
        else
        {
            NotificationManager.Instance.Generate_ConnotUseTheSkills();
        }
    }

    private void SkillMove()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.forward), 30f * Time.deltaTime);
        controller.Move(transform.forward * 20f * Time.deltaTime);
    }

    public void PlayerDashBtn()
    {
        if(GetWeaponQuest.state == QuestState.Complete)
        {
            if (!isSkill && controller.isGrounded && !isDash)
            {
                isDash = true;
                StartCoroutine(DashOut(skill_3_dash));
                mainCam.GetComponent<CameraController>().CameraShake(0.4f, 0.3f);
            }
        }
        else
        {
            NotificationManager.Instance.Generate_ConnotUseTheSkills();
        }
    }

    public void OnDamageNormal()
    {
        GetComponent<PlayerBase>().AttackToTarget(hitBox);
    }

    public void setSkill(bool state)
    {
        this.isSkill = state;
    }

    public void setDash(bool state)
    {
        this.isDash = state;
    }

    public void setSkillMove(bool state)
    {
        this.isSkillMove = state;
    }
}