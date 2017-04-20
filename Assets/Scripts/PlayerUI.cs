using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    CharacterV3 charaScript;

    Text scoreText;
    RectTransform speedDisplay;
    RectTransform heightDisplay;
    RectTransform boostDisplay;


    // Use this for initialization
    void Awake ()
    {
        charaScript = transform.parent.parent.GetComponentInChildren<CharacterV3>();
        scoreText = transform.Find("Score").GetComponent<Text>();
        speedDisplay = transform.Find("Speed").Find("slider").GetComponent<RectTransform>();
        heightDisplay = transform.Find("Height").Find("slider").GetComponent<RectTransform>();
        boostDisplay = transform.Find("Boost").Find("slider").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update ()
    {

        scoreText.text = "Score : " + (int)charaScript.currentScore;

        //speedDisplay.size = Mathf.InverseLerp(0, charaScript.minAltMaxSpeed, charaScript.currentFwdSpeed);
        //heightDisplay.size = Mathf.InverseLerp(0, charaScript.maxAltitude, charaScript.currentAltitude);
        //boostDisplay.size = Mathf.InverseLerp(0, 1, charaScript.currentBoostAmountLeft);

        speedDisplay.sizeDelta = new Vector2(Mathf.InverseLerp(0, charaScript.minAltMaxSpeed, charaScript.currentFwdSpeed) * 100, speedDisplay.sizeDelta.y);
        heightDisplay.sizeDelta = new Vector2(Mathf.InverseLerp(0, charaScript.maxAltitude, charaScript.currentAltitude) * 100, speedDisplay.sizeDelta.y);
        boostDisplay.sizeDelta = new Vector2(Mathf.InverseLerp(0, 1, charaScript.currentBoostAmountLeft) * 100, speedDisplay.sizeDelta.y);

    }
}
