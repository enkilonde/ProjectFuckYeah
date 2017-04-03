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

	public bool travellingOver = false;

	public Vector3 travellingPosA = Vector3.forward * 10f;
	public Vector3 travellingPosB = Vector3.forward * -4f;

	[HideInInspector]
	public Transform playerToFocus;

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
			Travelling(transform.position, travellingPosB);
			break;
		case CamBehaviorStates.DoOutroTravelling:
			Travelling(playerToFocus.position + travellingPosA + Vector3.up, playerToFocus.position + travellingPosB + Vector3.up);
			transform.LookAt(playerToFocus.position);
			break;
		}

	}

	void Travelling(Vector3 posA, Vector3 posB)
	{
		transform.position = Vector3.MoveTowards(transform.position, posB, 5f * Time.deltaTime);
		if(Vector3.Distance(transform.position, posB) < 0.1f)
			travellingOver = true;
	}


}
