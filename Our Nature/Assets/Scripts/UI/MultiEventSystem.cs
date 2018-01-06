using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MultiEventSystem : EventSystem {

	public string clearButton;

	[HideInInspector]
	public Button prevButton;

	[HideInInspector]
	public Item selectedCraftingItem;

	protected override void OnEnable() {
		// Do not assign EventSystem.current
		Input.GetAxis(clearButton);
	}

	protected override void Update() {
		if (currentSelectedGameObject != null) {
			CustomButton customButton = currentSelectedGameObject.GetComponent<CustomButton> ();

			if (customButton != null) {
				if (customButton.eventSystem != this && customButton.eventSystem != null) {
					return;
				} else {
					prevButton = customButton;
				}
			} else if (currentSelectedGameObject.GetComponent<Button> () != null) {
				prevButton = currentSelectedGameObject.GetComponent<Button> ();
			} else {
				Debug.Log ("No button or customButton found for previous");
			}
		}

		EventSystem originalCurrent = EventSystem.current;
		current = this; // Temporarily assign this EventSystem to be the globally current one
		base.Update();
		checkClearPress ();
		current = originalCurrent;
	}

	private void checkClearPress() {
		if (Input.GetAxis (clearButton) > 0) {
			GameObject selectedObj = currentSelectedGameObject;
			if (selectedObj && selectedObj.GetComponent<CustomButton>()) {
				selectedObj.GetComponent<CustomButton> ().Clear ();
			}
		}
	}

	public bool ProperSelectButtonDown(Character Char) {
		if (Char && Char.Multi_ES != this) {
			return false;
		}

		if (Input.GetAxis(this.GetComponent<StandaloneInputModule> ().submitButton) > 0) {
			return true;
		}

		return false;
	}
}
