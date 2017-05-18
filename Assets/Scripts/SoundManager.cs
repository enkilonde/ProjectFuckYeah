using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    CharacterV3[] Controllers;

    public static float musicVolume = 0.3f;

    FMOD.Studio.EventInstance collisionBounce;
    FMOD.Studio.EventInstance boostSound;
    FMOD.Studio.EventInstance flagTakenSound;
    FMOD.Studio.EventInstance itemUsedSound;
    FMOD.Studio.EventInstance inTrail; // not ready to be used
    FMOD.Studio.EventInstance targetProximity;

    FMOD.Studio.EventInstance activeMusic;

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
        activeMusic = musicMenu;
        StartCoroutine(fadeSound(musicGameplay, 1, 0));
        StartCoroutine(fadeSound(musicMenu, 1, musicVolume, 1));

    }

    public void OnGameplayStart()
    {
        if (fmodFailed) return;
        activeMusic = musicGameplay;
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

    public void ModifyGameplayMusic(float value)
    {
        float vol = 0;
        float volMax = 0;
        musicGameplay.getVolume(out vol, out volMax);

        musicGameplay.setVolumeClamped(vol + value, 0, musicVolume);

    }

    public void ModifyMenuMusic(float value)
    {
        float vol = 0;
        float volMax = 0;
        musicMenu.getVolume(out vol, out volMax);

        musicMenu.setVolumeClamped(vol + value, 0, musicVolume);

    }

    public void UpdateMusic()
    {
        float vol = 0;
        float volMax = 0;
        musicGameplay.getVolume(out vol, out volMax);
        if(activeMusic == musicGameplay)
        {
            musicGameplay.setVolume(musicVolume);
        }


        musicMenu.getVolume(out vol, out volMax);

        if (activeMusic == musicMenu)
        {
            musicMenu.setVolume(musicVolume);
        }
            
    }





    IEnumerator fadeSound(FMOD.Studio.EventInstance sound, float speed, float direction, float delay = 0, FMOD.Studio.EventInstance soundNext = null)
    {
        if (fmodFailed) yield break;
        float volume;
        float finalVolume;
        sound.getVolume(out volume, out finalVolume);

        yield return new WaitForSeconds(delay);

        direction = Mathf.Clamp(direction, 0, musicVolume);


        while (!Mathf.Approximately(direction, volume))
        {
            //sound.setVolume(volume - Time.deltaTime * speed);
            sound.setVolumeClamped(Mathf.MoveTowards(volume, direction, Time.deltaTime * speed), 0, musicVolume);
            sound.getVolume(out volume, out finalVolume);
            yield return null;
        }
        sound.setVolumeClamped(direction, 0, musicVolume);

        if (soundNext != null)
        {
            StartCoroutine(fadeSound(soundNext, speed, 1 - direction));
        }
    }

    public void setMusicVolume(float volume, FMOD.Studio.EventInstance sound)
    {
        sound.setVolumeClamped(Mathf.Min(volume, musicVolume, 0, musicVolume));
    }

}


public static class soundExtention
{

    public static void setVolumeClamped (this FMOD.Studio.EventInstance sound, float volume, float min = 0, float max = 1)
    {
        sound.setVolume(Mathf.Clamp(volume, min, max));
    }

}