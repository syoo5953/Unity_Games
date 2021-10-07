using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Zoom : MonoBehaviour
{
    public CinemachineFreeLook freeLook;

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            freeLook.m_Lens.FieldOfView+=2;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            freeLook.m_Lens.FieldOfView-=2;
        }
    }
}
