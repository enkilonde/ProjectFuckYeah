using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseItem : MonoBehaviour
{

    [HideInInspector] public ControllerV3 controlerSet;
    [HideInInspector] public CharacterV3 cv3;

    public CollectibleBase objectCollected;

    private void Awake()
    {
        controlerSet = transform.parent.GetComponentInChildren<ControllerV3>();
        cv3 = GetComponent<CharacterV3>();
        objectCollected = null;
    }

    // Update is called once per frame
    void Update ()
    {
		
        if(objectCollected != null && controlerSet.Get_UseItemInput() != 0) // use imediatly for now
        {
            objectCollected.UseObjet();
            objectCollected = null;
        }

	}
}
