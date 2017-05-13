using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class ControllerV3 : MonoBehaviour {

    public static bool[] controllersSet = new bool[] { false, false, false, false };

    [Range(0, 3)]
	public int playerNumero = 1;

    public GamePadState state;
    //GamePadState prevState;

    public bool useKeyboard = false;

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
    private AllAxis horizontalRotation = AllAxis.StickG_X;
    //	public AllAxis lateralBoostLeft = AllAxis.LT;
    //	public AllAxis lateralBoostRight = AllAxis.RT;
    private AllAxis horizontalCamera = AllAxis.StickD_X;
    private AllAxis verticalCamera = AllAxis.StickD_Y;
    private AllAxis accelAxis = AllAxis.RT;
    private AllAxis verticalBoost = AllAxis.LT;
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
	private AllButtons LatLeftBoost = AllButtons.LB;
    private AllButtons LatRightBoost = AllButtons.RB;
    private AllButtons lockOn = AllButtons.StickD;
    private AllButtons forwardBoost = AllButtons.XBOX_A;
    private AllButtons useItem = AllButtons.XBOX_B;

    private void Update()
    {
        
        state = GamePad.GetState((PlayerIndex)playerNumero);

    }

    #region Axes
    public float Get_HorizontalRotInput ()
    {
        if (useKeyboard && state.ThumbSticks.Left.X == 0)
            return (Input.GetAxis(PlayerManager.keyboardIndex + "_" + horizontalRotation.ToString() + "_Axis"));
        return state.ThumbSticks.Left.X;
	}

	public float Get_HorizontalCameraInput()
    {
        if (useKeyboard && state.ThumbSticks.Right.X == 0)
            return (Input.GetAxis(PlayerManager.keyboardIndex + "_" + horizontalCamera.ToString() + "_Axis"));
        return state.ThumbSticks.Right.X;
	}

	public float Get_VerticalCameraInput()
    {
        if (useKeyboard && state.Triggers.Right == 0)
            return (Input.GetAxis(PlayerManager.keyboardIndex + "_" + verticalCamera.ToString() + "_Axis"));
        return state.ThumbSticks.Right.Y;
    }

	public float Get_AccelAxisInput()
	{
        if (useKeyboard && state.Triggers.Right == 0)
            return (Input.GetAxis(PlayerManager.keyboardIndex + "_" + accelAxis.ToString() + "_Axis"));
        return state.Triggers.Right;
	}

	public float Get_VertcalBoostAxisInput()
    {
        if (useKeyboard && state.Triggers.Left == 0)
            return (Input.GetAxis(PlayerManager.keyboardIndex + "_" + verticalBoost.ToString() + "_Axis"));
        return state.Triggers.Left;
	}
	#endregion

	#region buttons
	public float Get_LatLeftBoostInput()
    {
        if (useKeyboard && 1 - (float)state.Buttons.Y == 0)
            return (Input.GetAxisRaw(PlayerManager.keyboardIndex + "_" + LatLeftBoost.ToString()));
        return 1 - (float)state.Buttons.LeftShoulder;
	}

	public float Get_LatRightBoostInput()
    {
        if (useKeyboard && 1 - (float)state.Buttons.Y == 0)
            return (Input.GetAxisRaw(PlayerManager.keyboardIndex + "_" + LatRightBoost.ToString()));
        return 1 - (float)state.Buttons.RightShoulder;
	}

	public float Get_LockOnInput()
    {
        if (useKeyboard && 1 - (float)state.Buttons.RightStick == 0)
            return (Input.GetAxisRaw(PlayerManager.keyboardIndex + "_" + lockOn.ToString()));
        return 1 - (float)state.Buttons.RightStick;
	}

	public float Get_ForwardBoostInput()
    {
        if (useKeyboard && 1 - (float)state.Buttons.A == 0)
            return (Input.GetAxisRaw(PlayerManager.keyboardIndex + "_" + forwardBoost.ToString()));
        return 1 - (float)state.Buttons.A;
	}

    public float Get_UseItemInput()
    {
        if (useKeyboard && 1 - (float)state.Buttons.B == 0)
            return (Input.GetAxisRaw(PlayerManager.keyboardIndex + "_" + useItem.ToString()));
        return 1 - (float)state.Buttons.B;
    }

    public float Get_StartInput()
    {
        return 1 - (float)state.Buttons.Start;
    }

    public float Get_SelectInput()
    {
        return 1 - (float)state.Buttons.Back;
    }

    #endregion

    void OnGUI()
    {
        return;


        string text = "Use left stick to turn the cube, hold A to change color\n";
        text += string.Format("IsConnected {0} Packet #{1}\n", state.IsConnected, state.PacketNumber);
        text += string.Format("\tTriggers {0} {1}\n", state.Triggers.Left, state.Triggers.Right);
        text += string.Format("\tD-Pad {0} {1} {2} {3}\n", state.DPad.Up, state.DPad.Right, state.DPad.Down, state.DPad.Left);
        text += string.Format("\tButtons Start {0} Back {1} Guide {2}\n", state.Buttons.Start, state.Buttons.Back, state.Buttons.Guide);
        text += string.Format("\tButtons LeftStick {0} RightStick {1} LeftShoulder {2} RightShoulder {3}\n", state.Buttons.LeftStick, state.Buttons.RightStick, state.Buttons.LeftShoulder, state.Buttons.RightShoulder);
        text += string.Format("\tButtons A {0} B {1} X {2} Y {3}\n", state.Buttons.A, state.Buttons.B, state.Buttons.X, state.Buttons.Y);
        text += string.Format("\tSticks Left {0} {1} Right {2} {3}\n", state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y, state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text);
    }

}



//#region Axes
//public string Get_HorizontalRotInput()
//{
//    return playerNumero.ToString() + "_" + horizontalRotation.ToString() + "_Axis";
//}
//public string Get_HorizontalCameraInput()
//{
//    return playerNumero.ToString() + "_" + horizontalCamera.ToString() + "_Axis";
//}
//public string Get_VerticalCameraInput()
//{
//    return playerNumero.ToString() + "_" + verticalCamera.ToString() + "_Axis";
//}
////	public string Get_LateralBoostLeftInput()
////	{
////		return playerNumero.ToString() + "_" + lateralBoostLeft.ToString() + "_Axis";
////	}
////	public string Get_LateralBoostRightInput()
////	{
////		return playerNumero.ToString() + "_" + lateralBoostRight.ToString() + "_Axis";
////	}
//public string Get_AccelAxisInput()
//{
//    return playerNumero.ToString() + "_" + accelAxis.ToString() + "_Axis";
//}
//public string Get_VertcalBoostAxisInput()
//{
//    return playerNumero.ToString() + "_" + verticalBoost.ToString() + "_Axis";
//}
//#endregion

//#region buttons
////	public string Get_AccelButtonInput ()
////	{
////		return playerNumero.ToString() + "_" + accelButton.ToString();
////	}
////	public string Get_SchockWaveInput ()
////	{
////		return playerNumero.ToString() + "_" + shockWave.ToString();
////	}
//public string Get_LatLeftBoostInput()
//{
//    return playerNumero.ToString() + "_" + LatLeftBoost.ToString();
//}
//public string Get_LatRightBoostInput()
//{
//    return playerNumero.ToString() + "_" + LatRightBoost.ToString();
//}
//public string Get_LockOnInput()
//{
//    return playerNumero.ToString() + "_" + lockOn.ToString();
//}
//public string Get_ForwardBoostInput()
//{
//    return playerNumero.ToString() + "_" + forwardBoost.ToString();
//}
//public string Get_UseItemInput()
//{
//    return playerNumero.ToString() + "_" + useItem.ToString();
//}

//#endregion
