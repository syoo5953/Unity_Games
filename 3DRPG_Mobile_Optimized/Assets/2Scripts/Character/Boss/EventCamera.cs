using System.Collections;
using UnityEngine;

public class EventCamera : MonoBehaviour {
    public GameObject[] ExclamationMarks;
    private GameObject BossDialogUI;
    public Animator blackScreen;

    private TypeEffect DialogText;

    private Vector3 BossPos = new Vector3(60.88f, 9.37f, -4.28f);
    private Vector3 PrincessPos = new Vector3(-49.1f, 26f, 262.3f);
    private Vector3 PlayerPos = new Vector3(62f, 2.83f, -57.089f);

    private bool NextScene = false;

    private void Awake() {
        DialogText = BossQuest.Instance.dialogText.GetComponent<TypeEffect>();
        BossDialogUI = BossQuest.Instance.BossDialogUI;
    }
    public void StartAnimation() {
        if(!GameManager.Instance.bossSceneOn)
        {
            UIManager.Instance.joystick.PointerUp();
            blackScreen.SetTrigger("animationStart");

            //이벤트카메라의 시작위치를 메인카메라 위치로 지정
            transform.position = PlayerPos;

            //메인카메라 비활성화
            GameManager.Instance.Cam.gameObject.SetActive(false);

            StartCoroutine(CameraStop());
        } else
        {
            AlreadyBossEntered();
        }
    }

    private IEnumerator CameraStop() {
        yield return new WaitForSeconds(1.5f);

        //느낌표 오브젝트 활성화
        ExclamationMarks[0].SetActive(true);
        ExclamationMarks[1].SetActive(true);
        ExclamationMarks[2].SetActive(true);

        yield return new WaitForSeconds(2f);
        StartCoroutine(MoveToBoss());
    }

    private IEnumerator MoveToBoss() {
        while(transform.position != BossPos) {
            transform.position = Vector3.MoveTowards(transform.position, BossPos, 22f * Time.deltaTime);
            yield return null;
        }
        ExclamationMarks[0].SetActive(false);
        ExclamationMarks[1].SetActive(false);
        ExclamationMarks[2].SetActive(false);

        //다이얼로그창 활성화를 위해 해당 Canvas를 활성화
        BossDialogUI.SetActive(true);
        DialogText.SetMsg("잌!");

        //사용자가 다이얼로그창을 누르기전까지 대기
        yield return new WaitUntil(() => DialogText.EndCursor.activeSelf && NextScene);

        BossDialogUI.SetActive(false);
        //StartCoroutine(MoveToPrincess());
        StartCoroutine(MoveToPlayer());
    }

    private IEnumerator MoveToPrincess() {
        NextScene = false;

        while(transform.position != PrincessPos) {
            transform.position = Vector3.MoveTowards(transform.position, PrincessPos, 15f * Time.deltaTime);
            yield return null;
        }
        BossDialogUI.SetActive(true);
        DialogText.SetMsg("구해주세요..!!!흑흑..");

        yield return new WaitUntil(() => DialogText.EndCursor.activeSelf && NextScene);
        BossDialogUI.SetActive(false);
        StartCoroutine(MoveToPlayer());
    }

    private IEnumerator MoveToPlayer() {
        Vector3 playerPos = PlayerPos;
        while(transform.position != playerPos) {
            transform.position = Vector3.MoveTowards(transform.position, playerPos, 30f * Time.deltaTime);
            yield return null;
        }

        UIManager.Instance.OnOffCanvas(true, true);
        BossQuest.Instance.AnimationScreen.SetActive(false);
        BossQuest.Instance.BossUI.SetActive(true);
        BossDialogUI.SetActive(false);

        //메인카메라로 시점을 바꾸기 위해 메인카메라 활성화
        GameManager.Instance.Cam.gameObject.SetActive(true);
        gameObject.SetActive(false);

       BossQuest.Instance.OnAnimation = false;
       BossQuest.Instance.OnFighting = true;
       GameManager.Instance.bossSceneOn = true;
    }

    private void AlreadyBossEntered()
    {
        UIManager.Instance.OnOffCanvas(true, true);
        BossQuest.Instance.AnimationScreen.SetActive(false);
        BossQuest.Instance.BossUI.SetActive(true);
        GameManager.Instance.Cam.gameObject.SetActive(true);
        gameObject.SetActive(false);
        BossQuest.Instance.OnAnimation = false;
        BossQuest.Instance.OnFighting = true;
    }

    public void OnNextDialogBtn() {
        NextScene = true;
    }

    public void OffNextDialogBtn() {
        NextScene = false;
    }
}