using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RobToolsNameSpace;

public class UpdateUIState : MonoBehaviour {

	public CharacterV3 CV3;
	public Scrollbar heightScroll;
	public Scrollbar speedScroll;

	void Start () {
		if(!CV3 || !heightScroll || !speedScroll)
			print("Variables not set in Update UI State (t'occupes pas de ce message d'erreur, je m'en occupe)");
	}
	
	void Update () {

		if(!CV3 || !heightScroll || !speedScroll)
			return;

        heightScroll.size = CV3.getHeightRatio();

		speedScroll.size = RobToolsClass.GetNormalizedValue(CV3.currentFwdSpeed, 0f, CV3.minAltMaxSpeed);

	}
}
