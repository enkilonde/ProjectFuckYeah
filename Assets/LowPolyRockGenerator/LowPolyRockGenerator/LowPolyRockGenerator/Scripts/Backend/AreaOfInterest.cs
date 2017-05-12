using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AreaOfInterest : MonoBehaviour
{  
    public int scale;    

    void OnDrawGizmos ()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(scale, scale, scale));
    }

}
