using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Resource class, extends Item
[CreateAssetMenu(menuName="Resource")]
public class Resource : Item {
	
	public int startingAmount;


	public int Total() {
		return GameConstants.Totals [this.name];
	}

	// Use this to increment the amount of this resource
	public void addToTotal(int amount) {
		GameConstants.Totals[this.name] += amount;
		if (!GameConstants.Obtained [this.name]) {
			GameConstants.Obtained [this.name] = true;
			GameConstants.checkAllUnlocked ();
		}
		EventManager.TriggerEvent ("ChangeResource" + this.name);
	}

	// Use this to remove from the total of this resource
	public bool subtractFromTotal(int amount) {
		if (Total() < amount) {
			return false;
		} else {
			GameConstants.Totals[this.name] -= amount;
			EventManager.TriggerEvent ("ChangeResource" + this.name);
			return true;
		}
	}

	public bool Craftable() {
		return (Recipe != null && Recipe.Length == 9);
	}

	public override string getClassification ()
	{
		if (Craftable()) {
			return "Resource - Craftable";
		} else {
			return "Resource - Collectible";
		}
	}

	public override bool usableInCrafting ()
	{
		return true;
	}
}
