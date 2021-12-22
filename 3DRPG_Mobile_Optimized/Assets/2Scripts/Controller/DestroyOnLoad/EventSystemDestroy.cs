using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemDestroy : MonoBehaviour
{
    private static EventSystemDestroy eventSystemInstance;
    // Start is called before the first frame update
    void Awake()
    {
        if (eventSystemInstance == null)
        {
            eventSystemInstance = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if (eventSystemInstance != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }
}
