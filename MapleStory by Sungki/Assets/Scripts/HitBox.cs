using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Player player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            player.OnDamage(collision);
        }
    }
}
