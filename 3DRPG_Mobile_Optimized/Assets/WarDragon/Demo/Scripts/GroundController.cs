using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour {

    public bool m_flyMode = false;
    public float m_speed = 1f;


    public void SetFlyMode(bool toggle)
    {
        if (!toggle)
            m_flyMode = false;
        else
            StartCoroutine(SetFlymodeOn());
    }


    public void SetFlyMode( UnityEngine.UI.Toggle toggle )
    {
        if (!toggle.isOn)
            m_flyMode = false;
        else
            StartCoroutine(SetFlymodeOn());
    }


    IEnumerator SetFlymodeOn()
    {
        yield return new WaitForSeconds(1f);
        m_flyMode = true;
    }


    void Update()
    {
        if(!m_flyMode)
        {
            transform.position = Vector3.Lerp(transform.position, Vector3.zero, Time.deltaTime*2.5f * m_speed);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, -Vector3.up*50f, Time.deltaTime);
        }
    }

}
