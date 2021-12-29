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
    /*
    public virtual bool CheckRaycastHit(string layerName) {
        return Physics.Raycast(transform.position + new Vector3(0, 0.3f, 0), transform.forward, out raycastHit, 2f, 1 << LayerMask.NameToLayer(layerName)) && raycastHit.collider != null;
    }
    */

    public void AttackToTarget(HitBox hitBox) {
        List<Collider> targetList = new List<Collider>(hitBox.targetList);

        foreach (Collider one in targetList)
        {
            if (one != null)
            {
                if (one.gameObject.tag.Equals("Boss"))
                {
                    Debug.Log("boss hitted");
                }
                CharacterBase character = one.GetComponent<CharacterBase>();

                if (one.GetComponent<CharacterBase>() != null)
                {

                    if (!character.IsDie)
                    {
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
            ParticleController.Instance.PlayParticles(particleName, AttackEffectPos);
            SoundManager.Instance.playAudio("Hit");
    }

    protected virtual void DieAnimEvent() {
        ParticleController.Instance.PlayParticles("DieParticle", transform);
        gameObject.SetActive(false);
    }

    public void doDie()
    {
        gameObject.layer = 17;
        Anim.SetTrigger("doDie");
    }

    public IEnumerator SkillOut(int skill_number, GameObject skill_particle, GameObject sub_particle)
    {
        if(skill_number == 1)
        {
            SoundManager.Instance.playAudio("Skill1");
            GameObject skill_effect = Instantiate(skill_particle, transform.position + new Vector3(0f, 1.2f, 0f), Quaternion.Euler(new Vector3(-90, 0, 0)));
            Destroy(skill_effect, 2.3f);
            yield return new WaitForSeconds(2f);
            sub_particle.SetActive(false);
            player.setSkill(false);
        } else if(skill_number == 2)
        {
            SoundManager.Instance.playAudio("Skill2");
            GameObject skill_effect = Instantiate(skill_particle, transform.position + new Vector3(0f, 1.2f, 0f), player.transform.rotation);
            skill_effect.transform.SetParent(player.R_Weapon.transform);
            skill_effect.transform.Rotate(0f, -180f, 0f);
            Destroy(skill_effect, 0.9f);
            yield return new WaitForSeconds(0.6f);
            GameObject skill_effect_2 = Instantiate(sub_particle, player.transform.position + (player.transform.forward * 2), player.transform.rotation);
            skill_effect_2.transform.Rotate(-90f, 0f, 0f);
            Destroy(skill_effect_2, 1f);
            player.setSkill(false);
            player.setSkillMove(false);
            GameManager.Instance.Cam.GetComponent<CameraController>().CameraShake(0.7f, 0.6f);
        }
    }


    public IEnumerator DashOut(GameObject skill)
    {
        SoundManager.Instance.playAudio("Skill2");
        GameObject skill_effect = Instantiate(skill, transform.position + new Vector3(0f, 1.2f, 0f), player.transform.rotation);
        skill_effect.transform.SetParent(player.transform);
        skill_effect.transform.Rotate(0f, -180f, 0f);
        Destroy(skill_effect, 1.5f);
        yield return new WaitForSeconds(1.5f);
        player.setDash(false);
    }
}