using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultiEventSystem : EventSystem {

	protected override void OnEnable() {
		// Do not assign EventSystem.current
	}

	protected override void Update() {
		EventSystem originalCurrent = EventSystem.current;
		current = this; // Temporarily assign this EventSystem to be the globally current one
		base.Update();
		current = originalCurrent;
	}
}
