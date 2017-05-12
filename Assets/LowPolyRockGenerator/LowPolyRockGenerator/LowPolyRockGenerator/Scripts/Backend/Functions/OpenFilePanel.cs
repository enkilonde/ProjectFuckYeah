using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OpenFilePanel : MonoBehaviour {

    /// <summary>
    /// Opens a Windows File Panel.
    /// </summary>
    /// <param name="method"></param>
    public static string Open()
    {
        string t = EditorUtility.SaveFilePanel("Save Rock", Application.dataPath, "Procedural Rock.obj", "Obj");

        if (!string.IsNullOrEmpty(t))
        {
            return t;
        }
        else
        {
            Debug.LogError("Export Cancelled");
            return "";            
        }
    }
}
