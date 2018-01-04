using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Resource class, extends Item
[CreateAssetMenu(menuName="Resource")]
public class Resource : Item {
	public int Total;
	public bool Craftable;

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
