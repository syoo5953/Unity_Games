using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingSkill : MonoBehaviour
{
    private int lightning_damage = 300;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Skeleton")
        {
            if(!other.GetComponent<EnemyHealth>().isDead)
            other.GetComponent<EnemyHealth>().TakeDamage(lightning_damage);
        }
    }
}
