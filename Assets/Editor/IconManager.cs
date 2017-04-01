using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class IconManager {

	//Not working
	/*
	public enum LabelIcon {
		Gray = 0,
		Blue,
		Teal,
		Green,
		Yellow,
		Orange,
		Red,
		Purple
	}

	public enum Icon {
		CircleGray = 0,
		CircleBlue,
		CircleTeal,
		CircleGreen,
		CircleYellow,
		CircleOrange,
		CircleRed,
		CirclePurple,
		DiamondGray,
		DiamondBlue,
		DiamondTeal,
		DiamondGreen,
		DiamondYellow,
		DiamondOrange,
		DiamondRed,
		DiamondPurple
	}

	private static GUIContent[] labelIcons;
	private static GUIContent[] largeIcons;

	public static void SetIcon( GameObject gObj, LabelIcon icon ) {
		if ( labelIcons == null ) {
			labelIcons = GetTextures( "sv_label_", string.Empty, 0, 8 );
		}

		SetIcon( gObj, labelIcons[(int)icon].image as Texture2D );
	}

	public static void SetIcon( GameObject gObj, Icon icon ) {
		if ( largeIcons == null ) {
			largeIcons = GetTextures( "sv_icon_dot", "_pix16_gizmo", 0, 16 );
		}

		SetIcon( gObj, largeIcons[(int)icon].image as Texture2D );
	}

	private static void SetIcon( GameObject gObj, Texture2D texture ) {
		var ty = typeof( EditorGUIUtility );
		var mi = ty.GetMethod( "SetIconForObject", BindingFlags.NonPublic | BindingFlags.Static );
		mi.Invoke( null, new object[] { gObj, texture } );
	}

	private static GUIContent[] GetTextures( string baseName, string postFix, int startIndex, int count ) {
		GUIContent[] guiContentArray = new GUIContent[count];

		var t = typeof( EditorGUIUtility );
		var mi = t.GetMethod( "IconContent", BindingFlags.NonPublic | BindingFlags.Static, null, new Type[] { typeof( string ) }, null );

		for ( int index = 0; index < count; ++index ) {
			guiContentArray[index] = mi.Invoke( null, new object[] { baseName + (object)(startIndex + index) + postFix } ) as GUIContent;
		}

		return guiContentArray;
	}
	*/


	public static void DrawIcon(GameObject gameObject, int idx)
	{
		var largeIcons = GetTextures("sv_label_", string.Empty, 0, 8);
		var icon = largeIcons[idx];
		var egu = typeof(EditorGUIUtility);
		var flags = BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic;
		var args = new object[] { gameObject, icon.image };
		var setIcon = egu.GetMethod("SetIconForObject", flags, null, new Type[]{typeof(UnityEngine.Object), typeof(Texture2D)}, null);
		setIcon.Invoke(null, args);
	}
	public static GUIContent[] GetTextures(string baseName, string postFix, int startIndex, int count)
	{
		GUIContent[] array = new GUIContent[count];
		for (int i = 0; i < count; i++)
		{
			array[i] = EditorGUIUtility.IconContent(baseName + (startIndex + i) + postFix);
		}
		return array;
	}
}