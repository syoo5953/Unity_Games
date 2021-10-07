using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Weapon weapon;
    public int normalDamage;
    public HitBox hitbox;

    private void Awake()
    {
        normalDamage = weapon.damage;
    }

    public void NormalAttack()
    {
        List<Collider> targetList = new List<Collider>(hitbox.targetList);

        foreach(Collider one in targetList)
        {
            if(one != null)
            {
                EnemyHealth enemy = one.GetComponent<EnemyHealth>();
               
                if (one.GetComponent<EnemyHealth>() != null)
                {
                    if (!enemy.isDead)
                    {
                        enemy.TakeDamage(normalDamage);
                    }
                }
                
            }
        }
    }
}
