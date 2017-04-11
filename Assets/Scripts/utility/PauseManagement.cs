
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseManagement : MonoBehaviour
{

    public const string menuSceneName = "Menu";

    public static PauseManagement manager;

    public bool paused = false;

    public Canvas pauseCanvas;

	// Use this for initialization
	void Awake ()
    {
        if (manager == null)
            manager = this;
        else
            Destroy(this);

        pauseCanvas = GameObject.Find("PauseCanvas").GetComponent<Canvas>();
        TogglePause(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(!paused);
        }
	}


    public void TogglePause(bool state)
    {
        paused = state;

        pauseCanvas.enabled = state;
    }


    public void BackMainMenu()
    {
        TogglePause(false);

        string currentScene = SceneManager.GetActiveScene().name;
        if (SceneManager.GetSceneByName(menuSceneName).isLoaded)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(menuSceneName));
            SceneManager.UnloadSceneAsync(currentScene);
        }
        else
        {
            SceneManager.LoadScene(menuSceneName, LoadSceneMode.Additive);
            StartCoroutine(waitForSceneLoad(menuSceneName, removeCurrentScene));
        }

        
    }

    IEnumerator waitForSceneLoad(string levelName, System.Action callback)
    {
        while (!SceneManager.GetSceneByName(levelName).isLoaded)
        {
            yield return null;
        }
        callback();
    }

    void removeCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(menuSceneName));
        SceneManager.UnloadSceneAsync(currentScene);
        SceneManager.UnloadSceneAsync("players");
        SceneManager.UnloadSceneAsync("Pause");
    }

}
