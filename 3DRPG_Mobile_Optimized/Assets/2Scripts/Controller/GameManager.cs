using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    public Camera Cam;
    public ObjectPool objectPool;
    public PlayerBase player;
    public GameObject snowParticle;
    public bool sceneHasChanged = false;
    public bool bossSceneOn = false;
    private static GameManager gameManagerInstance;
    public GameObject NPC;
    public GameObject Ellen;
    public int passTheScene = 0;
    private void Awake() {
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if (gameManagerInstance != this)
            Destroy(gameObject);
    }

    public void LoadScene(int passTheScene)
    {
        player.gameObject.SetActive(false);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        this.passTheScene = passTheScene;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            sceneHasChanged = true;
        }
    }
}