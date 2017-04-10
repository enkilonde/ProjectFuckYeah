using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobToolsNameSpace;

public class CharacterV2 : MonoBehaviour {

	[Header("Pitch input")]
	public bool reverse_Y_Axis = false;
	[Header("Nb Embraillages max")]
	public int nbEmbrayMax = 15;
	[Header("Clamp la vitesse max à cette valeur")]
	public float maxFinalSpeed = 200f;

	[Space(25)]

	#region move
	[SerializeField]
	[Header("La vitesse (en unités) initiale")]
	private float initialSpeed = 1f;
	[SerializeField]
	[Header("Le boost de vitesse (en unités) au fil du temps")]
	private float automaticAcceleration = 2f;
	[SerializeField]
	[Header("Plafond de vitesse (en unités) initial")]
	private float initialMaxSpeed = 100;
	[Range(1f, 100f)]
	[Header("Pourcentage de la vitesse max actuelle neccessaire avant de pouvoir enclencher l'embraillage")]
	public float embraPercent = 80;
	[SerializeField]
	[Range(1f, 100f)]
	[Header("A l'activation de l'embraillage, la vitesse maximum est additionnée par ce pourcentage")]
	private float limitSpeedBoostPercent = 50f;
	[SerializeField]
	[Header("Le boost de vitesse (en unités) au passage d'embraillage")]
	private float palierBoostImpulsion = 10f;
	#endregion

	[Space(25)]

	#region rotation
	[SerializeField]
	[Header("La vitesse max (en angle/sec) d'assiette initiale")]
	private float initial_PitchRotSpeed = 10f;
	[SerializeField]
	[Header("La vitesse max (en angle/sec) latérale initiale")]
	private float initial_YawRotSpeed = 10f;
	[SerializeField]
	[Header("La vitesse max (en unités) d'assiette maximum")]
	private float max_PitchRotSpeed = 100f;
	[SerializeField]
	[Header("La vitesse max (en unités) latérale maximum")]
	private float max_YawRotSpeed = 100f;
	#endregion

//	private ControllerScriptV1 controllerScriptInstance;
	private CharacterController myController;
	private float current_PitchRotSpeed = 0f;
	private float current_YawRotSpeed = 0f;

	[HideInInspector]
	public float currentMoveSpeed;
	[HideInInspector]
	public float currentMaxSpeed;
	[HideInInspector]
	public int currentAmbray = 0;



	void OnEnable () {
//		controllerScriptInstance = transform.parent.GetComponentInChildren<ControllerScriptV1>();
		myController = GetComponent<CharacterController>();

		currentMoveSpeed = initialSpeed;
		currentMaxSpeed = initialMaxSpeed;
	}

	void Update () {
		#region transform
		Vector3 _dir = transform.forward * currentMoveSpeed * Time.deltaTime;
		myController.Move(_dir);
//		transform.position += transform.forward * currentMoveSpeed * Time.deltaTime;
		RotPlayer();
		#endregion

		#region speed
		//Maj la vitesse (acceleration constante)
		Boost(automaticAcceleration, false);
		//Maj embraillage
		UpdateMaxSpeed();
		//Maj rotSpeed max
		UpdateMaxRotSpeed();
		#endregion

	}

	/// <summary>
	/// Rots the player.
	/// </summary>
	void RotPlayer()
	{
		float _vertInput = (!reverse_Y_Axis) ? -Input.GetAxis("Vertical") : Input.GetAxis("Vertical");
		float _pitch = _vertInput * current_PitchRotSpeed;
		float _yaw = Input.GetAxis("Horizontal") * current_YawRotSpeed;

		transform.Rotate(_pitch * Time.deltaTime, 0f,0f, Space.Self);
		transform.Rotate(0f, _yaw * Time.deltaTime, 0f, Space.World);

	}

	/// <summary>
	/// Updates the max rot speed.
	/// </summary>
	void UpdateMaxRotSpeed()
	{
		float _t = RobToolsClass.GetNormalizedValue(currentMoveSpeed, 0, maxFinalSpeed);

		//Get current Pitch speed
		current_PitchRotSpeed = Mathf.Lerp(initial_PitchRotSpeed, max_PitchRotSpeed, _t);

		//Get current Yaw speed
		current_YawRotSpeed = Mathf.Lerp(initial_YawRotSpeed, max_YawRotSpeed, _t);

	}

	/// <summary>
	/// Updates the max speed.
	/// </summary>
	void UpdateMaxSpeed()
	{
		float _speedPorcent = RobToolsClass.GetPercentFromValue(currentMoveSpeed, currentMaxSpeed);

		//Si on peux encore actionner le boost (Si on est en dessous de l'embrayage max)
		if(currentAmbray < nbEmbrayMax){
			//Si notre vitesse actuelle est au dessus de la valeur d'embraillage
			if(_speedPorcent > embraPercent)
			{
				//Alors Si le joueur appui sur la touche d'embraillage
				if(Input.GetKeyDown(KeyCode.Space))
				{
					float maxSpeedBoost = RobToolsClass.GetValueFromPercent(100 + limitSpeedBoostPercent, currentMaxSpeed);
					currentMaxSpeed = Mathf.Clamp(currentMaxSpeed + maxSpeedBoost, initialMaxSpeed, maxFinalSpeed);
					currentAmbray++;
					Boost(palierBoostImpulsion, true);
				}

			}
		}
	}

	/// <summary>
	/// Boost the currentSpeed by value parameter.
	/// </summary>
	/// <param name="value">Value.</param>
	/// <param name="instantBoost">If set to <c>true</c> The value is added to currentSpeed. If not, the value is multiplied by deltaTime before.</param>
	public void Boost(float value, bool instantBoost)
	{
		currentMoveSpeed += (instantBoost) ? value : value * Time.deltaTime;
		currentMoveSpeed = Mathf.Clamp(currentMoveSpeed, 0f, currentMaxSpeed);
	}

	public void LittleElementCollision(int _lostAmbray)
	{
		//Quel pourcentage de vitesse a t'on
		float _currentAmbrayPercentSpeed = RobToolsClass.GetPercentFromValue(currentMoveSpeed, currentMaxSpeed);

		//Perte d'ambraillage
		currentAmbray = Mathf.Clamp(currentAmbray - _lostAmbray, 0, nbEmbrayMax);

		//Mise à jour de la vitesse maximale
		float _tempMax = currentMaxSpeed;
		for (int i = 0; i < _lostAmbray; i++) {
			float _maxSpeedLoss = _tempMax/(1f+(limitSpeedBoostPercent/100f));	//Descendre d'un palier
			_tempMax = _maxSpeedLoss;
		}
		currentMaxSpeed = Mathf.Clamp(_tempMax, initialMaxSpeed, maxFinalSpeed);

		//Réappliquer le pourcentage que l'on avais
		if(currentAmbray == 0)
		{
			currentMoveSpeed = 0f;
		}else{
			currentMoveSpeed = RobToolsClass.GetValueFromPercent(_currentAmbrayPercentSpeed, currentMaxSpeed);
		}

	}

	public void BigElementCollision()
	{
		currentAmbray = 0;
		currentMaxSpeed = initialMaxSpeed;
		currentMoveSpeed = 0f;
		current_PitchRotSpeed = 0f;
		current_YawRotSpeed = 0f;
	}

}
