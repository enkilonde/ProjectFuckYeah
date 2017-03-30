using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (CharacterV3))]
public class PlaneCollisionV3 : MonoBehaviour {

	CharacterV3 CV3;

	void Start () {
		CV3 = GetComponent<CharacterV3>();
	}
	

	void OnCollisionStay(Collision coll)
	{

		if(coll.gameObject.CompareTag("Obstacle"))
		{
//			CV3.inertieVector

			Debug.DrawRay(coll.contacts[0].point, CV3.inertieVector * -10f, Color.magenta);	//inertie
			Debug.DrawRay(coll.contacts[0].point, coll.contacts[0].normal * 10f, Color.green);	//normale
			Debug.DrawRay(coll.contacts[0].point, Vector3.Reflect(CV3.inertieVector, coll.contacts[0].normal) * 10f, Color.red);	//reflect

			Vector3 _reflectionvector = Vector3.Reflect(CV3.inertieVector, coll.contacts[0].normal);

			CV3.ObstacleHit(_reflectionvector, coll.contacts[0].normal, coll.contacts[0].point);

		}

	}

}
