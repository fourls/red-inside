using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Exit : ObjectWithPrerequisites {
	public Transform otherSide;


	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player") && IsOn()) {
			if(otherSide != null)
				other.transform.position = otherSide.position;
		}
	}
}
