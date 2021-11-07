using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
public class Zoom : MonoBehaviour
{
    public CinemachineFreeLook freeLook;

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            freeLook.m_Lens.FieldOfView += 2;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            freeLook.m_Lens.FieldOfView -= 2;
        }

        if (IsMouseOverUI())
        {
            freeLook.enabled = false;
        }
        else
        {
            freeLook.enabled = true;
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
