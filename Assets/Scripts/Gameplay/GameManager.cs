using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public enum GameState
    {
        Playing, 
        Paused, 
        Ended
    }
    public GameState currentGameState = GameState.Paused;

    [HideInInspector] public PlayerManager playerManagerScript;
    bool initDone = false;

    public static int targetScoreToWin = 10000;

    public Canvas endGameCanvas;

    ControllerV3 controller;

    static GameManager instance;
    public static GameManager get()
    {
        if(instance == null || instance.transform == null)
        {
            instance = FindObjectOfType<GameManager>();
        }
        return instance;
    }

    public void Initianisation()
    {
        playerManagerScript = GetComponent<PlayerManager>();
        controller = GetComponent<ControllerV3>();
        controller.useKeyboard = true;
        endGameCanvas.enabled = false;

        initDone = true;
    }

	// Update is called once per frame
	void Update ()
    {
        if (!initDone) return;

        CheckPlayersScore();

        if (currentGameState == GameState.Ended)
            WaitRestart();
	}

    void CheckPlayersScore()
    {

        for (int i = 0; i < playerManagerScript.characters.Length; i++)
        {
            float score = playerManagerScript.characters[i].currentScore;
            if(score >= targetScoreToWin && Cheatskaude.canGameEnd)
            {
                endGame();
            }
        }

    }

    void endGame()
    {
        currentGameState = GameState.Ended;
        endGameCanvas.enabled = true;
        endGameCanvas.transform.Find("Scores").GetComponent<Text>().text = "";

        for (int i = 0; i < playerManagerScript.characters.Length; i++)
        {
            float score = playerManagerScript.characters[i].currentScore;
            if(score >= targetScoreToWin)
                endGameCanvas.transform.Find("Winner").GetComponent<Text>().text = "Player " + (i+1) + " won!";

            endGameCanvas.transform.Find("Scores").GetComponent<Text>().text += "Player " + (i+1) + " : " + (int)score + "\n";
            
        }

    }

    void ResetPlayers()
    {
        for (int i = 0; i < playerManagerScript.characters.Length; i++)
        {
            playerManagerScript.characters[i].currentScore = 0;
            playerManagerScript.characters[i].CurrentVelocity = Vector3.zero;
            playerManagerScript.characters[i].rigidbody.velocity = Vector3.zero;
            playerManagerScript.characters[i].inertieVector = Vector3.zero;

        }
    }

    public void WaitRestart()
    {
        if(controller.Get_UseItemInput() != 0)
        {
            ResetPlayers();
            PauseManagement.manager.BackMainMenu();
        }

        if (controller.Get_ForwardBoostInput() != 0)
        {
            StartCoroutine(resetScene());
        }
    }

    IEnumerator resetScene()
    {
        ResetPlayers();
        PlayerManager.loadingEnded = false;
        currentGameState = GameState.Paused;

        Scene current = SceneManager.GetActiveScene();
        string sceneName = current.name;

        AsyncOperation async = SceneManager.UnloadSceneAsync(current);

        while (!async.isDone) yield return null;

        Debug.Log("load " + sceneName);

        endGameCanvas.enabled = false;

        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        while (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            yield return null;
        }

        Debug.Log("New active scene : " + sceneName);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        playerManagerScript.init(playerManagerScript.playerNumber);
        currentGameState = GameState.Playing;
    }



    void redoInit(Scene level)
    {


    }

    public static bool isPaused()
    {
        if (get() == null) return false;

        return get().currentGameState == GameState.Paused || get().currentGameState == GameState.Ended;
    }

}
