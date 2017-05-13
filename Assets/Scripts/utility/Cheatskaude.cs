using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheatskaude : MonoBehaviour {

    public static bool canGameEnd = true;
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKey(KeyCode.F1)) RenderSettings.ambientIntensity = RenderSettings.ambientIntensity - Time.deltaTime;
        if (Input.GetKey(KeyCode.F2)) RenderSettings.ambientIntensity = RenderSettings.ambientIntensity + Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.F12)) canGameEnd = !canGameEnd;

        if (Input.GetKey(KeyCode.F10) && FindObjectOfType<FlagBehaviour>().targetPlayer) FindObjectOfType<FlagBehaviour>().targetPlayer.currentScore += Time.deltaTime * GameManager.targetScoreToWin / 10;

    }


}
