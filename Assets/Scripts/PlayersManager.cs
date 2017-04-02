using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour {

	public GameObject playerPrefab;
	[Header("Attention, ne pas supprimer les gameObjects référencés dans ce tableau")]
	public Transform[] playerAmount = new Transform[4];

//	private Transform playersStartingPointsContainer;
	private Transform playersContainer;
	private UpdateUIStatePlayers uiManager;

	void Start()
	{
		uiManager = GameObject.Find("UI").GetComponent<UpdateUIStatePlayers>();
	}

	/// <summary>
	/// Generates the players.
	/// </summary>
	public void GeneratePlayers()
	{
		if(!playersContainer)
			playersContainer = new GameObject("Players Container").transform;
//		if(!playersStartingPointsContainer)
//		{
//			Debug.Log("No starting point located. No players will be generated. Please, go add players in the Manager gameobject");
//		}

		//Combien de canvas on doit générer
		uiManager.SetPlayerCanvasArrayLength(playerAmount.Length);

		for (int i = 0; i < playerAmount.Length; i++) {

			GameObject _lastInstance = (GameObject)Instantiate(playerPrefab, playerAmount[i].position, playerAmount[i].rotation, playersContainer);
			_lastInstance.name = _lastInstance.name + "_" + (i + 1).ToString();
			_lastInstance.GetComponentInChildren<ControllerV3>().playerNumero = i + 1;
			_lastInstance.GetComponentInChildren<TextMesh>().text = (i + 1).ToString();
			uiManager.SetPlayerCanvasId(i, _lastInstance.GetComponentInChildren<CharacterV3>());
			CameraSettup(playerAmount.Length, _lastInstance, i);
		}
	}

	/// <summary>
	/// Clean the scene.
	/// </summary>
	public void DestroyAllPlayers()
	{
		
	}

	/// <summary>
	/// Settup the camera for each player.
	/// </summary>
	/// <param name="amount">Amount of players.</param>
	public void CameraSettup(int amount, GameObject playerInst, int id)
	{
		Camera _cam = playerInst.GetComponentInChildren<Camera>();
		switch(amount)
		{
		case 1 :
			_cam.rect = new Rect(0f,0f,1f,1f);
			break;
		case 2:
			switch(id)
			{
			case 1:
				_cam.rect = new Rect(0f,0f, 0.5f, 1f);
				break;
			case 2:
				_cam.rect = new Rect(0.5f,0f, 0.5f, 1f);
				break;
			}
			break;
		case 3:
			switch(id)
			{
			case 1:
				_cam.rect = new Rect(0f,0f, 0.5f, 0.5f);
				break;
			case 2:
				_cam.rect = new Rect(0.5f,0f, 0.5f, 0.5f);
				break;
			case 3:
				_cam.rect = new Rect(0f,0.5f, 0.5f, 0.5f);
				break;
			}
			break;
		case 4:
			id++;
			switch(id)
			{
			case 1:
				_cam.rect = new Rect(0f,0.5f, 0.5f, 0.5f);
				break;
			case 2:
				_cam.rect = new Rect(0.5f,0.5f, 0.5f, 0.5f);
				break;
			case 3:
				_cam.rect = new Rect(0f,0f, 0.5f, 0.5f);
				break;
			case 4:
				_cam.rect = new Rect(0.5f,0f, 0.5f, 0.5f);
				break;
			}
			break;
		}
	}

//	#if UNITY_EDITOR
//	void OnValidate()
//	{
//		if(GameObject.Find("Players Starting Points"))
//		{
//			playersStartingPointsContainer = GameObject.Find("Players Starting Points").transform;
//			//Si il y a plus de joueurs que de positions de depart
//			if(playersStartingPointsContainer.childCount < playerAmount.Length)
//			{
//				for (int i = playersStartingPointsContainer.childCount; i < playerAmount.Length; i++) {
//					Debug.Log("PlayerStartingPoint " + i.ToString() + " created");
//					Transform _newStartingPoint = new GameObject("StartingPoint_P" + i.ToString()).transform;
//					_newStartingPoint.parent = playersStartingPointsContainer;
//					_newStartingPoint.position = Vector3.right * i * 10f;
//					IconManager.DrawIcon(_newStartingPoint.gameObject, 1);
//					playerAmount[i] = _newStartingPoint;
//				}
//			}
//			//S'il y a moins de joueurs que de positions de départ
//			if(playersStartingPointsContainer.childCount > playerAmount.Length)
//			{
//				for (int i = playersStartingPointsContainer.childCount; i > playerAmount.Length; i++) {
//					DestroyImmediate(playersStartingPointsContainer.GetChild(i).gameObject);
//				}
//			}
//		}
//	}
//	#endif

//	#if UNITY_EDITOR
//	void OnValidate()
//	{
//		//On check si nos objets n'existent pas deja
//		if(GameObject.Find("Players Starting Points"))
//		{
//			playersStartingPointsContainer = GameObject.Find("Players Starting Points").transform;
//			for (int i = 0; i < playerAmount.Length; i++) {
//				playerAmount[i] = playersStartingPointsContainer.GetChild(i);
//			}
//			return;
//		}
//		//Sinon, on les crée
//		if(!playersStartingPointsContainer)
//		{
//			if(playerAmount.Length == 0)
//			{
//				Debug.Log("Attention, mettre cette variable à 0 signifie qu'il y a 0 joueurs");
//				return;
//			}
//
//			Debug.Log("PlayerStartingPointsContainer created");
//			playersStartingPointsContainer = new GameObject("Players Starting Points").transform;
//
//			for (int i = 0; i < playerAmount.Length; i++) {
//				Debug.Log("PlayerStartingPoint 1 created");
//				Transform _newStartingPoint = new GameObject("StartingPoint_P" + i.ToString()).transform;
//				_newStartingPoint.parent = playersStartingPointsContainer;
//				_newStartingPoint.position = Vector3.right * i * 10f;
//				IconManager.DrawIcon(_newStartingPoint.gameObject, 1);
//				playerAmount[i] = _newStartingPoint;
//			}
//		}
//
//	}
//	#endif
}
