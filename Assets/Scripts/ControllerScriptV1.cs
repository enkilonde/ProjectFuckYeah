using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScriptV1 : MonoBehaviour {
	
	public enum axisEnum
	{
		QD,
		ZS,
		MouseX,
		Revert_MouseX,
		MouseY,
		Revert_MouseY,
		None
	}

	public axisEnum speedInputAxis = axisEnum.ZS;
	public axisEnum inclinaisonInputAxis = axisEnum.MouseY;
	public axisEnum assietteInputAxis = axisEnum.MouseX;
	public axisEnum lacetInputAxis = axisEnum.QD;

	[HideInInspector]
	public float speedInputValue = 0f;
	[HideInInspector]
	public float inclinnaisonInputValue = 0f;
	[HideInInspector]
	public float assietteInputValue = 0f;
	[HideInInspector]
	public float lacetInputValue = 0f;

	
	void Update () {
		speedInputValue = InputValueByInputType(speedInputAxis);
		inclinnaisonInputValue = InputValueByInputType(inclinaisonInputAxis);
		assietteInputValue = InputValueByInputType(assietteInputAxis);
		lacetInputValue = InputValueByInputType(lacetInputAxis);
	}

	float InputValueByInputType (axisEnum curState)
	{
		switch(curState)
		{
		case axisEnum.QD:
			return Input.GetAxis("Horizontal");
		case axisEnum.ZS:
			return Input.GetAxis("Vertical");
		case axisEnum.MouseX:
			return Input.GetAxis("Mouse X");	//TODO convert axis raw value to mousePos
		case axisEnum.Revert_MouseX:
			return -Input.GetAxis("Mouse X");	//TODO convert axis raw value to mousePos
		case axisEnum.MouseY:
			return Input.GetAxis("Mouse Y");
		case axisEnum.Revert_MouseY:
			return -Input.GetAxis("Mouse Y");
		default:
			break;
		}
		return 0;
	}
}
