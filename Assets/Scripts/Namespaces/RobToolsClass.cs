using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RobToolsNameSpace
{

	public static class RobToolsClass {

		/// <summary>
		/// Gets the porcent from value.
		/// </summary>
		/// <returns>The porcent from value.</returns>
		/// <param name="_percent">Percent.</param>
		/// <param name="_max">Max.</param>
		public static float GetValueFromPercent(float _percent, float _max)
		{
			return (_max * _percent) / 100f;
		}

		/// <summary>
		/// Gets the value from percent.
		/// </summary>
		/// <returns>The value from percent.</returns>
		/// <param name="_value">Value.</param>
		/// <param name="_max">Max.</param>
		public static float GetPercentFromValue(float _value, float _max)
		{
			return (_value * 100f) / _max;
		}

		/// <summary>
		/// Gets the normalized value (0 to 1).
		/// </summary>
		/// <returns>The normalized value.</returns>
		/// <param name="value">Value.</param>
		/// <param name="min">Minimum.</param>
		/// <param name="max">Max.</param>
		public static float GetNormalizedValue(float value, float min, float max)
		{
			if(min > max)
				Debug.LogWarning("min float is superior to max value ! The result of GetNormalizedValue will be incorect");
			float _tempMax = max + Mathf.Abs(min);
			float _tempVal = value + Mathf.Abs(min);
			_tempVal /= _tempMax;
			return _tempVal;
		}

		/// <summary>
		/// More complette version of Instantiate.
		/// </summary>
		/// <returns>The instantiate.</returns>
		/// <param name="prefab">Prefab.</param>
		/// <param name="instName">Name.</param>
		/// <param name="instParent">Parent.</param>
		/// <param name="instLocalPosition">Local position.</param>
		/// <param name="intLocalRotation">Local rotation.</param>
		public static GameObject UberInstantiate(GameObject prefab, string instName, Transform instParent, Vector3 instLocalPosition, Quaternion instLocalRotation)
		{
			GameObject _instance = (GameObject) GameObject.Instantiate(prefab);
			_instance.name = instName;
			_instance.transform.parent = instParent;
			_instance.transform.position = instLocalPosition;
			_instance.transform.rotation = instLocalRotation;
			return _instance;
		}

		//TODO error
		/// <summary>
		/// Remap
		/// </summary>
		/// <returns>The range value.</returns>
		/// <param name="value">Value.</param>
		/// <param name="oldMin">Old minimum.</param>
		/// <param name="oldMax">Old max.</param>
		/// <param name="newMin">New minimum.</param>
		/// <param name="newMax">New max.</param>
		public static float MappedRangeValue(float value, float oldMin, float oldMax, float newMin, float newMax)
		{
			//float oldRange = oldMax - oldMin;
			float newRange = newMax - newMin;
			return (((value - oldMin) * newRange) / newRange) + newMin;
		}


	}

	public static class ExtensionMethods {

		public static float Remap (this float value, float from1, float to1, float from2, float to2) {
			return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
		}

        public static float pow(this float value, float power)
        {
            return Mathf.Pow(value, power);
        }

	}




}