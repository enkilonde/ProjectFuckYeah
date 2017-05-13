using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnPlace : MonoBehaviour {

	CharacterV3 CV3;

	float currentSpeed = 0f;

	float anglePerSecond = 1000f;

	void Start () {
		CV3 = transform.parent.parent.parent.GetComponent<CharacterV3>();
	}

	void Update () {

		currentSpeed = Mathf.MoveTowards(currentSpeed, CV3.getSpeedRatio(), Time.deltaTime * 4f);

//		transform.Rotate(transform.forward * (currentSpeed * anglePerSecond) * Time.deltaTime, Space.Self);

//		transform.localRotation = Quaternion.AngleAxis(100f * Time.deltaTime, transform.forward);

		transform.rotation *= Quaternion.AngleAxis(50f * Time.deltaTime, transform.forward);

	}
}
