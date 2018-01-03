using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for items (e.g. resources, equipment, etc.)
public class Item {

	public string Name;
	public string Classification;
	public string Description;
	public Sprite Icon;

	public Item[] Recipe;

	public Character character;

	// Item constructor
	public Item(string name, string description, Sprite icon, Item[] recipe, Character charact) {
		Name = name;
		Description = description;
		Icon = icon;
		Recipe = recipe;
		character = charact;
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


// Resource class, extends Item
public class Resource : Item {
	public int Total;

	public Resource(string name, string description, Sprite icon, Item[] recipe, Character charact) : base(name, description, icon, recipe, charact) {
		Classification = "Resource";
	}
}


// Equipment class, extends Item
public class Equipment : Item {
	public bool Equipped;

	public Equipment(string name, string description, Sprite icon, Item[] recipe, Character charact) : base(name, description, icon, recipe, charact) {
		Classification = "Equipment";
	}
}