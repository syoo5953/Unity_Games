using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingSkill : MonoBehaviour
{
    public float lightning_damage = 20f;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            other.GetComponentInParent<MonsterBase>().TakeDamage(lightning_damage);
        }
    }
}
