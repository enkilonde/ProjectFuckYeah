using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobToolsNameSpace;
using UnityStandardAssets.ImageEffects;

public class CameraV3Placement : MonoBehaviour {

	[Header("Ceci est la vitesse de slide de la caméra le long de la ligne blanche")]
	public float speedCamPosAlongLine = 15f;

	public float maxSpeedLateralDelay = 2f;
	public float minSpeedLateralDelay = 10f;

	public float lateralSpeedTransition = 5f;

	[HideInInspector]
	public Camera mainCam;
	[HideInInspector]
	public DepthOfField DOF;
	[HideInInspector]
	public float minApperture = 0f;
	[HideInInspector]
	public float maxApperture = 0.87f;
	[HideInInspector]
	public int maxAppertureSteps = 5;

	[HideInInspector]
	public Transform posA_recul;
	[HideInInspector]
	public Transform posB_recul;
	[HideInInspector]
	public Transform posA_targetLookAt;
	[HideInInspector]
	public Transform posB_targetLookAt;

	[Range(0f, 1f)]
	public float _t_recul = 0.5f;
	[Range(0f, 1f)]
	public float _t_lateralDelay = 0.5f;
	[Range(0f, 1f)]
	public float _t_lookAt = 0.5f;
	[Range(0f, 1f)]
	public float _t_apperture = 0.5f;
	//C'est global, ça regle les deux au dessus
	private float _t_Embray = 0f;

	//TODO comment on fait pour ne pouvoir que get de l'exterieur ?
	[HideInInspector]
	public Vector3 currentPos;
	[HideInInspector]
	public Vector3 currentTargetLookAt;

	private CharacterV3 CV3;

	void OnEnable()
	{
		CV3 = transform.parent.GetComponentInChildren<CharacterV3>();
        DOF = GetComponentInChildren<DepthOfField>();
	}

	void LateUpdate () {

		//Sync _t with speed
		float _targetPos_t = CV3.currentFwdSpeed / CV3.minAltMaxSpeed;

		_t_Embray = Mathf.MoveTowards(_t_Embray, _targetPos_t, speedCamPosAlongLine * Time.deltaTime);
		_t_recul = _t_Embray;
		float _currentLateralInput = CV3.I_lateralBoostRight - CV3.I_lateralBoostLeft;
		_t_lateralDelay = Mathf.MoveTowards(_t_lateralDelay, _currentLateralInput.Remap(1f, -1f, 0f, 1f), lateralSpeedTransition * Time.deltaTime);
		_t_lookAt = _t_Embray;

		//Update pos
		SetCameraAlongLine();
		SetCameraLateralDelay();
		SetTargetAlongLine();
		//Update Cam effects
		SetApperture();

	}

	public void SetApperture ()
	{
		if(!DOF)
			DOF = Camera.main.GetComponent<DepthOfField>();
		if(!CV3)
			CV3 = GameObject.Find("Character").GetComponent<CharacterV3>();

		//DOF.aperture = Mathf.Lerp(minApperture, maxApperture, _t_apperture);

	}

	public void SetCameraAlongLine()
	{
		currentPos = Vector3.Lerp(posA_recul.position, posB_recul.position, _t_recul);
		mainCam.transform.position = currentPos;
	}

	public void SetCameraLateralDelay()
	{
		float _currentSpeedLateralDelay = Mathf.Lerp(minSpeedLateralDelay, maxSpeedLateralDelay, _t_recul);
		mainCam.transform.position = mainCam.transform.position + (mainCam.transform.right * Mathf.Lerp(-_currentSpeedLateralDelay, _currentSpeedLateralDelay, _t_lateralDelay));
	}

	public void SetTargetAlongLine()
	{
		float _currentSpeedLateralDelay = Mathf.Lerp(minSpeedLateralDelay, maxSpeedLateralDelay, _t_recul);
		currentTargetLookAt = Vector3.Lerp(posA_targetLookAt.position, posB_targetLookAt.position, _t_lookAt);
		mainCam.transform.LookAt(currentTargetLookAt + (mainCam.transform.right * Mathf.Lerp(-_currentSpeedLateralDelay, _currentSpeedLateralDelay, _t_lateralDelay)));
	}

}
