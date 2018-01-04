using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUIManager : MonoBehaviour {

	private Item item;

	void Awake() {
		if (GetComponent<ButtonReferenceProvider> () != null) {
			item = GetComponent<ButtonReferenceProvider> ().item;
		} else {
			Debug.Log ("No ButtonReferenceProvider found on button: " + gameObject);
		}
	}

	// Display 
	void DisplayIcon() {
		if (item.Icon == null) {
			Debug.Log ("Missing icon for: " + item.ItemName + " equipment");
			return;
		}

		gameObject.GetComponent<Image> ().sprite = item.Icon;

		ColorBlock CB = GetComponent<CustomButton> ().colors;
		CB.normalColor = Color.white;
		CB.highlightedColor = Color.gray;
		GetComponent<CustomButton> ().colors = CB;
	}
}
