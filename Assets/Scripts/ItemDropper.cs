using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour {
	public GameObject itemPrefab;
	public float delayAtStart;
	public Vector2 offset = Vector2.zero;

	private GameObject currentItem = null;

	void Update() {
		if(delayAtStart > 0)
			delayAtStart -= Time.deltaTime;

		if(currentItem == null && delayAtStart <= 0) {
			currentItem = Instantiate(itemPrefab);
			currentItem.transform.position = (Vector2)transform.position + offset;
		}
	}
}
