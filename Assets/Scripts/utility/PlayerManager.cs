using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager manager;

    public GameObject[] Players;

    public GameObject[] CurrentSceneGameObjects;

    private void Awake()
    {
        if (manager == null)
            manager = this;
        else
            Destroy(this);
    }

    public void init(int NumberOfPlayers)
    {
        CurrentSceneGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();


        for (int i = 0; i < Players.Length; i++)
        {
            GameObject player = Players[i];


            if(i >= NumberOfPlayers)
            Players[i].SetActive(false);

            GameObject pos = GetGameObjectByName("pos" + (i+1).ToString(), CurrentSceneGameObjects);
            if (pos != null)
                player.transform.position = pos.transform.position;

            SetCamera(player.transform.Find("Character").Find("Camera").GetComponentInChildren<Camera>(), NumberOfPlayers, i);

        }

    }

    void SetCamera(Camera cam, int numberOfPlayers, int playerIndex)
    {
        switch (numberOfPlayers)
        {
            case 1:
                cam.rect = new Rect(0, 0, 1, 1);
                break;

            case 2:
                switch (playerIndex)
                {
                    case 0:
                        cam.rect = new Rect(0, 0.5f, 1, 0.5f);
                        break;
                    case 1:
                        cam.rect = new Rect(0, 0, 1, 0.5f);
                        break;
                }
                break;

            case 3:
                switch (playerIndex)
                {
                    case 0:
                        cam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                        break;
                    case 1:
                        cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                        break;
                    case 2:
                        cam.rect = new Rect(0, 0, 1, 0.5f);
                        break;
                }
                break;

            case 4:
                switch (playerIndex)
                {
                    case 0:
                        cam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                        break;
                    case 1:
                        cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                        break;
                    case 2:
                        cam.rect = new Rect(0, 0, 0.5f, 0.5f);
                        break;
                    case 3:
                        cam.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                        break;
                }
                break;


        }
    }

    GameObject GetGameObjectByName(string name, GameObject[] objects)
    {
        name = name.ToLower();
        for (int i = 0; i < objects.Length; i++)
        {
            if(objects[i].name == name)
                return objects[i];

            GameObject resultinClildren = GetGameObjectByName(name, objects[i]);
            if (resultinClildren != null)
                return resultinClildren;
        }

        return null;
    }

    GameObject GetGameObjectByName(string name, GameObject obj)
    {
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            if (obj.transform.GetChild(i).gameObject.name == name)
                return obj.transform.GetChild(i).gameObject;

            GameObject resultinClildren = GetGameObjectByName(name, obj.transform.GetChild(i).gameObject);
            if (resultinClildren != null)
                return resultinClildren;
        }

        return null;
    }

}
