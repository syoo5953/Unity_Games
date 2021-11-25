using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private DontDestroyOnLoad canvas;
    private void Awake()
    {
        if (canvas == null)
        {
            canvas = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if (canvas != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }
}
