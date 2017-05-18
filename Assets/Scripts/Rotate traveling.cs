using UnityEngine;
using System.Collections;

public class Rotatetraveling : MonoBehaviour {

    public float speed;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.left * speed * Time.deltaTime);

    }
}
