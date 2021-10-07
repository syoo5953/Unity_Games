using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyHitBox enemyHitBox;
    public int damage = 10;


    public void NormalAttack()
    {
        List<Collider> targetList = new List<Collider>(enemyHitBox.targetList);

        foreach (Collider one in targetList)
        {
            PlayerHealth player = one.GetComponent<PlayerHealth>();

            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
}
