﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (CharacterV3))]
public class PlaneCollisionV3 : MonoBehaviour {

	public float impForce = 2f;
	CharacterV3 CV3;
	ChasingState chaseState;


	void Start () {
		CV3 = GetComponent<CharacterV3>();
		chaseState = GetComponent<ChasingState>();
	}
	

	void OnCollisionStay(Collision coll)
	{
		//print(coll.gameObject.tag);

		if(coll.gameObject.CompareTag("Obstacle"))
		{
//			CV3.inertieVector

			Debug.DrawRay(coll.contacts[0].point, CV3.inertieVector * -10f, Color.magenta);	//inertie
			Debug.DrawRay(coll.contacts[0].point, coll.contacts[0].normal * 10f, Color.green);	//normale
			Debug.DrawRay(coll.contacts[0].point, Vector3.Reflect(CV3.inertieVector, coll.contacts[0].normal) * 10f, Color.red);	//reflect

			Vector3 _reflectionvector = Vector3.Reflect(CV3.inertieVector, coll.contacts[0].normal);

			CV3.ObstacleHit(_reflectionvector, coll.contacts[0].normal, coll.contacts[0].point);

		}
//		else if(coll.gameObject.CompareTag("Impulse"))
//		{
//			print("coll");
//			if(chaseState.currentChaseState == ChasingState.ChaseStates.Target)
//				return;
//
//			Vector3 _impulseVec = transform.position - coll.transform.position;
//			_impulseVec *= impForce;
//			CV3.ImpulseInfluence(_impulseVec);
//		}

	}

	void OnTriggerStay(Collider coll)
	{
//		print(coll.transform.name);
		if(coll.gameObject.CompareTag("Impulse"))
		{
//			print("coll");
			if(chaseState.currentChaseState == ChasingState.ChaseStates.Target)
				return;

			Vector3 _impulseVec = transform.position - coll.transform.position;
			_impulseVec *= impForce;
			CV3.ImpulseInfluence(_impulseVec);
		}
	}

}
