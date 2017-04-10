using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    public static void LoadGameScene(string SceneName)
    {

    }

    public static void LoadMainMenu()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (SceneManager.GetSceneByName("Menu").isLoaded)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu"));
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
        
        SceneManager.UnloadSceneAsync(currentScene);
    }


	
}
