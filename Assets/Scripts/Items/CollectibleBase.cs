using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum ITEM_LIST // Ajoutez le nom d'un item dans cet enum quand vous le créez
{
    RefillBoost,
    Impulse
};

public class ItemsUtility
{
    public static CollectibleBase GetItemFromEnum(ITEM_LIST enumElement)
    {
        CollectibleBase c = (CollectibleBase)Activator.CreateInstance(Type.GetType(enumElement.ToString()));
        c.name = enumElement.ToString();
        c.Initialise();
        return c;
    }

    public static CollectibleBase GetRandomItem()
    {
        return (GetItemFromEnum(GetRandomEnum<ITEM_LIST>()));
    }

    public static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }

    public static ITEM_LIST GetEnum(string enumElement)
    {
        return (ITEM_LIST)Enum.Parse(typeof(ITEM_LIST), enumElement);
    }

}

public class CollectibleBase
{

    public PlayerUseItem character;
    [HideInInspector]public string name;
    public Color color = Color.white;

    public virtual void OnCollect(PlayerUseItem owner)
    {
        character = owner;
        character.objectCollected = this;
        ItemUI.instance.SetCameraOnItem(PlayerManager.manager.getManagerID(character.cv3), (int)ItemsUtility.GetEnum(name));
    }

    public virtual void Initialise()
    {
        //Debug.Log("Init");
    }

    public virtual void UseObjet()
    {
        SoundManager.instance.playSoundItemUsed(name);
        OnDestroy(); // change this if whe happen to have item that are not one use only
    }

    public virtual void OnDestroy()
    {
        ItemUI.instance.SetCameraOnItem(PlayerManager.manager.getManagerID(character.cv3), -1);
    }

}


public class RefillBoost : CollectibleBase
{
    public override void Initialise()
    {
        base.Initialise();
        color = Color.red;
    }

    public override void UseObjet()
    {
        base.UseObjet();
        character.cv3.RefillBoost();
    }

}

public class Impulse : CollectibleBase
{
    public override void Initialise()
    {
        base.Initialise();
        color = Color.yellow;
    }

    public override void UseObjet()
    {
        base.UseObjet();
        
    }

}