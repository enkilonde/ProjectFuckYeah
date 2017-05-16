using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    private AddPlayers addPlayersScript;
    private Text displayNumberOfPlayers;
    private string nextLevel;

    private void Awake()
    {
        StartCoroutine(waitForSceneLoad("Common", init));
    }

    IEnumerator waitForSceneLoad(string levelName, System.Action callback)
    {
        while (!SceneManager.GetSceneByName(levelName).isLoaded)
        {
            yield return null;
        }
        callback();
    }

    void init()
    {
        addPlayersScript = FindObjectOfType<AddPlayers>();
        displayNumberOfPlayers = GameObject.Find("Number of Players").GetComponent<Text>();
        ChangeNumberOfPlayers(0);
        SoundManager.instance.OnMenuStart();
    }

    public void Play(string levelName)
    {
        nextLevel = levelName;
        
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        SoundManager.instance.OnButtonClicked();

        StartCoroutine(waitForSceneLoad(levelName, setMainScene));
    }

    void setMainScene()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextLevel));
        SceneManager.UnloadSceneAsync("Menu");
        addPlayersScript.init();
    }

    public void ChangeNumberOfPlayers(int value)
    {
        AddPlayers.NumberOfPlayers = Mathf.Clamp(AddPlayers.NumberOfPlayers + value, 1, 4);
        displayNumberOfPlayers.text = "number of players : <size=19>" + AddPlayers.NumberOfPlayers + "</size>";

    }

    public void QuitGame()
    {
        Application.Quit();
    } 

}
