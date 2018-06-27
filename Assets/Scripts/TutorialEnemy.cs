using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : PickUppable, IShootable {
	public GameObject bloodSpatter;

	private Animator animator;

	void Start() {
		animator = GetComponent<Animator>();
	}

	public void OnShot() {
		animator.SetTrigger("shot");
		bloodSpatter.SetActive(true);
		rb2d.isKinematic = true;
		foreach(Collider2D collider in GetComponents<Collider2D>()) {
			collider.enabled = false;
		}
	}

	public override void OnPickup() {
		base.OnPickup();
		normalCollider.enabled = false;
	}

	public override void OnDrop() {
		base.OnDrop();
		normalCollider.enabled = true;
	}
}
