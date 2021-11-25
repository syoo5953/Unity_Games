using UnityEngine;

public class BossQuest : Singleton<BossQuest> {
    public Quest KillBossQuest;
    public EventCamera eventCamera;

    public GameObject Player, BossObj, Portal;

    public bool OnQuest { get; set; }
    public bool OnAnimation { get; set; }
    public bool OnFighting { get; set; }

    private Vector3 BridgeEntrancePos = new Vector3(-9.6f, 22f, 86f);


    public void StartQuest() {
        OnQuest = true;
        BossObj.SetActive(true);

        //막혀있던 보스 길 열기
        Portal.SetActive(true);
    }

    public void EnterCastle() {
        UIManager.Instance.BossExitPanel.SetActive(false);

        OnAnimation = true;
        eventCamera.StartAnimation();
    }

    public void ExitCastle() {
        OnFighting = false;
        UIManager.Instance.OnOffCanvas(true, true, false);
    }

    public void OnExitBtn() {
        UIManager.Instance.BossExitPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnExitAnswerBtn(bool IsExit) {
        UIManager.Instance.BossExitPanel.SetActive(true);
        if(IsExit) {
            Player.transform.position = BridgeEntrancePos;
            ExitCastle();
        }

        Time.timeScale = 1f;
    }
}