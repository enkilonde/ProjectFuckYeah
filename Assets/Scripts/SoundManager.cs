using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    CharacterV3[] Controllers;

    List<FMOD.Studio.EventInstance> fmod_moving = new List<FMOD.Studio.EventInstance>();
    List<FMOD.Studio.ParameterInstance> fmod_moving_speed = new List<FMOD.Studio.ParameterInstance>();

    // Use this for initialization
    void Awake ()
    {
        Controllers = FindObjectsOfType<CharacterV3>();

        for (int i = 0; i < Controllers.Length; i++)
        {
            FMOD.Studio.EventInstance moving = FMODUnity.RuntimeManager.CreateInstance("event:/Moving"); // Moving
            moving.start();
            fmod_moving.Add(moving);

            FMOD.Studio.ParameterInstance moving_speed; // Moving _ Speed
            moving.getParameter("Speed", out moving_speed);
            moving_speed.setValue(0.0f);
            fmod_moving_speed.Add(moving_speed);
        }

	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < Controllers.Length; i++)
        {
            float v = Mathf.InverseLerp(0, Controllers[i].minAltMaxSpeed, Controllers[i].currentFwdSpeed);
            fmod_moving_speed[i].setValue(v);
        }
	}



}
