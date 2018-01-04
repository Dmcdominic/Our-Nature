using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameConstants : MonoBehaviour {

	public static GameConstants GameConstantsStatic;
	public EventSystem baseES;

	public Character[] Characters;
	public GameObject[] UI_Parents;
	public EventSystem[] MES;

	public Item[] Items;

	public void Awake() {
		// Singleton management
		if (GameConstantsStatic == null) {
			GameConstantsStatic = this;

		} else if (GameConstantsStatic != this) {
			Destroy (gameObject);
			return;
		}

		// Update Character references
		for (int i = 0; i < 4; i++) {
			if (Characters [i] != null) {
				Characters [i].UI_parent = UI_Parents [i];
				Characters [i].Multi_ES = MES [i];
			} else {
				Debug.Log ("Missing character " + (i+1) + " in GameConstants");
			}
		}
	}
}
