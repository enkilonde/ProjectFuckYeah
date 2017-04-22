using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCollision : MonoBehaviour {


	public GameObject impulsePrefab;

	public float mapRadius = 10f;
	public float onPlayerRadius = 15f;


	bool onAPlayer = false;
	Transform myPlayer;
	CapsuleCollider myCollider;
	Transform radiusFeedback;

	public float timeToRemainIncollision = 2f;

	float[] playersTimers;
	List<Transform> playersList;

	void Start()
	{
		myCollider = GetComponent<CapsuleCollider>();

		myCollider.radius = mapRadius;
		radiusFeedback = transform.Find("FeedbackRadius");
		radiusFeedback.localScale = new Vector3(myCollider.radius,myCollider.radius,myCollider.radius) * 2f;

		playersList = new List<Transform>();
        for (int i = 0; i < PlayerManager.manager.Players.Length; i++)
        {
            playersList.Add(PlayerManager.manager.Players[i].transform);
        }

		playersTimers = new float[AddPlayers.NumberOfPlayers];
		print(playersTimers.Length);
		CleanTheTimers();
	}

	void OnTriggerStay(Collider otherCol)
	{
		if(onAPlayer)
		{
			if(otherCol.transform != myPlayer)
			{
				if(otherCol.tag == "GameController")
				{
					//Si c'est le trigger
					if(otherCol.name == "ImpulseCollisionDetection")
						return;

					//remplir jauge de ce joueur
					int _id = playersList.IndexOf(otherCol.transform.parent);
					playersTimers[_id] += Time.deltaTime;

					if(playersTimers[_id] >= timeToRemainIncollision)
					{
						//Alors provoquer échange de drapeau
						CleanTheTimers();
						myPlayer.GetComponent<ChasingState>().BecomeHunter();
						SetTheTargetPlayer(otherCol.transform);
					}

				}
			}
			return;
		}

		if(otherCol.tag == "GameController")
		{
			//Si c'est le trigger
			if(otherCol.name == "ImpulseCollisionDetection")
				return;
			SetTheTargetPlayer(otherCol.transform);
		}
	}

	void OnTriggerExit(Collider otherCol)
	{
		if(onAPlayer)
		{
			if(otherCol.transform != myPlayer)
			{
				if(otherCol.GetComponent<Collider>().tag == "GameController")
				{
					//Si c'est le trigger
					if(otherCol.name == "ImpulseCollisionDetection")
						return;
						
					//vider la jauge de ce joueur
					int _id = playersList.IndexOf(otherCol.transform.parent);
					playersTimers[_id] = 0f;
				}
			}
		}
	}

	void SetTheTargetPlayer(Transform theNewTarget)
	{
//		Debug.Log("new target : " + theNewTarget.gameObject.name);
		transform.parent = theNewTarget;
		myPlayer = theNewTarget;
		transform.localPosition = Vector3.zero;
		theNewTarget.GetComponent<ChasingState>().BecomeTarget();
		onAPlayer = true;
		theNewTarget.GetComponentInChildren<CameraV1>().CameraType = CameraV1.camModes.Target;
		CameraV1.targetToLock = theNewTarget;
		theNewTarget.GetComponent<CharacterV3>().RefillBoost();
		myCollider.radius = onPlayerRadius;
		radiusFeedback.localScale = new Vector3(myCollider.radius,myCollider.radius,myCollider.radius) * 2f;
		Instantiate(impulsePrefab, transform.position, Quaternion.identity);
	}

	void CleanTheTimers()
	{
		for (int i = 0; i < playersTimers.Length; i++) {
			playersTimers[i] = 0f;
		}
	}

}
