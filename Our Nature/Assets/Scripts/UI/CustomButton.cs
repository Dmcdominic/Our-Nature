using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomButton : Button {

	public EventSystem eventSystem;

	protected override void Awake ()
	{
		base.Awake ();
		//eventSystem = GetComponent<EventSystemProvider> ().eventSystem;	// Replaced by getEventSystem();
		getEventSystem();
	}

	private void getEventSystem() {
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
