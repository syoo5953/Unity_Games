using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    DontDestroy canvas;
    // Start is called before the first frame update
    void Start()
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
