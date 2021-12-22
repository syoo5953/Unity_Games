using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractable : MonoBehaviour
{
    SphereCollider spCollider;
    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        spCollider = GetComponent<SphereCollider>();
    }

    private void FixedUpdate()
    {
        float targetRadius = 4f;
        float targetRange = 4f;
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));

        if (rayHits.Length > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(GameManager.Instance.player.transform.position - transform.position), 15f * Time.deltaTime);
            transform.position += transform.forward * 6.5f * Time.deltaTime;
        }
    }
}
