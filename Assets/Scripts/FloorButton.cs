using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : ConditionalObject {
	private List<IActivatesFloorButtons> activators = new List<IActivatesFloorButtons>();

	void OnTriggerEnter2D(Collider2D other) {
		if(other.GetComponent<IActivatesFloorButtons>() != null) {
			IActivatesFloorButtons activator = other.GetComponent<IActivatesFloorButtons>();

			if(activator.ActivatesFloorButtons) {
				activators.Add(activator);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.GetComponent<IActivatesFloorButtons>() != null) {
			IActivatesFloorButtons activator = other.GetComponent<IActivatesFloorButtons>();

			if(activators.Contains(activator)) {
				activators.Remove(activator);
			}
		}
	}

	public override bool IsOn() {
		return activators.Count > 0;
	}
}
