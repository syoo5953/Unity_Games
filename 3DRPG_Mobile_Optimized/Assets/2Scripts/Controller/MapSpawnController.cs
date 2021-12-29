using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSpawnController : MonoBehaviour
{
    public GameObject player;
    public Vector3 PlayerPos;
    public void Start()
    {
;       player = GameObject.Find("Player");
        player.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        player.transform.position = PlayerPos;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            GameManager.Instance.snowParticle.SetActive(false);
            GameManager.Instance.NPC.SetActive(true);
            UIManager.Instance.OnOffCanvas(true, true);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            GameManager.Instance.snowParticle.SetActive(false);
            UIManager.Instance.OnOffCanvas(true, true);
            GameManager.Instance.NPC.SetActive(false);

        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            GameManager.Instance.snowParticle.SetActive(true);
            UIManager.Instance.OnOffCanvas(false, false);
            GameManager.Instance.NPC.SetActive(false);
        }
    }
}
