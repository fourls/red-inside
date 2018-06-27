using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D),typeof(Animator))]
public class PlayerController : MonoBehaviour, IActivatesFloorButtons {
	[Header("References")]
	public Transform firstCheckpoint;
	public UnityEngine.UI.Text contextText;
	[Header("Movement Values")]
	public float speed;
	public float jumpForce;
	public float fallMultiplier;
	public float airMovementMultiplier = 0.5f;
	[Header("Other Values")]
	public float pickupRadius;
	public Vector2 holdOffset;

	public bool IsGrounded { 
		get {
			return Physics2D.Raycast(transform.position,Vector2.down,0.6f,GameManager.ins.groundLayer);
		}
	}
	public bool ActivatesFloorButtons { get { return true; }}

	private float vert;
	private float horiz;
	private bool jump = false;

	private Transform latestCheckpoint;
	private Vector2 currentHoldOffset;

	private PickUppable carriedObject = null;
	private bool IsCarryingObject { get { return carriedObject != null; }}

	private Rigidbody2D rb2d;
	private Animator animator;

	void Start() {
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		currentHoldOffset = holdOffset;
		latestCheckpoint = firstCheckpoint;
	}

	void Update() {
		contextText.text = "";

		vert = Input.GetAxisRaw("Vertical");
		horiz = Input.GetAxisRaw("Horizontal");

		animator.SetBool("walking",horiz != 0);
		if(horiz < 0) {
			transform.localScale = new Vector3(-1,1,1);
			currentHoldOffset = new Vector2(-holdOffset.x,holdOffset.y);
		} else if (horiz > 0) {
			transform.localScale = Vector3.one;
			currentHoldOffset = holdOffset;
		}
		if(carriedObject != null)
			carriedObject.transform.localScale = transform.localScale;

		if(Input.GetKeyDown(KeyCode.Space) && IsGrounded) {
			jump = true;
			animator.SetTrigger("jump");
		}

		if(Input.GetMouseButtonDown(0)) {
			if(IsCarryingObject) {
				UseObject();
			}
		} else if (Input.GetMouseButtonDown(1)) {
			// RMB drops current object
			if(IsCarryingObject) {
				DropObject();
				return;
			}
		}
		
		// checks if mouse is over a pickuppable and highlights it
		if(IsGrounded) {
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(mousePosition,Vector2.zero,0.1f,GameManager.ins.pickuppableLayer);
			if(hit.collider != null) {
				IInteractable usableObject = hit.collider.gameObject.GetComponent<IInteractable>();
				if(usableObject != null && usableObject.IsActive) {
					if(Vector2.Distance(transform.position,hit.collider.transform.position) <= pickupRadius) {
						contextText.text = usableObject.ContextText;
					}
					if(Input.GetMouseButtonDown(1)) {
						usableObject.Interact(this);
					}
				}
			}
		}
	}

	public void UseObject() {
		carriedObject.OnUse();
	}

	public void DropObject() {
		carriedObject.OnDrop();
		carriedObject = null;
	}

	public void PickUpObject(PickUppable pickuppable) {
		if(carriedObject != null)
			DropObject();
		pickuppable.OnPickup();
		carriedObject = pickuppable;
	}

	void FixedUpdate() {
		float groundedMultiplier = IsGrounded ? 1f : airMovementMultiplier;
		rb2d.velocity = new Vector2(horiz * speed * groundedMultiplier,rb2d.velocity.y);

		if(jump && IsGrounded) {
			rb2d.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
			jump = false;
		}

		if(rb2d.velocity.y < 0) {
			rb2d.velocity = new Vector2(rb2d.velocity.x,rb2d.velocity.y + Physics2D.gravity.y * (fallMultiplier-1));
		}

		animator.SetBool("grounded",IsGrounded);

	}

	void LateUpdate() {
		if(IsCarryingObject) {
			carriedObject.MoveTo(rb2d.position + currentHoldOffset);			
		}
	}

	public void Respawn() {
		transform.position = latestCheckpoint.position;
	}

	public void SetCheckpoint(Transform newCheckpoint) {
		latestCheckpoint = newCheckpoint;
		rb2d.velocity = Vector2.zero;
	}

	public void DestroyCarriedObject() {
		if(!IsCarryingObject)
			return;
		GameObject temp = carriedObject.gameObject;
		DropObject();
		Destroy(temp);
	}

	public bool IsCarriedObject(PickUppable pickuppable) {
		return pickuppable == carriedObject;
	}
}
