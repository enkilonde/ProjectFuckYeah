using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUI : MonoBehaviour {

    const int ecart = 100;

    public static ItemUI instance;

    public Camera[] cameras = new Camera[4];

	// Use this for initialization
	void Awake ()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        cameras = transform.Find("Cameras").GetComponentsInChildren<Camera>();

        for (int i = 0; i < System.Enum.GetValues(typeof(ITEM_LIST)).Length; i++)
        {
            GameObject item = CollectibleObject.SpawnItem((ITEM_LIST)i, transform);
            item.transform.position += new Vector3(ecart, 0, 0) * (i+1);
        }
               	
	}
	

    public void SetCameraOnItem(int playerID, int itemID)
    {
        Debug.Log(playerID);
        cameras[playerID].transform.position = cameras[playerID].transform.parent.position + new Vector3((itemID+1) * ecart, 0, 0);
    }
    
}
