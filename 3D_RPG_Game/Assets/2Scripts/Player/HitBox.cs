using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public List<Collider> targetList;

    private void Awake()
    {
        targetList = new List<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            targetList.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            targetList.Remove(other);
        }
    }
}
