using UnityEngine;

public class MapSpawnController : MonoBehaviour
{
    public GameObject player;
    public Vector3 PlayerPos;
    public void Start()
    {
;       player = GameObject.Find("Player");
        player.gameObject.SetActive(false);
        player.gameObject.SetActive(true);

        if (GameManager.Instance.passTheScene == 0)
        {
            GameManager.Instance.snowParticle.SetActive(false);
            GameManager.Instance.NPC.SetActive(true);
            UIManager.Instance.OnOffCanvas(true, true);
        }
        else if (GameManager.Instance.passTheScene == 2)
        {
            GameManager.Instance.snowParticle.SetActive(false);
            UIManager.Instance.OnOffCanvas(true, true);
            GameManager.Instance.NPC.SetActive(false);

        }
        else if (GameManager.Instance.passTheScene == 3)
        {
            GameManager.Instance.snowParticle.SetActive(true);
            if (GameManager.Instance.bossSceneOn)
            {
                UIManager.Instance.OnOffCanvas(true, true);
                GameManager.Instance.NPC.SetActive(false);
            }
        }
    }
}
