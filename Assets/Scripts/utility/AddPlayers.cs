using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AddPlayers : MonoBehaviour
{
    [Range(1, 4)]
    public int NumberOfPlayers = 1;

	// Use this for initialization
	void Awake ()
    {

        if(!SceneManager.GetActiveScene().name.ToLower().Contains("menu"))
        {
            init();
        }

	}

    public void init()
    {
        SceneManager.LoadScene("players", LoadSceneMode.Additive);

        StartCoroutine(waitLevelLoad());
    }

    IEnumerator waitLevelLoad()
    {

        while(!SceneManager.GetSceneByBuildIndex(1).isLoaded)
        {
            yield return null;
        }

        SceneEndedLoad();
    }
	
    public void SceneEndedLoad()
    {
        GameObject[] GOFromOtherScene = SceneManager.GetSceneByBuildIndex(1).GetRootGameObjects();


        PlayerManager pm = null;

        for (int i = 0; i < GOFromOtherScene.Length; i++)
        {
            if (GOFromOtherScene[i].GetComponent<PlayerManager>())
                pm = GOFromOtherScene[i].GetComponent<PlayerManager>();
        }

        pm.init(NumberOfPlayers);
    }

}
