using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable {
	string ContextText { get; }
	bool IsActive { get; }
	void Interact(PlayerController player);
}
