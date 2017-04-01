using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCam : MonoBehaviour {


	public enum CamBehaviorStates
	{
		Static,
		DoIntroTravelling,
		DoOutroTravelling
	}
	public CamBehaviorStates camState = CamBehaviorStates.Static;

	public bool introTravellingOver = false;

	public Vector3 travellingPosA = Vector3.forward * 10f;
	public Vector3 travellingPosB = Vector3.forward * -4f;

	void Start()
	{
		transform.position = travellingPosA;
	}

	void Update()
	{
	
		switch(camState)
		{
		case CamBehaviorStates.Static:
			break;
		case CamBehaviorStates.DoIntroTravelling:
			Travelling();
			break;
		case CamBehaviorStates.DoOutroTravelling:
			break;
		}

	}

	void Travelling()
	{
		transform.position = Vector3.MoveTowards(transform.position, travellingPosB, 5f * Time.deltaTime);
		if(Vector3.Distance(transform.position, travellingPosB) < 0.1f)
			introTravellingOver = true;
	}


}
