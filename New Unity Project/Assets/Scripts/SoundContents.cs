using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundContents : MonoBehaviour
{
    AudioSource footStepSound;

    private void Start()
    {
        footStepSound = GetComponent<AudioSource>();
        this.footStepSound.playOnAwake = false;
    }

    void PlayerFootstepSound()
    {
        footStepSound.volume = 0.1f;
        footStepSound.Play();
    }
}
