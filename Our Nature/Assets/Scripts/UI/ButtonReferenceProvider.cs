﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonReferenceProvider : MonoBehaviour {

	public Item item;
	public GameObject craftingHighlight;
	public Text resourceTag;
	public Text resourceCounter;
	public Image resourceIcon;

	void Awake() {
		if (item) {
			if (resourceTag) {
				resourceTag.text = item.name;
			}
			if (resourceIcon) {
				resourceIcon.sprite = item.Icon;
			}
		} else {
			//Debug.Log ("Null item in ButtonItemLink on button: " + gameObject);
		}
	}
		
	public void UpdateCounter() {
		if (item) {
			if (item.usableInCrafting() && resourceCounter != null) {
				resourceCounter.text = ((Resource)item).Total.ToString();
			}
		}
	}

}
