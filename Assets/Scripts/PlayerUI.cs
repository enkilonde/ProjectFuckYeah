using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    CharacterV3 charaScript;

    Text scoreText;
    Scrollbar speedDisplay;
    Scrollbar heightDisplay;
    Scrollbar boostDisplay;


	// Use this for initialization
	void Awake ()
    {
        charaScript = transform.parent.parent.GetComponentInChildren<CharacterV3>();
        scoreText = transform.Find("Score").GetComponent<Text>();
        speedDisplay = transform.Find("speed").GetComponent<Scrollbar>();
        heightDisplay = transform.Find("height").GetComponent<Scrollbar>();
        boostDisplay = transform.Find("boost").GetComponent<Scrollbar>();


    }

    // Update is called once per frame
    void Update ()
    {

        scoreText.text = "Score : " + (int)charaScript.currentScore;

        speedDisplay.size = Mathf.InverseLerp(0, charaScript.minAltMaxSpeed, charaScript.currentFwdSpeed);
        heightDisplay.size = Mathf.InverseLerp(0, charaScript.maxAltitude, charaScript.currentAltitude);
        boostDisplay.size = Mathf.InverseLerp(0, 1, charaScript.currentBoostAmountLeft);
    }
}
