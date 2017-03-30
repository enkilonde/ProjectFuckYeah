using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RobToolsNameSpace;

public class UIManager : MonoBehaviour {

	CharacterV2 characterScript;
	Slider SpeedSlider;
	Slider embraySignIndicator;	//Indique ou se trouve la possibilité d'embraillage
	Text speedtext;

	void Awake () {
		characterScript = GameObject.Find("Character").GetComponent<CharacterV2>();
		SpeedSlider = transform.Find("SpeedSlider").GetComponent<Slider>();
		embraySignIndicator = transform.Find("SpeedSliderEmbrayIndicator").GetComponent<Slider>();
		speedtext = transform.Find("SpeedText").GetComponentInChildren<Text>();
	}
	
	void Update () {

		//Slider
		float normalizedValue = RobToolsClass.GetNormalizedValue(characterScript.currentMoveSpeed, 0, characterScript.currentMaxSpeed);
		SpeedSlider.value = normalizedValue;

		//embray indicator
		normalizedValue = RobToolsClass.GetNormalizedValue(characterScript.embraPercent, 0, 100);
		embraySignIndicator.value = normalizedValue;

		//TextBox
		speedtext.text = "Speed : " + Mathf.Round(characterScript.currentMoveSpeed).ToString();	//TODO Comment on fait pour réduire le nombre de chiffres après la virgule mais en garder quand même ?

	}
}
