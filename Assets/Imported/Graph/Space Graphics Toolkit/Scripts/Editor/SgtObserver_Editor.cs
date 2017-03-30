using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SgtObserver))]
public class SgtObserver_Editor : SgtEditor<SgtObserver>
{
	protected override void OnInspector()
	{
		SgtObserver.maxVelocityMagnitude = EditorGUILayout.Slider("Etirement maximum de l'effet NOVA", SgtObserver.maxVelocityMagnitude, 1f, 100f);
//		DrawDefault("RollAngle");
	}
}