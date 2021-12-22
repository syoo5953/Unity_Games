using UnityEngine;

public class LightingSkill : MonoBehaviour
{
    public float lightning_damage = 20f;
    private bool release = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Boss"))
        {
            if (!release && !other.GetComponent<MonsterBase>().IsDie)
            {
                other.GetComponentInParent<MonsterBase>().TakeDamage(lightning_damage);
                release = true;
            }
        }

        if((other.gameObject.tag.Equals("Skeleton") || other.gameObject.tag.Equals("Turtleshell")) && !other.GetComponent<MonsterBase>().IsDie)
        {
            other.GetComponentInParent<MonsterBase>().TakeDamage(lightning_damage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(release)
        {
            release = false;
        }
    }
}
