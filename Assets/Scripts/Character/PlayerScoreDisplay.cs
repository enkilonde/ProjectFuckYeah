using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RobToolsNameSpace;

public class PlayerScoreDisplay : MonoBehaviour {

	private Scrollbar jauge;
	private Text scoreText;

	private float finalScore = 0f;	//Score from my player
	private float offsetAnimation = 0f;		//Time to wait before beginning the animation
	public float offsetBetweenEachPlayer = 1f;		//OffsetValue between each player
	public AnimationCurve jaugeSizeAnimation = AnimationCurve.Linear(0f,0f, 1f,1f);

	private bool doActualisation = false;

	public void SetPlayerData(CharacterV3 cv3, int _id)
	{
		jauge = GetComponentInChildren<Scrollbar>();
		scoreText = GetComponentInChildren<Text>();

		finalScore = cv3.currentScore;
		offsetAnimation = offsetBetweenEachPlayer * _id;

		//Now we have the data, let's animate it
		doActualisation = true;
	}

	private float timer = 0f;
	private float _t_jauge = 0f;
	float _val = 0f;

	void Update()
	{
		//Wait to get the datas
		if(!doActualisation)
			return;

		//Offset
		if(timer < offsetAnimation)
		{
			timer += Time.deltaTime;
			return;
		}

		//Update the jauge
		_val = jaugeSizeAnimation.Evaluate(_t_jauge);
		jauge.size = RobToolsClass.GetNormalizedValue(finalScore * _val, 0f, PlayersManager.scoreToWinStatic);
		_t_jauge += Time.deltaTime;

		//Update the text
		scoreText.text = Mathf.Clamp(Mathf.RoundToInt(finalScore * _val), 0, Mathf.RoundToInt(PlayersManager.scoreToWinStatic)).ToString();

	}

}
