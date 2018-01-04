using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SharedCraftingIcon : MonoBehaviour {

	public Item currentItem;

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
			if (selectingES.Equals(GameConstants.GameConstantsStatic.Characters [i].Multi_ES)) {
				highlights [i].SetActive (true);
				selected [i] = true;
				return;
			}
		}
	}

	public void OnDeselectIcon(EventSystem selectingES) {
		for (int i = 0; i < 4; i++) {
			if (selectingES.Equals(GameConstants.GameConstantsStatic.Characters [i].Multi_ES)) {
				highlights [i].SetActive (false);
				selected [i] = false;
				return;
			}
		}
	}

	public void updateItem(Item newItem) {
		if (newItem == null) {
			currentItem = null;
			currentIcon.sprite = null;
		} else {
			currentItem = newItem;
			currentIcon.sprite = newItem.Icon;
		}

		// Call craftingManager function here
	}
}
