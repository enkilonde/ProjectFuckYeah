using UnityEngine;
using UnityEditor;
using RockGeneration;

[CustomEditor(typeof(RockBuilder))]
public class BuilderEditor : Editor
{
   RockBuilder builder;

    void OnEnable ()
    {         
        builder = (RockBuilder)target;
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Preview Rock"))
        {
            builder.PreviewRock(false);
        }

        if (GUILayout.Button("Preview Random"))
        {
            Random.seed = Random.Range(0, 100000);
            builder.PreviewRock(false);
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Generate Rock"))
        {
            builder.PreviewRock(true);
        }

        if (GUILayout.Button("Generate Random"))
        {
            builder.RandomSeed();
            builder.PreviewRock(true);
        }

        GUILayout.EndHorizontal();

    }


}
