using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDontDestroy : MonoBehaviour
{
    private static DynamicDontDestroy dynamicDontDestroy;
    // Start is called before the first frame update
    void Awake()
    {
        if (dynamicDontDestroy == null)
        {
            dynamicDontDestroy = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if (dynamicDontDestroy != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }
}
