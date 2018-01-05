using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonSprites : MonoBehaviour {

	public Sprite BlankSprite;

	public Sprite UnknownItem;
	public Sprite InvalidItem;
	public Sprite CraftingBlankIcon;

	// Singleton management
	public static CommonSprites CS_Static;
	public void Awake() {
		if (CS_Static == null) {
			CS_Static = this;

		} else if (CS_Static != this) {
			Destroy (gameObject);
			return;
		}
	}
}
