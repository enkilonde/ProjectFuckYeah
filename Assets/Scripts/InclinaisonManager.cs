using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobToolsNameSpace;

public class InclinaisonManager : MonoBehaviour {

	[Header("Feedback inclinaison")]

	public float rollAngle_lateralBoost = 45f;
	public float rollAngle_lateralRotation = 45f;
	public float pitchAngle = 20f;
	public float degPerSecond = 40f;

	CharacterV3 characterv3;

	void Start () {
		characterv3 = transform.parent.GetComponent<CharacterV3>();
	}

	void Update () {
	
//		float ternarydoesnotwork = (characterv3.currentVerticalForce > 0f) ? characterv3._verticalBoostInput : -1f;
		float _pitchNormalized = characterv3.currentVerticalForce.Remap(-characterv3.maxFallingSpeed, characterv3.maxVerticalAscentionSpeed, -1f, 1f); // RobToolsClass.MappedRangeValue(characterv3.currentVerticalForce, -characterv3.maxFallingSpeed, characterv3.maxVerticalAscentionSpeed, -1f, 1f);
        
        _pitchNormalized = characterv3.dirToMove.y.Remap(-characterv3.maxFallingSpeed, characterv3.maxVerticalAscentionSpeed, -1 * characterv3.maxFallingSpeed / characterv3.maxVerticalAscentionSpeed, 1);






        float _rollAngleTotal = rollAngle_lateralBoost * (characterv3.I_lateralBoostRight - characterv3.I_lateralBoostLeft);
		_rollAngleTotal += rollAngle_lateralRotation * -characterv3.I_lateralPlayerRot;
		transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(-pitchAngle * _pitchNormalized,  0f, _rollAngleTotal), degPerSecond * Time.deltaTime);

//		print((characterv3.currentVerticalForce > 0f) ? characterv3._verticalBoostInput : -1f);
	}


}
