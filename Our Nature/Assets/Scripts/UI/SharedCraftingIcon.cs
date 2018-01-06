using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SharedCraftingIcon : MonoBehaviour {

	public Resource currentResource;

	public GameObject[] highlights;
	[HideInInspector]
	public bool[] selected;

	private Image currentIcon;

	void Awake () {
		currentIcon = GetComponent<Image> ();
		selected = new bool[4];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnSelectIcon(EventSystem selectingES) {
		for (int i = 0; i < 4; i++) {
			if (selectingES.Equals(GameConstants.GC_Static.Characters [i].Multi_ES)) {
				highlights [i].SetActive (true);
				selected [i] = true;
				return;
			}
		}
	}

	public void OnDeselectIcon(EventSystem selectingES) {
		for (int i = 0; i < 4; i++) {
			if (selectingES.Equals(GameConstants.GC_Static.Characters [i].Multi_ES)) {
				highlights [i].SetActive (false);
				selected [i] = false;
				return;
			}
		}
	}

	public void updateItem(Item newItem) {
		Resource newResource = (Resource)newItem;
		if (newResource == currentResource) {
			return;
		}

		if (currentResource != null) {
			currentResource.addToTotal (1);
		}

		if (newItem == null) {
			clear (false);
		} else {
			if (newResource.subtractFromTotal(1)) {
				currentResource = newResource;
				currentIcon.sprite = newItem.Icon;
			} else {
				// You are out of that resource!
				return;
			}
		}

		CraftingManager.CM_Static.updateCraftingStatus ();
	}

	public void clear(bool refund) {
		if (refund) {
			updateItem (null);
		} else {
			currentResource = null;
			currentIcon.sprite = CommonSprites.CS_Static.CraftingBlankIcon;
		}
	}
}
