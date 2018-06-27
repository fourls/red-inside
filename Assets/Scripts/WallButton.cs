using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallButton : ConditionalObject, IInteractable {
	public bool onAtStart = false;
	private bool on = false;

	public string ContextText { get { return "Toggle"; } }
	public bool IsActive { get { return true; }}

	void Start() {
		on = onAtStart;
	}

	public override bool IsOn() {
		return on;
	}

	public void Interact(PlayerController player) {
		on = !on;
	}

}
