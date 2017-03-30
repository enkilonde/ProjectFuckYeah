using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterV1 : MonoBehaviour {

	#region enable/disable features
	[Header("Activer/desactiver features")]
	public bool isUsing_inputSpeed = true;
	public bool isUsing_inputInclinnaison = true;
	public bool isUsing_inputAssiette = true;
	public bool isUsing_inputLacet = true;

	public bool isUsing_deregInclinnaison = true;
	public bool isUsing_deregAssiette = true;
	public bool isUsing_deregLacet = true;
	#endregion

	#region influence values
	[Header("Influence des inputs sur les valeurs finales")]
	[Range(0f, 50f)]
	public float input_Speed_Power = 10f;
	[Range(0f, 50f)]
	public float input_Inclinnaison_Power = 10f;
	[Range(0f, 50f)]
	public float input_Assiette_Power = 10f;
	[Range(0f, 50f)]
	public float input_Lacet_Power = 10f;

	[Header("Influence des déreglements sur les valeurs finales")]
	[Range(0f, 10f)]
	public float dereg_Inclinnaison = 5f;
	[Range(0f, 10f)]
	public float dereg_Assiette = 5f;
	[Range(0f, 10f)]
	public float dereg_Lacet = 5f;
	#endregion

	#region speed values
	[Header("Debug only")]//TODO max et min
	public float currentMoveSpeed = 50f;
	public float min_moveSpeed = 0f;
	public float max_moveSpeed = 100f;
	public float currentInclinaisonSpeed = 0f;
	public float min_inclSpeed = -50f;
	public float max_inclSpeed = 50f;
	public float currentAssietteSpeed = 0f;
	public float min_assSpeed = -50f;
	public float max_assSpeed = 50f;
	public float currentLacetSpeed = 0f;
	public float min_laceSpeed = -50f;
	public float max_laceSpeed = 50f;
	#endregion

	private ControllerScriptV1 controllerScriptInstance;

	void Start () {

		controllerScriptInstance = transform.parent.GetComponentInChildren<ControllerScriptV1>();

	}
	

	void Update () {

		#region transform
		transform.position += transform.forward * currentMoveSpeed * Time.deltaTime;
		transform.Rotate(currentAssietteSpeed * Time.deltaTime, currentLacetSpeed * Time.deltaTime, currentInclinaisonSpeed * Time.deltaTime);
		#endregion

		#region speed
		UpdateSpeeds_Input();
		UpdateSpeeds_Dereg();
		ClampEveryValue();
		#endregion
	}

	#region checking feature utilisation
	void UpdateSpeeds_Input()
	{
		if(isUsing_inputSpeed)
			currentMoveSpeed = 			SetInputSpeed(currentMoveSpeed, controllerScriptInstance.speedInputValue, input_Speed_Power);
		if(isUsing_inputInclinnaison)
			currentInclinaisonSpeed =	SetInputSpeed(currentInclinaisonSpeed, controllerScriptInstance.inclinnaisonInputValue, input_Inclinnaison_Power);
		if(isUsing_inputAssiette)
			currentAssietteSpeed =		SetInputSpeed(currentAssietteSpeed, controllerScriptInstance.assietteInputValue, input_Assiette_Power);
		if(isUsing_inputLacet)
			currentLacetSpeed = 		SetInputSpeed(currentLacetSpeed, controllerScriptInstance.lacetInputValue, input_Lacet_Power);
	}

	void UpdateSpeeds_Dereg()
	{
		if(isUsing_deregInclinnaison)
			currentInclinaisonSpeed = SetDeregSpeed(currentInclinaisonSpeed, dereg_Inclinnaison);
		if(isUsing_deregAssiette)
			currentAssietteSpeed = SetDeregSpeed(currentAssietteSpeed, dereg_Assiette);
		if(isUsing_deregLacet)
			currentLacetSpeed = SetDeregSpeed(currentLacetSpeed, dereg_Lacet);
	}
	#endregion

	#region calculation
	void ClampEveryValue()
	{
		currentMoveSpeed = 			Mathf.Clamp(currentMoveSpeed, min_moveSpeed, max_moveSpeed);
		currentInclinaisonSpeed = 	Mathf.Clamp(currentInclinaisonSpeed, min_inclSpeed, max_inclSpeed);
		currentAssietteSpeed = 		Mathf.Clamp(currentAssietteSpeed, min_assSpeed, max_assSpeed);
		currentLacetSpeed = 		Mathf.Clamp(currentLacetSpeed, min_laceSpeed, max_laceSpeed);
	}

	/// <summary>
	/// Sets the input speed.
	/// </summary>
	/// <returns>The input speed.</returns>
	/// <param name="current">Current value of the speed.</param>
	/// <param name="inputValue">Input value.</param>
	/// <param name="inputPower">Input multiplier, the power of the input upon the Current parameter.</param>
	float SetInputSpeed(float current, float inputValue, float inputPower)
	{
		return current + ((inputPower * inputValue) * Time.deltaTime);
	}

	float SetDeregSpeed(float current, float factor)
	{

		if(current == 0f)
			return current;
		
		return current + ((Mathf.Sign(current) * factor) * Time.deltaTime);
	}
	#endregion
}
