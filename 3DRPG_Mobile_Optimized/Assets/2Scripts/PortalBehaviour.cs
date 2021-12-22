using UnityEngine;
public class PortalBehaviour : MonoBehaviour
{
    public int passTheScene;
    public bool ToScene;
    public Color loadToColor = Color.white;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            if(ToScene)
            {
                UIManager.Instance.OnOffCanvas(false, false);
                GameManager.Instance.LoadScene(passTheScene);
            } else
            {
                Initiate.Fade(loadToColor, 1.0f);
            }
        }
    }
}
