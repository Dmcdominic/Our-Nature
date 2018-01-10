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

	public static Dictionary<string, int> Totals;
	public static Dictionary<string, bool> Obtained;
	public static Dictionary<string, bool> Unlocked;

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
	}

	// Get items from Resources folder in Assets
	private void getResources() {
		// Loading resource items
		Object[] objArray = Resources.LoadAll("", typeof(Resource));

		int collectibleCount = 0;
		int craftableCount = 0;
		foreach (Object obj in objArray) {
			if (((Resource)obj).Craftable()) {
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
			if (((Resource)obj).Craftable()) {
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

		// Assigning each item its respective character based on Resource path
		for (int i = 0; i < 4; i++) {
			Object[] playerItems = Resources.LoadAll ("Player " + (i+1).ToString (), typeof(Item));
			foreach (Item item in playerItems) {
				item.character = Characters [i];
			}
		}

		// Initialize Resource "Totals" and "Obtained" dicts
		Totals = new Dictionary<string, int>();
		Obtained = new Dictionary<string, bool> ();

		foreach (Resource res in Resources_Collectible) {
			Totals.Add (res.name, res.startingAmount);
			Obtained.Add (res.name, (res.Total () > 0));
		}
		foreach (Resource res in Resources_Craftable) {
			Totals.Add (res.name, res.startingAmount);
			Obtained.Add (res.name, (res.Total () > 0));
		}

		// Initialize Resource "Unlocked" dict
		Unlocked = new Dictionary<string, bool> ();

		foreach (Resource res in Resources_Collectible) {
			Unlocked.Add (res.name, (res.checkIfUnlocked()));
		}
		foreach (Resource res in Resources_Craftable) {
			Unlocked.Add (res.name, (res.checkIfUnlocked()));
		}
	}

	// Update the "Unlocked" dictionary to be set true for any resources recently unlocked
	public static void checkAllUnlocked() {
		foreach (Resource res in GC_Static.Resources_Collectible) {
			if (!Unlocked[res.name]) {
				Unlocked.Add (res.name, (res.checkIfUnlocked()));
			}
		}
		foreach (Resource res in GC_Static.Resources_Craftable) {
			if (!Unlocked[res.name]) {
				Unlocked.Add (res.name, (res.checkIfUnlocked()));
			}
		}
	}
}
