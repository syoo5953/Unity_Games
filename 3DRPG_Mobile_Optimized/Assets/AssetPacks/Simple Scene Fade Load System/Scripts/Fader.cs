using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [HideInInspector]
    public bool start = false;
    [HideInInspector]
    public float fadeDamp = 0.0f;
    [HideInInspector]
    public float alpha = 0.0f;
    [HideInInspector]
    public Color fadeColor;
    [HideInInspector]
    public bool isFadeIn = false;
    CanvasGroup myCanvas;
    Image bg;
    float lastTime = 0;
    bool startedLoading = false;
    private CharacterController characterController;
    private CameraController cameraController;

    private void Awake()
    {
        characterController = GameManager.Instance.player.GetComponent<CharacterController>();
        cameraController = GameManager.Instance.Cam.GetComponent<CameraController>();
    }
    public void InitiateFader()
    {

        //Getting the visual elements
        if (transform.GetComponent<CanvasGroup>())
            myCanvas = transform.GetComponent<CanvasGroup>();

        if (transform.GetComponentInChildren<Image>())
        {
            bg = transform.GetComponent<Image>();
            bg.color = fadeColor;
        }
        //Checking and starting the coroutine
        if (myCanvas && bg)
        {
            myCanvas.alpha = 0.0f;
            StartCoroutine(FadeIt());
        }
        else
            Debug.LogWarning("Something is missing please reimport the package.");
    }

    IEnumerator FadeIt()
    {

        while (!start)
        {
            //waiting to start
            yield return null;
        }
        lastTime = Time.time;
        float coDelta = lastTime;
        bool hasFadedIn = false;

        while (!hasFadedIn)
        {
            coDelta = Time.time - lastTime;
            if (!isFadeIn)
            {
                //Fade in
                alpha = newAlpha(coDelta, 1, alpha);
                if (alpha == 1 && !startedLoading)
                {
                    startedLoading = true;
                    characterController.enabled = false;
                    GameManager.Instance.player.transform.position = new Vector3(-5.57f, 11.04f, -170.42f);
                    GameManager.Instance.player.transform.rotation = Quaternion.Euler(0, 90, 0);
                    characterController.enabled = true;
                    cameraController.transform.rotation = Quaternion.Euler(47, 88, 0);
                    cameraController.x = 88;
                    cameraController.y = 47;
                    CursorManager.Instance.SetCursorToDefault();
                    OnLevelFinishedLoading();
                }
            }
            else
            {
                //Fade out
                alpha = newAlpha(coDelta, 0, alpha);

                if (alpha == 0)
                {
                    hasFadedIn = true;
                }
            }
            lastTime = Time.time;
            myCanvas.alpha = alpha;
            yield return null;
        }

        yield return null;
    }


    float newAlpha(float delta, int to, float currAlpha)
    {

        switch (to)
        {
            case 0:
                currAlpha -= fadeDamp * delta;
                if (currAlpha <= 0)
                    currAlpha = 0;

                break;
            case 1:
                currAlpha += fadeDamp * delta;
                if (currAlpha >= 1)
                    currAlpha = 1;

                break;
        }

        return currAlpha;
    }

    void OnLevelFinishedLoading()
    {
        StartCoroutine(FadeIt());
        //We can now fade in
        isFadeIn = true;
    }
}
