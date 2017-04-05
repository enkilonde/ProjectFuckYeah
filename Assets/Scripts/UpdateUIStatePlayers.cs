using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RobToolsNameSpace;

public class UpdateUIStatePlayers : MonoBehaviour {

	public GameObject canvasPrefab;
	public GameObject scorePrefab;

	private PlayerAndCanvasLink[] playersData = new PlayerAndCanvasLink[4];

	void Update () {

		if(GameState.curGameState != GameState.AllGameStates.Play)
			return;

		//Maj the ui for each player according to it's CharacterV3 script variables
		for (int i = 0; i < playersData.Length; i++) {
			CharacterV3 _curCV3 = playersData[i].characterData;
			playersData[i].heightScroll.size = RobToolsClass.GetNormalizedValue(_curCV3.currentAltitude, _curCV3.minAltitude, _curCV3.maxAltitude);
			playersData[i].speedScroll.size = RobToolsClass.GetNormalizedValue(_curCV3.currentFwdSpeed, 0f, _curCV3.minAltMaxSpeed);
			playersData[i].score.text = Mathf.RoundToInt(_curCV3.currentScore).ToString();
			playersData[i].boostScroll.size = RobToolsClass.GetNormalizedValue(_curCV3.currentBoostAmountLeft, 0f, 1f);
		}

	}

	public void SetCanvasForEachPlayer(int playerAmount)
	{
		

		switch(playerAmount)
		{
		case 1:
			RectTransform newCanvas = Instantiate(canvasPrefab, transform.GetChild(0)).GetComponent<RectTransform>();
			newCanvas.name = "J1_Canvas";
			SetCanvasDataByID(0, newCanvas);
			break;
		case 2:
			//J1
			newCanvas = Instantiate(canvasPrefab, transform.GetChild(0)).GetComponent<RectTransform>();
			newCanvas.name = "J1_Canvas";
			newCanvas.localScale = Vector3.one;
			newCanvas.offsetMax = new Vector2(-400f, 0f);
			newCanvas.offsetMin = new Vector2(0f, 0f);
			SetCanvasDataByID(0, newCanvas);
			//J2
			newCanvas = Instantiate(canvasPrefab, transform.GetChild(0)).GetComponent<RectTransform>();
			newCanvas.name = "J2_Canvas";
			newCanvas.localScale = Vector3.one;
			newCanvas.offsetMax = new Vector2(0f, 0f);
			newCanvas.offsetMin = new Vector2(400f, 0f);
			SetCanvasDataByID(1, newCanvas);
			break;
		case 3:
			//J1
			newCanvas = Instantiate(canvasPrefab, transform.GetChild(0)).GetComponent<RectTransform>();
			newCanvas.name = "J1_Canvas";
			newCanvas.localScale = Vector3.one;
			newCanvas.offsetMax = new Vector2(-400f, 0f);
			newCanvas.offsetMin = new Vector2(0f, 200f);
			SetCanvasDataByID(0, newCanvas);
			//J2
			newCanvas = Instantiate(canvasPrefab, transform.GetChild(0)).GetComponent<RectTransform>();
			newCanvas.name = "J2_Canvas";
			newCanvas.localScale = Vector3.one;
			newCanvas.offsetMax = new Vector2(0f, 0f);
			newCanvas.offsetMin = new Vector2(400f, 200f);
			SetCanvasDataByID(1, newCanvas);
			//J3
			newCanvas = Instantiate(canvasPrefab, transform.GetChild(0)).GetComponent<RectTransform>();
			newCanvas.name = "J3_Canvas";
			newCanvas.localScale = Vector3.one;
			newCanvas.offsetMax = new Vector2(-400f, -200f);
			newCanvas.offsetMin = new Vector2(0f, 0f);
			SetCanvasDataByID(2, newCanvas);
			break;
		case 4:
			//J1
			newCanvas = Instantiate(canvasPrefab, transform.GetChild(0)).GetComponent<RectTransform>();
			newCanvas.name = "J1_Canvas";
			newCanvas.localScale = Vector3.one;
			newCanvas.offsetMax = new Vector2(-400f, 0f);
			newCanvas.offsetMin = new Vector2(0f, 200f);
			SetCanvasDataByID(0, newCanvas);
			//J2
			newCanvas = Instantiate(canvasPrefab, transform.GetChild(0)).GetComponent<RectTransform>();
			newCanvas.name = "J2_Canvas";
			newCanvas.localScale = Vector3.one;
			newCanvas.offsetMax = new Vector2(0f, 0f);
			newCanvas.offsetMin = new Vector2(400f, 200f);
			SetCanvasDataByID(1, newCanvas);
			//J3
			newCanvas = Instantiate(canvasPrefab, transform.GetChild(0)).GetComponent<RectTransform>();
			newCanvas.name = "J3_Canvas";
			newCanvas.localScale = Vector3.one;
			newCanvas.offsetMax = new Vector2(-400f, -200f);
			newCanvas.offsetMin = new Vector2(0f, 0f);
			SetCanvasDataByID(2, newCanvas);
			//J4
			newCanvas = Instantiate(canvasPrefab, transform.GetChild(0)).GetComponent<RectTransform>();
			newCanvas.name = "J4_Canvas";
			newCanvas.localScale = Vector3.one;
			newCanvas.offsetMax = new Vector2(0, -200f);
			newCanvas.offsetMin = new Vector2(400f, 0f);
			SetCanvasDataByID(3, newCanvas);
			break;
		}
	}

	public void SetPlayerCanvasArrayLength(int _length)
	{
		playersData = new PlayerAndCanvasLink[_length];
		for (int i = 0; i < playersData.Length; i++) {
			playersData[i] = new PlayerAndCanvasLink();
		}
	}

	public void SetPlayerCanvasId(int id, CharacterV3 cv3)
	{
		playersData[id].name = "P" + id.ToString();
		playersData[id].characterData = cv3;
	}

	public void SetCanvasDataByID(int id, Transform _canvas)
	{
		playersData[id].heightScroll = _canvas.Find("HeightScrollBar").GetComponent<Scrollbar>();
		playersData[id].speedScroll = _canvas.Find("SpeedScrollBar").GetComponent<Scrollbar>();
		playersData[id].score = _canvas.Find("Score").GetComponentInChildren<Text>();
		playersData[id].boostScroll = _canvas.Find("BoostScrollBar").GetComponentInChildren<Scrollbar>();
	}

	public void SetScoreScreen()
	{
		transform.Find("ScoreScreen").gameObject.SetActive(true);
		Transform _scoreScreen = transform.Find("PlayersScores_Container");

		for (int i = 0; i < playersData.Length; i++) {

			PlayerScoreDisplay PSD = Instantiate(scorePrefab, _scoreScreen).GetComponent<PlayerScoreDisplay>();
			PSD.SetPlayerData(playersData[i].characterData, i);

		}
	}

}

[System.Serializable]
public class PlayerAndCanvasLink
{

	public PlayerAndCanvasLink()
	{
		name = "test";
	}
	public string name;
	public CharacterV3 characterData;
	public Scrollbar heightScroll;
	public Scrollbar speedScroll;
	public Text score;
	public Scrollbar boostScroll;
}