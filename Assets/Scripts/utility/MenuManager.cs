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
        addPlayersScript = SceneManager.GetSceneByName("Common").GetRootGameObjects()[0].GetComponent<AddPlayers>();
        displayNumberOfPlayers = GameObject.Find("Number of Players").GetComponent<Text>();
    }

    public void Play(string levelName)
    {
        nextLevel = levelName;
        
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        

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
        addPlayersScript.NumberOfPlayers = Mathf.Clamp(addPlayersScript.NumberOfPlayers + value, 1, 4);
        displayNumberOfPlayers.text = "Number of players : " + addPlayersScript.NumberOfPlayers;

    }

}
