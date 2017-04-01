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

	void Awake()
	{
		playersManagerScript = GetComponent<PlayersManager>();
		sceneCamScript = GetComponentInChildren<SceneCam>();
		FlagGenerator = GameObject.Find("FlagPoint").GetComponent<GenerateFlag>();
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
				//Generate players
				playersManagerScript.GeneratePlayers();
				//Generate Flag
				FlagGenerator.GenerateTheFlag();
			}
			break;
		case AllGameStates.PresentLevel:
			//Do the traveling
			sceneCamScript.camState = SceneCam.CamBehaviorStates.DoIntroTravelling;
			if(sceneCamScript.introTravellingOver)
			{
				curGameState = AllGameStates.CountDown;
				sceneCamScript.gameObject.SetActive(false);
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
