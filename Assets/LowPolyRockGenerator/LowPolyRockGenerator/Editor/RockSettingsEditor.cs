using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RockSettings))]
public class RockSettingsEditor : PropertyDrawer
{
    bool fold;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        
        property.serializedObject.Update();        

        SerializedProperty verts = property.FindPropertyRelative("verts");
        SerializedProperty randomType = property.FindPropertyRelative("randomType");
        SerializedProperty min = property.FindPropertyRelative("min");
        SerializedProperty max = property.FindPropertyRelative("max");
        SerializedProperty product = property.FindPropertyRelative("unitCircleProduct");

        fold = EditorGUILayout.Foldout(fold, "Settings");
        if (fold)
        {
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(verts, new GUIContent("Vertices"));

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(randomType, new GUIContent("Type"));

            if (randomType.enumValueIndex == 0)
            {
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(min, new GUIContent("Min"));
                EditorGUILayout.PropertyField(max, new GUIContent("Max"));
            }
            else
            {
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(product, new GUIContent("Multiplier"));
            }
        }      

        property.serializedObject.ApplyModifiedProperties();
    }
}