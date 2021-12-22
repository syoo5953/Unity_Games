using UnityEngine.UI;
using UnityEngine;

public class ItemSlots : MonoBehaviour
{
    public int slotNum;
    public InventoryItem inventoryItem;

    public Button slotBtn;
    public Image icon;
    public Text num;

    public void AddItem(InventoryItem newItem)
    {
        inventoryItem = newItem;
        icon.sprite = inventoryItem.item.icon;

        slotBtn.enabled = true;
        icon.enabled = true;
        num.enabled = true;
        UpdateNumText();
    }
    public void UpdateNumText()
    {
        num.text = inventoryItem.NumPerCell.ToString();
        num.enabled = true;
    }
    public void ClearSlot()
    {
        inventoryItem = null;
        icon.sprite = null;

        slotBtn.enabled = false;
        icon.enabled = false;
        num.enabled = false;
    }

    public void OnSlotBtn(int slotNum)
    {
        if (inventoryItem != null)
        {
            Sprite findIcon = ItemUIScript.Instance.slots[slotNum].inventoryItem.item.icon;
            int findIconIndex = FindItem(findIcon);
            Inventory.Instance.inventoryUIScript.slots[findIconIndex].inventoryItem.item.Use();
            Inventory.Instance.inventoryUIScript.DeleteFromInventoryForItemSlot(findIconIndex);
        }
    }

    private int FindItem(Sprite findIcon)
    {

        for(int i = 0; i < Inventory.Instance.items.Count; i++)
        {
            if(Inventory.Instance.items[i].item.icon == findIcon)
            {
                return i;
            }
        }
        return 0;
    }
}
