using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GraphChasingState : MonoBehaviour {

	public Material materialOfTheHunter;
	public Material materialOfTheTarget;

	private List<MeshRenderer> myRenderers = new List<MeshRenderer>();


	void Start()
	{
		myRenderers = GetComponentsInChildren<MeshRenderer>().ToList();
	}

	public void SetGraphState(ChasingState.ChaseStates _curChaseState)
	{
		switch(_curChaseState)
		{
		case ChasingState.ChaseStates.Hunter:
			foreach(MeshRenderer _mr in myRenderers)
			{
				_mr.material = materialOfTheHunter;
			}
			break;
		case ChasingState.ChaseStates.Target:
			foreach(MeshRenderer _mr in myRenderers)
			{
				_mr.material = materialOfTheTarget;
			}
			break;
		}
	}

}
