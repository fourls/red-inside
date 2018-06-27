using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PickUppable : MonoBehaviour, IActivatesFloorButtons, IInteractable {
	[Header("References")]
	public Collider2D normalCollider;
	public Collider2D triggerCollider;
	[Header("Values")]
	public string itemName;
	public bool activatesFloorButtons = true;


	public bool ActivatesFloorButtons { get { return activatesFloorButtons; }}
	public string ContextText { get { return "Pick Up"; } }
	public bool IsActive { get { return !isPickedUp; } }

	protected Rigidbody2D rb2d;

	protected bool isPickedUp = false;

	protected virtual void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
	}

	public virtual void OnPickup() {
		rb2d.isKinematic = true;
		rb2d.velocity = Vector2.zero;
		rb2d.angularVelocity = 0;
		rb2d.rotation = 0;
		isPickedUp = true;
		
	}

	public virtual void OnDrop() {
		rb2d.isKinematic = false;
		isPickedUp = false;
	}

	public virtual void OnUse() {

	}

	public void Interact(PlayerController player) {
		player.PickUpObject(this);
	}

	public void MoveTo(Vector3 position) {
		transform.position = position;
	}
}
