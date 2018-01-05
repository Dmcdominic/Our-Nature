using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameConstants : MonoBehaviour {

	public static GameConstants GC_Static;

	public EventSystem baseES;

	[HideInInspector]
	public Character[] Characters;
	public GameObject[] UI_Parents;
	public EventSystem[] MES;

	[HideInInspector]
	public Resource[] Resources_Collectible, Resources_Craftable;
	[HideInInspector]
	public Equipment[] Equipment_;

	public Dictionary<string, int> Totals;

	public void Awake() {
		// Singleton management
		if (GC_Static == null) {
			GC_Static = this;

		} else if (GC_Static != this) {
			Destroy (gameObject);
			return;
		}

		// Get items from Resources folder in Assets
		getResources ();

		// Update Character references
		for (int i = 0; i < 4; i++) {
			if (Characters [i] != null) {
				Characters [i].UI_parent = UI_Parents [i];
				Characters [i].Multi_ES = MES [i];
			} else {
				Debug.Log ("Missing character " + (i+1) + " in GameConstants");
			}
		}

		// Initialize Resource Totals array
		Totals = new Dictionary<string, int>();

		foreach (Resource res in Resources_Collectible) {
			Totals.Add (res.name, res.startingAmount);
		}
		foreach (Resource res in Resources_Craftable) {
			Totals.Add (res.name, res.startingAmount);
		}
	}

	// Get items from Resources folder in Assets
	private void getResources() {
		// Loading resource items
		Object[] objArray = Resources.LoadAll("", typeof(Resource));

		int collectibleCount = 0;
		int craftableCount = 0;
		foreach (Object obj in objArray) {
			if (((Resource)obj).Craftable) {
				craftableCount++;
			} else {
				collectibleCount++;
			}
		}

		Resources_Collectible = new Resource[collectibleCount];
		Resources_Craftable = new Resource[craftableCount];
		collectibleCount = 0;
		craftableCount = 0;

		foreach (Object obj in objArray) {
			if (((Resource)obj).Craftable) {
				Resources_Craftable [craftableCount] = ((Resource)obj);
				craftableCount++;
			} else {
				Resources_Collectible [collectibleCount] = ((Resource)obj);
				collectibleCount++;
			}
		}

		// Loading equipment items
		objArray = Resources.LoadAll("", typeof(Equipment));
		Equipment_ = new Equipment[objArray.Length];

		for (int i = 0; i < objArray.Length; i++) {
			Equipment_ [i] = (Equipment)objArray [i];
		}

		// Loading Characters
		objArray = Resources.LoadAll("", typeof(Character));
		if (objArray.Length != 4) {
			Debug.Log ("More or less than 4 characters found in Resources folder");
		}
		Characters = new Character[objArray.Length];

		for (int i = 0; i < objArray.Length; i++) {
			Characters[i] = (Character)objArray [i];
		}
	}
}
