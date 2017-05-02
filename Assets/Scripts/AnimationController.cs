using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{


    public Animator anim;
	CharacterV3 cv3;

    ParticleSystem speedFX;

    [Range(0, 1)]
    public float normalizedTime = 0;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
		cv3 = transform.parent.parent.GetComponentInChildren<CharacterV3>();
        speedFX = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
		normalizedTime = cv3.getSpeedRatioWithBoost();
        jumpToTime(currentAnimationName(), normalizedTime);

        if(cv3.previous_I_forwardBoost == 1)
        {
            speedFX.Play();
        }
        else
        {
            speedFX.Stop();
        }


    }

    void jumpToTime(string name, float nTime)
    {
        anim.Play(name, 0, nTime);
    }

    string currentAnimationName()
    {
        var currAnimName = "";
        foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(clip.name))
            {
                currAnimName = clip.name.ToString();
            }
        }
		return currAnimName;

    }
}
