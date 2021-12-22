using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    public Slider slider;
    public Text loadText;
    PlayerBase player;
    

    private void OnEnable()
    {
        player = GameManager.Instance.player;
        GameManager.Instance.NPC.SetActive(false);
    }
    private void Start()
    {
        StartCoroutine(LoadScene());
    }


    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation;

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            operation = SceneManager.LoadSceneAsync(0);
        } else
        {
            operation = SceneManager.LoadSceneAsync(GameManager.Instance.passTheScene);
        }
        
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;
            if(slider.value < 0.9f)
            {
                slider.value = Mathf.MoveTowards(slider.value, operation.progress, Time.deltaTime);
            } else
            {
                slider.value = Mathf.MoveTowards(slider.value, 1f, Time.deltaTime);
                loadText.text = "아무곳이나 클릭해주세요.";
            }
            if (((Input.touchCount > 0) || (Input.GetButtonDown("Jump"))) && slider.value >= 1f && operation.progress >= 0.9f)
            {
                player.gameObject.SetActive(true);
                operation.allowSceneActivation = true;
            }
        }
    }
}
