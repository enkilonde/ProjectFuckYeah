using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFlag : MonoBehaviour {

	public GameObject flagPrefab;

	public bool useRandom = true;

	public void GenerateTheFlag()
	{
		if(useRandom)
		{
			int Spawn_ID = Random.Range(0, transform.childCount);
			Instantiate(flagPrefab, transform.GetChild(Spawn_ID).position, Quaternion.identity, transform);
		}
		else
		{
			Instantiate(flagPrefab, transform.position, Quaternion.identity, transform);
		}
	}

}
