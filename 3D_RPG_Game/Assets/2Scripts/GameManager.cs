using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    new AudioSource audio;
    public AudioClip otherClip;
    public GameObject skeleton;
    public GameObject snowingParticle;
    public int xPos;
    public int zPos;
    private static GameManager gameManagerInstance;
    void Awake()
    {
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if (gameManagerInstance != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.

        audio = GetComponent<AudioSource>();
        audio.clip = otherClip;
        audio.volume = 5;
        audio.playOnAwake = true;
        audio.Play();
        Cursor.visible = false;
    }

    public void LoadScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = new Vector3(0, 0, 0);
            Invoke("ResetCollidier", 1f);
            snowingParticle.SetActive(true);
            //  transform.position = new Vector3(32.7f, 23, -21);
        }
        else
        {
            // SceneManager.LoadScene(0, LoadSceneMode.Single);
            //  transform.position = new Vector3(21.6f, 9.48f, 15.39f);
        }
    }
    public void SpawnEnemy()
    {
        Invoke("SpawnDelay", 6f);
    }

    private void SpawnDelay()
    {
        xPos = Random.Range(-13, 12);
        zPos = Random.Range(20, 40);
        Instantiate(skeleton, new Vector3(xPos, -5.16f, zPos), Quaternion.identity);
    }
}
