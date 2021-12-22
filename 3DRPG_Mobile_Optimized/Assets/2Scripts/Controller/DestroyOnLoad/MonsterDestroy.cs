using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDestroy : MonoBehaviour
{
    private static MonsterDestroy monsterDestroy;
    // Start is called before the first frame update
    void Awake()
    {
        if (monsterDestroy == null)
        {
            monsterDestroy = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if (monsterDestroy != this)
            Destroy(gameObject); // On reload, singleton already set, so destroy duplicate.
    }
}
