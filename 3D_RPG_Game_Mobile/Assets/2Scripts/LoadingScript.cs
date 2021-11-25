using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    public Slider slider;
    public Text loadText;
    PlayerBase player;
    private BossQuest bossQuest;

    private void Start()
    {
        player = GameManager.Instance.player;
        StartCoroutine(LoadScene());
        bossQuest = BossQuest.Instance;
    }
    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(2);
        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            yield return null;
            if(slider.value < 1f)
            {
                slider.value = Mathf.MoveTowards(slider.value, 1f, Time.deltaTime);
            } else
            {
                loadText.text = "Press SpaceBar";
            }

            if(((Input.touchCount > 0) || (Input.GetButtonDown("Jump"))) && slider.value >= 1f && operation.progress >= 0.9f)
            {
                player.GetComponent<CharacterController>().enabled = true;
                operation.allowSceneActivation = true;
                bossQuest.EnterCastle();
                GameManager.Instance.snowParticle.SetActive(true);
            }
        }
    }
}
