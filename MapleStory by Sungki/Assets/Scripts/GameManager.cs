using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    new AudioSource audio;
    public AudioClip otherClip;
    public AudioClip pickupSound;
    private GameObject golem;
    
    public List<GameObject> items = new List<GameObject>();
    public Dictionary<string, int> itemDict = new Dictionary<string, int>();

    void Start()
    {
        golem = (GameObject)Resources.Load("Golem");
        audio = GetComponent<AudioSource>();
        audio.clip = otherClip;
        audio.volume = 5;
        audio.Play();
    }

    // Update is called once per frame
    public void Respawn(Vector2 transPos)
    {
        StartCoroutine(Delay(transPos));
    }

    IEnumerator Delay(Vector2 transPos)
    {
        yield return new WaitForSeconds(4f);
        GameObject cloneGolem = Instantiate(golem);
        cloneGolem.transform.position = transPos;
    }
    public void AddItem(GameObject item)
    {
        audio.PlayOneShot(pickupSound);
        string name = item.GetComponent<Item>().itemName;
        GameObject gameObject = (GameObject)Resources.Load(name);
        if(!itemDict.ContainsKey(name))
        {
            itemDict.Add(name, 1);
        } else
        {
            itemDict[name] += 1;
        }

        if(!items.Contains(gameObject))
        {
            items.Add(gameObject);
        } else
        {
            Debug.Log("Already have that Item");
        }
        
        Destroy(item);
    }
}
