using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character {
	
	public string Name;
	public GameObject UI_parent;
	public EventSystem Multi_ES;

	// Character constructor
	public Character(string name, GameObject ui_parent, EventSystem multi_es) {
		Name = name;
		UI_parent = ui_parent;
		Multi_ES = multi_es;
	}
}
