using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameConstants : MonoBehaviour {

	public static GameConstants GameConstantsStatic;
	public EventSystem baseES;

	public static Character[] Characters;
	public static Item[] items;

	// Parent object of each player's UI
	public GameObject UI_P1;
	public GameObject UI_P2;
	public GameObject UI_P3;
	public GameObject UI_P4;

	// MultiEventSystem controlling each player's UI
	public EventSystem MES_P1;
	public EventSystem MES_P2;
	public EventSystem MES_P3;
	public EventSystem MES_P4;

	public void Awake() {
		// Singleton management
		if (GameConstantsStatic == null) {
			GameConstantsStatic = this;

			if (UI_P1 == null || UI_P2 == null || UI_P3 == null || UI_P4 == null || MES_P1 == null || MES_P2 == null || MES_P3 == null || MES_P4 == null) {
				Debug.Log ("Empty field(s) in GameConstants");
			}

		} else if (GameConstantsStatic != this) {
			Destroy (gameObject);
		}

		// Character array management
		Character A = new Character("Rabbit", UI_P1, MES_P1);
		Character B = new Character("Woodpecker", UI_P2, MES_P2);
		Character C = new Character("Earthworm", UI_P3, MES_P3);
		Character D = new Character("Turtle", UI_P4, MES_P4);
		Characters = new Character[] {A, B, C, D};

		// Item info here
	}
}
