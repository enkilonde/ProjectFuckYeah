using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaytestScript : MonoBehaviour {

	public Transform[] targetPlaces;



	void Start()
	{
		CameraV1.targetToLock = targetPlaces[0];
		print(CameraV1.targetToLock.name);
	}

}
