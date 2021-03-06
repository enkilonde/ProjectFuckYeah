﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

	public enum AllGameStates
	{
		Menu,
		PresentLevel,
		CountDown,
		Play,
		EndGameAnimation,
		Score
	}
	public static AllGameStates curGameState = AllGameStates.Menu;

	private PlayersManager playersManagerScript;
	private SceneCam sceneCamScript;
	private GenerateFlag FlagGenerator;
	private UpdateUIStatePlayers uiManager;

	public bool useFlag = true;

	void Awake()
	{
		playersManagerScript = GetComponent<PlayersManager>();
		sceneCamScript = GetComponentInChildren<SceneCam>();
		if(useFlag)
			FlagGenerator = GameObject.Find("FlagPoint").GetComponent<GenerateFlag>();
		uiManager = GameObject.Find("UI").GetComponent<UpdateUIStatePlayers>();
	}

	void Update()
	{
		switch(curGameState)
		{
		case AllGameStates.Menu:
			if(Input.anyKeyDown)
			{
				//Self actualisation
				curGameState = AllGameStates.PresentLevel;
				//Generate Flag
				if(useFlag)
					FlagGenerator.GenerateTheFlag();
				//Generate players
				playersManagerScript.GeneratePlayers();
				//Set the target for everyone
				if(useFlag)
					CameraV1.targetToLock = GameObject.Find("Flag").transform;
				//Do the traveling
				sceneCamScript.camState = SceneCam.CamBehaviorStates.DoIntroTravelling;
			}
			break;
		case AllGameStates.PresentLevel:
			if(sceneCamScript.travellingOver)
			{
				curGameState = AllGameStates.CountDown;
				//Disable cinematic camera
				sceneCamScript.gameObject.SetActive(false);
				sceneCamScript.travellingOver = false;
				sceneCamScript.camState = SceneCam.CamBehaviorStates.Static;
				//generate UI
				uiManager.SetCanvasForEachPlayer(playersManagerScript.playerAmount.Length);
			}
			break;
		case AllGameStates.CountDown:
			curGameState = AllGameStates.Play;
			break;
		case AllGameStates.Play:
			//Vehicules autorisés à bouger
			if(playersManagerScript.partyOver)
			{
				curGameState = AllGameStates.EndGameAnimation;
				//Disable ui
				uiManager.DisablePlayersUICanvas();
				//Do the traveling
				sceneCamScript.gameObject.SetActive(true);
				sceneCamScript.playerToFocus = playersManagerScript.winningPlayer;
				sceneCamScript.camState = SceneCam.CamBehaviorStates.DoOutroTravelling;
//				sceneCamScript.transform.position = ;	//position de depart du travelling de fin
				//Slow motion
				Time.timeScale = 0.75f;
			}
			break;
		case AllGameStates.EndGameAnimation:
			if(sceneCamScript.travellingOver)
			{
				curGameState = AllGameStates.Score;
				//print("Over !");
				//End Slow motion
				Time.timeScale = 1f;
				//SetUI score screen
				uiManager.SetScoreScreen();
			}
			break;
		case AllGameStates.Score:
			break;
		}


	}

}
