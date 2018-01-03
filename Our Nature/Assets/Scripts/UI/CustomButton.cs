using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomButton : Button {

	public static bool quitting;

	public EventSystem eventSystem;
	public Character character;

	private EventSystem attemptedES;
	private BaseEventData attemptedED;

	protected override void Start() {
		if (quitting) {
			return;
		}
		eventSystem = null;
		character = null;
		getEventSystem();
		base.Start();
	}

	// Find the MultiEventSystem that this button should associate with
	private void getEventSystem() {

		if (GameConstants.GameConstantsStatic == null) {
			Debug.Log ("GameConstantsStatic returned null for object: " + gameObject);
			return;
		}

		GameObject nextParent = gameObject;

		while (nextParent != null) {
			foreach (Character Char in GameConstants.Characters) {
				if (nextParent == Char.UI_parent) {
					eventSystem = Char.Multi_ES;
					character = Char;
					return;
				}
			}

			if (nextParent.transform.parent == null) {
				nextParent = null;
			} else {
				nextParent = nextParent.transform.parent.gameObject;
			}
		}

		if (nextParent == null) {
			eventSystem = null;
			character = null;
		}
	}

	void OnApplicationQuit() {
		quitting = true;
	}

	// MultiEventSystem edits
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

	// MultiEventSystem edits
	public override void Select() {
		if (eventSystem.alreadySelecting) {
			return;
		}

		eventSystem.SetSelectedGameObject (gameObject);
	}

	// Called when button is "highlighted"
	public override void OnSelect(BaseEventData eventData) {
		if (EventSystem.current != eventSystem && eventSystem != null) {
			attemptedES = EventSystem.current;
			attemptedED = eventData;
			trySelectPrevious();
			return;
		} else if (GetComponent<SharedCraftingIcon>() != null) {
			GetComponent<SharedCraftingIcon> ().OnSelectIcon (EventSystem.current);
		}

		EventSystem.current.GetComponent<MultiEventSystem> ().prevButton = this;
		base.OnSelect(eventData);
	}

	// Called on deselect. Edited to only deselect if current ES is the intended one
	public override void OnDeselect(BaseEventData eventData) {
		if (EventSystem.current != eventSystem && eventSystem != null) {
			return;
		} else if (GetComponent<SharedCraftingIcon>() != null) {
			GetComponent<SharedCraftingIcon> ().OnDeselectIcon (EventSystem.current);
		}

		base.OnDeselect (eventData);
	}

	// Try to select the button that the MultiEventSystem previously had selected
	public void trySelectPrevious() {
		if (attemptedES == GameConstants.GameConstantsStatic.baseES) {
			return;
		}

		if (attemptedES.alreadySelecting) {
			Invoke ("trySelectPrevious", 0.01f);
			return;
		} else {
			EventSystem temp = EventSystem.current;
			EventSystem.current = attemptedES;
			attemptedES.GetComponent<MultiEventSystem> ().prevButton.OnSelect (attemptedED);
			attemptedES.SetSelectedGameObject (attemptedES.GetComponent<MultiEventSystem> ().prevButton.gameObject);
			EventSystem.current = temp;
		}
	}
}
