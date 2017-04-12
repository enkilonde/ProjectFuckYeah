using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseEpl : MonoBehaviour {

//	public bool canAffectTargetPlayer = false;

	public float startingScale = 5f;
	public float endingScale = 15f;
	public float timeTransition = 10f;

	private float currentScale = 5f;

	void Start()
	{
		currentScale = startingScale;
		transform.localScale = Vector3.one * currentScale;
        SendExplosionForce();
	}

	void Update()
	{

		currentScale = Mathf.MoveTowards(currentScale, endingScale, timeTransition * Time.deltaTime);
		transform.localScale = Vector3.one * currentScale;

		if(currentScale >= endingScale)
		{
			GetComponent<Collider>().enabled = false;
			Destroy(gameObject, 1f);
		}
	}

    void SendExplosionForce()
    {
        CharacterV3[] players = PlayerManager.manager.characters;

        for (int i = 0; i < players.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, players[i].transform.position);
            if (dist > endingScale || dist < 0.1f) continue;

            Vector3 explosionDirection = players[i].transform.position - transform.position;
            explosionDirection = explosionDirection.normalized * (endingScale - dist);

            players[i].ReceiveExplosionForce(explosionDirection);

        }
    }

}
