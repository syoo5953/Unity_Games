using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIScript : Singleton<ItemUIScript>
{
    public ItemSlots[] slots;

    private void Awake()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ClearSlot();
        }
    }

    public void LateUpdate()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].inventoryItem != null)
            {
                if (slots[i].inventoryItem.NumPerCell > 0)
                {
                    slots[i].UpdateNumText();
                } else 
                {
                    slots[i].ClearSlot();
                    Inventory.Instance.inventoryUIScript.itemIndex--;
                } 
            }
        }
    }
}
