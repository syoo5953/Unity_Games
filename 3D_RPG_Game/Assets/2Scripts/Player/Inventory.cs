using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class Inventory : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    bool isOpen;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
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
        if (isOpen == false)
        {
            gameObject.SetActive(true);
            isOpen = true;
        }
        else
        {
            gameObject.SetActive(false);
            isOpen = false;
        }
    }

    private void Update()
    {
        if (isOpen)
        {
            Display();
        }
    }

    private void Display()
    {
    }
}
