using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonReferenceProvider : MonoBehaviour {

	public Item item;
	public GameObject craftingHighlight;
	public Text resourceTag;
	public Text resourceCounter;

	void Awake() {
		if (item == null) {
			//Debug.Log ("Null item in ButtonItemLink on button: " + gameObject);
		}
	}

}
