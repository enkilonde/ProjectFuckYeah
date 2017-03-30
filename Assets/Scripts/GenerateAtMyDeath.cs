using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAtMyDeath : MonoBehaviour {

	[Header("Pour changer la zone de spawn des littleOnes, il suffit de changer le radius du sphere collider")]

	public GameObject littleOne;

	public int numberToSpawn = 4;

	public void GenerateLittlesThenDie()
	{

		float _radius = GetComponent<SphereCollider>().radius;

		for (int i = 0; i < numberToSpawn; i++) {

			//TODO remplacer par object pool
			Transform _instance = Instantiate(littleOne, transform.position, Quaternion.identity).transform;
			_instance.parent = transform.parent;
			_instance.position += Random.insideUnitSphere * _radius;
			_instance.name = littleOne.name;

		}

	}

}
