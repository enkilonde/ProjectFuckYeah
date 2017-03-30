using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobToolsNameSpace;

public class CharacterV3 : MonoBehaviour {

	[Range(0f, 1f)]
	[Header("Valeur minimale pour prendre en compte l'input pour accelerer")]
	public float accel_minSensitivity = 0.1f;

	[Range(0f, 1f)]
	[Header("Valeur minimale pour prendre en compte l'input pour faire une rotation à gauche ou à droite")]
	public float horizontalRot_minSensitivity = 0.2f;
	[Range(0f, 1f)]
	[Header("Valeur minimale pour prendre en compte l'input pour faire un boost latéral")]
	public float boost_minSensitivity = 0.2f;

	//Private fields references
	private ControllerV3 controlerSet;
	private CharacterController myController;
	//Speed
	[HideInInspector]
	public float currentFwdSpeed = 0f;
	private float currentMaxSpeed = 0f;
	public float minAltMaxSpeed = 140f;
	public float maxAltMaxSpeed = 84f;
	public float minAltAccel = 20f;
	public float maxAltAccel = 12f;
	public float deccelNoInput = 5f;
	//Lacet
	private float currentLacetSpeed = 0f;
	private float currentMaxLacetSpeed = 5f;
	public float maxLacetSpeed = 70f;
	public float lacetTransitionSpeed = 45f;
	//Altitude
	[HideInInspector]
	public float currentAltitude = 0f;
	public float minAltitude = 0f;
	public float maxAltitude = 100f;
	[HideInInspector]
	public float currentVerticalForce = 0f;
	public float maxVerticalAscentionSpeed = 25f;
	public float maxFallingSpeed = 10f;
	public float verticalSpeedTransitionSpeed = 20f;
	//Lateral boost
	private float currentLateralSpeed = 0f;
	public float maxLateralSpeed = 50f;
	public float lateralBoostAcceleration = 40f;	//TODO marche pas

	//Inertie
	public bool useInertyFeature = true;
	[HideInInspector]
	public Vector3 inertieVector = Vector3.forward;
	[Header("Transition entre la direction d'inertie actuelle vers la direction de l'avatar en angles/sec")]
	public float transitionAngleDelta = 45f;
	public float airResistance = 25f;

	//Gethit
	private bool hitSomething = false;
	[Range(0f, 0.5f)]
	public float transitionTimeToReflectVector = 0.2f;
	private float currentTimerToReflect = 0f;
	private Vector3 forcedInerty = Vector3.forward;
	private Vector3 hit_initialInetry, hit_initialForward;
	private Vector3 lastNormal = Vector3.right;
	private Vector3 hitPosition = Vector3.zero;
	public float maxSpeedRebondForce = 10f;
	public float minSpeedRebondForce = 2f;
	private float currentSpeedRebondForce = 0f;
	[Range(0f, 100f)]
	public float deccelHitPorcent = 50f;

	//Inputs translated as floats
	[HideInInspector]
	public float I_accel = 0f;
	[HideInInspector]
	public float I_lateralBoostLeft = 0f;
	[HideInInspector]
	public float I_lateralBoostRight = 0f;
	[HideInInspector]
	public float I_lateralPlayerRot = 0f;
	[HideInInspector]
	public float _verticalBoostInput = 0f;
	[HideInInspector]
	public Vector3 lateralBoostDirInput = Vector3.right;
	[HideInInspector]
	public float I_verticalBoost = 0f;

	void Start () {
		controlerSet = transform.parent.GetComponentInChildren<ControllerV3>();
		myController = GetComponent<CharacterController>();
	}
	
	void Update () {

		//Reinitialise
		Vector3 dirToMove = Vector3.zero;

		//Update Hit wall transition
		if(hitSomething)
			UpdateObstacleHitTranslation();

		//Check Inputs and assign all values in local floats to play with
		CheckInputs();

		//Rotate upon Input (LACET)
		//Update rotation speed
		currentLacetSpeed = Mathf.MoveTowards(currentLacetSpeed, maxLacetSpeed * I_lateralPlayerRot, lacetTransitionSpeed * Time.deltaTime);
		//Rotate by speed
		if(!hitSomething) transform.Rotate(Vector3.up * currentLacetSpeed * Time.deltaTime, Space.World);

		//UpdateSpeedFwd
		//Accel en fonction de l'altitude
		float _t_alti = RobToolsClass.GetNormalizedValue(currentAltitude, minAltitude, maxAltitude);
		float _tempAccel = Mathf.Lerp(minAltAccel, maxAltAccel, _t_alti);
		_tempAccel *= I_accel;		//Multiply by input
		//MaxSpeed
		currentMaxSpeed = Mathf.Lerp(minAltMaxSpeed, maxAltMaxSpeed, _t_alti);
		//CurrentSpeed
		if(I_accel != 0f)	//if input : accelerate
			currentFwdSpeed += _tempAccel * Time.deltaTime;
		else				//else : decelerate
			currentFwdSpeed -= deccelNoInput * Time.deltaTime;
		if(useInertyFeature)
		{
			float dotIF = (-Vector3.Dot(inertieVector.normalized, transform.forward) + 1f) / 2f;
			currentFwdSpeed -= (dotIF * airResistance) * Time.deltaTime;
//			print("Dot " + dotIF);
		}
		currentFwdSpeed = Mathf.Clamp(currentFwdSpeed, 0f, currentMaxSpeed); //clamp

		//Update Inertie
		dirToMove = Vector3.zero;
		if(useInertyFeature)
		{
			if(I_accel > accel_minSensitivity)
			inertieVector = Vector3.RotateTowards(inertieVector, transform.forward, Mathf.Deg2Rad * transitionAngleDelta * Time.deltaTime, 1);

			dirToMove = inertieVector * currentFwdSpeed;

		}
		else
		{
			dirToMove = transform.forward * currentFwdSpeed;
		}

		//LateralBoost
		lateralBoostDirInput = transform.right * (I_lateralBoostRight - I_lateralBoostLeft);
//		currentLateralSpeed = Mathf.MoveTowards(currentLateralSpeed, maxLateralSpeed * -_lateralBoostDir.x, lateralBoostAcceleration * Time.deltaTime);	//TODO marche pas, corriger
		//Vitesse
		currentLateralSpeed = maxLateralSpeed;
		//Apply
		dirToMove += lateralBoostDirInput * currentLateralSpeed;

		//Vertical boost/gravity
		//Get Input value
		//Inputvertical = 1 touche
		_verticalBoostInput = I_verticalBoost;
			//Inputvertical = combinaison de deux touches
//			_verticalBoostInput = Mathf.Min(I_lateralBoostLeft, I_lateralBoostRight);
//			_verticalBoostInput *= (I_lateralBoostLeft > boost_minSensitivity && I_lateralBoostRight > boost_minSensitivity) ? 1f : 0f;
		//Vertical Speed
		float _verticalSpeedMultiplier = (_verticalBoostInput > 0f) ? _verticalBoostInput : 1f;
		if(currentVerticalForce < 0 && _verticalBoostInput > 0f)	//Permet d'annuler instantanément la gravité si l'input vertical est pressé
		{
			currentVerticalForce = 0f;
		}
		currentVerticalForce = Mathf.MoveTowards(currentVerticalForce, (_verticalBoostInput > 0f) ? maxVerticalAscentionSpeed : (currentAltitude > minAltitude) ? -maxFallingSpeed : 0f, verticalSpeedTransitionSpeed * _verticalSpeedMultiplier * Time.deltaTime);
		//Apply
		float _verticalBoost = currentVerticalForce;
		dirToMove.y += _verticalBoost;
		//Clamp
//		dirToMove.y = Mathf.Clamp(currentAltitude + dirToMove.y, minAltitude, maxAltitude);


		//Apply
		myController.Move(dirToMove * Time.deltaTime);

		//Update Altitude
		transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minAltitude, maxAltitude), transform.position.z);	//C'est dégeu TODO rendre ça plus propre et + smooth (avec des repbonds et tout)
		currentAltitude = transform.position.y;

	}

	void CheckInputs()
	{
		#region accelerate
		I_accel = 0f;
		I_accel = Input.GetAxis(controlerSet.Get_AccelAxisInput());
//		print(Input.GetAxis(controlerSet.Get_AccelAxisInput()));
		print(I_accel);
		if(I_accel > accel_minSensitivity)
		{
//			print("Accelerate Axis");
		}
//		if(Input.GetButton(controlerSet.Get_AccelButtonInput()))
//		{
////			print("Accelerate Button");
//			I_accel = 1f;
//		}
		#endregion

//		if(Input.GetButtonDown(controlerSet.Get_SchockWaveInput()))
//		{
//			print("SchockWave");
//		}

		I_lateralPlayerRot = Input.GetAxis(controlerSet.Get_HorizontalRotInput());
		if(Mathf.Abs(I_lateralPlayerRot) > horizontalRot_minSensitivity)
		{
//			print("Lateral rot" + I_lateralPlayerRot);
		}
		else
		{
			I_lateralPlayerRot = 0f;
		}

		#region lateral boost
		I_lateralBoostLeft = 0f;
		I_lateralBoostRight = 0f;
//		I_lateralBoostLeft = Input.GetAxis(controlerSet.Get_LateralBoostLeftInput());
//		if(Mathf.Abs(I_lateralBoostLeft) > boost_minSensitivity)
//		{
////			print("BoostLeft" + I_lateralBoostLeft);
//		}
//		else
//		{
//			I_lateralBoostLeft = 0f;
//		}
//		print(transform.parent.name + " "+  controlerSet.Get_LatLeftBoostInput());
		if(Input.GetButton(controlerSet.Get_LatLeftBoostInput()))
		{
			I_lateralBoostLeft = 1f;
		}
//		I_lateralBoostRight = Input.GetAxis(controlerSet.Get_LateralBoostRightInput());
//		if(Mathf.Abs(I_lateralBoostRight) > boost_minSensitivity)
//		{
////			print("BoostRight" + I_lateralBoostRight);
//		}
//		else
//		{
//			I_lateralBoostRight = 0f;
//		}
		if(Input.GetButton(controlerSet.Get_LatRightBoostInput()))
		{
			I_lateralBoostRight = 1f;
		}
		#endregion

		#region vertical boost
		I_verticalBoost = Input.GetAxis(controlerSet.Get_VertcalBoostAxisInput());
		#endregion
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawRay(transform.position, transform.forward * 10);
//		Debug.DrawRay(transform.position, transform.forward * 10, Color.blue);
		Gizmos.color = Color.green;
		Debug.DrawRay(transform.position, inertieVector * 10, Color.green);
		Gizmos.color = Color.blue;
		Debug.DrawRay(hitPosition, lastNormal);
	}


	/// <summary>
	/// Updates the changes smoothly after collision.
	/// </summary>
	private void UpdateObstacleHitTranslation()
	{
		if(currentTimerToReflect > 0f)
		{
			currentTimerToReflect-= Time.deltaTime;
			float _t = currentTimerToReflect.Remap(0f, transitionTimeToReflectVector, 0f, 1f);
			inertieVector = Vector3.Lerp(forcedInerty, hit_initialInetry, _t);		//Transition vers la nouvelle inertie
			transform.forward = Vector3.Lerp(forcedInerty, hit_initialForward, _t);		//Transition du forward
//			transform.position = Vector3.Lerp(transform.position + lastNormal * maxSpeedRebondForce, hitPosition, _t);	//Rebond selon la normale
			transform.position = Vector3.Lerp(transform.position + forcedInerty * currentSpeedRebondForce, hitPosition, _t);	//Rebond vers la nouvelle inertie
			if(currentTimerToReflect <= 0f)
				hitSomething = false;
		}

	}

	/// <summary>
	/// Gets the current rebond force.
	/// </summary>
	/// <returns>The current rebond force.</returns>
	float GetCurrentRebondForce()
	{
		return Mathf.Lerp(minSpeedRebondForce, maxSpeedRebondForce, RobToolsClass.GetNormalizedValue(currentFwdSpeed, 0f, minAltMaxSpeed));
	}

	/// <summary>
	/// When avatar hit obstacle.
	/// </summary>
	/// <param name="_newInertyDirection">New inerty direction.</param>
	/// <param name="normalSurf">Normal surf.</param>
	/// <param name="hitPos">Hit position.</param>
	public void ObstacleHit(Vector3 _newInertyDirection, Vector3 normalSurf, Vector3 hitPos)
	{
		if(hitSomething)
			return;
		hitSomething = true;
		currentTimerToReflect = transitionTimeToReflectVector;	//Set timer
		forcedInerty = _newInertyDirection;	//Direction d'inertie cible

		//Force de rebond dépendante de la vitesse
		currentSpeedRebondForce = GetCurrentRebondForce();

		//Ralentir
		currentFwdSpeed -= RobToolsClass.GetValueFromPercent(deccelHitPorcent, currentFwdSpeed);

		//Stock pour pouvoir faire un lerp
		lastNormal = normalSurf;	//Normale de la surface percutée
		hitPosition = hitPos;	//Point d'impact
		hit_initialForward = transform.forward;		//Le forward avant impact
		hit_initialInetry = inertieVector;		//Inertie avant impact

		//Instantané
//		inertieVector = _newInertyDirection;
//		transform.forward = _newInertyDirection;
	}

}
