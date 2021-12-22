using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour {

    private float xAngle;
    private float yAngle;

    private Vector3 FirstPoint;
    private Vector3 SecondPoint;
    private float xAngleTemp;
    private float yAngleTemp;

    [HideInInspector]
    public bool isCanRotate = true;

    private bool isMouseDown = false;
    private Transform playerPos;
    private static CameraController cameraInstance;

    //Mine
    private int rightFingerId;
    float halfScreenWidth;
    private Vector2 lookInput;
    public float x = 88f;
    public float y = 47f;
    public float distance = 10f;
    public float distance_y = 3f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
    public float distanceMin = .5f;
    public float distanceMax = 15f;
    private Quaternion rotation;
    private void Awake() {
        xAngle = 88;
        yAngle = 47;
        this.rightFingerId = -1;
        this.halfScreenWidth = Screen.width / 2;
        rotation = Quaternion.Euler(y, x, 0);
        playerPos = GameManager.Instance.player.transform;

        if (cameraInstance == null)
        {
            cameraInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (cameraInstance != this)
            Destroy(gameObject);
    }

    private void Update() {
        CameraInput();
    }

    private void FixedUpdate()
    {
        rotation = Quaternion.Euler(y, x, 0);
        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
        Vector3 negDistance = new Vector3(0.0f, distance_y, -distance);
        Vector3 position = rotation * negDistance + playerPos.position;

        transform.rotation = rotation;
        transform.position = position;
    }

    private void FadeObstacles()
    {
        float characterDistance = Vector3.Distance(transform.position, playerPos.position);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, playerPos.position - transform.position, characterDistance, LayerMask.GetMask("Building"));
        if(hits.Length > 0)
        {
            Debug.Log("hitted");
        }

    }

    private void CameraInput() {
        
#if (!UNITY_ANDROID || !UNITY_IPHONE) && UNITY_EDITOR
            // 마우스가 눌림
            if(Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject.layer == 6) {
                isMouseDown = true;
            }

            // 마우스가 떼짐
            if(Input.GetMouseButtonUp(0)) {
                isMouseDown = false;
            }

            if (isMouseDown)
            {
                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);
            }

        /*       
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    FirstPoint = Input.GetTouch(0).position;
                    xAngleTemp = xAngle;
                    yAngleTemp = yAngle;
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    SecondPoint = Input.GetTouch(0).position;
                    xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                    yAngle = yAngleTemp - (SecondPoint.y - FirstPoint.y) * 90 * 3f / Screen.height; // Y값 변화가 좀 느려서 3배 곱해줌.
                    yAngle = ClampAngle(yAngle, yMinLimit, yMaxLimit);
                    rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(yAngle, xAngle, 0.0f), Time.deltaTime * 3f);
                }
            }*/
#else
   GetTouchInput();
#endif
    }

    private void GetTouchInput()
    {
        //몇개의 터치가 입력되는가
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            switch (t.phase)
            {
                case TouchPhase.Began:

                    if (t.position.x > this.halfScreenWidth && this.rightFingerId == -1)
                    {
                        this.rightFingerId = t.fingerId;
                        FirstPoint = Input.GetTouch(0).position;
                        xAngleTemp = xAngle;
                        yAngleTemp = yAngle;
                    }
                    break;

                case TouchPhase.Moved:

                    //이것을 추가하면 시점 원상태 버튼을 누를 때 화면이 돌아가지 않는다
                    if (!EventSystem.current.IsPointerOverGameObject(i))
                    {
                        if (t.fingerId == this.rightFingerId)
                        {
                            SecondPoint = Input.GetTouch(0).position;
                            xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                            yAngle = yAngleTemp - (SecondPoint.y - FirstPoint.y) * 90 * 3f / Screen.height; // Y값 변화가 좀 느려서 3배 곱해줌.
                            yAngle = ClampAngle(yAngle, yMinLimit, yMaxLimit);
                            rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(yAngle, xAngle, 0.0f), Time.deltaTime * 3f);
                        }
                    }
                    break;

                case TouchPhase.Stationary:

                    if (t.fingerId == this.rightFingerId)
                    {
                        this.lookInput = Vector2.zero;

                    }
                    break;

                case TouchPhase.Ended:

                    if (t.fingerId == this.rightFingerId)
                    {
                        this.rightFingerId = -1;
                        Debug.Log("오른쪽 손가락 끝");

                    }
                    break;

                case TouchPhase.Canceled:

                    if (t.fingerId == this.rightFingerId)
                    {
                        this.rightFingerId = -1;
                        Debug.Log("오른쪽 손가락 끝");

                    }
                    break;
            }
        }
    }

    public void CameraShake(float amount, float duration) {
        StartCoroutine(Shake(amount, duration));
    }

    private IEnumerator Shake(float _amount, float _duration) {
        float timer = 0;
        while(timer <= _duration) {
            transform.localPosition = (Vector3)Random.insideUnitCircle * _amount + transform.position;

            timer += Time.deltaTime;
            yield return null;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}