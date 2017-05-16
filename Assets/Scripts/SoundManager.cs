using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    CharacterV3[] Controllers;


    FMOD.Studio.EventInstance collisionBounce;
    FMOD.Studio.EventInstance boostSound;
    FMOD.Studio.EventInstance flagTakenSound;
    FMOD.Studio.EventInstance itemUsedSound;
    FMOD.Studio.EventInstance musicGameplay;
    FMOD.Studio.EventInstance musicMenu;


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


        collisionBounce = FMODUnity.RuntimeManager.CreateInstance("event:/Events/Collision Bounce");
        boostSound = FMODUnity.RuntimeManager.CreateInstance("event:/Events/Boost");
        flagTakenSound = FMODUnity.RuntimeManager.CreateInstance("event:/Events/Pick-Up");
        itemUsedSound = FMODUnity.RuntimeManager.CreateInstance("event:/Events/Impulse");

        musicGameplay = FMODUnity.RuntimeManager.CreateInstance("event:/Musique/Musique InGame");
        musicMenu = FMODUnity.RuntimeManager.CreateInstance("event:/Musique/Musique menu principal");


    }


    public void PlaySoundCollision()
    {
        collisionBounce.start();
    }

    public void PlaySoundFlagCaptured()
    {
        flagTakenSound.start();
    }

    public void playSoundBoost()
    {
        boostSound.start();
    }

    public void playSoundItemUsed(string itemName)
    {
        itemUsedSound.start();
    }

    public void OnMenuStart()
    {
        StartCoroutine(fadeSound(musicGameplay, 1, 0, musicMenu));
    }



    public void OnGameplayStart()
    {
        StartCoroutine(fadeSound(musicMenu, 1, 0, musicGameplay));
    }

    public void OnButtonClicked()
    {
        flagTakenSound.start();
    }



    IEnumerator fadeSound(FMOD.Studio.EventInstance sound, float speed, int direction, FMOD.Studio.EventInstance soundNext = null)
    {
        float volume;
        float finalVolume;
        sound.getVolume(out volume, out finalVolume);

        if (direction < 0.5f)
        {
            while (volume > 0)
            {
                sound.setVolume(volume - Time.deltaTime * speed);
                sound.getVolume(out volume, out finalVolume);
                //Debug.Log("Volume = " + volume);
                yield return null;
            }
        }
        else
        {
            while (volume < 1)
            {
                sound.setVolume(volume + Time.deltaTime * speed);
                sound.getVolume(out volume, out finalVolume);
                yield return null;
            }
        }
        Debug.Log("Fade ended");
        sound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        sound.setVolume(1);
        if (soundNext != null) soundNext.start();
    }

}
