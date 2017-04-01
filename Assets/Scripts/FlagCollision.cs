using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCollision : MonoBehaviour {

	bool onAPlayer = false;

	void OnCollisionEnter(Collision colInfo)
	{
		if(onAPlayer)
			return;

		if(colInfo.collider.tag == "GameController")
		{
			Debug.Log("Collision with " + colInfo.gameObject.name);
			transform.parent = colInfo.transform;
			transform.localPosition = Vector3.zero;
			colInfo.transform.GetComponent<ChasingState>().BecomeTarget();
			onAPlayer = true;
		}
	}

}
