﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    CharacterV3[] Controllers;

    public const float musicVolume = 0.1f;

    FMOD.Studio.EventInstance collisionBounce;
    FMOD.Studio.EventInstance boostSound;
    FMOD.Studio.EventInstance flagTakenSound;
    FMOD.Studio.EventInstance itemUsedSound;
    FMOD.Studio.EventInstance inTrail; // not ready to be used
    FMOD.Studio.EventInstance targetProximity;



    FMOD.Studio.EventInstance musicGameplay;
    FMOD.Studio.EventInstance musicMenu;


    bool fmodFailed = false;

    // Use this for initialization
    void Awake ()
    {
        if(instance == null || instance.transform == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        try
        {
            collisionBounce = FMODUnity.RuntimeManager.CreateInstance("event:/Events/Collision Bounce");
            boostSound = FMODUnity.RuntimeManager.CreateInstance("event:/Events/Boost");
            flagTakenSound = FMODUnity.RuntimeManager.CreateInstance("event:/Events/Pick-Up");
            itemUsedSound = FMODUnity.RuntimeManager.CreateInstance("event:/Events/Impulse");
            inTrail = FMODUnity.RuntimeManager.CreateInstance("event:/Events/InTrail");
            targetProximity = FMODUnity.RuntimeManager.CreateInstance("event:/Events/Target proximity");

            musicGameplay = FMODUnity.RuntimeManager.CreateInstance("event:/Musique/Musique InGame");
            musicMenu = FMODUnity.RuntimeManager.CreateInstance("event:/Musique/Musique menu principal");

            musicGameplay.start();
            musicMenu.start();
            musicGameplay.setVolume(0);
            musicMenu.setVolume(0);

            targetProximity.start();
            targetProximity.setParameterValue("Distance to Pursuer", 0);
        }
        catch
        {
            fmodFailed = true;
            throw;
        }

    }


    public void PlaySoundCollision()
    {
        if (fmodFailed) return;
        collisionBounce.start();
    }

    public void PlaySoundFlagCaptured()
    {
        if (fmodFailed) return;
        flagTakenSound.start();
    }

    public void playSoundBoost()
    {
        if (fmodFailed) return;
        boostSound.start();
    }

    public void playSoundItemUsed(string itemName)
    {
        if (fmodFailed) return;
        itemUsedSound.start();
    }

    public void OnMenuStart()
    {
        if (fmodFailed) return;
        StartCoroutine(fadeSound(musicGameplay, 1, 0));
        StartCoroutine(fadeSound(musicMenu, 1, musicVolume, 1));

    }

    public void OnGameplayStart()
    {
        if (fmodFailed) return;
        StartCoroutine(fadeSound(musicMenu, 1, 0));
        StartCoroutine(fadeSound(musicGameplay, 1, musicVolume, 1));

    }

    public void OnButtonClicked()
    {
        if (fmodFailed) return;
        flagTakenSound.start();
    }

    public void FadeGameplaySound(int direction)
    {
        if (fmodFailed) return;
        StartCoroutine(fadeSound(musicGameplay, 1, direction));
    }

    public void SetTargetProximityDistance(float distance)
    {
        if (fmodFailed) return;
        targetProximity.setParameterValue("Distance to Pursuer", distance);
    }


    IEnumerator fadeSound(FMOD.Studio.EventInstance sound, float speed, float direction, float delay = 0, FMOD.Studio.EventInstance soundNext = null)
    {
        if (fmodFailed) yield break;
        float volume;
        float finalVolume;
        sound.getVolume(out volume, out finalVolume);

        yield return new WaitForSeconds(delay);

        if (/*direction < 0.5f*/true)
        {
            while (!Mathf.Approximately(direction, volume))
            {
                //sound.setVolume(volume - Time.deltaTime * speed);
                sound.setVolume(Mathf.MoveTowards(volume, direction, Time.deltaTime * speed));
                sound.getVolume(out volume, out finalVolume);
                yield return null;
            }
            sound.setVolume(direction);
        }
        else
        {
            while (!Mathf.Approximately(1, volume))
            {
                sound.setVolume(volume + Time.deltaTime * speed);
                sound.getVolume(out volume, out finalVolume);
                yield return null;
            }
            sound.setVolume(1);

        }

        if (soundNext != null)
        {
            StartCoroutine(fadeSound(soundNext, speed, 1 - direction));
        }
    }

}
