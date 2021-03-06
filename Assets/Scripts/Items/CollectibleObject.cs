﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObject : MonoBehaviour
{

    public ITEM_LIST itemType;
    public CollectibleBase item;

    GameObject itemDisplay;

    public float respawnTime = 10;
    float timer;

	// Use this for initialization
	void Start ()
    { 
        OnChangeObject();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
        if(itemDisplay == null)
        {
            timer -= Time.deltaTime;
            if (timer < 0) OnChangeObject();
        }

	}

    void OnChangeObject()
    {
        timer = respawnTime;
        item = ItemsUtility.GetItemFromEnum(itemType);

        if (itemDisplay != null) Destroy(itemDisplay);

        itemDisplay = SpawnItem(itemType, transform);
    }

    public static GameObject SpawnItem(ITEM_LIST type, Transform parent = null)
    {
        CollectibleBase itemTemp = ItemsUtility.GetItemFromEnum(type);
        GameObject itemSpawned = Instantiate(Resources.Load("Items/" + type.ToString())) as GameObject;
        itemSpawned.transform.SetParent(parent);
        itemSpawned.transform.position = parent.position;
        itemSpawned.GetComponent<Renderer>().material.color = itemTemp.color;

        return itemSpawned;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag != "PlayerCharacter" || itemDisplay == null) return;
        Debug.Log(" collision : " + other.name, other.gameObject);
        item.OnCollect(other.GetComponent<PlayerUseItem>());
        Destroy(itemDisplay);
        itemDisplay = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, transform.localScale * 10);
    }
}
