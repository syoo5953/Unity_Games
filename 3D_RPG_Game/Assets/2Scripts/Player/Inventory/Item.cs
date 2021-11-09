using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public enum ItemType { Equipment, Consumables, Etc }
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;
    bool pickupAllowed;
    public GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (pickupAllowed && Input.GetKeyDown(KeyCode.Z))
        {
            PickUp();
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            pickupAllowed = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            pickupAllowed = false;
        }
    }

    void PickUp()
    {
        if(itemType == ItemType.Consumables)
        {
            gameManager.AddItem(gameObject);
        } else if(itemType == ItemType.Etc)
        {
            gameManager.AddGold(gameObject);
        }
    }
}
