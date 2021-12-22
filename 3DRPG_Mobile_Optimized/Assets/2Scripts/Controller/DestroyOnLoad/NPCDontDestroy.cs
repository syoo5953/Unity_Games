using UnityEngine;

public class NPCDontDestroy : MonoBehaviour
{
    private static NPCDontDestroy nPCDontDestroy;
    // Start is called before the first frame update
    void Awake()
    {
        if (nPCDontDestroy == null)
        {
            nPCDontDestroy = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if (nPCDontDestroy != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }
}
