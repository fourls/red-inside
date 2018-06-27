using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalWall : ObjectWithPrerequisites {	
	public Collider2D toggledCollider;

	protected override void Update() {
		base.Update();
		toggledCollider.enabled = IsOn();
	}
}
