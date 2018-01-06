using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour {

	public SharedCraftingIcon[] CraftingSlots;
	[HideInInspector]
	public PlayerCraftButton[] CraftButtons;

	private static bool clearing;

	// Singleton management
	public static CraftingManager CM_Static;
	public void Awake() {
		if (CM_Static == null) {
			CM_Static = this;

		} else if (CM_Static != this) {
			Destroy (gameObject);
			return;
		}
	}

	public void updateCraftingStatus() {
		if (clearing) {
			return;
		}

		// Testing array equality

		Item[] gridItems = getItemArray ();
		foreach (Item item in GameConstants.GC_Static.Resources_Craftable) {
			if (item.checkRecipe(gridItems)) {
				updateCraftButtons (item);
				return;
			}
		}

		foreach (Item item in GameConstants.GC_Static.Equipment_) {
			if (gridItems.Equals (item.Recipe)) {
				updateCraftButtons (item);
				return;
			}
		}
	}

	private Item[] getItemArray() {
		Item[] items = new Item[9];
		for (int i = 0; i < 9; i++) {
			items [i] = CraftingSlots [i].currentResource;
		}
		return items;
	}

	private static void updateCraftButtons(Item craftable) {
		Character charCanCraft = null;
		if (craftable) {
			charCanCraft = craftable.character;
		}
		for (int i = 0; i < 4; i++) {
			if (GameConstants.GC_Static.Characters [i] == charCanCraft) {
				// Craft button can now craft item
				CM_Static.CraftButtons[i].updateItem(craftable);
			} else {
				// Craft button can no longer craft anything
				if (CM_Static.CraftButtons [i]) {
					CM_Static.CraftButtons [i].updateItem (null);
				}
			}
		}
	}

	public static void clearAll(bool refund) {
		clearing = true;
		foreach (SharedCraftingIcon SCI in CM_Static.CraftingSlots) {
			if (refund) {
				SCI.clear (true);
			} else {
				SCI.clear (false);
			}
		}
		updateCraftButtons (null);
		clearing = false;
	}
}
