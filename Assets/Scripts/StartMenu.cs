using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

    public AnimationCurve alpha;
    public Text texte;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.anyKeyDown)
        {
            Destroy(gameObject);
        }
        texte.color = new Color(texte.color.r, texte.color.g, texte.color.b, alpha.Evaluate(Mathf.Repeat(Time.time / 2, 1)));


	}
}
