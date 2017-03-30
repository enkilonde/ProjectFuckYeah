using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCollision : MonoBehaviour {

	[Header("Nb d'embraillages a perdre sur collision avec un element petit")]
	public int embrayToLoss = 2;

	Collider myCollider;
	CharacterController myController;
	CharacterV2 charaScript;

	void OnEnable () {
		myCollider = GetComponent<Collider>();
		myController = GetComponent<CharacterController>();
		charaScript = GetComponent<CharacterV2>();
	}

	void OnTriggerEnter(Collider coll)
	{
		//TODO remplacer destroy et spawn par l'object pool
		switch(coll.name)
		{
		case "BigOne":
			charaScript.BigElementCollision();
			coll.GetComponent<GenerateAtMyDeath>().GenerateLittlesThenDie();
			Destroy(coll.gameObject);
			//TODO spawn littleOnes
			break;
		case "LittleOne":
			charaScript.LittleElementCollision(embrayToLoss);
			Destroy(coll.gameObject);
			break;
		default:
			Debug.Log("What did i just collided ?");
			break;
		}

	}

}
