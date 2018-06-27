using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWithPrerequisites : ConditionalObject {
	public List<ConditionalObject> prerequisites = new List<ConditionalObject>();
	public bool inverted = false;


	public override bool IsOn() {
		return inverted ? !AllPrerequisitesOn() : AllPrerequisitesOn();
	}

	protected bool AllPrerequisitesOn() {
		List<ConditionalObject> notOn = prerequisites.FindAll((x) => !x.IsOn());
		return (notOn == null || notOn.Count == 0);
	}
}
