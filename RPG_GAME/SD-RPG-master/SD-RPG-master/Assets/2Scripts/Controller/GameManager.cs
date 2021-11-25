using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    public Camera Cam;
    public ObjectPool objectPool;
    public PlayerBase player;
    public GameObject snowParticle;
    private static GameManager gameManagerInstance;

    private void Awake() {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        if (gameManagerInstance == null)
        {
            gameManagerInstance = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if (gameManagerInstance != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }

    public void LoadScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = new Vector3(64.97f, 3.39f, 11.92f);
        }
        else
        {
            // SceneManager.LoadScene(0, LoadSceneMode.Single);
            //  transform.position = new Vector3(21.6f, 9.48f, 15.39f);
        }
    }
}