using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDontDestroy : MonoBehaviour
{
    private static ParticleDontDestroy particleDontDestroy;
    // Start is called before the first frame update
    void Awake()
    {
        if (particleDontDestroy == null)
        {
            particleDontDestroy = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if (particleDontDestroy != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }
}
