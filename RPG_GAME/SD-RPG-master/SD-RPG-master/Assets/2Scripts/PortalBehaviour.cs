using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(9))
        {
            UIManager.Instance.OnOffCanvas(false, false, true);
            gameManager.LoadScene();
        }
    }
}
