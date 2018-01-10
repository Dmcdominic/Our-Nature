using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for items (e.g. resources, equipment, etc.)
public class Item : ScriptableObject {

	public string Description;
	public Sprite Icon;

	public Item[] Recipe;

	[HideInInspector]
	public Character character;

	public virtual string getClassification() {
		return "Item";
	}

	public virtual bool usableInCrafting() {
		return false;
	}

	public bool Obtained() {
		return GameConstants.Obtained [this.name];
	}

	public bool Unlocked() {
		return GameConstants.Unlocked [this.name];
	}

	public bool checkIfUnlocked() {
		if (Recipe == null) {
			return true;
		}

		foreach (Item ingredient in Recipe) {
			if (ingredient && !GameConstants.Obtained [ingredient.name]) {
				return false;
			}
		}
		return true;
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
