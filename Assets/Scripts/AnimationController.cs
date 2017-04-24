using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{


    public Animator anim;
	CharacterV3 cv3;

    [Range(0, 1)]
    public float normalizedTime = 0;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
		cv3 = transform.parent.parent.GetComponentInChildren<CharacterV3>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

        }
		normalizedTime = Mathf.InverseLerp(0, cv3.minAltMaxSpeed, cv3.currentFwdSpeed);
        jumpToTime(currentAnimationName(), normalizedTime);

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
