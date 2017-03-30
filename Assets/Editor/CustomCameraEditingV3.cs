using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RobToolsNameSpace;

[CustomEditor(typeof(CameraV3Placement))]
public class CustomCameraEditingV3 : Editor {


	private void OnSceneGUI()
	{

		CameraV3Placement camInstance = target as CameraV3Placement;

		//TODO condition a revoir, c'est pas tres clean
		if(!camInstance.posA_recul || !camInstance.posB_recul)
		{
			Debug.Log("CamV2 n'avait pas d'objet pour guider la position de la caméra. T'inquiettes, je viens de lui en créer :)");

			camInstance.mainCam = Camera.main.GetComponent<Camera>();

			GameObject posObject = new GameObject("Empty");

			#region camTransformInterpolation
			GameObject container = RobToolsClass.UberInstantiate(posObject, "Recul container", camInstance.transform, Vector3.zero, Quaternion.identity);
			GameObject instancePos = RobToolsClass.UberInstantiate(posObject, "PosA", container.transform, new Vector3(0, 1, -10), Quaternion.identity);
			camInstance.posA_recul = instancePos.transform;
			instancePos = RobToolsClass.UberInstantiate(posObject, "PosB", container.transform, new Vector3(0, 3, -20), Quaternion.identity);
			camInstance.posB_recul = instancePos.transform;
			#endregion

			#region camtargetInterpolation
			container = RobToolsClass.UberInstantiate(posObject, "Target LooakAt container", camInstance.transform, Vector3.zero, Quaternion.identity);
			instancePos = RobToolsClass.UberInstantiate(posObject, "PosA", container.transform, new Vector3(0, 0, 3), Quaternion.identity);
			camInstance.posA_targetLookAt = instancePos.transform;
			instancePos = RobToolsClass.UberInstantiate(posObject, "PosB", container.transform, new Vector3(0, 0, 10), Quaternion.identity);
			camInstance.posB_targetLookAt = instancePos.transform;
			#endregion

			return;
		}

		Transform handleTransform = camInstance.transform;
		Quaternion handleRotation = (Tools.pivotRotation == PivotRotation.Local) ? handleTransform.rotation : Quaternion.identity;
		Vector3 p0 = camInstance.posA_recul.position;// handleTransform.TransformPoint(camInstance.posA_recul.position);
		Vector3 p1 = camInstance.posB_recul.position; //handleTransform.TransformPoint(camInstance.posB_recul.position);
		Vector3 lap0 = camInstance.posA_targetLookAt.position;// handleTransform.TransformPoint(camInstance.posA_targetLookAt.position);
		Vector3 lap1 = camInstance.posB_targetLookAt.position;// handleTransform.TransformPoint(camInstance.posB_targetLookAt.position);

		//Make the gizmos
		EditorGUI.BeginChangeCheck();
		p0 = Handles.DoPositionHandle(p0, handleRotation);
		if(EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(camInstance, "Move Point");
			EditorUtility.SetDirty(camInstance);
			camInstance.posA_recul.position = p0;// handleTransform.InverseTransformPoint(p0);
		}
		EditorGUI.BeginChangeCheck();
		p1 = Handles.DoPositionHandle(p1, handleRotation);
		if(EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(camInstance, "Move Point");
			EditorUtility.SetDirty(camInstance);
			camInstance.posB_recul.position = p1;//handleTransform.InverseTransformPoint(p1);
		}

		EditorGUI.BeginChangeCheck();
		lap0 = Handles.DoPositionHandle(lap0, handleRotation);
		if(EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(camInstance, "Move Point");
			EditorUtility.SetDirty(camInstance);
			camInstance.posA_targetLookAt.position = lap0;// handleTransform.InverseTransformPoint(lap0);
		}
		EditorGUI.BeginChangeCheck();
		lap1 = Handles.DoPositionHandle(lap1, handleRotation);
		if(EditorGUI.EndChangeCheck())
		{
			Undo.RecordObject(camInstance, "Move Point");
			EditorUtility.SetDirty(camInstance);
			camInstance.posB_targetLookAt.position = lap1;// handleTransform.InverseTransformPoint(lap1);
		}
	
		//Draw positionLines
		Handles.color = Color.white;
		Handles.DrawLine(p0, p1);
		Handles.DrawLine(lap0, lap1);

		//Draw max retard Cam
		Handles.color = Color.red;
		Vector3 _p0Tang = Vector3.Cross(Vector3.up, p0).normalized;	//TODO Rendre ça pas bugé quand l'avatar se deplace (le feedback hein)
		Handles.DrawLine(p0 -_p0Tang * camInstance.minSpeedLateralDelay, p0 + _p0Tang * camInstance.minSpeedLateralDelay);
		Vector3 _p1Tang = Vector3.Cross(Vector3.up, p1).normalized;
		Handles.DrawLine(p1 -_p1Tang * camInstance.maxSpeedLateralDelay, p1 + _p1Tang * camInstance.maxSpeedLateralDelay);
		//Draw lateral lines
		Handles.DrawLine(p0 - _p0Tang * camInstance.minSpeedLateralDelay, p1 - _p1Tang * camInstance.maxSpeedLateralDelay);
		Handles.DrawLine(p0 + _p0Tang * camInstance.minSpeedLateralDelay, p1 + _p1Tang * camInstance.maxSpeedLateralDelay);

		//Draw embrayages
//		ShowDoted(camInstance.steps, p0, p1, 1);
//		ShowDoted(camInstance.steps, lap0, lap1, 0.25f);

		if(Previsualize){
			//Draw cam to lookat target
			Handles.color = Color.green;
			Handles.DrawLine(camInstance.currentPos, camInstance.currentTargetLookAt);
		}
	}

	bool Previsualize = false;

	public override void OnInspectorGUI()
	{
		CameraV3Placement camInstance = target as CameraV3Placement;
		EditorStyles.label.wordWrap = true;

		if(GUILayout.Button("Reset positions"))
		{
			ResetPositions(camInstance);
		}

		float _speedCam = EditorGUILayout.Slider("vitesse de slide de la caméra le long de la ligne blanche", camInstance.speedCamPosAlongLine, 0.1f, 5f);
		camInstance.speedCamPosAlongLine = _speedCam;

		//Délai lateral de la camera, minimum et maximum
		camInstance.minSpeedLateralDelay = EditorGUILayout.Slider("Retard latéral sur boost à vitesse minimum", camInstance.minSpeedLateralDelay, 0.1f, 5f);
		camInstance.maxSpeedLateralDelay = EditorGUILayout.Slider("Retard latéral sur boost à vitesse maximum", camInstance.maxSpeedLateralDelay, 0.1f, 10f);
		//Vitesse de transition de la cam
		camInstance.lateralSpeedTransition = EditorGUILayout.Slider("Vitesse transition latérale cam", camInstance.lateralSpeedTransition, 1f, 10f);

		#region CamEffects
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Camera Effects", EditorStyles.boldLabel);
		camInstance.minApperture = EditorGUILayout.Slider("Min apperture", camInstance.minApperture, 0f, 1f);
		camInstance.maxApperture = EditorGUILayout.Slider("Max apperture", camInstance.maxApperture, 0f, 1f);
		camInstance.maxAppertureSteps = (int) EditorGUILayout.Slider("Nb d'étapes de l'apperture", camInstance.maxAppertureSteps, 0, 30);
		EditorGUILayout.EndVertical();
		#endregion

		Previsualize = EditorGUILayout.Toggle("Previsualize", Previsualize);

		if(Previsualize){
			EditorGUILayout.LabelField("Cette partie là ne fonctionne pas en realTime. Elle ne sert qu'a la prévisualisation dans la scene view");
			float _t = EditorGUILayout.Slider("Cam position", camInstance._t_recul, 0f, 1f);
			camInstance._t_recul = _t;
			camInstance.SetCameraAlongLine();
			float _t2 = EditorGUILayout.Slider("LookAt position", camInstance._t_lookAt, 0f, 1f);
			camInstance._t_lookAt = _t2;
			camInstance.SetTargetAlongLine();
			float _t3 = EditorGUILayout.Slider("Lateral delay position", camInstance._t_lateralDelay, 0f, 1f);
			camInstance._t_lateralDelay = _t3;
			camInstance.SetCameraLateralDelay();

			//Maj cam effect
			camInstance.SetApperture();

		}

		//Force update les modifs
		SceneView.RepaintAll();
	}
		

	void ResetPositions(CameraV3Placement camInst)
	{
		camInst.posA_recul.localPosition = new Vector3(0, 1, -10);
		camInst.posB_recul.localPosition = new Vector3(0, 5, -20);
		camInst.posA_targetLookAt.localPosition = new Vector3(0, 0, 3);
		camInst.posB_targetLookAt.localPosition = new Vector3(0, 2, 10);
	}

	void ShowDoted(int _steps, Vector3 _a, Vector3 _b, float _lineSize)
	{
		Vector3 _middlePoint;
		Vector3 _tang;
		for (int i = 0; i < _steps; i++) {

			float i_t = i/(float)_steps;

			_middlePoint = Vector3.Lerp(_a, _b, i_t);
			_tang = Vector3.Cross(Vector3.up, _middlePoint).normalized * _lineSize;// * 0.05f;

			Handles.DrawDottedLine(_middlePoint - _tang, _middlePoint + _tang, 2);
		}
	}

}
