using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type {Ammo, Coin, Grenade, Heart, Weapon};
    public Type type;
    public int value;
    Rigidbody rigidBody;
    SphereCollider sphereCollider;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * 50 * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            rigidBody.isKinematic = true;
            sphereCollider.enabled = false;
        }
    }
}
