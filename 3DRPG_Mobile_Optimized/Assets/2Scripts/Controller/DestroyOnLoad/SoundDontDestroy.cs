using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDontDestroy : MonoBehaviour
{
    private static SoundDontDestroy soundDontDestroy;
    // Start is called before the first frame update
    void Awake()
    {
        if (soundDontDestroy == null)
        {
            soundDontDestroy = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if (soundDontDestroy != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }
}
