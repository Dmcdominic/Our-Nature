using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReferenceProvider : MonoBehaviour {

	public Item item;
	public GameObject craftingHighlight;

	void Awake() {
		if (item == null) {
			Debug.Log ("Null item in ButtonItemLink on button: " + gameObject);
		}
	}

}
