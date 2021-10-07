using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public enum ItemType { Equipment, Consumables, Etc}
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            pickupAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pickupAllowed = false;
        }
    }

    void PickUp()
    {
        Debug.Log(itemName);
        gameManager.AddItem(gameObject);
    }
}
