using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : MonoBehaviour {


	public enum ChaseStates
	{
		Hunter,
		Target
	}
	public ChaseStates currentChaseState = ChaseStates.Hunter;

	public void BecomeTarget()
	{
		currentChaseState = ChaseStates.Target;
	}
		

}
