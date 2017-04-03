﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : MonoBehaviour {


	public enum ChaseStates
	{
		Hunter,
		Target
	}
	public ChaseStates currentChaseState = ChaseStates.Hunter;

	private GraphChasingState GraChaSta;

	void Start()
	{
		GraChaSta = GetComponentInChildren<GraphChasingState>();
	}

	public void BecomeTarget()
	{
		currentChaseState = ChaseStates.Target;
		GraChaSta.SetGraphState(currentChaseState);
	}
		

}
