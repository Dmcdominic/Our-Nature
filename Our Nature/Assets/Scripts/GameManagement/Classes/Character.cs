using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName="Character")]
[System.Serializable]
public class Character : ScriptableObject {
	
	public string Name;
	public int index;
	public GameObject UI_parent;
	public EventSystem Multi_ES;

}
