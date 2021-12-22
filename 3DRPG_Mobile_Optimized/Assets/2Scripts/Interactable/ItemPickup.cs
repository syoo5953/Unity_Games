using System.Collections;
using UnityEngine;

public class ItemPickup : Interactable {
    public Item item;
    private Effect particle;
    private Rigidbody rigid;
    private bool showingMessage = false;

    protected override void Awake() {
        base.Awake();
        rigid = GetComponent<Rigidbody>();
    }

    public void Init(Transform monsterPos) {
        transform.position = monsterPos.position + new Vector3(0, 3f, 0);
        rigid.AddForce(new Vector3(Random.Range(-1, 1), Random.Range(2, 5), Random.Range(-1, 1)), ForceMode.Impulse);
        
        particle = ParticleController.Instance.PlayParticles("ItemIdleParticle", transform);
        StartCoroutine(DisableItemCoroutine());
    }

    public override void Interact() {
        bool wasPickedup = Inventory.Instance.Add(item);
        if(wasPickedup) {
            NotificationManager.Instance.Generate_GetItem(item.name, 1);
            SoundManager.Instance.playAudio("GetItem");
            DisableItem();
        }
        else {
            if(!showingMessage) {
                showingMessage = true;
                NotificationManager.Instance.Generate_InventoryIsFull();
                Invoke("SetNotificationInterval", 2f);
            }
        }
    }

    private void SetNotificationInterval() {
        showingMessage = false;
    }

    private IEnumerator DisableItemCoroutine() {
        yield return new WaitForSeconds(5);
        DisableItem();
    }

    private void DisableItem() {
        HasInteracted = true;
        particle.Disable();
        gameObject.SetActive(false);
    }
}