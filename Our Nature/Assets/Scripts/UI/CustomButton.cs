using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomButton : Button {

	public static bool quitting;

	public EventSystem eventSystem;
	public Character character;

	public Item item;
	public GameObject craftingHighlight;

	private EventSystem attemptedES;
	private BaseEventData attemptedED;

	protected override void Awake() {
		ButtonReferenceProvider BRP = GetComponent<ButtonReferenceProvider> ();
		if (BRP) {
			item = BRP.item;
			craftingHighlight = BRP.craftingHighlight;
		}

		base.Awake ();
	}

	protected override void Start() {
		if (quitting) {
			return;
		}

		eventSystem = null;
		character = null;
		getEventSystem();

		if (character) {
			EventManager.StartListening ("ClearCraftingSelect" + character.Name, deselectForCrafting);
		}
		onClick.AddListener (OnClickDelegate);

		base.Start();
	}

	// Find the MultiEventSystem that this button should associate with
	private void getEventSystem() {

		if (GameConstants.GC_Static == null) {
			Debug.Log ("GC_Static returned null for object: " + gameObject);
			return;
		}

		GameObject nextParent = gameObject;

		while (nextParent != null) {
			foreach (Character Char in GameConstants.GC_Static.Characters) {
				if (Char != null) {
					if (nextParent == Char.UI_parent) {
						eventSystem = Char.Multi_ES;
						character = Char;
						return;
					}
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

	void OnClickDelegate() {
		if (item != null && item.usableInCrafting() && character) {
			EventManager.TriggerEvent ("ClearCraftingSelect" + character.Name);
			selectForCrafting ();
		}
	}

	public void selectForCrafting() {
		eventSystem.GetComponent<MultiEventSystem> ().selectedCraftingItem = item;
		craftingHighlight.SetActive (true);
	}

	public void deselectForCrafting() {
		if (eventSystem.GetComponent<MultiEventSystem> ().selectedCraftingItem == item) {
			eventSystem.GetComponent<MultiEventSystem> ().selectedCraftingItem = null;
		}
		if (craftingHighlight) {
			craftingHighlight.SetActive (false);
		}
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
		if (attemptedES == GameConstants.GC_Static.baseES) {
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
