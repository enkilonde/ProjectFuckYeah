using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobToolsNameSpace;
using UnityStandardAssets.ImageEffects;

public class CameraV2 : MonoBehaviour {

	[Header("Nombre d'embraillages à prendre en compte dans le recul de la caméra")]
	[Range(1, 30)]
	public int steps = 15;

	[Header("Lorsque l'ambraillage change, ceci est la vitesse à laquelle la caméra change de position")]
	public float speedCamPosAlongLine = 15f;

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

	private CharacterV2 CV2;

	void OnEnable()
	{
		CV2 = GameObject.Find("Character").GetComponent<CharacterV2>();
		DOF = Camera.main.GetComponent<DepthOfField>();
	}

	void LateUpdate () {

		//Sync _t with speed
		float _target_t = (float)CV2.currentAmbray / (float)steps;
//		print("cur:" + CV2.currentAmbray + " / max:" + steps + " = " + _target_t);

		_t_Embray = Mathf.MoveTowards(_t_Embray, _target_t, speedCamPosAlongLine * Time.deltaTime);
		_t_recul = _t_Embray;
		_t_lookAt = _t_Embray;

		//Update pos
		SetCameraAlongLine();
		SetTargetAlongLine();
		//Update Cam effects
		SetApperture();

	}

	public void SetApperture ()
	{
		if(!DOF)
			DOF = Camera.main.GetComponent<DepthOfField>();
		if(!CV2)
			CV2 = GameObject.Find("Character").GetComponent<CharacterV2>();

		if(Application.isPlaying)
			_t_apperture = (float)CV2.currentAmbray / (float)maxAppertureSteps;

		DOF.aperture = Mathf.Lerp(minApperture, maxApperture, _t_apperture);

	}

	public void SetCameraAlongLine()
	{
		currentPos = Vector3.Lerp(posA_recul.position, posB_recul.position, _t_recul);
		mainCam.transform.position = currentPos;
	}

	public void SetTargetAlongLine()
	{
		currentTargetLookAt = Vector3.Lerp(posA_targetLookAt.position, posB_targetLookAt.position, _t_lookAt);
		mainCam.transform.LookAt(currentTargetLookAt);
	}

}
