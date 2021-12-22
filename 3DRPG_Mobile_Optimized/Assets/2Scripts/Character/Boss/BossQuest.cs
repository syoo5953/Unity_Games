using UnityEngine;
using UnityEngine.SceneManagement;
public class BossQuest : Singleton<BossQuest> {
    public Quest KillBossQuest;
    public EventCamera eventCamera;

    public GameObject dialogText, BossUI, BossDialogUI, AnimationScreen, BossExitPanel;
    public bool OnQuest { get; set; }
    public bool OnAnimation { get; set; }
    public bool OnFighting { get; set; }

    private void Awake()
    {
        EnterCastle();
    }

    public void EnterCastle() {
        GameManager.Instance.passTheScene = 0;
        BossExitPanel.SetActive(false);
        OnAnimation = true;
        eventCamera.StartAnimation();
    }

    public void ExitCastle() {
        OnFighting = false;
        UIManager.Instance.OnOffCanvas(false, false);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

    public void OnExitBtn() {
        BossExitPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnExitAnswerBtn(bool IsExit) {
        BossExitPanel.SetActive(false);
        if(IsExit) {
            ExitCastle();
        }

        Time.timeScale = 1f;
    }
}