using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public int skillDmg;
    AudioSource audio; 


    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        skillDmg = Random.Range(250, 350);
    }
}
