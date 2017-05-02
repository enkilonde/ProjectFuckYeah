using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RobToolsNameSpace;

public class TrailManager : MonoBehaviour {

	[Header("Le coté dont la trail doit grossir")]
	[Range(-1f, 1f)]
	public float sideRepartition = 1f;

	public float minWidth = 0.1f;
	public float maxWidth = 0.25f;

	private TrailRenderer trailA;
	private TrailRenderer trailB;
    private TrailRenderer trailReactor;

    CharacterV3 cv3;

    private float side;

	void Start () {
		trailA = transform.Find("L_Trail").GetComponent<TrailRenderer>();
		trailB = transform.Find("R_Trail").GetComponent<TrailRenderer>();
        trailReactor = transform.Find("Reactor_Trail").GetComponent<TrailRenderer>();
        cv3 = GetComponentInParent<CharacterV3>();
    }

    void Update () {
		
		side = Input.GetAxis("Horizontal") * sideRepartition;

		side += 1;
		side /= 2;

		trailA.widthMultiplier = Mathf.Lerp(minWidth, maxWidth, side);
		trailB.widthMultiplier = Mathf.Lerp(minWidth, maxWidth, 1 - side);

        Color trailColor = (cv3.previous_I_forwardBoost == 1) ? Color.red : Color.blue;
        trailReactor.material.SetColor("_TintColor", trailColor);

    }
}
