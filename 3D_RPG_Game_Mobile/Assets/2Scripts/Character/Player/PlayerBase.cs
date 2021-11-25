using UnityEngine;

public class PlayerBase : CharacterBase{
    public int Level = 1;

    private ExpBar expBar;
    public Vector3 MoveDir { get; set; }
    public Vector3 StartPos { get { return new Vector3(62f, 22f, -59f); } }

    private int resurrectCountDown = 5;

    public Animator Anim_levelup;
    private bool isJump;
    private bool isAttack;
    public bool AttackBtnPressed { get; set; }
    private readonly bool attackEvent;

    private Joystick joystick;
    public HitBox hitBox;

    //mine
    public float jumpSpeed;
    private float gravity = 9.8f;
    private float h, v;
    private float speed = 6.0f;
    private float Yvelocity;
    private Animator anim;
    private CharacterController controller;
    public GameObject skill_1;
    public GameObject skill_wing;
    private bool isSkill;

    //Attack
    int combo = 1;
    public float weapon_rate;
    private float fireDelay;
    private bool isFireReady = true;
    public GameObject[] weapons;
    private static PlayerBase playerInstance;
    protected override void Awake() {
        base.Awake();
        controller = GetComponent<CharacterController>();
        expBar = GetComponent<ExpBar>();
        joystick = UIManager.Instance.joystick;
        UIManager.Instance.playerLevelText.text = "Lv. " + Level.ToString();
        anim = GetComponent<Animator>();

        if (playerInstance == null)
        {
            playerInstance = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if (playerInstance != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }

    protected override void OnEnable() {
        base.OnEnable();
        transform.position = StartPos;
        transform.rotation = Quaternion.identity;
    }
    private void Update()
    {
        GetInput();

        if (!isSkill && !isAttack)
        {
            Move();
        }

        fireDelay += Time.deltaTime;
        if (controller.isGrounded)
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }

    void GetInput()
    {
        h = joystick.Horizontal;
        v = joystick.Vertical;

    }

    void Move()
    {
        if (h == 0 || v == 0)
        {
            anim.SetFloat("Movement", 0f);
        }

        Vector3 direction = new Vector3(h, 0, v).normalized;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            MoveController.FaceDirection(transform, direction);
            if (h != 0 || v != 0)
            {
                if (v != -1)
                {
                    anim.SetFloat("Movement", 1f);
                    speed = 13f;
                }
            }
        }

        Yvelocity -= gravity * Time.deltaTime;
        direction.y = Yvelocity;
        controller.Move(direction * speed * Time.deltaTime);
    }

    public bool PlayerInMonsterRange(Vector3 minRange, Vector3 maxRange) {
        return transform.position.x < maxRange.x && transform.position.x > minRange.x && transform.position.z < maxRange.z && transform.position.z > minRange.z;
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
        ParticleController.PlayParticles("PlayerLevelUpParticle", transform);
    }

    public void PlayerJumpBtn()
    {
        if(!isSkill)
        {
            if (controller.isGrounded)
            {
                SoundManager.Instance.playAudio("Jump");
                Yvelocity = jumpSpeed;
                isJump = true;
                anim.SetBool("isJump", true);
            }
        }
    }

    public void PlayerAttackBtn() {
        if (!isJump)
        {
            isAttack = true;
            StartCoroutine(AttackToFalse());
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
        if (!isSkill && !isJump)
        {
            anim.SetTrigger("doSkill");
            isSkill = true;
            skill_wing.SetActive(true);
            StartCoroutine(SkillOut(skill_wing, skill_1));
        }
    }

    public void OnDamageNormal()
    {
        GetComponent<PlayerBase>().AttackToTarget(hitBox);
    }

    protected override void DieAnimEvent() {
        SoundManager.Instance.playAudio("PlayerDie");
        base.DieAnimEvent();
        if(BossQuest.Instance.OnFighting) {
            BossQuest.Instance.ExitCastle();
        }
        UIManager.Instance.ResurrectUI.SetActive(true);
        ResurrectTimer();
    }

    private void ResurrectTimer() {
        UIManager.Instance.countDownText.text = resurrectCountDown.ToString();
        if(resurrectCountDown-- != 0)
            Invoke("ResurrectTimer", 1f);
        else {
            resurrectCountDown = 5;
            UIManager.Instance.ResurrectUI.SetActive(false);
            gameObject.SetActive(true);
            ParticleController.PlayParticles("PlayerResurrectParticle", transform);
        }
    }

    public void setSkill(bool state)
    {
        this.isSkill = state;
    }

    public void setAttack(bool state)
    {
        this.isAttack = state;
    }
}