using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRotors : MonoBehaviour {


	CharacterV3 CV3;

	Transform[] rotors;

	float currentSpeed = 0f;

	float anglePerSecond = 1000f;

	void Start () {
		CV3 = transform.parent.parent.GetComponent<CharacterV3>();
		rotors = new Transform[transform.childCount];
		rotors = transform.GetComponentsInChildren<Transform>();
	}
	
	void Update () {

		currentSpeed = Mathf.MoveTowards(currentSpeed, CV3.getSpeedRatio(), Time.deltaTime * 4f);

		for (int i = 1; i < rotors.Length; i++) {
//			rotors[i].rotation *= Quaternion.AngleAxis(100f * Time.deltaTime, rotors[i].up);
			rotors[i].Rotate(rotors[i].up * (currentSpeed * anglePerSecond) * Time.deltaTime, Space.World);
//			rotors[i].localRotation *= Quaternion.Euler(rotors[i].up * 100f);
//			rotors[i].Rotate(Vector3.up * 100f * Time.deltaTime);

		}


	}
}
