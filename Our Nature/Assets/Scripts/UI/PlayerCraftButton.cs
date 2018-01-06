using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCraftButton : MonoBehaviour {

	public Image CraftIcon;
	public GameObject CraftableHighlight;

	private Item CraftableItem;

	void Start() {
		Character parentChar = CustomButton.getParentChar (gameObject);
		for (int i = 0; i < 4; i++) {
			if (parentChar.Equals (GameConstants.GC_Static.Characters[i])) {
				CraftingManager.CM_Static.CraftButtons [i] = this;
			}
		}
	}

	public void updateItem(Item newItem) {
		if (newItem) {
			CraftableItem = newItem;
			CraftIcon.sprite = newItem.Icon;
			CraftableHighlight.SetActive (true);
		} else {
			CraftableItem = null;
			CraftIcon.sprite = CommonSprites.CS_Static.InvalidItem;
			CraftableHighlight.SetActive (false);
		}
	}

	public bool tryCraftItem() {
		if (!CraftableItem) {
			return false;
		}

		if (CraftableItem.GetType () == typeof(Resource)) {
			((Resource)CraftableItem).addToTotal (1);
		} else if (CraftableItem.GetType () == typeof(Equipment)) {
			((Equipment)CraftableItem).Equip ();
		} else {
			Debug.Log ("CraftableItem type not recognized as resource or equipment");
		}

		CraftingManager.clearAll (false);
		return true;
	}
}
