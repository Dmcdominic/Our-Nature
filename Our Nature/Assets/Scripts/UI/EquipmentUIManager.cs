﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUIManager : MonoBehaviour {

	public string Name;
	public Sprite EquipmentIcon;
	public string Description;

	// Use this for initialization
	void Start () {
		DisplayIcon ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Display 
	void DisplayIcon() {
		if (EquipmentIcon == null) {
			Debug.Log ("Missing icon for: " + Name + " equipment");
			return;
		}

		gameObject.GetComponent<Image> ().sprite = EquipmentIcon;

		ColorBlock CB = GetComponent<CustomButton> ().colors;
		CB.normalColor = Color.white;
		CB.highlightedColor = Color.gray;
		GetComponent<CustomButton> ().colors = CB;
	}
}