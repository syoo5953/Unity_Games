using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class Inventory : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    bool isOpen;
    GameManager gameManager;
    public GameObject[] slots;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
        isOpen = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OpenClose()
    {
        if(isOpen == false)
        {
            gameObject.SetActive(true);
            isOpen = true;
        } else
        {
            gameObject.SetActive(false);
            isOpen = false;
        }
    }

    private void Update()
    {
        if(isOpen)
        {
            Display();
        }
    }

    private void Display()
    {
        for(int i = 0; i < gameManager.items.Count; i++)
        {
            slots[i].GetComponent<Image>().sprite = gameManager.items[i].GetComponent<Item>().itemImage;
            slots[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            string name = gameManager.items[i].GetComponent<Item>().itemName;
            slots[i].GetComponentInChildren<TextMeshProUGUI>().text = gameManager.itemDict[name].ToString();
            slots[i].GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
        }
    }
}
