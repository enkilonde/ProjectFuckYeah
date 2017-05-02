using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerV3 : MonoBehaviour {

	[Range(1, 4)]
	public int playerNumero = 1;



	public enum AllAxis
	{
		StickG_X,
		StickG_Y,
		StickD_X,
		StickD_Y,
		Cross_X,
		Cross_Y,
		LT_RT,
		LT,
		RT
	}
	public AllAxis horizontalRotation = AllAxis.StickG_X;
//	public AllAxis lateralBoostLeft = AllAxis.LT;
//	public AllAxis lateralBoostRight = AllAxis.RT;
	public AllAxis horizontalCamera = AllAxis.StickD_X;
	public AllAxis verticalCamera = AllAxis.StickD_Y;
	public AllAxis accelAxis = AllAxis.RT;
	public AllAxis verticalBoost = AllAxis.LT;
	public enum AllButtons
	{
		XBOX_A,
		XBOX_X,
		XBOX_Y,
		XBOX_B,
		LB,
		RB,
		StickG,
		StickD,
		Back,
		Start
	}
//	public AllButtons accelButton = AllButtons.XBOX_A;
//	public AllButtons shockWave = AllButtons.XBOX_X;
	public AllButtons LatLeftBoost = AllButtons.LB;
	public AllButtons LatRightBoost = AllButtons.RB;
	public AllButtons lockOn = AllButtons.StickD;
	public AllButtons forwardBoost = AllButtons.XBOX_A;
    public AllButtons useItem = AllButtons.XBOX_B;

    #region Axes
    public string Get_HorizontalRotInput ()
	{
		return playerNumero.ToString() + "_" + horizontalRotation.ToString() + "_Axis";
	}
	public string Get_HorizontalCameraInput()
	{
		return playerNumero.ToString() + "_" + horizontalCamera.ToString() + "_Axis";
	}
	public string Get_VerticalCameraInput()
	{
		return playerNumero.ToString() + "_" + verticalCamera.ToString() + "_Axis";
	}
//	public string Get_LateralBoostLeftInput()
//	{
//		return playerNumero.ToString() + "_" + lateralBoostLeft.ToString() + "_Axis";
//	}
//	public string Get_LateralBoostRightInput()
//	{
//		return playerNumero.ToString() + "_" + lateralBoostRight.ToString() + "_Axis";
//	}
	public string Get_AccelAxisInput()
	{
		return playerNumero.ToString() + "_" + accelAxis.ToString() + "_Axis";
	}
	public string Get_VertcalBoostAxisInput()
	{
		return playerNumero.ToString() + "_" + verticalBoost.ToString() + "_Axis";
	}
	#endregion

	#region buttons
//	public string Get_AccelButtonInput ()
//	{
//		return playerNumero.ToString() + "_" + accelButton.ToString();
//	}
//	public string Get_SchockWaveInput ()
//	{
//		return playerNumero.ToString() + "_" + shockWave.ToString();
//	}
	public string Get_LatLeftBoostInput()
	{
		return playerNumero.ToString() + "_" + LatLeftBoost.ToString();
	}
	public string Get_LatRightBoostInput()
	{
		return playerNumero.ToString() + "_" + LatRightBoost.ToString();
	}
	public string Get_LockOnInput()
	{
		return playerNumero.ToString() + "_" + lockOn.ToString();
	}
	public string Get_ForwardBoostInput()
	{
		return playerNumero.ToString() + "_" + forwardBoost.ToString();
	}
    public string Get_UseItemInput()
    {
        return playerNumero.ToString() + "_" + useItem.ToString();
    }

    #endregion

}
