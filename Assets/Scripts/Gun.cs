using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : PickUppable {
	[Header("Asset References")]
	public GameObject gunLinePrefab;
	public GameObject gunTargetPrefab;
	public Sprite inRangeSprite;
	public Sprite outOfRangeSprite;
	[Header("Scene References")]
	public GameObject gunTarget;
	public Transform barrel;
	[Header("Values")]
	public float cooldownTime = 0.5f;
	public float gunLineShowTime = 0.2f;
	public float maxDot = 0.404f;
	public float rotLerpSpeed = 6f;


	private float cooldown = 0;
	private float wantedRotation = 0;

	void Start() {
		gunTarget = Instantiate(gunTargetPrefab);
	}

	void Update() {
		rb2d.rotation = Mathf.LerpAngle(rb2d.rotation,wantedRotation,rotLerpSpeed * Time.deltaTime);
	}

	void LateUpdate() {
		cooldown -= Time.deltaTime;

		
		Vector2 targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		gunTarget.transform.position = targetPoint;
		
		Vector2 direction = (targetPoint - (Vector2)barrel.position);
		direction.Normalize();

		if(Vector2.Dot(direction,Vector2.right*transform.localScale.x) <= maxDot) {
			gunTarget.GetComponent<SpriteRenderer>().sprite = outOfRangeSprite;
			if(GameManager.ins.player.IsCarriedObject(this))
				RotateTowardsDirection(Vector2.right);
		} else {
			gunTarget.GetComponent<SpriteRenderer>().sprite = inRangeSprite;
			if(GameManager.ins.player.IsCarriedObject(this))
				RotateTowardsDirection(direction);
		}
	}

	void RotateTowardsDirection(Vector2 direction) {
		wantedRotation = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;

		if(direction.x < 0)
			wantedRotation = wantedRotation - 180;
	}

	public override void OnPickup() {
		gunTarget.GetComponent<SpriteRenderer>().enabled = true;
		base.OnPickup();
	}

	public override void OnDrop() {
		gunTarget.GetComponent<SpriteRenderer>().enabled = false;
		base.OnDrop();
	}

	public override void OnUse() {
		if(cooldown > 0)
			return;
		cooldown = cooldownTime;

		Vector2 targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		Vector2 direction = (targetPoint - (Vector2)barrel.position).normalized;

		if(Vector2.Dot(direction,Vector2.right*transform.localScale.x) <= maxDot)
			return;

		triggerCollider.enabled = false;
		RaycastHit2D hit = Physics2D.Raycast((Vector2)barrel.position,direction,50f,GameManager.ins.shootableLayers);
		triggerCollider.enabled = true;

		if(hit.collider != null) {
			ShowGunLine(hit.point);

			IShootable shotObject = hit.collider.gameObject.GetComponent<IShootable>();
			if(shotObject != null) {
				shotObject.OnShot();
			}
		}
	}

	void ShowGunLine(Vector2 hitPoint) {
		GameObject gunLine = Instantiate(gunLinePrefab);
		LineRenderer renderer = gunLine.GetComponent<LineRenderer>();

		renderer.SetPosition(0,barrel.position);
		renderer.SetPosition(1,hitPoint);
	}

	void OnDestroy() {
		Destroy(gunTarget);
	}
}
