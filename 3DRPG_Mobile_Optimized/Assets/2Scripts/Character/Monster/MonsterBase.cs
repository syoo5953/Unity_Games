using UnityEngine;
using UnityEngine.SceneManagement;
public class MonsterBase : CharacterBase {
    public Vector3 limitRange_Min, limitRange_Max;
    private DropItemController dropItemController;
    private CameraController cameraController;
    protected override void Awake() {
        base.Awake();
        dropItemController = GetComponent<DropItemController>();
        cameraController = GameManager.Instance.Cam.GetComponent<CameraController>();
    }
  /*
    public override bool CheckRaycastHit(string layerName) {
        return (Physics.Raycast(transform.position + new Vector3(0, 0.3f, 0), transform.forward, out raycastHit, 1.5f, 1 << LayerMask.NameToLayer(layerName))
                || Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), transform.right, 1.5f, 1 << LayerMask.NameToLayer(layerName))
                || Physics.Raycast(transform.position + new Vector3(0f, 0.2f, 0), -transform.right, 1.5f, 1 << LayerMask.NameToLayer(layerName))) && raycastHit.collider != null;
    }*/

    public override void TakeDamage(float damage) {
        base.TakeDamage(damage);

        //데미지 텍스트 프리팹으로 생성
        DamageText damageText = GameManager.Instance.objectPool.GetObject("DamageText 1").GetComponent<DamageText>();
        damageText.Init(damageText.gameObject, transform.position, damage);
        if(gameObject.tag.Equals("Skeleton"))
        {
            gameObject.GetComponent<Animator>().SetTrigger("doDamage");
        }
        ParticleController.Instance.PlayParticles("PlayerAttackParticle", transform);
        SoundManager.Instance.playAudio("Hit");
    }

    private void AttackJumpAnimEvent()
    {
        //AttackToTarget("Player");
        ParticleController.Instance.PlayParticles("BossAttackJumpParticle", transform);
        cameraController.CameraShake(0.5f, 0.4f);
    }

    protected override void DieAnimEvent() {
        SoundManager.Instance.playAudio("MonsterDie");
        healthBar.healthBarObj.SetActive(false);
        base.DieAnimEvent();

        //플레이어 능력치 추가
        GameManager.Instance.player.IncreaseExp(MaxExp);

        //아이템 드랍
        dropItemController.DropItem();
        if(!gameObject.tag.Equals("Boss"))
            Invoke("Resurrect", 3);

        gameObject.layer = 17;
        gameObject.GetComponentInChildren<HitBox>().targetList.Clear();
        GameManager.Instance.player.hitBox.targetList.Remove(gameObject.GetComponentInChildren<HitBox>().GetComponent<CapsuleCollider>());
        GameManager.Instance.player.hitBox.targetList.Remove(gameObject.GetComponent<BoxCollider>());
    }

    private void Resurrect()
    {
        transform.position = new Vector3(Random.Range(limitRange_Min.x, limitRange_Max.x), transform.position.y, Random.Range(limitRange_Min.z, limitRange_Max.z));
        gameObject.SetActive(true);
    }


    public void CreateFireBall()
    {
        ParticleController.Instance.PlayParticles("BossAttackFireParticle", GameManager.Instance.player.transform);
            AttackStart = false;
    }
    private void AttackFireAnimEvent()
    {
        CreateFireBall();
    }

    public void DieBossAnimEvent()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }
}