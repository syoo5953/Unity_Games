using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDontDestroy : MonoBehaviour
{
    private static StaticDontDestroy staticDontDestroy;
    // Start is called before the first frame update
    void Awake()
    {
        if (staticDontDestroy == null)
        {
            staticDontDestroy = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if (staticDontDestroy != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }
}
