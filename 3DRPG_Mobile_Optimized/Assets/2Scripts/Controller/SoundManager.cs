﻿using UnityEngine;

public class SoundManager : Singleton<SoundManager> {
    public AudioSource[] audioSources;

    public void playAudio(string audioName) {
        switch(audioName) {
            case "PlayerAttack":
                audioSources[0].Play();
                break;

            case "GetItem":
                audioSources[1].Play();
                break;

            case "Jump":
                audioSources[2].Play();
                break;

            case "PlayerDie":
                audioSources[3].Play();
                break;

            case "Hit":
                audioSources[4].Play();
                break;

            case "MonsterDie":
                audioSources[5].Play();
                break;
            case "Skill1":
                audioSources[6].Play();
                break;
            case "Skill2":
                audioSources[7].Play();
                break;
            case "DragonFly":
                audioSources[8].Play();
                break;
            case "DragonRoar":
                audioSources[9].Play();
                break;
        }
    }
}