using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RockSettings
{
    [Range (10f, 10000f)]
    public int verts;

    public RandomType randomType;


    public Vector3 min;
    public Vector3 max;

    public Vector3 unitCircleProduct;

}
