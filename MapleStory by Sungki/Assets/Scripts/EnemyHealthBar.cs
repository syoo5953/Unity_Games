using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Enemy enemy;
    public Slider hpBar;
    public float maxHealth;
    public float curHealth;
    public float yPnt;
    void Start()
    {
        maxHealth = enemy.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.transform.position = Camera.main.WorldToScreenPoint(enemy.transform.position + new Vector3(0, 1f, 0));
        curHealth = enemy.CurHealth;
        transform.position = enemy.transform.position;
        hpBar.value = curHealth / maxHealth;
    }
}
