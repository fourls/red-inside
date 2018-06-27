using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emancipator : ConditionalWall {
	void OnTriggerEnter2D(Collider2D other) {
		PickUppable pickuppable = other.GetComponent<PickUppable>();
		if(pickuppable != null) {
			Destroy(pickuppable.gameObject);
		}
	}
}
