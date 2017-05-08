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


        collisionBounce = FMODUnity.RuntimeManager.CreateInstance("event:/Collision Bounce");
        boostSound = FMODUnity.RuntimeManager.CreateInstance("event:/Crash");
        flagTakenSound = FMODUnity.RuntimeManager.CreateInstance("event:/Target Destruct");
        itemUsedSound = FMODUnity.RuntimeManager.CreateInstance("event:/Crash");

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


}
