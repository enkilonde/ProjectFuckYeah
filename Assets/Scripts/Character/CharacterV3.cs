﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobToolsNameSpace;

public class CharacterV3 : MonoBehaviour {

	//Private fields references
	[HideInInspector]public ControllerV3 controlerSet;
    [HideInInspector] public new Rigidbody rigidbody;

    //Speed
    //	[HideInInspector]
    public float currentFwdSpeed = 0f;
	private float currentMaxSpeed = 0f;
	public float minAltMaxSpeed = 140f;
	public float maxAltMaxSpeed = 84f;
	public float minAltAccel = 20f;
	public float maxAltAccel = 12f;
	public float deccelNoInput = 20f;
    public AnimationCurve accelBySpeed;

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
	[HideInInspector]
	public Vector3 inertieVector = Vector3.forward;
	[Header("Transition entre la direction d'inertie actuelle vers la direction de l'avatar en angles/sec")]
	public float transitionAngleDelta = 45f;
	public float airResistance = 25f;
    [HideInInspector] public Vector3? CurrentVelocity = null;

	//Gethit

	[Range(0f, 0.5f)]
	public float transitionTimeToReflectVector = 0.2f;


	public float maxSpeedRebondForce = 10f;
	public float minSpeedRebondForce = 2f;
	[Range(0f, 100f)]
	public float deccelHitPorcent = 50f;

	//Boost
    [Header("Boost")]
    public float I_forwardBoost = 0f;
    public float previous_I_forwardBoost = 0;
    public float currentBoostAmountLeft = 0f;
    public float boostSpeedMultiplier = 5;
    public float boostGainedPerSeconds = 0.1f;
    public float boostUsedPerSeconds = 0.5f;

    //Score
    [Header("Score")]
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

    private float gravityImpactOnAcceleration = 1f;

    public Vector3 dirToMove;

    private Vector3 explosionForce;

    private Coroutine rotateOnCollisionCoroutine;

    Vector3 explVector = Vector3.zero;

    Vector3 bumbVector = Vector3.zero;

    RaycastHit downRaycast;

    [HideInInspector]public FlagBehaviour flagBehavoirScript; //  /!\ ne marche que si il n'y a que 1 seul flag

    Vector3 collisionRotationVector;

    public GameObject ImpactFx;

    public void init () {
		controlerSet = transform.parent.GetComponentInChildren<ControllerV3>();
        flagBehavoirScript = FindObjectOfType<FlagBehaviour>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        //Empeche unity de mettre une autre valeur (vu que les public hideininspector semblent ne pas se réinitialiser sur le bouton play)
        currentFwdSpeed = 0f;
		currentAltitude = 0f;
		currentVerticalForce = 0f;
		inertieVector = transform.forward;
		currentScore = 0f;
        dirToMove = Vector3.zero;

        CameraV1 cam =  transform.parent.GetComponentInChildren<CameraV1>();
        if(cam == null)
        {
            cam = GetComponentInChildren<CameraV1>();
        }
        cam.Init();

	}

    void Update ()
    {
        if (GameManager.isPaused()) return;

        if (inputsSet)
            CheckInputs();      //Check Inputs and assign all values in local floats to play with	//TODO les inputs sont remis à 0 plutot qu elaissé dans leur état actuel
        else if (useKeyboard)
            CheckInputs(); // check inputs for keyboard
    }

    private void FixedUpdate()
    {
        

        if (!PlayerManager.loadingEnded) return;

        if (GameManager.isPaused())
        {
            if (CurrentVelocity == null)
                CurrentVelocity = rigidbody.velocity;
            rigidbody.velocity = Vector3.zero;
            return;
        }
        else if(CurrentVelocity != null)
        {
            rigidbody.velocity = (Vector3)CurrentVelocity;
            CurrentVelocity = null;
        }

        //Debug.Log("displacement = " + (transform.position - previousP).magnitude + " velocity : " + dirToMove.magnitude);

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
    }



    void computeDirectionHorizontale()
    {
        I_forwardBoost *= ((previous_I_forwardBoost == 1 || currentBoostAmountLeft > 0.25f) && currentBoostAmountLeft > 0) ? 1f : 0f;    //Reste t'il du boost dans la jauge
        if (previous_I_forwardBoost == 0 && I_forwardBoost == 1) OnStartBoost();

        previous_I_forwardBoost = I_forwardBoost;
        currentBoostAmountLeft = Mathf.Clamp(currentBoostAmountLeft +=  boostGainedPerSeconds * ((IsInTrail())?10:1) * Time.fixedDeltaTime, 0, 1);

        //Rotate upon Input (LACET)
        //Update rotation speed
        currentLacetSpeed = Mathf.Lerp(currentLacetSpeed, maxLacetSpeed * I_lateralPlayerRot * ((I_accel == 0) ? 1.5f : 1), lacetTransitionSpeed * Time.fixedDeltaTime);
        //Rotate by speed
        transform.Rotate(Vector3.up * currentLacetSpeed * Time.fixedDeltaTime, Space.World);

        //Accel en fonction de l'altitude
        float _t_alti = getHeightRatio();
        if (Vector3.Distance(downRaycast.point, transform.position) < 5) _t_alti = 0;

        float _tempAccel = Mathf.Lerp(minAltAccel, maxAltAccel, _t_alti);

        _tempAccel *= I_accel;//If not boost, so we use the accelerate basic axis //Multiply by input


        //MaxSpeed
        float maxSpeed = Mathf.Lerp(minAltMaxSpeed, maxAltMaxSpeed, _t_alti);



        //Do we use the boost?
        if(I_forwardBoost != 0)
        {
            _tempAccel *= boostSpeedMultiplier;
            currentBoostAmountLeft -=  Time.fixedDeltaTime;
        }

        currentMaxSpeed = Mathf.Lerp(currentMaxSpeed, (I_forwardBoost == 0)?maxSpeed : maxSpeed * 2, Time.fixedDeltaTime);
        int isAscending = (I_verticalBoost != 0)?0:-1;
        if (IsOnGround()) isAscending = 0;
        //CurrentSpeed
        if (I_accel != 0 || I_forwardBoost != 0f)  //if input : accelerate
            currentFwdSpeed += (_tempAccel * (1 - gravityImpactOnAcceleration * isAscending)) * Time.fixedDeltaTime * accelBySpeed.Evaluate(getSpeedRatio());
        else                //else : decelerate
            currentFwdSpeed -= deccelNoInput * Time.fixedDeltaTime;



        float dotIF = (-Vector3.Dot(inertieVector.normalized, transform.forward) + 1f) / 2f;
        currentFwdSpeed -= (dotIF * airResistance) * Time.fixedDeltaTime;


        currentFwdSpeed = Mathf.Clamp(currentFwdSpeed, 0f, currentMaxSpeed); //clamp

        //Update Inertie
        dirToMove = Vector3.zero;

        //Inertie
        if (I_accel != 0 || I_forwardBoost > 0.5f)
        {
            inertieVector = Vector3.Lerp(inertieVector, transform.forward, Time.fixedDeltaTime * transitionAngleDelta); // bon feeling sur les demis tours, mais bof quand on tourne
        }



        dirToMove = inertieVector * currentFwdSpeed;
    }

    void computeDirectionVerticale()
    {
        float AdjustTime = 1;

        float speedfactor = Mathf.InverseLerp(maxAltMaxSpeed, minAltMaxSpeed, currentFwdSpeed);

        float heightFactor = getHeightRatio();

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



        float suspension = 20; // make a var

        if (currentAltitude <= minAltitude)
        {
            currentVerticalForce =  Mathf.Max(currentVerticalForce, minAltitude - currentAltitude);
            AdjustTime = suspension;
        }

        if (downRaycast.transform != null && Vector3.Distance(downRaycast.point, transform.position) < 5 && I_verticalBoost == 0)
        {
            currentVerticalForce = 5 - Vector3.Distance(downRaycast.point, transform.position);
            AdjustTime = suspension;

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
        else
        {
            downRaycast = new RaycastHit();
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
		I_accel = controlerSet.Get_AccelAxisInput();
        #endregion

        #region rotation
        I_lateralPlayerRot =controlerSet.Get_HorizontalRotInput();
        #endregion

        #region lateral boost
        I_lateralBoostLeft = 0f;
		I_lateralBoostRight = 0f;
        //I_lateralBoostLeft = Input.GetAxis(controlerSet.Get_LateralBoostLeftInput());
        //if (Mathf.Abs(I_lateralBoostLeft) > boost_minSensitivity)
        //{
        //    //			print("BoostLeft" + I_lateralBoostLeft);
        //}
        //else
        //{
        //    I_lateralBoostLeft = 0f;
        //}
        //print(transform.parent.name + " " + controlerSet.Get_LatLeftBoostInput());
        //if (Input.GetButton(controlerSet.Get_LatLeftBoostInput()))
        //{
        //    I_lateralBoostLeft = 1f;
        //}
        //I_lateralBoostRight = Input.GetAxis(controlerSet.Get_LateralBoostRightInput());
        //if (Mathf.Abs(I_lateralBoostRight) > boost_minSensitivity)
        //{
        //    //			print("BoostRight" + I_lateralBoostRight);
        //}
        //else
        //{
        //    I_lateralBoostRight = 0f;
        //}
        //if (Input.GetButton(controlerSet.Get_LatRightBoostInput()))
        //{
        //    I_lateralBoostRight = 1f;
        //}
        #endregion

        #region vertical boost
        I_verticalBoost = controlerSet.Get_VertcalBoostAxisInput();
		#endregion

		#region boost
		I_forwardBoost = controlerSet.Get_ForwardBoostInput();
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
	}

    IEnumerator rotateFromCollision(Vector3 targetDirection)
    {

        float rotSpeed = 10;
        float addedRot = 0.01f;
        float ntime = 0;

        while (Vector3.Angle(transform.forward, targetDirection) > 2f && ntime < 0.5f)
        {
            inertieVector = Vector3.Lerp(inertieVector, targetDirection, Time.deltaTime * rotSpeed + addedRot);
            Vector3 lookDir = Vector3.Lerp(transform.forward, targetDirection, Time.deltaTime * rotSpeed + addedRot);
            lookDir.y = 0;
            transform.LookAt(transform.position + lookDir);

            ntime += Time.deltaTime;

            yield return null;
        }
        rotateOnCollisionCoroutine = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Obstacle") return;

        SoundManager.instance.PlaySoundCollision();

        if(Vector3.Dot(collision.contacts[0].normal, Vector3.up) > 0.9f) // Si la face qu'on a touché pointe vers le haut
        {
            return;
        }

        if (rotateOnCollisionCoroutine != null)
        {
            StopCoroutine(rotateOnCollisionCoroutine);
            transform.LookAt(transform.position +  new Vector3(collisionRotationVector.x, 0, collisionRotationVector.z));
            inertieVector = collisionRotationVector;
        }

        Vector3 reflect = Vector3.Reflect(inertieVector, collision.contacts[0].normal);
        collisionRotationVector = inertieVector;


        if(Vector3.Angle(-inertieVector, reflect) < 60 && flagBehavoirScript.targetPlayer == this)
        {
            //Crash
            flagBehavoirScript.Drop();
        }

        collisionRotationVector = Vector3.Cross(collision.contacts[0].normal, Vector3.up);
        if (Vector3.Angle(inertieVector, collisionRotationVector) > 90) collisionRotationVector = -collisionRotationVector;

        collisionRotationVector = Vector3.RotateTowards(collisionRotationVector, collision.contacts[0].normal, 5 * Mathf.Deg2Rad, 0.0f);


        GameObject fx = Instantiate(ImpactFx, collision.contacts[0].point, Quaternion.identity) as GameObject;
        fx.transform.LookAt(fx.transform.position + reflect.normalized);
        Destroy(fx, 1f);

        bumbVector = reflect * 175 * getSpeedRatio();

        float bumpForce = Mathf.InverseLerp(360, 0, 180 - Vector3.Angle(-inertieVector, reflect));
        currentFwdSpeed *= bumpForce;


        rotateOnCollisionCoroutine = StartCoroutine(rotateFromCollision(collisionRotationVector));
    }

    public void RefillBoost()
	{
		currentBoostAmountLeft = 1f;
	}

    public bool IsInTrail()
    {
        if (flagBehavoirScript == null) return false;
        if (flagBehavoirScript.targetPlayer == this || flagBehavoirScript.targetPlayer == null) return false;

        for (int i = 0; i < flagBehavoirScript.previousPos.Count; i++)
        {
            if (Vector3.Distance(transform.position, flagBehavoirScript.previousPos[i]) < Vector3.Distance(flagBehavoirScript.previousPos[i], flagBehavoirScript.targetPlayer.transform.position)) return true;
        }
        return false;
    }

    public float getSpeedRatio()
    {
        return Mathf.InverseLerp(0, minAltMaxSpeed, currentFwdSpeed);
    }

    public float getSpeedRatioWithBoost()
    {
        return Mathf.InverseLerp(0, minAltMaxSpeed * boostSpeedMultiplier, currentFwdSpeed);
    }

    public float getHeightRatio()
    {
        return Mathf.InverseLerp(minAltitude, maxAltitude, currentAltitude);
    }

    public void OnStartBoost()
    {
        SoundManager.instance.playSoundBoost();
    }

    public bool IsOnGround()
    {

        if (currentAltitude <= minAltitude) return true;

        if (downRaycast.transform != null && downRaycast.distance < 5) return true;

        return false;

    }
}
