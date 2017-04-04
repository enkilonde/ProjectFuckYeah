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



}
