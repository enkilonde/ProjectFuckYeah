using System.Collections;
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

	void Awake()
	{
		playersManagerScript = GetComponent<PlayersManager>();
		sceneCamScript = GetComponentInChildren<SceneCam>();
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
				FlagGenerator.GenerateTheFlag();
				//Generate players
				playersManagerScript.GeneratePlayers();
				//Set the target for everyone
				CameraV1.targetToLock = GameObject.Find("Flag").transform;
			}
			break;
		case AllGameStates.PresentLevel:
			//Do the traveling
			sceneCamScript.camState = SceneCam.CamBehaviorStates.DoIntroTravelling;
			if(sceneCamScript.introTravellingOver)
			{
				curGameState = AllGameStates.CountDown;
				//Disable cinematic camera
				sceneCamScript.gameObject.SetActive(false);
				//generate UI
				uiManager.SetCanvasForEachPlayer(playersManagerScript.playerAmount.Length);
				uiManager.allowedToUpdateUi = true;
			}
			break;
		case AllGameStates.CountDown:
			curGameState = AllGameStates.Play;
			break;
		case AllGameStates.Play:
			//Vehicules autorisés à bouger
			break;
		case AllGameStates.EndGameAnimation:
			break;
		case AllGameStates.Score:
			break;
		}


	}

}
