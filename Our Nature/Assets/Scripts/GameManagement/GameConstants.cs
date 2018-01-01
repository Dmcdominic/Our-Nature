using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameConstants : MonoBehaviour {

	public static GameConstants GameConstantsStatic;

	public GameObject UI_P1;
	public GameObject UI_P2;
	public GameObject UI_P3;
	public GameObject UI_P4;

	public EventSystem MES_P1;
	public EventSystem MES_P2;
	public EventSystem MES_P3;
	public EventSystem MES_P4;

	public void Awake() {
		if (GameConstantsStatic == null) {
			GameConstantsStatic = this;

			if (UI_P1 == null || UI_P2 == null || UI_P3 == null || UI_P4 == null || MES_P1 == null || MES_P2 == null || MES_P3 == null || MES_P4 == null) {
				Debug.Log ("Empty field(s) in GameConstants");
			}

		} else if (GameConstantsStatic != this) {
			Destroy (gameObject);
		}
	}
}
