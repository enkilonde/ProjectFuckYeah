using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterV1))]
public class CharaStateToUIFeedback : MonoBehaviour {

	public Transform scrollersContainer;
	public Scrollbar[] scrollers = new Scrollbar[4];

	private CharacterV1 characterScript;

	void Start () {
		characterScript = GetComponent<CharacterV1>();
		if(scrollersContainer != null)
		{
			for (int i = 0; i < scrollers.Length; i++) {
				switch(i)
				{
				case 0:
					scrollers[i] = scrollersContainer.FindChild("Speed").GetComponentInChildren<Scrollbar>();
					break;
				case 1:
					scrollers[i] = scrollersContainer.FindChild("Incl").GetComponentInChildren<Scrollbar>();
					break;
				case 2:
					scrollers[i] = scrollersContainer.FindChild("Ass").GetComponentInChildren<Scrollbar>();
					break;
				case 3:
					scrollers[i] = scrollersContainer.FindChild("Lacet").GetComponentInChildren<Scrollbar>();
					break;
				default:
					break;
				}
			}
		}
		else
		{
			Debug.LogError("Variable /Scrollers Container/ non assignée ! Les feedbacks UI sont désactivés. Il faut y mettre LE gameobject contenant les objets /Lacet, Incl, Ass, Speed/. Sinon, le script ne peut pas trouver les UI à actualiser");
			this.enabled = false;
		}
	}

	void Update () {

		for (int i = 0; i < scrollers.Length; i++) {
			switch(i)
			{
			case 0:
				scrollers[i].value = GetNormalizedValue(characterScript.currentMoveSpeed, characterScript.min_moveSpeed, characterScript.max_moveSpeed);
				break;
			case 1:
				scrollers[i].value = GetNormalizedValue(characterScript.currentInclinaisonSpeed, characterScript.min_inclSpeed, characterScript.max_inclSpeed);
				break;
			case 2:
				scrollers[i].value = GetNormalizedValue(characterScript.currentAssietteSpeed, characterScript.min_assSpeed, characterScript.max_assSpeed);
				break;
			case 3:
				scrollers[i].value = GetNormalizedValue(characterScript.currentLacetSpeed, characterScript.min_laceSpeed, characterScript.max_laceSpeed);
				break;
			default:
				break;
			}
		}

	}

	float GetNormalizedValue(float value, float min, float max)
	{
		float _tempMax = max + Mathf.Abs(min);
		float _tempVal = value + Mathf.Abs(min);
		_tempVal /= _tempMax;
		return _tempVal;
	}

	void OnValidate()
	{
		if(scrollers.Length != 4)
		{
			Debug.LogWarning("Ne resize pas ce tableau !");
			scrollers = new Scrollbar[4];
		}
	}
}
