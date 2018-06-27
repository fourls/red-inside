using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public PlayerController target;
	public Vector3 offset;
	public float lerpSpeed = 5f;

	void LateUpdate() {
		Vector2 wanted = new Vector2(target.transform.position.x + offset.x,target.transform.position.y + offset.y);

		Vector2 lerped = Vector2.Lerp(transform.position,wanted,lerpSpeed * Time.deltaTime);

		transform.position = new Vector3(lerped.x,lerped.y,transform.position.z);
	}
}
