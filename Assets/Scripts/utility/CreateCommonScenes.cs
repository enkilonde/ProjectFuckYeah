using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CreateCommonScenes : MonoBehaviour
{


    //Load la scene "CommonBase" au lancement du jeu
    [RuntimeInitializeOnLoadMethodAttribute]
    static public void CreateScene()
    {
        SceneManager.LoadScene("Common", LoadSceneMode.Additive);
        SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
    }


}
