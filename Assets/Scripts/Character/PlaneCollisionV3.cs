using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (CharacterV3))]
public class PlaneCollisionV3 : MonoBehaviour {

	public float impForce = 2f;
	CharacterV3 CV3;


	void Start () {
		CV3 = GetComponent<CharacterV3>();
	}
	

	void OnCollisionEnter(Collision coll)
	{
        //print(coll.gameObject.tag);
        return;
		if(coll.gameObject.CompareTag("Obstacle"))
		{
//			CV3.inertieVector

			Debug.DrawRay(coll.contacts[0].point, CV3.inertieVector * -10f, Color.magenta);	//inertie
			Debug.DrawRay(coll.contacts[0].point, coll.contacts[0].normal * 10f, Color.green);	//normale
			Debug.DrawRay(coll.contacts[0].point, Vector3.Reflect(CV3.inertieVector, coll.contacts[0].normal) * 10f, Color.red);	//reflect


            float impactAngle = Vector3.Angle(CV3.inertieVector, coll.contacts[0].normal) - 90;

            if(impactAngle < 90)
            {
                //CV3.ObstacleHit(_reflectionvector, coll.contacts[0].normal, coll.contacts[0].point);

            }
            //CV3.ObstacleHit(_reflectionvector, coll.contacts[0].normal, coll.contacts[0].point, impactAngle);

        }


	}

}
