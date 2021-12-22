using UnityEngine;
using UnityEngine.UI;

public class InventoryUIScript : MonoBehaviour {
    private Inventory inventory;

    public Transform itemsParent;
    public InventorySlot[] slots;

    //DetailsUI
    public int selectedSlotNum { get; set; }

    public int discardNum { get; set; }

    public Text ItemNameText;
    public Image ItemImg;
    public GameObject UseItemBtn, DiscardUI, EquipBtn;
    public InputField discardNumIF;

    public int itemIndex = 0;

    private void Awake() {
        Inventory.Instance.Space = slots.Length;
        inventory = Inventory.Instance;
        UpdateUI();
    }

    public void UpdateUI() {
        for(int i = 0; i < slots.Length; i++) {
            if(i < inventory.items.Count)
                slots[i].AddItem(inventory.items[i]);
            else
                slots[i].ClearSlot();
        }
        QuestUIScript.Instance.UpdateAllObjectives();
    }

    public void ShowItemInform(int slotNum) {
        selectedSlotNum = slotNum;

        Item item = inventory.items[selectedSlotNum].item;
        ItemNameText.text = item.name;
        ItemImg.sprite = item.icon;
        UseItemBtn.SetActive(item.isConsumable);
        EquipBtn.SetActive(item.isConsumable);

        UIManager.Instance.ItemDetailsUI.SetActive(true);
    }

    public void OnUseBtn() {
        slots[selectedSlotNum].inventoryItem.item.Use();
        DeleteFromInventory(1);
    }

    public void OnDiscardBtn() {
        DiscardUI.SetActive(!DiscardUI.activeSelf);
        if(DiscardUI.activeSelf)
            discardNumIF.text = 1.ToString();
    }

    public void OnEquipBtn()
    {
        if (itemIndex < ItemUIScript.Instance.slots.Length)
        {
            if(CanEquip())
            {
                for(int i = 0; i < ItemUIScript.Instance.slots.Length; i++)
                {
                    if(ItemUIScript.Instance.slots[i].inventoryItem == null && i <= itemIndex)
                    {
                        ItemUIScript.Instance.slots[i].AddItem(inventory.items[selectedSlotNum]);
                        UIManager.Instance.ItemDetailsUI.SetActive(false);
                    }
                }
                itemIndex++;
            }
        }
    }

    private bool CanEquip()
    {
        foreach (ItemSlots IS in ItemUIScript.Instance.slots)
        {
            if (IS.inventoryItem != null)
            {
                if (IS.inventoryItem.item.icon == inventory.items[selectedSlotNum].item.icon)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void OnDiscardOKBtn() {
        discardNum = int.Parse(discardNumIF.text);
        DeleteFromInventory(discardNum);
    }

    public void LimitInputRange() {
        int inputNum = int.Parse(discardNumIF.text);
        if(inputNum < 1) {
            discardNumIF.text = 1.ToString();
        }
        if(inputNum > inventory.items[selectedSlotNum].NumPerCell) {
            discardNumIF.text = inventory.items[selectedSlotNum].NumPerCell.ToString();
        }
    }

    public void DeleteFromInventory(int num) {
        bool deleteFromCell = inventory.Remove(num);
        UIManager.Instance.ItemDetailsUI.SetActive(!deleteFromCell);
    }

    public void DeleteFromInventoryForItemSlot(int index)
    {
        bool deleteFromCell = inventory.RemoveFromItemSlot(index);
        UIManager.Instance.ItemDetailsUI.SetActive(!deleteFromCell);
    }
}