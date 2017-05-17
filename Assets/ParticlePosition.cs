using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePosition : MonoBehaviour {

	RaycastHit rayhit;

	CharacterV3 cv3;

	ParticleSystem partSys;

	void Start () {
		cv3 = transform.parent.GetComponent<CharacterV3>();
		partSys = GetComponent<ParticleSystem>();
	}
	
	void Update () {

		if(cv3.currentAltitude > 5f)
		{
			partSys.Stop();
		}
		else
		{
			partSys.Play();
		}

		if(Physics.Raycast(transform.parent.position, -transform.parent.up, out rayhit, 2f))
		{
			transform.position = rayhit.point;
		}

	}
}
