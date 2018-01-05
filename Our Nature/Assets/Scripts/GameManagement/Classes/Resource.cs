using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Resource class, extends Item
[CreateAssetMenu(menuName="Resource")]
public class Resource : Item {
	public int startingAmount;
	public bool Craftable;

	public int Total() {
		return GameConstants.GC_Static.Totals [this.name];
	}

	public void addToTotal(int amount) {
		GameConstants.GC_Static.Totals[this.name] += amount;
		EventManager.TriggerEvent ("ChangeResource" + this.name);
	}

	public bool subtractFromTotal(int amount) {
		if (Total() < amount) {
			return false;
		} else {
			GameConstants.GC_Static.Totals[this.name] -= amount;
			EventManager.TriggerEvent ("ChangeResource" + this.name);
			return true;
		}
	}

	public override string getClassification ()
	{
		if (Craftable) {
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
