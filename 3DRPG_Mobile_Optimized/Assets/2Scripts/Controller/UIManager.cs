using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {
    private GameObject activeMenuObj;

    [Header("Canvas")]
    public Canvas StaticCanvas;

    public Canvas DynamicCanvas;
    public Canvas HPCanvas;

    [Header("UI")]
    public Joystick joystick;

    public Text playerLevelText, countDownText;
    public GameObject ResurrectUI, InventoryUI, ItemDetailsUI, QuestUI;


    public void UITransition(GameObject newMenuObj) {
        if(activeMenuObj != null && !activeMenuObj.Equals(newMenuObj))
            activeMenuObj.SetActive(false);

        activeMenuObj = newMenuObj;
    }

    public void OnOffCanvas(bool staticCanvas, bool dynamicCanvas) {
        StaticCanvas.enabled = staticCanvas;
        DynamicCanvas.enabled = dynamicCanvas;
    }

    public void OnInventoryBtn() {
        InventoryUI.SetActive(!InventoryUI.activeSelf);
        ItemDetailsUI.SetActive(false);

        UITransition(InventoryUI);
    }

    public void OnQuestBtn() {
        QuestUI.SetActive(!QuestUI.activeSelf);

        //QuestUIScript.Instance.SetListCountText();
        QuestUIScript.Instance.SetObjectivesUI();

        UITransition(QuestUI);
    }
}