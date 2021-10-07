using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    private GameManager gameManager;
    public int maxHealth;
    public int currHealth;
    public EnemyMove enemyMove;
    public bool isDead = false;
    public bool damaged;
    public Slider sliderBar;
    public float yPnt;
    Animator anim;
    public GameObject canvas;
    public GameObject damagePopup;
    private Camera cam;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
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
        if (currHealth <= 0)
        {
            isDead = true;
            gameObject.layer = 8;
            anim.SetTrigger("doDie");
            GetComponent<NavMeshAgent>().enabled = false;
            gameManager.SpawnEnemy();
            Destroy(gameObject, 3f);
        }
        else
        {
            GameObject point = Instantiate(damagePopup, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity) as GameObject;
            anim.SetTrigger("doDamaged");
        }
    }
}
