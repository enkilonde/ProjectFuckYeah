using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RobToolsNameSpace;

[CustomEditor(typeof(CharacterV3))]
public class CustomV3CharacterEditing : Editor {


	public override void OnInspectorGUI()
	{
		CharacterV3 charaInstance = target as CharacterV3;
		EditorStyles.label.wordWrap = true;

		#region speed
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Speed", EditorStyles.boldLabel);
		charaInstance.minAltMaxSpeed = EditorGUILayout.Slider("Min altitude max speed", charaInstance.minAltMaxSpeed, 50f, 300f);
		charaInstance.maxAltMaxSpeed = EditorGUILayout.Slider("Max altitude max speed", charaInstance.maxAltMaxSpeed, 50f, 300f);
		charaInstance.minAltAccel = EditorGUILayout.Slider("Min altitude acceleration speed", charaInstance.minAltAccel, 1f, 100f);
		charaInstance.maxAltAccel = EditorGUILayout.Slider("Max altitude acceleration speed", charaInstance.maxAltAccel, 1f, 100f);
		charaInstance.deccelNoInput = EditorGUILayout.Slider("Decceleration speed on no input", charaInstance.deccelNoInput, 1f, 100f);
		EditorGUILayout.EndVertical();
		#endregion

		#region altitude
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Altitude", EditorStyles.boldLabel);
		charaInstance.minAltitude = EditorGUILayout.Slider("Min altitude", charaInstance.minAltitude, 0f, 300f);
		charaInstance.maxAltitude = EditorGUILayout.Slider("Max altitude", charaInstance.maxAltitude, 0f, 300f);
		charaInstance.maxVerticalAscentionSpeed = EditorGUILayout.Slider("Vertical ascension speed", charaInstance.maxVerticalAscentionSpeed, 0f, 300f);
		charaInstance.maxFallingSpeed = EditorGUILayout.Slider("Falling speed", charaInstance.maxFallingSpeed, 0f, 300f);
		charaInstance.verticalSpeedTransitionSpeed = EditorGUILayout.Slider("Transition vitesse verticale /sec", charaInstance.verticalSpeedTransitionSpeed, 0f, 300f);
		EditorGUILayout.EndVertical();
		#endregion

		#region lacet
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Lacet", EditorStyles.boldLabel);
		charaInstance.maxLacetSpeed = EditorGUILayout.Slider("Lacet speed", charaInstance.maxLacetSpeed, 0f, 1000f);
		charaInstance.lacetTransitionSpeed = EditorGUILayout.Slider("Lacet transition speed", charaInstance.lacetTransitionSpeed, 0f, 1000f);
		EditorGUILayout.EndVertical();
		#endregion

		#region lateralboost
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Lateral boost", EditorStyles.boldLabel);
		charaInstance.maxLateralSpeed = EditorGUILayout.Slider("Lateral boost speed", charaInstance.maxLateralSpeed, 0f, 500f);
		EditorGUILayout.EndVertical();
		#endregion

		#region inerty
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Inerty", EditorStyles.boldLabel);
		charaInstance.useInertyFeature = EditorGUILayout.Toggle("Use inerty feature", charaInstance.useInertyFeature);
		if(charaInstance.useInertyFeature)
		{
			EditorGUILayout.LabelField("Transition de l'inertie vers le forward du joueur", EditorStyles.boldLabel);
			charaInstance.transitionAngleDelta = EditorGUILayout.Slider("Angle transition angle/sec", charaInstance.transitionAngleDelta, 0f, 300f);
			EditorGUILayout.LabelField("Puissance du ralentissement lorsque le fwd du joueur est opposé à son inertie", EditorStyles.boldLabel);
			charaInstance.airResistance = EditorGUILayout.Slider("Air resistance dist/sec", charaInstance.airResistance, 0f, 300f);
		}
		EditorGUILayout.EndVertical();
		#endregion

		#region hit
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Get hit", EditorStyles.boldLabel);
		charaInstance.transitionTimeToReflectVector = EditorGUILayout.Slider("Durée transition vers rebond (sec)", charaInstance.transitionTimeToReflectVector, 0f, 0.5f);
		charaInstance.minSpeedRebondForce = EditorGUILayout.Slider("Force de rebond à vitesse min (dist)", charaInstance.minSpeedRebondForce, 0f, 50f);
		charaInstance.maxSpeedRebondForce = EditorGUILayout.Slider("Force de rebond à vitesse max (dist)", charaInstance.maxSpeedRebondForce, 0f, 50f);
		charaInstance.deccelHitPorcent = EditorGUILayout.Slider("Decceleration sur impact (%)", charaInstance.deccelHitPorcent, 0f, 100f);
		EditorGUILayout.EndVertical();
		#endregion

		#region boost
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Boost", EditorStyles.boldLabel);
		charaInstance.timeToReload = EditorGUILayout.Slider("Time to fully reload (/sec)", charaInstance.timeToReload, 1f, 20f);
		charaInstance.timeToUnload = EditorGUILayout.Slider("Time to fully unload (/sec)", charaInstance.timeToUnload, 1f, 20f);
		charaInstance.maxSpeedwhileBoost = EditorGUILayout.Slider("Max speed while boost (dist/sec)", charaInstance.maxSpeedwhileBoost, 1f, 400f);
		charaInstance.maxBoostSpeed_DeccelerationSpeed = EditorGUILayout.FloatField("Max boost speed decceleration (/sec)", charaInstance.maxBoostSpeed_DeccelerationSpeed);
		charaInstance.accelerationSpeedWhileBoost = EditorGUILayout.Slider("Acceleration speed while boost (multiplier)", charaInstance.accelerationSpeedWhileBoost, 0f, 20f);
		EditorGUILayout.EndVertical();
		#endregion

		#region score
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Score", EditorStyles.boldLabel);
		charaInstance.speedScoreGain = EditorGUILayout.FloatField("Score speed gain while target (/sec)", charaInstance.speedScoreGain);
		EditorGUILayout.EndVertical();
		#endregion

		//Force update les modifs
		SceneView.RepaintAll();
	}

	private void OnSceneGUI()
	{
		CharacterV3 charaInstance = target as CharacterV3;

		#region speed
		Handles.color = Color.green;
		Handles.DrawLine(Vector3.up * charaInstance.minAltitude, Vector3.up * charaInstance.minAltitude + Vector3.forward * charaInstance.minAltMaxSpeed);
		DrawString("Min altitude max speed /sec", Vector3.up * charaInstance.minAltitude + Vector3.forward * charaInstance.minAltMaxSpeed, Color.green);
		Handles.DrawLine(Vector3.up * charaInstance.maxAltitude, Vector3.up * charaInstance.maxAltitude + Vector3.forward * charaInstance.maxAltMaxSpeed);
		DrawString("Max altitude max speed /sec", Vector3.up * charaInstance.maxAltitude + Vector3.forward * charaInstance.maxAltMaxSpeed, Color.green);
		#endregion

		#region altitude
		Handles.color = Color.green;
		Handles.DrawLine(Vector3.up * charaInstance.minAltitude, Vector3.up * charaInstance.maxAltitude);
		DrawString("Min altitude", Vector3.up * charaInstance.minAltitude, Color.green);
		DrawString("Max altitude", Vector3.up * charaInstance.maxAltitude, Color.green);
		Handles.color = Color.cyan;
		//Monter
		Handles.DrawLine(Vector3.forward * 4f + Vector3.up * charaInstance.minAltitude, Vector3.forward * 4f + Vector3.up * charaInstance.minAltitude + Vector3.up * charaInstance.maxVerticalAscentionSpeed);
		Handles.DrawLine(Vector3.forward * 4f + Vector3.up * charaInstance.minAltitude + Vector3.up * charaInstance.maxVerticalAscentionSpeed, Vector3.forward * 4f + Vector3.up * charaInstance.minAltitude + Vector3.up * charaInstance.maxVerticalAscentionSpeed + Vector3.up * -4f + Vector3.forward * 4f);
		Handles.DrawLine(Vector3.forward * 4f + Vector3.up * charaInstance.minAltitude + Vector3.up * charaInstance.maxVerticalAscentionSpeed, Vector3.forward * 4f + Vector3.up * charaInstance.minAltitude + Vector3.up * charaInstance.maxVerticalAscentionSpeed + Vector3.up * -4f + Vector3.forward * -4f);
		DrawString("Vertical ascension /sec", Vector3.forward * 4f + Vector3.up * charaInstance.minAltitude + Vector3.up * charaInstance.maxVerticalAscentionSpeed, Color.cyan);
		//Descendre
		Handles.DrawLine(Vector3.forward * 8f + Vector3.up * charaInstance.minAltitude, Vector3.forward * 8f + Vector3.up * charaInstance.minAltitude + Vector3.up * charaInstance.maxFallingSpeed);
		Handles.DrawLine(Vector3.forward * 8f + Vector3.up * charaInstance.minAltitude, Vector3.forward * 8f + Vector3.up * charaInstance.minAltitude + Vector3.up * 4f + Vector3.forward * 4f);
		Handles.DrawLine(Vector3.forward * 8f + Vector3.up * charaInstance.minAltitude, Vector3.forward * 8f + Vector3.up * charaInstance.minAltitude + Vector3.up * 4f + Vector3.forward * -4f);
		DrawString("Falling speed /sec", Vector3.forward * 4f + Vector3.up * charaInstance.minAltitude + Vector3.up * charaInstance.maxFallingSpeed, Color.cyan);
		#endregion

		#region lateral boost
		Handles.color = Color.red;
		Handles.DrawLine(Vector3.up + Vector3.right * -charaInstance.maxLateralSpeed, Vector3.up + Vector3.right * charaInstance.maxLateralSpeed);
		DrawString("Lateral boost /sec", Vector3.up + Vector3.right * charaInstance.maxLateralSpeed, Color.red);
		#endregion


		#region inerty
		if(charaInstance.useInertyFeature)
		{
			Handles.color = Color.blue;
			Handles.DrawLine(Vector3.zero, new Vector3(0.5f,0f,0.5f) * charaInstance.minAltMaxSpeed);
			Handles.DrawLine(Vector3.forward * charaInstance.minAltMaxSpeed * 0.75f, new Vector3(0.5f,0f,0.5f) * charaInstance.minAltMaxSpeed * 0.75f);
			Handles.DrawLine(new Vector3(0.5f,0f,0.5f) * charaInstance.minAltMaxSpeed * 0.75f, new Vector3(0.5f,0f,0.5f) * charaInstance.minAltMaxSpeed * 0.75f + -Vector3.right * 4f);
			Handles.DrawLine(new Vector3(0.5f,0f,0.5f) * charaInstance.minAltMaxSpeed * 0.75f, new Vector3(0.5f,0f,0.5f) * charaInstance.minAltMaxSpeed * 0.75f + Vector3.forward * 4f);
			DrawString("Inerty transition", new Vector3(0.5f,0f,0.5f) * charaInstance.minAltMaxSpeed * 0.75f, Color.blue);
		}
		#endregion

	}


	void DrawString(string text, Vector3 worldPos, Color? colour = null) {
		UnityEditor.Handles.BeginGUI();

		var restoreColor = GUI.color;

		if (colour.HasValue) GUI.color = colour.Value;
		var view = UnityEditor.SceneView.currentDrawingSceneView;
		Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);

		if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
		{
			GUI.color = restoreColor;
			UnityEditor.Handles.EndGUI();
			return;
		}

		Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
		GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y), text);
		GUI.color = restoreColor;
		UnityEditor.Handles.EndGUI();
	}
}
