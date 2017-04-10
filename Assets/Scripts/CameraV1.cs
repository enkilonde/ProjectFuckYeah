using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraV1 : MonoBehaviour {

	public static Transform targetToLock;

	public enum camModes
	{
		InstantFollow,
		LockedBehind,
		AssCombat,
		Hunter,
		Target
	}

	public camModes CameraType = camModes.LockedBehind;

	[Range(0f, 1f)]
	[Header("Valeur minimale pour prendre en compte l'input pour bouger la caméra")]
	public float camRot_minSensitivity = 0.3f;
	[Range(0f, 180f)]
	public float maxLateralRotAngle = 180f;
	[Range(0f, 180f)]
	public float maxVerticalRotAngle = 45f;
	[Header("Angle par seconde de rotation de la camera autour du joueur selon son input")]
	public float camInputSpeed = 10f;


	Vector3 initialPosition;
	Transform character;
	ControllerV3 controler;
	CharacterV3 cv3;

	void Start () {
		character = transform.parent.transform.FindChild("Character").transform;
		controler = transform.parent.GetComponentInChildren<ControllerV3>();
		cv3 = character.GetComponent<CharacterV3>();

		switch(CameraType)
		{
		case camModes.InstantFollow:
			if(character.transform.localPosition != Vector3.zero)
			{
				Debug.LogError("ATTENTION ! La position du Character n'est pas set en 0,0,0. Afin que la caméra se place bien, laisse le Character en 0,0,0 par rapport à l'objet 3C");
			}

			initialPosition = transform.localPosition;	
			break;
		case camModes.LockedBehind:
			transform.parent = character;
			break;
		case camModes.AssCombat:
			transform.parent = character;
			break;
		case camModes.Hunter:
			transform.parent = character;
			break;
		case camModes.Target:
			transform.parent = character;
			break;
		}


	}
	
	void LateUpdate () {
		
		switch(CameraType)
		{
		case camModes.InstantFollow:
			transform.localPosition = character.position + initialPosition;
			break;
		case camModes.LockedBehind:
			if(transform.parent != character)
				transform.parent = character;
			break;
		case camModes.AssCombat:
			if(transform.parent != character)
				transform.parent = character;
			float lateralCamRot = Input.GetAxis(controler.Get_HorizontalCameraInput());
//			print("Lateral cam rot" + lateralCamRot);
			if(Mathf.Abs(lateralCamRot) > camRot_minSensitivity)
			{
//				print("Lateral cam rot" + lateralCamRot);
			}
			else
			{
				lateralCamRot = 0;
			}
			float verticalCamRot = Input.GetAxis(controler.Get_VerticalCameraInput());
			if(Mathf.Abs(verticalCamRot) > camRot_minSensitivity)
			{
//				print("Vertical cam rot" + verticalCamRot);
			}
			else
			{
				verticalCamRot = 0;
			}
			Vector3 _eulerCamRot = new Vector3(Mathf.Lerp(0, maxVerticalRotAngle, Mathf.Abs(verticalCamRot)) * verticalCamRot, Mathf.Lerp(0, maxLateralRotAngle, Mathf.Abs(lateralCamRot)) * lateralCamRot, 0);
			transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(_eulerCamRot), camInputSpeed * Time.deltaTime);
//			Debug.DrawRay(new Vector3(transform.position.x,transform.position.y,transform.position.z - 2f), new Vector3(lateralCamRot, verticalCamRot, 0f), Color.red);
			break;
		case camModes.Hunter:
			transform.position = character.position;
			if(Input.GetButton(controler.Get_LockOnInput()))
			{
				print(targetToLock.name);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetToLock.position - transform.position, Vector3.up), camInputSpeed * Time.deltaTime);
			}
			else
			{
				transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.identity, camInputSpeed * Time.deltaTime);
//				transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.LookRotation(-cv3.inertieVector.normalized, transform.up), camInputSpeed * Time.deltaTime);
			}
			break;
		case camModes.Target:
			transform.position = character.position;
			if(Input.GetButton(controler.Get_LockOnInput()))
			{
				transform.rotation = Quaternion.LookRotation(-transform.parent.forward, Vector3.up);
			}
			else
			{
				transform.localRotation = Quaternion.identity;
			}
			break;
		}


	}
		

}
