using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    private GameManager gameManager;
    public int maxHealth;
    public int currHealth;
    public int goldAmount;
    public EnemyMove enemyMove;
    public bool isDead = false;
    public bool damaged;
    public Slider sliderBar;
    public float yPnt;
    Animator anim;
    public GameObject canvas;
    bool isCanvasOn;
    public GameObject damagePopup;
    List<GameObject> popUPList;
    private Camera cam;
    public float dropY;

    public GameObject[] DropItems;
    private float ItemPosX = 0;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        popUPList = new List<GameObject>();
        anim = GetComponent<Animator>();
        currHealth = maxHealth;
        cam = Camera.main;
        sliderBar.maxValue = maxHealth;
        sliderBar.value = maxHealth;
    }
    private void Update()
    {
        sliderBar.transform.position = cam.WorldToScreenPoint(transform.position + new Vector3(0, yPnt, 0));
        foreach (GameObject pop in popUPList)
        {
            // pop.transform.position = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
            pop.transform.rotation = Quaternion.LookRotation(cam.transform.forward);
        }
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        isCanvasOn = true;
        canvas.SetActive(true);
        currHealth -= amount;
        sliderBar.value = currHealth;
        if (currHealth <= 0)
        {
            isDead = true;
            gameObject.layer = 8;
            anim.SetTrigger("doDie");
            GetComponent<NavMeshAgent>().enabled = false;
            StartCoroutine(DropItem());
            gameManager.SpawnEnemy();
            Destroy(gameObject, 3f);
        }
        else
        {
            anim.SetTrigger("doDamaged");
            PopUpText();
        }
    }

    void PopUpText()
    {
        GameObject clone = Instantiate(damagePopup, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), Quaternion.identity);
        popUPList.Add(clone);
        StartCoroutine(Wait(clone));
    }

    IEnumerator Wait(GameObject clone)
    {
        yield return new WaitForSeconds(.5f);
        popUPList.Remove(clone);
        Destroy(clone);
    }

    IEnumerator DropItem()
    {
        foreach (GameObject ob in DropItems)
        {
            GameObject instantItem = Instantiate(ob, new Vector3(transform.position.x + ItemPosX, (transform.position.y - (GetComponent<CapsuleCollider>().bounds.size.y / 2) + dropY), transform.position.z), transform.rotation);
            instantItem.transform.rotation = Quaternion.LookRotation(cam.transform.forward);
            ItemPosX += 0.5f;
            if (instantItem != null)
            {
                Destroy(instantItem, 7f);
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
}
