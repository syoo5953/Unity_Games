using UnityEngine;
using UnityEngine.UI;

public class NPCUIScript : Singleton<NPCUIScript> {
    public GameObject NPCUI;
    public NPC InteractableNPC { get; set; }
    public Image npcImg;
    public Text npcNameText;

    public QuestGiverUIScript questGiverUIScript;

    public GameObject StartTalkingBtn;
    private Camera mainCam;
    public Camera NPCCamera;
    public bool NPCCameraOn = false;

    private void Awake()
    {
        mainCam = GameManager.Instance.Cam;
    }

    public void OnStartTalkingBtn() {
        StartTalkingBtn.SetActive(false);
        InteractableNPC.ShowNPCUI();
        /*
        mainCam.gameObject.SetActive(false);
        NPCCamera.gameObject.SetActive(true);
        UIManager.Instance.OnOffCanvas(false, false);
        NPCCameraOn = true;
        GameManager.Instance.player.gameObject.SetActive(false);
        */
    }

    public void OnNPCUIExitBtn() {
        InteractableNPC.HasInteracted = false;

        NPCUI.SetActive(false);
        questGiverUIScript.QuestGiverUI.SetActive(false);
    }
}