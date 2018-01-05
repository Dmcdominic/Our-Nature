using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTagManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Character thisChar = CustomButton.getParentChar (gameObject);
		if (thisChar) {
			GetComponent<Text> ().text = thisChar.Name;
		}
	}

}
