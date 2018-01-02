using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomButton : Button {

	public static bool quitting;

	public EventSystem eventSystem;

	protected override void Start() {
		if (quitting) {
			return;
		}
		getEventSystem();
		base.Start();
	}

	private void getEventSystem() {
		if (eventSystem != null) {
			return;
		}

		if (GameConstants.GameConstantsStatic == null) {
			Debug.Log ("GameConstantsStatic returned null for object: " + gameObject);
			return;
		}

		GameObject nextParent = gameObject;
		GameObject P1_TargetUI = GameConstants.GameConstantsStatic.UI_P1;
		GameObject P2_TargetUI = GameConstants.GameConstantsStatic.UI_P2;
		GameObject P3_TargetUI = GameConstants.GameConstantsStatic.UI_P3;
		GameObject P4_TargetUI = GameConstants.GameConstantsStatic.UI_P4;

		while (nextParent != null) {
			if (nextParent == P1_TargetUI) {
				eventSystem = GameConstants.GameConstantsStatic.MES_P1;
				return;
			} else if (nextParent == P2_TargetUI) {
				eventSystem = GameConstants.GameConstantsStatic.MES_P2;
				return;
			} else if (nextParent == P3_TargetUI) {
				eventSystem = GameConstants.GameConstantsStatic.MES_P3;
				return;
			} else if (nextParent == P4_TargetUI) {
				eventSystem = GameConstants.GameConstantsStatic.MES_P4;
				return;
			}

			nextParent = nextParent.transform.parent.gameObject;
		}

		if (nextParent == null) {
			Debug.Log ("No identifying player UI found by CustomButton script");
		}
	}

	void OnApplicationQuit() {
		quitting = true;
	}

	public override void OnPointerDown(PointerEventData eventData) {
		if (eventData.button != PointerEventData.InputButton.Left) {
			return;
		}

		// Selection tracking
		if (IsInteractable () && navigation.mode != Navigation.Mode.None) {
			eventSystem.SetSelectedGameObject (gameObject, eventData);
		}

		base.OnPointerDown (eventData);
	}

	public override void Select() {
		if (eventSystem.alreadySelecting) {
			return;
		}

		eventSystem.SetSelectedGameObject (gameObject);
	}
}
