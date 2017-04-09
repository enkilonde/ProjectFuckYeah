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
//	[HideInInspector]
	public float currentFwdSpeed = 0f;
	private float currentMaxSpeed = 0f;
	public float minAltMaxSpeed = 140f;
	public float maxAltMaxSpeed = 84f;
	public float minAltAccel = 20f;
	public float maxAltAccel = 12f;
	public float deccelNoInput = 5f;

	//Lacet
	private float currentLacetSpeed = 0f;
//	private float currentMaxLacetSpeed = 5f;
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
	public bool straffUseInerty = true;

	
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

	//Boost
	[HideInInspector]
	public float currentBoostAmountLeft = 0f;
	public float timeToReload = 10f;
	public float timeToUnload = 5f;
	private float currentMaxSpeedWhileBoost = 0f;
	public float maxSpeedwhileBoost = 200f;
	public float maxBoostSpeed_DeccelerationSpeed = 50f;	//Amount of speed plafond lost in a second
	public float accelerationSpeedWhileBoost = 2f;
	public float minBoostRecquired = 20f;	//en %
	public float lateralBoostAcceleration = 25f;
	public float lateralBoostDecceleration = 25f;

	//Score
	private ChasingState chasingStateScript;
	[HideInInspector]
	public float currentScore = 0f;
	public float speedScoreGain = 100f;
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

    private float gravityImpactOnAcceleration = 0.5f;



//	[HideInInspector]
	public float I_forwardBoost = 0f;

	private float _t_boostLoad = 0f;
	private bool isInBoost = false;
	private float currentBoostSpeed = 0f;

	private float _lastLatoostDirUsed = 1f;

    private float effectiveSpeedFactor = 0;
    private float effectiveHeightFactor;

	Vector3 lateralInertie;
    [HideInInspector] public Vector3 dirToMove;


    void Start () {
		controlerSet = transform.parent.GetComponentInChildren<ControllerV3>();
		myController = GetComponent<CharacterController>();
		chasingStateScript = GetComponent<ChasingState>();

		//Empeche unity de mettre une autre valeur (vu que les public hideininspector semblent ne pas se réinitialiser sur le bouton play)
		currentFwdSpeed = 0f;
		currentAltitude = 0f;
		currentVerticalForce = 0f;
		inertieVector = transform.forward;
		currentScore = 0f;
	}

	private float noBoostTimer = 0f;
	public float timeTowaitForBoostReload = 2f;

	void Update () {

        //Maj score
        //if (chasingStateScript.currentChaseState == ChasingState.ChaseStates.Target)
        //    currentScore += Time.deltaTime * speedScoreGain;

        //Reinitialise
        dirToMove = Vector3.zero;

        computeDirectionHorizontale();
        computeDirectionVerticale();

        //Apply
        myController.Move(dirToMove * Time.deltaTime);

        //Update Altitude
		currentAltitude = transform.position.y;

	}

    void computeDirectionHorizontale()
    {
        //Boost

        //Activation du boost
        //		if(!isInBoost)
        //		{
        //			noBoostTimer += Time.deltaTime;
        //			//BoostCooldown and enough boost left
        //			if(noBoostTimer > timeTowaitForBoostReload && currentBoostAmountLeft > 0.2f)
        //			{
        //				//Boost Input
        //				if(I_forwardBoost > 0.5f)
        //				{
        //					isInBoost = true;
        //					currentBoostSpeed = 20f;
        //					currentBoostAmountLeft -= 0.2f;
        //				}
        //				
        //			}
        //		}

        currentBoostAmountLeft = RobToolsClass.GetNormalizedValue(_t_boostLoad, 0f, 1f); //Maj la jauge
        I_forwardBoost *= (currentBoostAmountLeft > 0.1f) ? 1f : 0f;    //Reste t'il du boost dans la jauge
        _t_boostLoad += ((I_forwardBoost > 0.5f) ? -1f / timeToUnload : 1f / timeToReload) * Time.deltaTime;    //Are the jauge being used ?
        _t_boostLoad = Mathf.Clamp(_t_boostLoad, -1f, 1f);


        //Update Hit wall transition
        if (hitSomething)
            UpdateObstacleHitTranslation();

        if (GameState.curGameState == GameState.AllGameStates.Play || true)
            CheckInputs();      //Check Inputs and assign all values in local floats to play with	//TODO les inputs sont remis à 0 plutot qu elaissé dans leur état actuel

        //Rotate upon Input (LACET)
        //Update rotation speed
        currentLacetSpeed = Mathf.MoveTowards(currentLacetSpeed, maxLacetSpeed * I_lateralPlayerRot, lacetTransitionSpeed * Time.deltaTime);
        //Rotate by speed
        if (!hitSomething) transform.Rotate(Vector3.up * currentLacetSpeed * Time.deltaTime, Space.World);

        //UpdateSpeedFwd
        //Accel en fonction de l'altitude

        float _t_alti = RobToolsClass.GetNormalizedValue(currentAltitude, minAltitude, maxAltitude);
        float _tempAccel = Mathf.Lerp(minAltAccel, maxAltAccel, _t_alti);

        if (I_forwardBoost > 0.5f)
            _tempAccel *= accelerationSpeedWhileBoost;//If boost, we accelerate fully
        else
            _tempAccel *= I_accel;//If not boost, so we use the accelerate basic axis //Multiply by input


        //MaxSpeed
        currentMaxSpeed = Mathf.Lerp(minAltMaxSpeed, maxAltMaxSpeed, _t_alti);


        //MaxSpeedBoost
        if (I_forwardBoost > 0.5f)
            currentMaxSpeedWhileBoost = maxSpeedwhileBoost;//If input, the max absolute speed is instantanely equal to a new maximum
        else
            currentMaxSpeedWhileBoost = Mathf.MoveTowards(currentMaxSpeedWhileBoost, 0f, maxBoostSpeed_DeccelerationSpeed * Time.deltaTime);//If no more boost, smoothly decrease max speed


        currentMaxSpeed += currentMaxSpeedWhileBoost;


        //CurrentSpeed
        if (I_accel != 0f || I_forwardBoost != 0f)  //if input : accelerate
            currentFwdSpeed += (_tempAccel * (1 - gravityImpactOnAcceleration * Mathf.Sign(currentVerticalForce)) ) * Time.deltaTime;
        else                //else : decelerate
            currentFwdSpeed -= deccelNoInput * Time.deltaTime;

        float dotIF = (-Vector3.Dot(inertieVector.normalized, transform.forward) + 1f) / 2f;
        currentFwdSpeed -= (dotIF * airResistance) * Time.deltaTime;


        currentFwdSpeed = Mathf.Clamp(currentFwdSpeed, 0f, currentMaxSpeed); //clamp

        //Update Inertie
        dirToMove = Vector3.zero;

        //Inertie
        if (I_accel > accel_minSensitivity || I_forwardBoost > 0.5f)
        {
            inertieVector = Vector3.Lerp(inertieVector, transform.forward, Time.deltaTime * transitionAngleDelta); // bon feeling sur les demis tours, mais bof quand on tourne
        }

        dirToMove = inertieVector * currentFwdSpeed;

        #region enki : J'ai viré les boost latéraux pour voir si quelqu'un s'en rend compte
        /*
//LateralBoost
if (straffUseInerty)
{
    //input
    //			lateralBoostDirInput.x = I_lateralBoostRight - I_lateralBoostLeft;
    if (I_lateralBoostRight - I_lateralBoostLeft > 0.2f)
    {
        _lastLatoostDirUsed = 1f;
        lateralBoostDirInput.x = 1f;
    }
    else if (I_lateralBoostRight - I_lateralBoostLeft < -0.2f)
    {
        _lastLatoostDirUsed = -1f;
        lateralBoostDirInput.x = -1f;
    }
    else
    {
        lateralBoostDirInput.x = 0f;
    }
    //Vitesse
    //CurrentSpeed
    //			currentLateralSpeed = maxLateralSpeed;
    if (lateralBoostDirInput.x > 0.2f || lateralBoostDirInput.x < -0.2f)    //if input : accelerate
        currentLateralSpeed += lateralBoostAcceleration * Time.deltaTime;
    else                //else : decelerate
        currentLateralSpeed -= lateralBoostDecceleration * Time.deltaTime;
    currentLateralSpeed = Mathf.Clamp(currentLateralSpeed, 0f, maxLateralSpeed);
    //apply
    lateralInertie = (transform.right * _lastLatoostDirUsed);
    dirToMove += lateralInertie * currentLateralSpeed;
    Debug.DrawRay(transform.position, lateralInertie * currentLateralSpeed, Color.red);
}
else
{
    //input
    lateralBoostDirInput = transform.right * (I_lateralBoostRight - I_lateralBoostLeft);
    //Vitesse
    currentLateralSpeed = maxLateralSpeed;
    //Apply
    dirToMove += lateralBoostDirInput * currentLateralSpeed;
}
*/
        #endregion

    }

    void computeDirectionVerticale()
    {




        

        float speedfactor = Mathf.InverseLerp(maxAltMaxSpeed, minAltMaxSpeed, currentFwdSpeed);

        effectiveSpeedFactor = Mathf.Lerp(effectiveSpeedFactor, speedfactor, Time.deltaTime);

        float heightFactor = Mathf.InverseLerp(0, maxAltitude, currentAltitude);

        float effectifMaxHeight = maxAltitude * speedfactor;

        float oldVerticalForce = currentVerticalForce;

        if(currentAltitude < effectifMaxHeight)
            _verticalBoostInput = Mathf.Lerp(_verticalBoostInput, I_verticalBoost, Time.deltaTime * 50);
        else
            _verticalBoostInput = Mathf.Lerp(_verticalBoostInput, 0, Time.deltaTime * 50);





        if (I_verticalBoost == 1)
            currentVerticalForce = (maxVerticalAscentionSpeed) * (1 - heightFactor) * _verticalBoostInput;
        else
            currentVerticalForce = (maxFallingSpeed) * _verticalBoostInput.Remap(0, 1, -1, 1);


        if (currentAltitude <= minAltitude && currentVerticalForce < 0)
        {
            currentVerticalForce = 0;
        }

        float _verticalBoost = Mathf.Lerp(oldVerticalForce, currentVerticalForce, Time.deltaTime);
        currentVerticalForce = _verticalBoost;
        dirToMove.y += _verticalBoost;
        
        //Vector3 Hspeed = new Vector3(dirToMove.x, 0, dirToMove.z);
        //currentFwdSpeed -= 50 * Time.deltaTime * effectiveHeightFactor;



        //Explosion
        //dirToMove += explVector;
        //Attenuate
        explVector = Vector3.MoveTowards(explVector, Vector3.zero, 20f * Time.deltaTime);

    }

    void CheckInputs()
	{
		#region accelerate
		I_accel = 0f;
//		I_accel = Input.GetAxis("1_RT_Axis");	//Meme entrer la valeur en dur ça ne marche pas en build oO
		I_accel = Input.GetAxisRaw(controlerSet.Get_AccelAxisInput());
        //print(Input.GetAxis(controlerSet.Get_AccelAxisInput()));
        //print("Player " + transform.parent.GetComponentInChildren<ControllerV3>().playerNumero + ", speed : " + I_accel);
        if (I_accel > accel_minSensitivity)
		{
//			print(controlerSet.Get_AccelAxisInput());
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

		I_lateralPlayerRot = Input.GetAxisRaw(controlerSet.Get_HorizontalRotInput());
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
		I_verticalBoost = Input.GetAxisRaw(controlerSet.Get_VertcalBoostAxisInput());
		#endregion

		#region boost
		I_forwardBoost = Input.GetButton(controlerSet.Get_ForwardBoostInput()) ? 1f : 0f;
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

	Vector3 explVector = Vector3.zero;

	public void ImpulseInfluence(Vector3 _dirAndForce)
	{
		explVector = _dirAndForce;
//		myController.Move(_dirAndForce * Time.deltaTime);
//		print("imp");
	}

	public void RefillBoost()
	{
		currentBoostAmountLeft = 1f;
		_t_boostLoad = 1f;
	}

}
