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
    private new Rigidbody rigidbody;

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

	public float maxLateralSpeed = 50f;
	public bool straffUseInerty = true;

	//Inertie
	public bool useInertyFeature = true;
	[HideInInspector]
	public Vector3 inertieVector = Vector3.forward;
	[Header("Transition entre la direction d'inertie actuelle vers la direction de l'avatar en angles/sec")]
	public float transitionAngleDelta = 45f;
	public float airResistance = 25f;

	//Gethit

	[Range(0f, 0.5f)]
	public float transitionTimeToReflectVector = 0.2f;


	private Vector3 hit_initialInetry, hit_initialForward;
	private Vector3 lastNormal = Vector3.right;
	private Vector3 hitPosition = Vector3.zero;
	public float maxSpeedRebondForce = 10f;
	public float minSpeedRebondForce = 2f;
	[Range(0f, 100f)]
	public float deccelHitPorcent = 50f;

	//Boost
	[HideInInspector]
    public float I_forwardBoost = 0f;
    public float currentBoostAmountLeft = 0f;
    private float boostSpeedMultiplier = 5;
    public float boostGainedPerSeconds = 0.1f;

	//Score
	[HideInInspector]
	public float currentScore = 0f;
	public float speedScoreGain = 100f;


    public bool inputsSet = false;
    public bool useKeyboard = false;
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


	//	[HideInInspector]



    //private float noBoostTimer = 0f;

    private float gravityImpactOnAcceleration = 0.5f;

    public Vector3 dirToMove;

    private Vector3 explosionForce;

    private Coroutine rotateOnCollisionCoroutine;

    Vector3 explVector = Vector3.zero;

    Vector3 bumbVector = Vector3.zero;

    RaycastHit downRaycast;

    FlagBehaviour flagBehavoirScript; //  /!\ ne marche que si il n'y a que 1 seul flag


    public List<Vector3> previousPos = new List<Vector3>(30);
    float registerPosLastDistance = 0;
    Vector3 previousP;



    void Start () {
		controlerSet = transform.parent.GetComponentInChildren<ControllerV3>();
        flagBehavoirScript = FindObjectOfType<FlagBehaviour>();
        rigidbody = GetComponent<Rigidbody>();

        //Empeche unity de mettre une autre valeur (vu que les public hideininspector semblent ne pas se réinitialiser sur le bouton play)
        currentFwdSpeed = 0f;
		currentAltitude = 0f;
		currentVerticalForce = 0f;
		inertieVector = transform.forward;
		currentScore = 0f;
        dirToMove = Vector3.zero;
	}


    void Update ()
    { 

        if (inputsSet)
            CheckInputs();      //Check Inputs and assign all values in local floats to play with	//TODO les inputs sont remis à 0 plutot qu elaissé dans leur état actuel
        else if (useKeyboard)
            CheckInputs(); // check inputs for keyboard
    }


    private void FixedUpdate()
    {

        //Debug.Log("displacement = " + (transform.position - previousP).magnitude + " velocity : " + dirToMove.magnitude);

        if ( (registerPosLastDistance += (transform.position - previousP).magnitude ) >= 10)
        {
            RegisterPos();
            registerPosLastDistance = 0;
        }

        dirToMove = Vector3.zero;

        raycastDown();
        computeDirectionHorizontale();
        computeDirectionVerticale();

        dirToMove += explVector; // on ajoute la force d'explosion
        dirToMove += bumbVector;

        bumbVector *= 0.9f;
        if (bumbVector.magnitude < 0.1f) bumbVector = Vector3.zero;

        //Apply
        rigidbody.velocity = dirToMove;


        //Update Altitude
        currentAltitude = transform.position.y;

        previousP = transform.position;
    }

    void computeDirectionHorizontale()
    {
        I_forwardBoost *= (currentBoostAmountLeft > 0.1f) ? 1f : 0f;    //Reste t'il du boost dans la jauge
        currentBoostAmountLeft = Mathf.Clamp(currentBoostAmountLeft +=  boostGainedPerSeconds * ((IsInTrail())?10:1) * Time.fixedDeltaTime, 0, 1);

        //Rotate upon Input (LACET)
        //Update rotation speed
        currentLacetSpeed = Mathf.MoveTowards(currentLacetSpeed, maxLacetSpeed * I_lateralPlayerRot, lacetTransitionSpeed * Time.fixedDeltaTime);
        //Rotate by speed
        transform.Rotate(Vector3.up * currentLacetSpeed * Time.fixedDeltaTime, Space.World);

        //Accel en fonction de l'altitude
        float _t_alti = RobToolsClass.GetNormalizedValue(currentAltitude, minAltitude, maxAltitude);
        float _tempAccel = Mathf.Lerp(minAltAccel, maxAltAccel, _t_alti);

        _tempAccel *= I_accel;//If not boost, so we use the accelerate basic axis //Multiply by input


        //MaxSpeed
        float maxSpeed = Mathf.Lerp(minAltMaxSpeed, maxAltMaxSpeed, _t_alti);



        //Do we use the boost?
        if(I_forwardBoost!= 0)
        {
            _tempAccel *= boostSpeedMultiplier;
            currentBoostAmountLeft -=  Time.fixedDeltaTime;
        }

        currentMaxSpeed = Mathf.Lerp(currentMaxSpeed, (I_forwardBoost == 0)?maxSpeed : maxSpeed * 2, Time.fixedDeltaTime);

        //CurrentSpeed
        if (I_accel > accel_minSensitivity || I_forwardBoost != 0f)  //if input : accelerate
            currentFwdSpeed += (_tempAccel * (1 - gravityImpactOnAcceleration * Mathf.Sign(currentVerticalForce))) * Time.fixedDeltaTime;
        else                //else : decelerate
            currentFwdSpeed -= deccelNoInput * Time.fixedDeltaTime;



        float dotIF = (-Vector3.Dot(inertieVector.normalized, transform.forward) + 1f) / 2f;
        currentFwdSpeed -= (dotIF * airResistance) * Time.fixedDeltaTime;


        currentFwdSpeed = Mathf.Clamp(currentFwdSpeed, 0f, currentMaxSpeed); //clamp

        //Update Inertie
        dirToMove = Vector3.zero;

        //Inertie
        if (I_accel > accel_minSensitivity || I_forwardBoost > 0.5f)
        {
            inertieVector = Vector3.Lerp(inertieVector, transform.forward, Time.fixedDeltaTime * transitionAngleDelta); // bon feeling sur les demis tours, mais bof quand on tourne
        }


        dirToMove = inertieVector * currentFwdSpeed;
    }

    void computeDirectionVerticale()
    {
        float AdjustTime = 1;

        float speedfactor = Mathf.InverseLerp(maxAltMaxSpeed, minAltMaxSpeed, currentFwdSpeed);

        float heightFactor = Mathf.InverseLerp(minAltitude, maxAltitude, currentAltitude);

        float effectifMaxHeight = maxAltitude * speedfactor;

        float oldVerticalForce = currentVerticalForce;

        if (currentAltitude <= effectifMaxHeight)
            _verticalBoostInput = Mathf.Lerp(_verticalBoostInput, I_verticalBoost, Time.fixedDeltaTime * 50);
        else
            _verticalBoostInput = Mathf.Lerp(_verticalBoostInput, 0, Time.fixedDeltaTime * 50);


        if (I_verticalBoost > 0)
            currentVerticalForce = (maxVerticalAscentionSpeed) * (1 - heightFactor) * _verticalBoostInput;
        else
            currentVerticalForce = (maxFallingSpeed) * _verticalBoostInput.Remap(0, 1, -1, 1);


        if (currentAltitude <= minAltitude && currentVerticalForce < 0)
        {
            currentVerticalForce = 0;
        }

        if(currentAltitude < minAltitude && I_verticalBoost == 0 )
        {
            currentVerticalForce = minAltitude - currentAltitude;
            AdjustTime = 20;
        }

        if (downRaycast.transform != null && Vector3.Distance(downRaycast.point, transform.position) < 5)
        {
            currentVerticalForce = 5 - Vector3.Distance(downRaycast.point, transform.position);
            AdjustTime = 20;
        }

        float _verticalBoost = Mathf.Lerp(oldVerticalForce, currentVerticalForce, Time.fixedDeltaTime * AdjustTime);
        currentVerticalForce = _verticalBoost;
        dirToMove.y += _verticalBoost;
    }

    void raycastDown()
    {
        Ray r = new Ray(transform.position, Vector3.down);
        if( Physics.Raycast(r, out downRaycast, 1000))
        {
            //Debug.Log(Vector3.Distance(downRaycast.point, transform.position), downRaycast.transform.gameObject);
        }
        Debug.DrawRay(transform.position, Vector3.down, Color.black);


    }

    public void ReceiveExplosionForce(Vector3 direction)
    {
        StartCoroutine(explosionForceAttenuate(direction * 10));
    }

    IEnumerator explosionForceAttenuate(Vector3 direction)
    {

        while(direction.magnitude > 1)
        {
            explVector = direction;
            direction *= 0.9f; // TODO - mettre ça dans une variable pour pouvoir gérer l'atténuation
            yield return null;
        }
        explVector = Vector3.zero;

    }

    void CheckInputs()
	{

		#region accelerate
		I_accel = 0f;
		//		I_accel = Input.GetAxis("1_RT_Axis");	//Meme entrer la valeur en dur ça ne marche pas en build oO
		I_accel = Input.GetAxisRaw(controlerSet.Get_AccelAxisInput());
		//		print(Input.GetAxis(controlerSet.Get_AccelAxisInput()));
		//print("Player " + transform.parent.GetComponentInChildren<ControllerV3>().playerNumero + ", speed : " + I_accel);
		if(I_accel > accel_minSensitivity)
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

        #region rotation
        I_lateralPlayerRot = Input.GetAxisRaw(controlerSet.Get_HorizontalRotInput());
        if (Mathf.Abs(I_lateralPlayerRot) < horizontalRot_minSensitivity)
            I_lateralPlayerRot = 0f; 
        #endregion

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

    void CheckInputsKeyboard()
    {
        #region accelerate
        I_accel = 0f;
        //print(GetKeyboardInputName(controlerSet.Get_AccelAxisInput()));
        I_accel = Input.GetAxisRaw(GetKeyboardInputName(controlerSet.Get_AccelAxisInput()));
        #endregion

        #region rotation
        I_lateralPlayerRot = Input.GetAxisRaw(GetKeyboardInputName(controlerSet.Get_HorizontalRotInput()));
        if (Mathf.Abs(I_lateralPlayerRot) < horizontalRot_minSensitivity)
            I_lateralPlayerRot = 0f;
        #endregion



        #region vertical boost
        I_verticalBoost = Input.GetAxisRaw(GetKeyboardInputName(controlerSet.Get_VertcalBoostAxisInput()));
        #endregion

        #region boost
        I_forwardBoost = Input.GetButton(GetKeyboardInputName(controlerSet.Get_ForwardBoostInput())) ? 1f : 0f;
        #endregion
    }

    string GetKeyboardInputName(string inputName)
    {
        inputName = inputName.Remove(0, 1);
        inputName = inputName.Insert(0, "K");
        return inputName;
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

    IEnumerator rotateFromCollision(Vector3 targetDirection)
    {

        float rotSpeed = 10;
        float addedRot = 0.01f;

        while (Vector3.Angle(transform.forward, targetDirection) > 1f)
        {
            inertieVector = Vector3.Lerp(inertieVector, targetDirection, Time.deltaTime * rotSpeed + addedRot);

            transform.LookAt(transform.position + Vector3.Lerp(transform.forward, targetDirection, Time.deltaTime * rotSpeed + addedRot));
            yield return null;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {


        Vector3 reflect = Vector3.Reflect(inertieVector, collision.contacts[0].normal);
        Vector3 newVector = inertieVector;


        if(Vector3.Angle(-inertieVector, reflect) < 30 && flagBehavoirScript.targetPlayer == this)
        {
            //Crash
            flagBehavoirScript.Drop();
        }

        newVector = Vector3.Cross(collision.contacts[0].normal, Vector3.up);
        if (Vector3.Angle(inertieVector, newVector) > 90) newVector = -newVector;

        newVector = Vector3.RotateTowards(newVector, collision.contacts[0].normal, 5 * Mathf.Deg2Rad, 0.0f);

        bumbVector = reflect * 100;

        if (rotateOnCollisionCoroutine != null)
            StopCoroutine(rotateOnCollisionCoroutine);

        rotateOnCollisionCoroutine = StartCoroutine(rotateFromCollision(newVector));
    }

    public void RefillBoost()
	{
		currentBoostAmountLeft = 1f;
	}

    public bool IsInTrail()
    {
        if (flagBehavoirScript.targetPlayer == this || flagBehavoirScript.targetPlayer == null) return false;

        for (int i = 0; i < flagBehavoirScript.targetPlayer.previousPos.Count; i++)
        {
            if (Vector3.Distance(transform.position, flagBehavoirScript.targetPlayer.previousPos[i]) < 10) return true;
        }


        return false;
    }

    public void RegisterPos()
    {
        if (previousPos.Count >= previousPos.Capacity) previousPos.RemoveAt(0);

        previousPos.Add(transform.position);
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < previousPos.Count; i++)
        {
            Gizmos.color = new Color(0, 1, 0, 0.2f);
            Gizmos.DrawSphere(previousPos[i], 10);
        }
    }
}
