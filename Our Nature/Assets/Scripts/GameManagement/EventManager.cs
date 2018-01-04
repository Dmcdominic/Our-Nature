using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

	private Dictionary <string, UnityEvent> eventDictionary;

	// Singleton Management
	private static EventManager privateInstance;
	public static EventManager EM_Static {
		get {
			if (!privateInstance) {
				privateInstance = FindObjectOfType (typeof(EventManager)) as EventManager;

				if (!privateInstance) {
					Debug.Log ("Missing an active EventManager script in the scene");
				} else {
					privateInstance.Init ();
				}
			}

			return privateInstance;
		}
	}

	void Init() {
		if (eventDictionary == null) {
			eventDictionary = new Dictionary<string, UnityEvent> ();
		}
	}

	// Public methods
	public static void StartListening(string eventName, UnityAction listener) {
		UnityEvent thisEvent = null;
		if (EM_Static.eventDictionary.TryGetValue (eventName, out thisEvent)) {
			thisEvent.AddListener (listener);
		} else {
			thisEvent = new UnityEvent ();
			thisEvent.AddListener (listener);
			EM_Static.eventDictionary.Add (eventName, thisEvent);
		}
	}

	public static void StopListening(string eventName, UnityAction listener) {
		if (privateInstance == null) {
			return;
		}

		UnityEvent thisEvent = null;
		if (EM_Static.eventDictionary.TryGetValue (eventName, out thisEvent)) {
			thisEvent.RemoveListener (listener);
		}
	}

	public static void TriggerEvent (string eventName) {
		UnityEvent thisEvent = null;
		if (EM_Static.eventDictionary.TryGetValue (eventName, out thisEvent)) {
			thisEvent.Invoke ();
		}
	}
}
