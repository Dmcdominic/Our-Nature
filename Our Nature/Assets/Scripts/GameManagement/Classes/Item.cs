using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for items (e.g. resources, equipment, etc.)
public class Item : ScriptableObject {

	//public string ItemName;
	public string Description;
	public Sprite Icon;

	public Item[] Recipe;

	public Character character;

	public virtual string getClassification() {
		return "Item";
	}

	public virtual bool usableInCrafting() {
		return false;
	}

	public bool checkRecipe(Item[] craftingGrid) {
		if (craftingGrid.Length != 9 || Recipe.Length != 9) {
			return false;
		}

		for (int i = 0; i < 9; i++) {
			if (!sameItems(craftingGrid[i], Recipe[i])) {
				return false;
			}
		}
		
		return true;
	}

	public static bool sameItems(Item A, Item B) {
		if (A == null && B == null) {
			return true;
		} else if (A == null || B == null) {
			return false;
		} else if (A.name == B.name) {
			return true;
		} else {
			return false;
		}
	}
}
