using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "RPG/Item/Consumable")]
public class Consumable : Item {
    public float HealthGain;

    public override void Use() {
        GameManager.Instance.player.Heal(HealthGain);
        ParticleController.PlayParticles("HealingEffect", GameManager.Instance.player.transform);
    }
}