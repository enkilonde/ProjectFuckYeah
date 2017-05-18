using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    CharacterV3 charaScript;

    public AnimationCurve textBump;

    Text scoreText;
//    RectTransform speedDisplay;
//    RectTransform heightDisplay;
//    RectTransform boostDisplay;

	Image speedDisplay;
	Image heightDisplay;
	Image boostDisplay;
	Image scoreDisplay;

    Vector3 textScale;

    // Use this for initialization
    void Awake ()
    {
        charaScript = transform.parent.parent.GetComponentInChildren<CharacterV3>();
        scoreText = transform.Find("Score").GetComponent<Text>();
//        speedDisplay = transform.Find("Speed").Find("slider").GetComponent<RectTransform>();
//        heightDisplay = transform.Find("Height").Find("slider").GetComponent<RectTransform>();
//        boostDisplay = transform.Find("Boost").Find("slider").GetComponent<RectTransform>();
		speedDisplay = transform.Find("SpeedJauge").GetComponent<Image>();
		heightDisplay = transform.Find("AltiJauge").GetComponent<Image>();
		boostDisplay = transform.Find("BoostJauge").GetComponent<Image>();
		scoreDisplay = transform.Find("ScoreJauge").GetComponent<Image>();
        textScale = scoreText.transform.localScale;
	}

    // Update is called once per frame
    void Update ()
    {
        
        scoreText.text = "Score : " + (int)charaScript.currentScore;

        if(charaScript.flagBehavoirScript != null && charaScript.flagBehavoirScript.targetPlayer != null && charaScript.flagBehavoirScript.targetPlayer == charaScript)
        {
            scoreText.color = Color.green;
            scoreText.transform.localScale = textScale * textBump.Evaluate(Mathf.Repeat(Time.time, 1));
        }
        else
        {
            scoreText.color = Color.white;
            scoreText.transform.localScale = textScale;
        }

        //speedDisplay.size = Mathf.InverseLerp(0, charaScript.minAltMaxSpeed, charaScript.currentFwdSpeed);
        //heightDisplay.size = Mathf.InverseLerp(0, charaScript.maxAltitude, charaScript.currentAltitude);
        //boostDisplay.size = Mathf.InverseLerp(0, 1, charaScript.currentBoostAmountLeft);

//        speedDisplay.sizeDelta = new Vector2(charaScript.getSpeedRatio() * 100, speedDisplay.sizeDelta.y);
//        heightDisplay.sizeDelta = new Vector2(charaScript.getHeightRatio() * 100, speedDisplay.sizeDelta.y);
//        boostDisplay.sizeDelta = new Vector2(Mathf.InverseLerp(0, 1, charaScript.currentBoostAmountLeft) * 100, speedDisplay.sizeDelta.y);
		speedDisplay.fillAmount = charaScript.getSpeedRatio();
		heightDisplay.fillAmount = charaScript.getHeightRatio();
		boostDisplay.fillAmount = Mathf.InverseLerp(0, 1, charaScript.currentBoostAmountLeft);
		scoreDisplay.fillAmount = charaScript.currentScore / GameManager.targetScoreToWin;



    }
}
