using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public int layerNum;
    public List<Collider> targetList;

    private void Awake()
    {
        targetList = new List<Collider>();
    }

    private void FixedUpdate()
    {
        if(GameManager.Instance.player.IsDie)
        {
            targetList.Clear();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == layerNum)
        {
            targetList.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == layerNum)
        {
            targetList.Remove(other);
        }
    }

    public void RemoveFromList()
    {
        targetList.Clear();
    }
}
