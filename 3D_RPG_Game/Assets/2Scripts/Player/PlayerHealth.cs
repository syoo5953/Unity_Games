using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int currHealth;
    public Slider sliderBar;
    public float yPnt;

    public bool isDead;
    public bool damaged;

    private Camera cam;
    public GameObject canvas;

    private void Awake()
    {
        currHealth = maxHealth;
        cam = Camera.main;
        sliderBar.maxValue = maxHealth;
        sliderBar.value = maxHealth;
    }

    private void Update()
    {
        sliderBar.transform.position = cam.WorldToScreenPoint(transform.position + new Vector3(0, yPnt, 0));
    }
    public void TakeDamage(int amount)
    {
        damaged = true;
        canvas.SetActive(true);
        currHealth -= amount;
        sliderBar.value = currHealth;
        Debug.Log(currHealth);
    }
}
