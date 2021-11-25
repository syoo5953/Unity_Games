using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public abstract class CharacterBase : MonoBehaviour {
    public Animator Anim { get; set; }
    protected HealthBar healthBar;
       
    protected RaycastHit raycastHit;

    public bool IsDie { get => CurrentHealth <= 0; }
    public bool AttackStart { get; set; }

    public Transform AttackEffectPos;

    [Header("Character Inform")]
    public float MaxHealth;

    public float CurrentHealth;

    public float MaxExp;
    public float CurrentExp { get; set; }

    public int MinimalDamage;
    public float Damage { get => Random.Range(MinimalDamage, MinimalDamage + 3); }
    private PlayerBase player;


    protected virtual void Awake() {
        Anim = GetComponent<Animator>();
        healthBar = GetComponent<HealthBar>();
        player = GameManager.Instance.player;
    }

    protected virtual void OnEnable() {
        CurrentHealth = MaxHealth;
    }   

    public virtual bool CheckRaycastHit(string layerName) {
        return Physics.Raycast(transform.position + new Vector3(0, 0.3f, 0), transform.forward, out raycastHit, 2f, 1 << LayerMask.NameToLayer(layerName)) && raycastHit.collider != null;
    }

    public void AttackToTarget(HitBox hitBox) {
        List<Collider> targetList = new List<Collider>(hitBox.targetList);

        foreach (Collider one in targetList)
        {
            if (one != null)
            {
                CharacterBase character = one.GetComponent<CharacterBase>();

                if (one.GetComponent<CharacterBase>() != null)
                {
                    if (!character.IsDie)
                    {
                        Debug.Log("hit");
                        character.TakeDamage(Damage);
                    }
                }
            }
        }
    }
    public virtual void TakeDamage(float damage) {
        CurrentHealth -= damage;
        if(CurrentHealth < 0) {
            CurrentHealth = 0;
        }

        //체력바 게이지 감소
        healthBar.OnHealthChanged(CurrentHealth, MaxHealth);
    }

    protected void AttackAnimEvent() {
        AttackStart = true;
    }

    private void AttackEffect(string particleName) {
            ParticleController.PlayParticles(particleName, AttackEffectPos);
            SoundManager.Instance.playAudio("Hit");
    }

    protected virtual void DieAnimEvent() {
        ParticleController.PlayParticles("DieParticle", transform);
        if(gameObject.layer == 10)
        {
            gameObject.GetComponent<MonsterFSM>().nav.enabled = false;
            gameObject.layer = 17;
            gameObject.GetComponentInChildren<HitBox>().targetList.Clear();
            player.hitBox.targetList.Remove(gameObject.GetComponentInChildren<HitBox>().GetComponent<CapsuleCollider>());
            player.hitBox.targetList.Remove(gameObject.GetComponent<BoxCollider>());
        } else if(gameObject.layer == 9)
        {
            player.hitBox.targetList.Clear();

        }
        gameObject.SetActive(false);
    }

    public void doDie()
    {
        Anim.SetTrigger("doDie");
    }

    public IEnumerator SkillOut(GameObject skill_wing, GameObject skill)
    {
        GameObject skill_effect = Instantiate(skill, transform.position + new Vector3(0f, 1.2f, 0f), Quaternion.Euler(new Vector3(-90, 0, 0)));
        Destroy(skill_effect, 2.3f);
        yield return new WaitForSeconds(2f);
        skill_wing.SetActive(false);
        player.setSkill(false);
    }

    public IEnumerator AttackToFalse()
    {
        yield return new WaitForSeconds(1.1f);
        player.setAttack(false);
    }
}