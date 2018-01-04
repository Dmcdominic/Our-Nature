using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for items (e.g. resources, equipment, etc.)
public class Item : ScriptableObject {

	public string ItemName;
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

	public Item[] getRecipe() {
		return Recipe;
	}

	// Returns the ingredient required in slot "index" to craft this item
	public Item getIngredient (int index) {
		if (Recipe == null) {
			return null;
		}
		return Recipe [index];
	}

	// Make a recipe from 9 Items, using null for empty slot
	public static Item[] newRecipe(Item a, Item b, Item c, Item d, Item e, Item f, Item g, Item h, Item i) {
		Item[] tempRecipe = {a, b, c, d, e, f, g, h, i};
		return tempRecipe;
	}
}
