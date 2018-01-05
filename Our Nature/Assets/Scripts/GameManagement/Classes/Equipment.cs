using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum upgradeEffect {runningSpeed, diggingSpeed, diggingCapability, essenceUnlock};

// Equipment class, extends Item
[CreateAssetMenu(menuName="Equipment")]
public class Equipment : Item {
	[HideInInspector]
	public bool Equipped;
	public upgradeEffect EffectType;
	public float UpgradePercentage;

	public void Equip() {
		Equipped = true;
		EventManager.TriggerEvent ("EquipmentEnabled" + this.name);
	}

	public override string getClassification ()
	{
		string text = "Equipment - ";

		if (EffectType == upgradeEffect.runningSpeed) {
			string.Concat (text, "Running Speed Upgrade: ", UpgradePercentage*100, "%");
		} else if (EffectType == upgradeEffect.diggingSpeed) {
			string.Concat (text, "Digging Speed Upgrade: ", UpgradePercentage*100, "%");
		} else if (EffectType == upgradeEffect.diggingCapability) {
			string.Concat (text, "Digging Potential Upgrade");
		} else if (EffectType == upgradeEffect.essenceUnlock) {
			string.Concat (text, "Essence Discovery");
		}

		return text;
	}

	public override bool usableInCrafting ()
	{
		return false;
	}
}
