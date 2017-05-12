using UnityEngine;
using UnityEditor;

using RockGeneration;

[CustomEditor (typeof (RockGenerator))]
public class RockGenEditor : Editor
{
    RockGenerator generator;

    SerializedProperty settings;


    void OnEnable ()
    {
        settings = serializedObject.FindProperty("settings");

        generator = (RockGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update(); 
    

        
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(settings, new GUIContent("Settings"), true);

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        generator.seed = EditorGUILayout.IntSlider("Seed", generator.seed, 0, 10000);

        if (GUILayout.Button ("Random Seed"))
        {
            generator.RandomSeed();
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Preview Rock"))
        {
            generator.PreviewRock(false);
        }

        if (GUILayout.Button("Preview Random"))
        {
            generator.PreviewRock(true);
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Generate Rock"))
        {
            generator.folderLocation = OpenFilePanel.Open();
            if (generator.folderLocation != string.Empty)
                generator.EditorExport(false);
        }

        if (GUILayout.Button("Generate Random"))
        {
            generator.folderLocation = OpenFilePanel.Open();
            if (generator.folderLocation != string.Empty)
                generator.EditorExport(true);
        }

        GUILayout.EndHorizontal();
    }
        
        
    
}
