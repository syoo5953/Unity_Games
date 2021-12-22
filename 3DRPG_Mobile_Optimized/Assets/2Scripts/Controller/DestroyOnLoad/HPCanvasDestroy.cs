using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPCanvasDestroy : MonoBehaviour
{
    private static HPCanvasDestroy hPCanvasDestroy;
    // Start is called before the first frame update
    void Awake()
    {
        if (hPCanvasDestroy == null)
        {
            hPCanvasDestroy = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if (hPCanvasDestroy != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }
}
