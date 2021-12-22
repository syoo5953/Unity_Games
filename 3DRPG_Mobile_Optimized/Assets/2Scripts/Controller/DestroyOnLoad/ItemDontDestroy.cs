using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDontDestroy : MonoBehaviour
{
    private static ItemDontDestroy itemDontDestroy;
    // Start is called before the first frame update
    void Awake()
    {
        if (itemDontDestroy == null)
        {
            itemDontDestroy = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if (itemDontDestroy != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }
}
