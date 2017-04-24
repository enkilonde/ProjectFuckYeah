using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public const int keyboardIndex = -5;

    public static PlayerManager manager;

    public GameObject[] Players;

    public CharacterV3[] characters = new CharacterV3[4];
    public ControllerV3[] controllers = new ControllerV3[4];

    public GameObject[] CurrentSceneGameObjects;

    public bool[] playersAttribs = new bool[4];
    public bool[] controllersUsed = new bool[4];

    int playerNumber = 0;

    private void Awake()
    {
        if (manager == null)
            manager = this;
        else
            Destroy(this);

        controllersUsed = new bool[4];
        playersAttribs = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            controllersUsed[i] = false;
            playersAttribs[i] = false;
        }
    }

    private void Update()
    {
        AttribInputs();
    }

    public void init(int NumberOfPlayers)
    {
        playerNumber = NumberOfPlayers;
        CurrentSceneGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();


        for (int i = 0; i < Players.Length; i++)
        {
            GameObject player = Players[i];

            characters[i] = player.GetComponentInChildren<CharacterV3>();
            controllers[i] = player.GetComponentInChildren<ControllerV3>();

            characters[0].useKeyboard = true;
            controllers[0].playerNumero = keyboardIndex;

            if(i >= NumberOfPlayers)
            {
                Players[i].SetActive(false);
                Players[i].transform.position = new Vector3(0, -100, 0);
                continue;
            }
            

            GameObject pos = GetGameObjectByName("pos" + (i+1).ToString(), CurrentSceneGameObjects);
            if (pos != null)
            {
                player.transform.position = pos.transform.position;
                player.transform.rotation = pos.transform.rotation;
            }

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

        Camera camUI = cam.transform.parent.Find("CameraUI").GetComponent<Camera>();
        camUI.rect = cam.rect;

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

    public void AttribInputs()
    {
        if (playersAttribs[3]) return;
        for (int i = 1; i <= 4; i++)
        {
            //Debug.Log("Controller : " + i + " : " +  Input.GetAxisRaw(i + "_XBOX_A"));
            if (controllersUsed[i - 1]) continue;

            if(Input.GetAxisRaw(i + "_XBOX_A") != 0)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (playersAttribs[j]) continue;

                    Debug.Log("Set player " + j + " on controller " + i);
                    controllers[j].playerNumero = i;
                    characters[j].inputsSet = true;
                    if(j < playerNumber-1)
                    {
                        characters[j].useKeyboard = false;
                        characters[j + 1].useKeyboard = true;
                        controllers[j + 1].playerNumero = keyboardIndex;
                    }
                    

                    controllersUsed[i-1] = true;
                    playersAttribs[j] = true;



                    break;
                }
            }

        }
    }


}
