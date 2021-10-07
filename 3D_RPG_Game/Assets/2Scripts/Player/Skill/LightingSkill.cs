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
            other.GetComponent<EnemyHealth>().TakeDamage(lightning_damage);
        }
    }
}
