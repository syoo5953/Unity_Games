using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    public Slider slider;
    public Text loadText;
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(LoadScene());
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

            if(Input.GetKeyDown(KeyCode.Space) && slider.value >= 1f && operation.progress >= 0.9f)
            {
                player.GetComponent<CharacterController>().enabled = true;
                operation.allowSceneActivation = true;
            }
        }
    }
}
